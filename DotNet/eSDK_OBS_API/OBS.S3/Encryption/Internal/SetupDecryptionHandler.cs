using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using OBS.Runtime;
using OBS.S3.Model;
using System.IO;

using OBS.S3.Util;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using OBS.Runtime.Internal.Util;
using OBS.Util;

namespace OBS.S3.Encryption.Internal
{
    public class SetupDecryptionHandler : GenericHandler
    {
        public SetupDecryptionHandler(ObsS3EncryptionClient encryptionClient)
        {
            this.EncryptionClient = encryptionClient;
        }

        public ObsS3EncryptionClient EncryptionClient
        {
            get;
            private set;
        }

        protected override void PostInvoke(IExecutionContext executionContext)
        {
            var request = executionContext.RequestContext.Request;
            var response = executionContext.ResponseContext.Response;

            var initiateMultiPartUploadRequest = request.OriginalRequest as InitiateMultipartUploadRequest;
            if (initiateMultiPartUploadRequest == null)
            {
                throw new ObsS3Exception("Request is null");
            }
            var initiateMultiPartResponse = response as InitiateMultipartUploadResponse;
            if (initiateMultiPartResponse != null)
            {
                byte[] envelopeKey = initiateMultiPartUploadRequest.EnvelopeKey;
                byte[] iv = initiateMultiPartUploadRequest.IV;

                UploadPartEncryptionContext contextForEncryption = new UploadPartEncryptionContext();
                contextForEncryption.EnvelopeKey = envelopeKey;
                contextForEncryption.NextIV = iv;
                contextForEncryption.FirstIV = iv;
                contextForEncryption.PartNumber = 0;

                //Add context for encryption of next part
                this.EncryptionClient.CurrentMultiPartUploadKeys.Add(initiateMultiPartResponse.UploadId, contextForEncryption);
            }

            var uploadPartRequest = request.OriginalRequest as UploadPartRequest;
            if (uploadPartRequest == null)
            {
                throw new ObsS3Exception("OriginalRequest is null");
            }
            var uploadPartResponse = response as UploadPartResponse;
            if (uploadPartResponse != null)
            {
                string uploadID = uploadPartRequest.UploadId;
                UploadPartEncryptionContext encryptedUploadedContext = null;

                if (!this.EncryptionClient.CurrentMultiPartUploadKeys.TryGetValue(uploadID, out encryptedUploadedContext))
                    throw new ObsS3Exception("encryption context for multi part upload not found");
                if (encryptedUploadedContext == null)
                {
                    throw new ObsS3Exception("encryption context for multi part upload not found");
                }

                if (uploadPartRequest.IsLastPart == false)
                {
                    object stream = null;

                    if (!uploadPartRequest.RequestState.TryGetValue(ObsS3EncryptionClient.S3CryptoStream, out stream))
                        throw new ObsS3Exception("cannot retrieve S3 crypto stream from request state, hence cannot get Initialization vector for next uploadPart ");

                    var encryptionStream = stream as AESEncryptionUploadPartStream;
                    if (encryptionStream == null)
                    {
                        throw new ObsS3Exception("cannot retrieve S3 crypto stream from request state, hence cannot get Initialization vector for next uploadPart ");
                    }
                    encryptedUploadedContext.NextIV = encryptionStream.InitializationVector;
                }

            }

            var getObjectResponse = response as GetObjectResponse;
            if (getObjectResponse != null)
            {
                if (EncryptionUtils.IsEncryptionInfoInMetadata(getObjectResponse) == true)
                {
                    DecryptObjectUsingMetadata(getObjectResponse);
                }
                else
                {
                    GetObjectResponse instructionFileResponse = null;
                    try
                    {
                        GetObjectRequest instructionFileRequest = EncryptionUtils.GetInstructionFileRequest(getObjectResponse);
                        instructionFileResponse = this.EncryptionClient.S3ClientForInstructionFile.GetObject(instructionFileRequest);
                    }
                    catch (ObsServiceException ace)
                    {
                        throw new ObsServiceException(string.Format(CultureInfo.InvariantCulture, "Unable to decrypt data for object {0} in bucket {1}",
                            getObjectResponse.Key, getObjectResponse.BucketName), ace);
                    }

                    if (EncryptionUtils.IsEncryptionInfoInInstructionFile(instructionFileResponse) == true)
                    {
                        DecryptObjectUsingInstructionFile(getObjectResponse, instructionFileResponse);
                    }
                }
            }

            var completeMultiPartUploadRequest = request.OriginalRequest as CompleteMultipartUploadRequest;
            if (completeMultiPartUploadRequest == null)
            {
                throw new ObsServiceException("OriginalRequest is null");
            }
            var completeMultipartUploadResponse = response as CompleteMultipartUploadResponse;
            if (completeMultipartUploadResponse != null)
            {
                if (this.EncryptionClient.S3CryptoConfig.StorageMode == CryptoStorageMode.InstructionFile)
                {
                    UploadPartEncryptionContext context = this.EncryptionClient.CurrentMultiPartUploadKeys[completeMultiPartUploadRequest.UploadId];
                    byte[] envelopeKey = context.EnvelopeKey;
                    byte[] iv = context.FirstIV;
                    byte[] encryptedEnvelopeKey = EncryptionUtils.EncryptEnvelopeKey(envelopeKey, this.EncryptionClient.EncryptionMaterials);
                    EncryptionInstructions instructions = new EncryptionInstructions(EncryptionMaterials.EmptyMaterialsDescription, envelopeKey, encryptedEnvelopeKey, iv);

                    PutObjectRequest instructionFileRequest = EncryptionUtils.CreateInstructionFileRequest(completeMultiPartUploadRequest, instructions);

                    this.EncryptionClient.S3ClientForInstructionFile.PutObject(instructionFileRequest);
                }

                //Clear Context data since encryption is completed
                this.EncryptionClient.CurrentMultiPartUploadKeys.Clear();
            }
        }

        /// <summary>
        /// Updates object where the object input stream contains the decrypted contents.
        /// </summary>
        /// <param name="instructionFileResponse">
        /// The getObject response of InstructionFile.
        /// </param>
        /// <param name="response">
        /// The getObject response whose contents are to be decrypted.
        /// </param>
        private void DecryptObjectUsingInstructionFile(GetObjectResponse response, GetObjectResponse instructionFileResponse)
        {
            // Create an instruction object from the instruction file response
            EncryptionInstructions instructions = EncryptionUtils.BuildInstructionsUsingInstructionFile(instructionFileResponse, this.EncryptionClient.EncryptionMaterials);

            // Decrypt the object with the instructions
            EncryptionUtils.DecryptObjectUsingInstructions(response, instructions);
        }

        /// <summary>
        /// Updates object where the object input stream contains the decrypted contents.
        /// </summary>
        /// <param name="objectResponse">
        /// The getObject response whose contents are to be decrypted.
        /// </param>
        private void DecryptObjectUsingMetadata(GetObjectResponse objectResponse)
        {
            // Create an instruction object from the object metadata
            EncryptionInstructions instructions = EncryptionUtils.BuildInstructionsFromObjectMetadata(objectResponse, this.EncryptionClient.EncryptionMaterials);

            // Decrypt the object with the instruction
            EncryptionUtils.DecryptObjectUsingInstructions(objectResponse, instructions);
        }
    }
}
