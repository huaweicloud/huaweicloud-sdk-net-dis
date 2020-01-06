using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OBS.S3.Model;
using System.Reflection;
using OBS.S3.Util;
using System.Net;
using OBS.Runtime;
using System.Text.RegularExpressions;
using eSDK_OBS_API.OBS.Util;

namespace OBS.S3
{

    public class ObsClient
    {
        private static ObsS3Client client;
        public static string strProduct = "eSDK-OBS-API-Windows-.NET";
        internal static string strProductVersion = "2.1.11";

        internal static string errorCode = string.Empty;
        internal static string errorMessage = string.Empty;
        public ObsS3Config clientConfig;
        /// <summary>
        /// Constructs ObsClient with Obs Access Key ID, Obs Secret Key and an
        /// ObsS3Client Configuration object. 
        /// </summary>
        /// <param name="accessKeyId">Obs Access Key ID</param>
        /// <param name="secretAccessKey">Obs Secret Key</param>
        /// <param name="clientConfig">ObsS3Client Configuration object</param>
        public ObsClient(string accessKeyId, string secretAccessKey, ObsS3Config clientConfig)
        {
            LoggerMgr.Log_Run_Info(strProduct, "ObsClient begin.");
            LoggerMgr.Log_Run_Info(strProduct, "ServiceURL=" + clientConfig.ServiceURL);
            LoggerMgr.Log_Run_Info(strProduct, "ForcePathStyle=" + clientConfig.ForcePathStyle.ToString());
            LoggerMgr.Log_Run_Info(strProduct, "UseSignatureVersion4=" + ObsConfigs.S3Config.UseSignatureVersion4);


            // crypt the sk
            client = new ObsS3Client(accessKeyId, AesCryptor.Encrypt(secretAccessKey), clientConfig);
            LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "ObsClient", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), "", "");

            // default use tls1.1 tls1.2 
            //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            LoggerMgr.Log_Run_Info(strProduct, "ObsClient end.");
        }

        #region  CreateBucket

        /// <summary>
        /// Creates a new bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketRequest used to execute the CreateBucket service method.</param>
        /// <returns>The response from the PutBucket service method, as returned by S3.</returns>
        internal PutBucketResponse CreateBucket(string bucketName)
        {
            DateTime reqTime = System.DateTime.Now;

            try
            {
                PutBucketRequest request = new PutBucketRequest();
                request.BucketName = bucketName;

                PutBucketResponse response = client.PutBucket(request);

                return response;
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Creates a new bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketRequest used to execute the CreateBucket service method.</param>
        /// <param name="storageClass">A property of PutBucketRequest used to execute the PutBucket service method.</param>
        /// <returns>The response from the PutBucket service method, as returned by S3.</returns>
        internal PutBucketResponse CreateBucket(string bucketName, S3StorageClass storageClass)
        {
            DateTime reqTime = System.DateTime.Now;

            try
            {
                PutBucketRequest request = new PutBucketRequest();
                request.BucketName = bucketName;
                request.StorageClass = storageClass;

                PutBucketResponse response = client.PutBucket(request);

                return response;
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the CreateBucket service method.</param>
        /// 
        /// <returns>The response from the CreateBucket service method, as returned by Obs.</returns>
        /// 
        /// <example>Creates a new bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PutBucketRequest request = new PutBucketRequest();
        ///     request.BucketName = "bucketName";
        ///     PutBucketResponse response = client.CreateBucket(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when CreateBucket: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutBucketResponse</c>'s value.
        /// </example>
        public PutBucketResponse CreateBucket(PutBucketRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "CreateBucket begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CreateBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", HttpStatusCode.BadRequest);
                }

                PutBucketResponse response = client.PutBucket(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("CreateBucket response HttpStatusCode: {0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "CreateBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "BuckerName=" + request.BucketName);
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "CreateBucket with request end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("CreateBucket exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CreateBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region HeadBucket

        /// <summary>
        /// This operation is useful to determine if a bucket exists and you have permission to
        /// access it.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the HeadBucket service method.</param>
        /// 
        /// <returns>The response from the HeadBucket service method, as returned by Obs.</returns>
        /// 
        /// <example>Determine if a bucket exists and you have permission to access it:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     HeadBucketRequest request = new HeadBucketRequest();
        ///     request.BucketName = "bucketName";
        ///     HeadBucketResponse response = client.HeadBucket(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when HeadBucket: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>HeadBucketResponse</c>'s value.
        /// </example>
        public HeadBucketResponse HeadBucket(HeadBucketRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "HeadBucket begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "HeadBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("BucketName={0}", request.BucketName));
                HeadBucketResponse response = client.HeadBucket(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("HeadBucket response HttpStatusCode: {0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "HeadBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "BuckerName=" + request.BucketName);

                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("HeadBucket RequestId: {0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("HeadBucket ResponseMetadata key: {0}, value: {1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "HeadBucket end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("HeadBucket exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "HeadBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }
        #region GetBucketMetadata

        /// <summary>
        /// This operation is useful to determine if a bucket exists and you have permission to
        /// access it.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the HeadBucket service method.</param>
        /// 
        /// <returns>The response from the HeadBucket service method, as returned by Obs.</returns>
        /// 
        /// <example>Determine if a bucket exists and you have permission to access it:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     HeadBucketRequest request = new HeadBucketRequest();
        ///     request.BucketName = "bucketName";
        ///     HeadBucketResponse response = client.HeadBucket(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when HeadBucket: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>HeadBucketResponse</c>'s value.
        /// </example>
        public GetBucketMetadataResponse GetBucketMetadata(GetBucketMetadataRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketMetadata begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "HeadBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("BucketName={0}", request.BucketName));
                GetBucketMetadataResponse response = client.GetBucketMetadata(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("GetBucketMetadata response HttpStatusCode: {0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetBucketMetadata", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "BuckerName=" + request.BucketName);

                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("GetBucketMetadata RequestId: {0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("GetBucketMetadata ResponseMetadata key: {0}, value: {1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketMetadata end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("GetBucketMetadata exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketMetadata", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }
        #endregion

        #region  SetBucketQuota

        /// <summary>
        /// Sets the quota for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketQuotaRequest used to execute the PutBucketQuota service method.</param>
        /// <param name="storageQuota">A property of PutBucketQuotaRequest used to execute the PutBucketQuota service method.</param>
        /// 
        /// <returns>The response from the SetBucketQuota service method, as returned by S3.</returns>
        internal PutBucketQuotaResponse SetBucketQuota(string bucketName, string storageQuota)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetBucketQuota with bucketName begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                var request = new PutBucketQuotaRequest();
                request.BucketName = bucketName;
                request.StorageQuota = storageQuota;
                LoggerMgr.Log_Run_Info(strProduct, "SetBucketQuota with bucket name end.");
                return SetBucketQuota(request);
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("SetBucketQuota exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketQuota", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Sets the quota for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the SetBucketQuota service method.</param>
        /// 
        /// <returns>The response from the SetBucketQuota service method, as returned by S3.</returns>
        /// 
        /// <example>Sets the quota for a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PutBucketQuotaRequest request = new PutBucketQuotaRequest();
        ///     request.BucketName = "bucketName";
        ///     request.StorageQuota = "storageQuota";
        ///     PutBucketQuotaResponse response = client.SetBucketQuota(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when SetBucketQuota {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutBucketQuotaResponse</c>'s value.
        /// </example>
        public PutBucketQuotaResponse SetBucketQuota(PutBucketQuotaRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetBucketQuota begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketQuota", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("BucketName={0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("StorageQuota={0}", request.StorageQuota));

                PutBucketQuotaResponse response = client.PutBucketQuota(request);
                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("HttpStatusCode={0}", response.HttpStatusCode));
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "SetBucketQuota with request end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("SetBucketQuota exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketQuota", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  SetBucketAcl

        /// <summary>
        /// Sets the permissions on a bucket using access control lists (ACL).
        /// </summary>
        /// <param name="bucketName">A property of PutACLRequest used to execute the SetBucketAcl service method.</param>
        /// <param name="acl">Access control lists (ACL)</param>
        /// <returns>The response from the SetBucketAcl service method, as returned by S3.</returns>
        internal PutACLResponse SetBucketAcl(string bucketName, S3AccessControlList acl)
        {
            try
            {
                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(bucketName) || string.IsNullOrEmpty(bucketName))
                {
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                PutACLRequest request = new PutACLRequest();
                request.BucketName = bucketName;
                request.AccessControlList = acl;

                PutACLResponse response = client.PutACL(request);

                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("SetBucketAcl exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                throw;
            }
        }

        /// <summary>
        /// Sets the permissions on a bucket using access control lists (ACL).
        /// </summary>
        /// <param name="bucketName">A property of PutACLRequest used to execute the SetBucketAcl service method.</param>
        /// <param name="cannedACL">Canned ACL</param>
        /// <returns>The response from the SetBucketAcl service method, as returned by S3.</returns>
        internal PutACLResponse SetBucketAcl(string bucketName, S3CannedACL cannedACL)
        {
            try
            {
                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(bucketName) || string.IsNullOrEmpty(bucketName))
                {
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-30 w00322557
                if (cannedACL == null)
                {
                    throw new ObsS3Exception(S3Constants.NoSuchKey, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }


                PutACLRequest request = new PutACLRequest();
                request.BucketName = bucketName;
                request.CannedACL = cannedACL;

                PutACLResponse response = client.PutACL(request);
                return response;
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Sets the permissions on a bucket using access control lists (ACL).
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the SetBucketAcl service method.</param>
        /// 
        /// <returns>The response from the SetBucketAcl service method, as returned by Obs.</returns>
        /// 
        /// <example>Sets the permissions on a bucket using ACL:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PutACLRequest request = new PutACLRequest();
        ///     request.BucketName = "bucketName";
        ///     PutACLResponse response = client.SetBucketAcl(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when SetBucketAcl {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutACLResponse</c>'s value.
        /// </example>
        public PutACLResponse SetBucketAcl(PutACLRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetBucketAcl begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("BucketName={0}", request.BucketName));
                if (request.CannedACL != null)
                {
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("CannedACL={0}", request.CannedACL.Value));
                }
                if (request.AccessControlList != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("Grants count={0}", request.AccessControlList.Grants.Count));
                    foreach (S3Grant grant in request.AccessControlList.Grants)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("GrantPermissionValue={0}", grant.Permission.Value));
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Grant grantee canonical user: {0}", grant.Grantee.CanonicalUser));
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Grant grantee display name: {0}", grant.Grantee.DisplayName));
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Grant grantee URI: {0}", grant.Grantee.URI));
                    }
                }

                PutACLResponse response = client.PutACL(request);

                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("SetBucketAcl response HttpStatus: {0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "SetBucketAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");

                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "SetBucketAcl end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("SetBucketAcl exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region ListBuckets

        /// <summary>
        /// Returns a list of all buckets owned by the authenticated sender of the request.
        /// </summary>
        /// 
        /// <returns>The response from the ListBuckets service method, as returned by S3.</returns>
        /// 
        /// <example>Returns a list of all buckets owned by the authenticated sender of the request:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     ListBucketsResponse response = client.ListBuckets();
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when ListBuckets: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>ListBucketsResponse</c>'s value.
        /// </example>
        internal ListBucketsResponse ListBuckets()
        {
            LoggerMgr.Log_Run_Info(strProduct, "ListBuckets begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                ListBucketsRequest request = new ListBucketsRequest();

                ListBucketsResponse response = client.ListBuckets(request);

                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "ListBuckets", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "");
                LoggerMgr.Log_Run_Info(strProduct, "ListBuckets end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "ListBuckets", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Returns a list of all buckets owned by the authenticated sender of the request.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListBuckets service method.</param>
        /// 
        /// <returns>The response from the ListBuckets service method, as returned by S3.</returns>
        /// 
        /// <example>Returns a list of all buckets owned by the authenticated sender of the request:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     ListBucketsRequest request = new ListBucketsRequest();
        ///     ListBucketsResponse response = client.ListBuckets(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when ListBuckets: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>ListBucketsResponse</c>'s value.
        /// </example>
        public ListBucketsResponse ListBuckets(ListBucketsRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "ListBuckets begin.");
            DateTime reqTime = System.DateTime.Now;

            try
            {
                ListBucketsResponse response = client.ListBuckets(request);

                LoggerMgr.Log_Run_Info(strProduct, string.Format("ListBuckets response HttpStatus: {0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "ListBuckets", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");

                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }

                LoggerMgr.Log_Run_Info(strProduct, "ListBuckets end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "ListBuckets", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetBucketLocation

        /// <summary>
        /// Returns the region the bucket resides in.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketLocationRequest used to execute the GetBucketLocation service method.</param>
        /// 
        /// <returns>The response from the GetBucketLocation service method, as returned by S3.</returns>
        internal GetBucketLocationResponse GetBucketLocation(string bucketName)
        {
            try
            {
                var request = new GetBucketLocationRequest();
                request.BucketName = bucketName;
                return GetBucketLocation(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns the region the bucket resides in.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketLocation service method.</param>
        /// 
        /// <returns>The response from the GetBucketLocation service method, as returned by Obs.</returns>
        /// 
        /// <example>Returns the region the bucket resides in:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetBucketLocationRequest request = new GetBucketLocationRequest();
        ///     request.BucketName = "bucketName";
        ///     GetBucketLocationResponse response = client.GetBucketLocation(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetBucketLocation: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetBucketLocationResponse</c>'s value.
        /// </example>
        public GetBucketLocationResponse GetBucketLocation(GetBucketLocationRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketLocation begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketLocation", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                GetBucketLocationResponse response = client.GetBucketLocation(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("GetBucketLocation response HttpStatusCode: {0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "CreatGetBucketLocationeBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketLocation end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketLocation", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  ListObjects

        /// <summary>
        /// Returns some or all (up to 1000) of the objects in a bucket. You can use the request
        /// parameters as selection criteria to return a subset of the objects in a bucket.
        /// </summary>
        /// <param name="bucketName">A property of ListObjectsRequest used to execute the ListObjects service method.</param>
        /// 
        /// <returns>The response from the ListObjects service method, as returned by S3.</returns>
        internal ListObjectsResponse ListObjects(string bucketName)
        {
            try
            {
                var request = new ListObjectsRequest();
                request.BucketName = bucketName;

                return ListObjects(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns some or all (up to 1000) of the objects in a bucket. You can use the request
        /// parameters as selection criteria to return a subset of the objects in a bucket.
        /// </summary>
        /// <param name="bucketName">A property of ListObjectsRequest used to execute the ListObjects service method.</param>
        /// <param name="prefix">Limits the response to keys that begin with the specified prefix.</param>
        /// 
        /// <returns>The response from the ListObjects service method, as returned by S3.</returns>
        internal ListObjectsResponse ListObjects(string bucketName, string prefix)
        {
            try
            {
                var request = new ListObjectsRequest();
                request.BucketName = bucketName;
                request.Prefix = prefix;


                return ListObjects(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns some or all (up to 1000) of the objects in a bucket. You can use the request
        /// parameters as selection criteria to return a subset of the objects in a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListObjects service method.</param>
        /// 
        /// <returns>The response from the ListObjects service method, as returned by Obs.</returns>
        /// 
        /// <example>Returns some or all of the objects in a bucket that begin with the prefix:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     ListObjectsRequest request = new ListObjectsRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Prefix = "prefix";
        ///     ListObjectsResponse response = client.ListObjects(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when ListObjects: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>ListObjectsResponse</c>'s value.
        /// </example>
        public ListObjectsResponse ListObjects(ListObjectsRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "ListObjects begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "ListObjects", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }
                ListObjectsResponse response = client.ListObjects(request);

                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "ListObjects", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "ListObjects end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "ListObjects", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetBucketStorageInfo
        /// <summary>
        /// Returns the request storage information of a bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketStorageInfoRequest used to execute the GetBucketStorageInfo service method.</param>
        /// <returns>The response from the GetBucketStorageInfo service method, as returned by S3.</returns>
        internal GetBucketStorageInfoResponse GetBucketStorageInfo(string bucketName)
        {
            try
            {
                var request = new GetBucketStorageInfoRequest();
                request.BucketName = bucketName;

                return GetBucketStorageInfo(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }


        }

        /// <summary>
        /// Returns the storage info of a bucket. To use GET, you must be the bucket owner.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketStorageInfo service method.</param>
        /// 
        /// <returns>The response from the GetBucketStorageInfo service method, as returned by Obs.</returns>
        /// 
        /// <example>Returns the request storage information of a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetBucketStorageInfoRequest request = new GetBucketStorageInfoRequest();
        ///     request.BucketName = "bucketName";
        ///     GetBucketStorageInfoResponse response = client.GetBucketStorageInfo(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetBucketStorageInfo: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetBucketStorageInfoResponse</c>'s value.
        /// </example>
        public GetBucketStorageInfoResponse GetBucketStorageInfo(GetBucketStorageInfoRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketStorageInfo begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketStorageInfo", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                GetBucketStorageInfoResponse response = client.GetBucketStorageInfo(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetBucketStorageInfo", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketStorageInfo end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketStorageInfo", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetBucketQuota

        /// <summary>
        /// Returns the request Quota of a bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketQuotaRequest used to execute the GetBucketQuota service method.</param>
        /// <returns>The response from the GetBucketQuota service method, as returned by S3.</returns>
        internal GetBucketQuotaResponse GetBucketQuota(string bucketName)
        {
            try
            {

                var request = new GetBucketQuotaRequest();
                request.BucketName = bucketName;

                return GetBucketQuota(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns the quota of a bucket. To use GET, you must be the bucket owner.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketQuota service method.</param>
        /// 
        /// <returns>The response from the GetBucketQuota service method, as returned by Obs.</returns>
        /// 
        /// <example>Returns the request Quota of a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetBucketQuotaRequest request = new GetBucketQuotaRequest();
        ///     request.BucketName = "bucketName";
        ///     GetBucketQuotaResponse response = client.GetBucketQuota(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetBucketQuota: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetBucketQuotaResponse</c>'s value.
        /// </example>
        public GetBucketQuotaResponse GetBucketQuota(GetBucketQuotaRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketQuota begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketQuota", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                GetBucketQuotaResponse response = client.GetBucketQuota(request);

                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetBucketQuota", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketQuota end.");

                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketQuota", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetBucketAcl

        /// <summary>
        /// Gets the access control policy for the bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetACLRequest used to execute the GetBucketAcl service method.</param>
        /// 
        /// <returns>The response from the GetBucketAcl service method, as returned by S3.</returns>
        internal GetACLResponse GetBucketAcl(string bucketName)
        {
            try
            {
                var request = new GetACLRequest();
                request.BucketName = bucketName;

                return GetBucketAcl(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the access control policy for the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetACL service method.</param>
        /// 
        /// <returns>The response from the GetACL service method, as returned by Obs.</returns>
        /// 
        /// <example>Gets the access control policy for the bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetACLRequest request = new GetACLRequest();
        ///     request.BucketName = "bucketName";
        ///     GetACLResponse response = client.GetACL(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetACL: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetACLResponse</c>'s value.
        /// </example>
        public GetACLResponse GetBucketAcl(GetACLRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketAcl begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }
                GetACLResponse response = client.GetACL(request);
                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetBucketAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketAcl with request end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  ListMultipartUploads

        /// <summary>
        /// This operation lists in-progress multipart uploads.
        /// </summary>
        /// <param name="bucketName">A property of ListMultipartUploadsRequest used to execute the ListMultipartUploads service method.</param>
        /// 
        /// <returns>The response from the ListMultipartUploads service method, as returned by S3.</returns>
        internal ListMultipartUploadsResponse ListMultipartUploads(string bucketName)
        {
            try
            {
                var request = new ListMultipartUploadsRequest();
                request.BucketName = bucketName;
                return ListMultipartUploads(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// This operation lists in-progress multipart uploads.
        /// </summary>
        /// <param name="bucketName">A property of ListMultipartUploadsRequest used to execute the ListMultipartUploads service method.</param>
        /// <param name="prefix">Lists in-progress uploads only for those keys that begin with the specified prefix.</param>
        /// 
        /// <returns>The response from the ListMultipartUploads service method, as returned by S3.</returns>
        internal ListMultipartUploadsResponse ListMultipartUploads(string bucketName, string prefix)
        {
            try
            {
                var request = new ListMultipartUploadsRequest();
                request.BucketName = bucketName;
                request.Prefix = prefix;
                return ListMultipartUploads(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// This operation lists in-progress multipart uploads.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListMultipartUploads service method.</param>
        /// 
        /// <returns>The response from the ListMultipartUploads service method, as returned by Obs.</returns>
        /// 
        /// <example>Lists in-progress multipart uploads that begin with the specified prefix:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     request = new ListMultipartUploadsRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Prefix = "prefix";
        ///     ListMultipartUploadsResponse response = client.ListMultipartUploads(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when ListMultipartUploads: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>ListMultipartUploadsResponse</c>'s value.
        /// </example>
        public ListMultipartUploadsResponse ListMultipartUploads(ListMultipartUploadsRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "ListMultipartUploads begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "ListMultipartUploads", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }
                ListMultipartUploadsResponse response = client.ListMultipartUploads(request);

                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "ListMultipartUploads", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "ListMultipartUploads end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "ListMultipartUploads", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  DeleteBucket

        /// <summary>
        /// Deletes the bucket. All objects (including all object versions and Delete Markers)
        /// in the bucket must be deleted before the bucket itself can be deleted.
        /// </summary>
        /// <param name="bucketName">A property of DeleteBucketRequest used to execute the DeleteBucket service method.</param>
        /// 
        /// <returns>The response from the DeleteBucket service method, as returned by S3.</returns>
        internal DeleteBucketResponse DeleteBucket(string bucketName)
        {
            try
            {
                var request = new DeleteBucketRequest();
                request.BucketName = bucketName;

                return DeleteBucket(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the bucket. All objects (including all object versions and Delete Markers)
        /// in the bucket must be deleted before the bucket itself can be deleted.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucket service method.</param>
        /// 
        /// <returns>The response from the DeleteBucket service method, as returned by Obs.</returns>
        /// 
        /// <example>Deletes the bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {
        ///     DeleteBucketRequest request = new DeleteBucketRequest();
        ///     request.BucketName = "bucketName";
        ///     
        ///     DeleteBucketResponse response = client.DeleteBucket(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when DeleteBucket: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>DeleteBucketResponse</c>'s value.
        /// </example>
        public DeleteBucketResponse DeleteBucket(DeleteBucketRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "DeleteBucket begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                DeleteBucketResponse response = client.DeleteBucket(request);

                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "DeleteBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "DeleteBucket with request end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  DeleteBucketWithObjects

        /// <summary>
        /// Delete bucket with objects.
        /// </summary>
        /// <param name="bucketName">A property of PostDeleteBucketRequest used to execute the DeleteBucketWithObjects service method.</param>
        /// <returns>The response from the DeleteBucketWithObjects service method, as returned by S3.</returns>
        internal PostDeleteBucketResponse DeleteBucketWithObjects(string bucketName)
        {
            try
            {
                PostDeleteBucketRequest request = new PostDeleteBucketRequest();
                request.BucketName = bucketName;
                //2015-5-28 w00322557
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }
                PostDeleteBucketResponse response = client.PostDeleteBucket(request);
                return response;
            }
            catch (ObsS3Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete bucket with objects.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketWithObjects service method.</param>
        /// 
        /// <returns>The response from the PostDeleteBucket service method, as returned by Obs.</returns>
        /// 
        /// <example>Delete bucket using bucket name:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PostDeleteBucketRequest request = new PostDeleteBucketRequest();
        ///     request.BucketName = "bucketName";
        ///     PostDeleteBucketResponse response = client.PostDeleteBucket(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when DeleteBucketWithObjects {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PostDeleteBucketResponse</c>'s value.
        /// </example>
        internal PostDeleteBucketResponse DeleteBucketWithObjects(PostDeleteBucketRequest request)
        {
            try
            {
                //2015-5-28 w00322557
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }
                PostDeleteBucketResponse response = client.PostDeleteBucket(request);
                return response;
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region  SetBucketLoggingConfiguration

        /// <summary>
        /// Set the logging parameters for a bucket and to specify permissions for who can view
        /// and modify the logging parameters. To set the logging status of a bucket, you must
        /// be the bucket owner.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the SetBucketLoggingConfiguration service method.</param>
        /// 
        /// <returns>The response from the SetBucketLoggingConfiguration service method, as returned by Obs.</returns>
        /// 
        /// <example>Set the logging parameters for a bucket and to specify permissions:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PutBucketLoggingRequest request = new PutBucketLoggingRequest();
        ///     request.BucketName = "bucketName";
        ///     PutBucketLoggingResponse response = client.SetBucketLoggingConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when PutBucketLogging: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutBucketLoggingResponse</c>'s value.
        /// </example>
        public PutBucketLoggingResponse SetBucketLoggingConfiguration(PutBucketLoggingRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetBucketLoggingConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketLoggingConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                if (request.LoggingConfig != null)
                {
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("BucketName={0}", request.BucketName));
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("TargetBucketName={0}", request.LoggingConfig.TargetBucketName));
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("TargetPrefix={0}", request.LoggingConfig.TargetPrefix));
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("GrantsCount={0}", request.LoggingConfig.Grants.Count));
                    foreach (S3Grant grant in request.LoggingConfig.Grants)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("GrantPermissionValue={0}", grant.Permission.Value));
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("GrantGranteeCanonicalUser={0}", grant.Grantee.CanonicalUser));
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("GrantGranteeDisplayName={0}", grant.Grantee.DisplayName));

                        LoggerMgr.Log_Run_Info(strProduct, string.Format("GrantGrantee URI={0}", grant.Grantee.URI));
                    }
                }

                PutBucketLoggingResponse response = client.PutBucketLogging(request);
                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "SetBucketLoggingConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "SetBucketLoggingConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketLoggingConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetBucketLoggingConfiguration

        /// <summary>
        /// Returns the logging status of a bucket and the permissions users have to view and
        /// modify that status. To use GET, you must be the bucket owner.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketLoggingRequest used to execute the GetBucketLoggingConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetBucketLoggingConfiguration service method, as returned by S3.</returns>
        internal GetBucketLoggingResponse GetBucketLoggingConfiguration(string bucketName)
        {
            try
            {
                var request = new GetBucketLoggingRequest();
                request.BucketName = bucketName;

                return GetBucketLoggingConfiguration(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns the logging status of a bucket and the permissions users have to view and
        /// modify that status. To use GET, you must be the bucket owner.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketLoggingConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetBucketLoggingConfiguration service method, as returned by Obs.</returns>
        /// 
        /// <example>Returns the logging status of a bucket and the permissions users have to view and modify that status:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {  
        ///     GetBucketLoggingRequest request = new GetBucketLoggingRequest();
        ///     request.BucketName = bucketName;
        ///     GetBucketLoggingResponse response = client.GetBucketLoggingConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetBucketLoggingConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetBucketLoggingResponse</c>'s value.
        /// </example>
        public GetBucketLoggingResponse GetBucketLoggingConfiguration(GetBucketLoggingRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketLoggingConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketLoggingConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                GetBucketLoggingResponse response = client.GetBucketLogging(request);

                //2015-6-3 w00322557 
                if (!string.IsNullOrEmpty(errorCode) && !string.IsNullOrEmpty(errorMessage))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketLoggingConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), errorCode, errorMessage);
                    throw new ObsS3Exception(errorMessage, OBS.Runtime.ErrorType.Sender, errorCode, response.ResponseMetadata.RequestId, response.HttpStatusCode);
                }

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetBucketLoggingConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketLoggingConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketLoggingConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                errorCode = string.Empty;
                errorMessage = string.Empty;
                throw;
            }
        }

        #endregion

        #region  SetBucketPolicy

        /// <summary>
        /// Replaces a policy on a bucket. If the bucket already has a policy, the one in this
        /// request completely replaces it.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketPolicyRequest used to execute the SetBucketPolicy service method.</param>
        /// <param name="policy">The bucket policy as a JSON document.</param>
        /// 
        /// <returns>The response from the SetBucketPolicy service method, as returned by S3.</returns>
        internal PutBucketPolicyResponse SetBucketPolicy(string bucketName, string policy)
        {
            try
            {
                var request = new PutBucketPolicyRequest();
                request.BucketName = bucketName;
                request.Policy = policy;

                return SetBucketPolicy(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Replaces a policy on a bucket. If the bucket already has a policy, the one in this
        /// request completely replaces it.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the SetBucketPolicy service method.</param>
        /// 
        /// <returns>The response from the SetBucketPolicy service method, as returned by S3.</returns>
        /// 
        /// <example>Replaces a policy on a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PutBucketPolicyRequest request = new PutBucketPolicyRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Policy = "policy";
        ///     PutBucketPolicyResponse response = client.SetBucketPolicy(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when SetBucketPolicy: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutBucketPolicyResponse</c>'s value.
        /// </example>
        public PutBucketPolicyResponse SetBucketPolicy(PutBucketPolicyRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetBucketPolicy begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketPolicy", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Policy: {0}", request.Policy));

                PutBucketPolicyResponse response = client.PutBucketPolicy(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "SetBucketPolicy", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "SetBucketPolicy end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketPolicy", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetBucketPolicy

        /// <summary>
        /// Returns the policy of a specified bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketPolicyRequest used to execute the GetBucketPolicy service method.</param>
        /// 
        /// <returns>The response from the GetBucketPolicy service method, as returned by S3.</returns>
        internal GetBucketPolicyResponse GetBucketPolicy(string bucketName)
        {
            try
            {
                var request = new GetBucketPolicyRequest();
                request.BucketName = bucketName;

                return GetBucketPolicy(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns the policy of a specified bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketPolicy service method.</param>
        /// 
        /// <returns>The response from the GetBucketPolicy service method, as returned by Obs.</returns>
        /// 
        /// <example>Returns the policy of a specified bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetBucketPolicyRequest request = new GetBucketPolicyRequest();
        ///     request.BucketName = bucketName;
        ///     GetBucketPolicyResponse response = client.GetBucketPolicy(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetBucketPolicy: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetBucketPolicyResponse</c>'s value.
        /// </example>
        public GetBucketPolicyResponse GetBucketPolicy(GetBucketPolicyRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketPolicy begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketPolicy", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                GetBucketPolicyResponse response = client.GetBucketPolicy(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetBucketPolicy", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }

                if (response.Policy.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase))
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.XmlResolver = new CustomUrlResovler();
                    doc.LoadXml(response.Policy);

                    //whole xml
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("response xml: {0}", doc.OuterXml));

                    System.Xml.XmlNode node = doc.SelectSingleNode("//Code");
                    string errorCode = node.InnerText;
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("node Code: {0}", errorCode));

                    node = doc.SelectSingleNode("//Message");
                    string errorMessage = node.InnerText;
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("node Message: {0}", errorMessage));

                    node = doc.SelectSingleNode("//RequestId");
                    string requestId = node.InnerText;
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("node RequestId: {0}", requestId));

                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketPolicy", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), errorCode, HttpStatusCode.NotFound.ToString());
                    throw new ObsS3Exception(errorMessage, ErrorType.Sender, errorCode, requestId, HttpStatusCode.NotFound);
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketPolicy end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketPolicy", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  DeleteBucketPolicy

        /// <summary>
        /// Deletes the policy from the bucket.
        /// </summary>
        /// <param name="bucketName">A property of DeleteBucketPolicyRequest used to execute the DeleteBucketPolicy service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketPolicy service method, as returned by S3.</returns>
        internal DeleteBucketPolicyResponse DeleteBucketPolicy(string bucketName)
        {
            try
            {
                var request = new DeleteBucketPolicyRequest();
                request.BucketName = bucketName;

                return DeleteBucketPolicy(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the policy from the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketPolicy service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketPolicy service method, as returned by Obs.</returns>
        /// 
        /// <example>Deletes the policy from the bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {        
        ///     DeleteBucketPolicyRequest request = new DeleteBucketPolicyRequest();
        ///     request.BucketName = "bucketName";
        ///     DeleteBucketPolicyResponse response = client.DeleteBucketPolicy(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when DeleteBucketPolicy: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>DeleteBucketPolicyResponse</c>'s value.
        /// </example>
        public DeleteBucketPolicyResponse DeleteBucketPolicy(DeleteBucketPolicyRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketPolicy begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucketPolicy", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                DeleteBucketPolicyResponse response = client.DeleteBucketPolicy(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "DeleteBucketPolicy", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketPolicy end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucketPolicy", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region SetBucketCORS

        /// <summary>
        /// Set the CORS configuration for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the SetBucketCORSConfiguration service method.</param>
        /// 
        /// <returns>The response from the SetBucketCORSConfiguration service method, as returned by Obs.</returns>
        /// 
        /// <example>Set the CORS configuration for a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PutCORSConfigurationRequest request = new PutCORSConfigurationRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Configuration = new OBS.S3.Model.CORSConfiguration();
        ///     CORSRule rule = new CORSRule();
        ///     List<![CDATA[<string>]]> allowedMethods = new List<![CDATA[<string>]]>();
        ///     allowedMethods.Add("POST");
        ///     allowedMethods.Add("PUT");
        ///     allowedMethods.Add("GET");
        ///     allowedMethods.Add("HEAD");
        ///     rule.AllowedMethods = allowedMethods;
        ///     
        ///     rule.Id = "id";
        ///     
        ///     rule.AllowedOrigins.Add("www.huawei.com");
        ///     rule.AllowedOrigins.Add("obs.huawei.com");
        ///     
        ///     rule.AllowedHeaders.Add("AllowedHeader_1");
        ///     rule.AllowedHeaders.Add("AllowedHeader_2");
        ///     
        ///     rule.MaxAgeSeconds = 100;
        ///     
        ///     rule.ExposeHeaders.Add("ExposeHeader_1");
        ///     rule.ExposeHeaders.Add("ExposeHeader_2");
        ///     
        ///     request.Configuration.Rules.Add(rule);
        ///     PutCORSConfigurationResponse response = client.SetBucketCORSConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when SetBucketCORSConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutCORSConfigurationResponse</c>'s value.
        /// </example>
        public PutCORSConfigurationResponse SetBucketCORSConfiguration(PutCORSConfigurationRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetBucketCORSConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketCORSConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                PutCORSConfigurationResponse response = client.PutCORSConfiguration(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "SetBucketCORSConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "SetBucketCORSConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketCORSConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region GetBucketCORS
        /// <summary>
        /// Returns the CORS configuration for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketCORSConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetBucketCORSConfiguration service method, as returned by Obs.</returns>
        /// 
        /// <example>Returns the CORS configuration for a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetCORSConfigurationRequest request = new GetCORSConfigurationRequest();
        ///     request.BucketName = "bucketName";
        ///     
        ///     GetCORSConfigurationResponse response = client.GetBucketCORSConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetBucketCORSConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetCORSConfigurationResponse</c>'s value.
        /// </example>
        public GetCORSConfigurationResponse GetBucketCORSConfiguration(GetCORSConfigurationRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketCORSConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketCORSConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                GetCORSConfigurationResponse response = client.GetCORSConfiguration(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetBucketCORSConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketCORSConfiguration end.");

                if (response.HttpStatusCode == HttpStatusCode.NotFound)
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketCORSConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ObsClient.errorCode, ObsClient.errorMessage);
                    throw new ObsS3Exception(ObsClient.errorMessage, ErrorType.Sender, ObsClient.errorCode, "", HttpStatusCode.NotFound);
                }
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketCORSConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region DeleteBucketCORS

        /// <summary>
        /// This operation removes the CORS configuration from the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketCORSConfiguration service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketCORSConfiguration service method, as returned by Obs.</returns>
        /// 
        /// <example>This operation removes the CORS configuration from the bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {        
        ///     DeleteCORSConfigurationRequest request = new DeleteCORSConfigurationRequest();
        ///     request.BucketName = "bucketName";
        ///     
        ///     DeleteCORSConfigurationResponse response = client.DeleteBucketCORSConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when DeleteBucketCORSConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>DeleteCORSConfigurationResponse</c>'s value.
        /// </example>
        public DeleteCORSConfigurationResponse DeleteBucketCORSConfiguration(DeleteCORSConfigurationRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketCORSConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucketCORSConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", HttpStatusCode.BadRequest);
                }

                DeleteCORSConfigurationResponse response = client.DeleteCORSConfiguration(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "DeleteBucketCORSConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketCORSConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucketCORSConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetBucketLifecycleConfiguration

        /// <summary>
        /// Returns the lifecycle configuration information set on the bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetLifecycleConfigurationRequest used to execute the GetBucketLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetBucketLifecycleConfiguration service method, as returned by S3.</returns>
        internal GetLifecycleConfigurationResponse GetBucketLifecycleConfiguration(string bucketName)
        {
            try
            {
                var request = new GetLifecycleConfigurationRequest();
                request.BucketName = bucketName;
                return GetBucketLifecycleConfiguration(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns the lifecycle configuration information set on the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetBucketLifecycleConfiguration service method, as returned by S3.</returns>
        /// 
        /// <example>Returns the lifecycle configuration information set on the bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetLifecycleConfigurationRequest request = new GetLifecycleConfigurationRequest();
        ///     request.BucketName = "bucketName";
        ///     GetLifecycleConfigurationResponse response = client.GetLifecycleConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetBucketLifecycleConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetLifecycleConfigurationResponse</c>'s value.
        /// </example>
        public GetLifecycleConfigurationResponse GetBucketLifecycleConfiguration(GetLifecycleConfigurationRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketLifecycleConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketLifecycleConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                GetLifecycleConfigurationResponse response = client.GetLifecycleConfiguration(request);

                if (!string.IsNullOrEmpty(errorCode) && !string.IsNullOrEmpty(errorMessage))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketLifecycleConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), errorCode, errorMessage);
                    throw new ObsS3Exception(errorMessage, OBS.Runtime.ErrorType.Sender, errorCode, response.ResponseMetadata.RequestId, response.HttpStatusCode);
                }

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetBucketLifecycleConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketLifecycleConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketLifecycleConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                errorCode = string.Empty;
                errorMessage = string.Empty;
                throw;
            }
        }

        #endregion

        #region  SetBucketLifecycleConfiguration

        /// <summary>
        /// Sets lifecycle configuration for your bucket. If a lifecycle configuration exists,
        /// it replaces it.
        /// </summary>
        /// <param name="bucketName">A property of PutLifecycleConfigurationRequest used to execute the PutLifecycleConfiguration service method.</param>
        /// <param name="configuration">A property of PutLifecycleConfigurationRequest used to execute the PutLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the PutLifecycleConfiguration service method, as returned by S3.</returns>
        internal PutLifecycleConfigurationResponse SetBucketLifecycleConfiguration(string bucketName, LifecycleConfiguration configuration)
        {
            try
            {
                var request = new PutLifecycleConfigurationRequest();
                request.BucketName = bucketName;
                request.Configuration = configuration;
                return SetBucketLifecycleConfiguration(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Sets lifecycle configuration for your bucket. If a lifecycle configuration exists,
        /// it replaces it.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the SetBucketLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the SetBucketLifecycleConfiguration service method, as returned by S3.</returns>
        /// 
        /// <example>Sets lifecycle configuration for your bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     LifecycleConfiguration config = new LifecycleConfiguration();
        ///     
        ///     PutLifecycleConfigurationRequest request = new PutLifecycleConfigurationRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Configuration = config;
        ///     LifecycleRule rule = new LifecycleRule();
        ///     rule.Id = "8D579823458299DD78934K23LK23234M324";
        ///     LifecycleRuleExpiration expiration = new LifecycleRuleExpiration();
        ///     expiration.Days = 2;
        ///     rule.Expiration = expiration;
        ///     rule.Prefix = "w";
        ///     rule.Status = LifecycleRuleStatus.Enabled;
        ///     
        ///     config.Rules.Add(rule);
        ///     
        ///     PutLifecycleConfigurationResponse response = client.SetBucketLifecycleConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when SetBucketLifecycleConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutLifecycleConfigurationResponse</c>'s value.
        /// </example>
        public PutLifecycleConfigurationResponse SetBucketLifecycleConfiguration(PutLifecycleConfigurationRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetBucketLifecycleConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketLifecycleConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                foreach (LifecycleRule rule in request.Configuration.Rules)
                {
                    if (null != rule.Expiration) {
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Expiration date: {0}", rule.Expiration.Date.ToShortDateString()));
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Expiration days: {0}", rule.Expiration.Days));
                    }
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Id: {0}", rule.Id));
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Prefix: {0}", rule.Prefix));
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Status: {0}", rule.Status.Value));
                    if (rule.Transition != null)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Transition date: {0}", rule.Transition.Date.ToShortDateString()));
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Transition days: {0}", rule.Transition.Days));
                    }
                    if (rule.NoncurrentVersionExpiration != null)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Expiration noncurrent days: {0}", rule.NoncurrentVersionExpiration.NoncurrentDays));
                    }

                    if (rule.NoncurrentVersionTransition != null)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Transition noncurrent days: {0}", rule.NoncurrentVersionTransition.NoncurrentDays));
                    }
                }

                PutLifecycleConfigurationResponse response = client.PutLifecycleConfiguration(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "SetBucketLifecycleConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "SetBucketLifecycleConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketLifecycleConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  DeleteBucketLifecycleConfiguration

        /// <summary>
        /// Deletes the lifecycle configuration from the bucket.
        /// </summary>
        /// <param name="bucketName">A property of DeleteLifecycleConfigurationRequest used to execute the DeleteBucketLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketLifecycleConfiguration service method, as returned by S3.</returns>
        internal DeleteLifecycleConfigurationResponse DeleteBucketLifecycleConfiguration(string bucketName)
        {
            try
            {
                var request = new DeleteLifecycleConfigurationRequest();
                request.BucketName = bucketName;
                return DeleteBucketLifecycleConfiguration(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the lifecycle configuration from the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketLifecycleConfiguration service method, as returned by S3.</returns>
        /// 
        /// <example>Deletes the lifecycle configuration from the bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     DeleteLifecycleConfigurationRequest request = new DeleteLifecycleConfigurationRequest();
        ///     request.BucketName = "bucketName";
        ///     DeleteLifecycleConfigurationResponse response = client.DeleteBucketLifecycleConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when DeleteBucketLifecycleConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>DeleteLifecycleConfigurationResponse</c>'s value.
        /// </example>
        public DeleteLifecycleConfigurationResponse DeleteBucketLifecycleConfiguration(DeleteLifecycleConfigurationRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketLifecycleConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucketLifecycleConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                DeleteLifecycleConfigurationResponse response = client.DeleteLifecycleConfiguration(request);
                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "DeleteBucketLifecycleConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketLifecycleConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucketLifecycleConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetBucketWebsiteConfiguration

        /// <summary>
        /// Returns the website configuration for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketWebsiteRequest used to execute the GetBucketWebsiteConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetBucketWebsiteConfiguration service method, as returned by S3.</returns>
        internal GetBucketWebsiteResponse GetBucketWebsiteConfiguration(string bucketName)
        {
            try
            {
                var request = new GetBucketWebsiteRequest();
                request.BucketName = bucketName;

                return GetBucketWebsiteConfiguration(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns the website configuration for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketWebsiteConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetBucketWebsiteConfiguration service method, as returned by Obs.</returns>
        /// 
        /// <example>Returns the website configuration for a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetBucketWebsiteRequest request = new GetBucketWebsiteRequest();
        ///     request.BucketName = "bucketName";
        ///     GetBucketWebsiteResponse response = client.GetBucketWebsiteConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetBucketWebsiteConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetBucketWebsiteResponse</c>'s value.
        /// </example>
        public GetBucketWebsiteResponse GetBucketWebsiteConfiguration(GetBucketWebsiteRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketWebsiteConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketWebsiteConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                GetBucketWebsiteResponse response = client.GetBucketWebsite(request);

                //2015-6-3 w00322557 
                if (!string.IsNullOrEmpty(errorCode) && !string.IsNullOrEmpty(errorMessage))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketWebsiteConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), errorCode, errorMessage);
                    throw new ObsS3Exception(errorMessage, OBS.Runtime.ErrorType.Sender, errorCode, response.ResponseMetadata.RequestId, response.HttpStatusCode);
                }

                //2015-5-30 w00322557                 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetBucketWebsiteConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketWebsiteConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketWebsiteConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                errorCode = string.Empty;
                errorMessage = string.Empty;
                throw;
            }
        }

        #endregion

        #region  SetBucketWebsiteConfiguration

        /// <summary>
        /// Set the website configuration for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketWebsiteRequest used to execute the PutBucketWebsite service method.</param>
        /// <param name="websiteConfiguration">A property of PutBucketWebsiteRequest used to execute the PutBucketWebsite service method.</param>
        /// 
        /// <returns>The response from the PutBucketWebsite service method, as returned by S3.</returns>
        internal PutBucketWebsiteResponse SetBucketWebsiteConfiguration(string bucketName, WebsiteConfiguration websiteConfiguration)
        {
            try
            {
                var request = new PutBucketWebsiteRequest();
                request.BucketName = bucketName;
                request.WebsiteConfiguration = websiteConfiguration;

                return SetBucketWebsiteConfiguration(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Set the website configuration for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the SetBucketWebsiteConfiguration service method.</param>
        /// 
        /// <returns>The response from the SetBucketWebsiteConfiguration service method, as returned by Obs.</returns>
        /// 
        /// <example>Set the website configuration for a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     var request = new PutBucketWebsiteRequest();
        ///     request.BucketName = "bucketName";
        ///     WebsiteConfiguration config = new WebsiteConfiguration();
        ///     config.ErrorDocument = "error1012";
        ///     config.IndexDocumentSuffix = "index.html";
        ///     
        ///     RoutingRule item = new RoutingRule();
        ///     item.Condition = new RoutingRuleCondition();
        ///     item.Condition.HttpErrorCodeReturnedEquals = "404";
        ///     
        ///     item.Redirect = new Obs.S3.Model.RoutingRuleRedirect();
        ///     item.Redirect.Protocol = "https";
        ///     item.Redirect.HostName = "www.huawei.com";
        ///     item.Redirect.ReplaceKeyWith = "not.html";
        ///     config.RoutingRules.Add(item);
        ///     
        ///     request.WebsiteConfiguration = config;
        ///     PutBucketWebsiteResponse response = client.SetBucketWebsiteConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when SetBucketWebsiteConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutBucketWebsiteResponse</c>'s value.
        /// </example>
        public PutBucketWebsiteResponse SetBucketWebsiteConfiguration(PutBucketWebsiteRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetBucketWebsiteConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketWebsiteConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557                
                if (request.WebsiteConfiguration != null)
                {
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("WebsiteConfiguration ErrorDocument: {0}", request.WebsiteConfiguration.ErrorDocument));
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("WebsiteConfiguration IndexDocumentSuffix: {0}", request.WebsiteConfiguration.IndexDocumentSuffix));
                    if (request.WebsiteConfiguration.RedirectAllRequestsTo != null)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("RedirectAllRequestsTo host name: {0}", request.WebsiteConfiguration.RedirectAllRequestsTo.HostName));
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("RedirectAllRequestsTo HttpRedirectCode: {0}", request.WebsiteConfiguration.RedirectAllRequestsTo.HttpRedirectCode));
                    }

                    if (request.WebsiteConfiguration.RoutingRules != null)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Routing rules count: {0}", request.WebsiteConfiguration.RoutingRules.Count));
                    }
                }
                else
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketWebsiteConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchWebsiteConfiguration);
                    throw new ObsS3Exception(S3Constants.NoSuchWebsiteConfiguration, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchWebsiteConfiguration, "", HttpStatusCode.BadRequest);
                }

                PutBucketWebsiteResponse response = client.PutBucketWebsite(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "SetBucketWebsiteConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "SetBucketWebsiteConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketWebsiteConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  DeleteBucketWebsiteConfiguration

        /// <summary>
        /// This operation removes the website configuration from the bucket.
        /// </summary>
        /// <param name="bucketName">A property of DeleteBucketWebsiteRequest used to execute the DeleteBucketWebsiteConfiguration service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketWebsiteConfiguration service method, as returned by S3.</returns>
        internal DeleteBucketWebsiteResponse DeleteBucketWebsiteConfiguration(string bucketName)
        {
            try
            {
                var request = new DeleteBucketWebsiteRequest();
                request.BucketName = bucketName;
                return DeleteBucketWebsiteConfiguration(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// This operation removes the website configuration from the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketWebsiteConfiguration service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketWebsiteConfiguration service method, as returned by Obs.</returns>
        /// 
        /// <example>This operation removes the website configuration from the bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {        
        ///     DeleteBucketWebsiteRequest request = new DeleteBucketWebsiteRequest();
        ///     request.BucketName = "bucketName";
        ///     DeleteBucketWebsiteResponse response = client.DeleteBucketWebsiteConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when DeleteBucketWebsiteConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>DeleteBucketWebsiteResponse</c>'s value.
        /// </example>
        public DeleteBucketWebsiteResponse DeleteBucketWebsiteConfiguration(DeleteBucketWebsiteRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketWebsiteConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucketWebsiteConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                DeleteBucketWebsiteResponse response = client.DeleteBucketWebsite(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "DeleteBucketWebsiteConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketWebsiteConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucketWebsiteConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  SetBucketVersioningConfiguration

        /// <summary>
        /// Sets the versioning state of an existing bucket. To set the versioning state, you
        /// must be the bucket owner.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the SetBucketVersioningConfiguration service method.</param>
        /// 
        /// <returns>The response from the SetBucketVersioningConfiguration service method, as returned by Obs.</returns>
        /// 
        /// <example>Sets the versioning state of an existing bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PutBucketVersioningRequest request = new PutBucketVersioningRequest();
        ///     request.BucketName = "bucketName";
        ///     PutBucketVersioningResponse response = client.SetBucketVersioningConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when SetBucketVersioningConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutBucketVersioningResponse</c>'s value.
        /// </example>
        public PutBucketVersioningResponse SetBucketVersioningConfiguration(PutBucketVersioningRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetBucketVersioningConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketVersioningConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Versioning config status: {0}", request.VersioningConfig.Status.Value));

                PutBucketVersioningResponse response = client.PutBucketVersioning(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "SetBucketVersioningConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "SetBucketVersioningConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketVersioningConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetBucketVersioningConfiguration

        /// <summary>
        /// Returns the versioning state of a bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketVersioningRequest used to execute the GetBucketVersioningConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetBucketVersioningConfiguration service method, as returned by S3.</returns>
        internal GetBucketVersioningResponse GetBucketVersioningConfiguration(string bucketName)
        {
            try
            {
                var request = new GetBucketVersioningRequest();
                request.BucketName = bucketName;
                return GetBucketVersioningConfiguration(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns the versioning state of a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketVersioningConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetBucketVersioningConfiguration service method, as returned by S3.</returns>
        /// 
        /// <example>Returns the versioning state of a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetBucketVersioningRequest request = new GetBucketVersioningRequest();
        ///     request.BucketName = bucketName;
        ///     GetBucketVersioningResponse response = client.GetBucketVersioningConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetBucketVersioningConfiguration: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetBucketVersioningResponse</c>'s value.
        /// </example>
        public GetBucketVersioningResponse GetBucketVersioningConfiguration(GetBucketVersioningRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketVersioningConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketVersioningConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }
                GetBucketVersioningResponse response = client.GetBucketVersioning(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetBucketVersioningConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketVersioningConfiguration end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketVersioningConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  ListVersions

        /// <summary>
        /// Returns metadata about all of the versions of objects in a bucket.
        /// </summary>
        /// <param name="bucketName">A property of ListVersionsRequest used to execute the ListVersions service method.</param>
        /// 
        /// <returns>The response from the ListVersions service method, as returned by S3.</returns>
        internal ListVersionsResponse ListVersions(string bucketName)
        {
            try
            {
                var request = new ListVersionsRequest();
                request.BucketName = bucketName;
                return ListVersions(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns metadata about all of the versions of objects in a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListVersions service method.</param>
        /// 
        /// <returns>The response from the ListVersions service method, as returned by Obs.</returns>
        /// 
        /// <example>Returns metadata about all of the versions of objects in a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     ListVersionsRequest request = new ListVersionsRequest();
        ///     request.BucketName = "bucketName";
        ///     ListVersionsResponse response = client.ListVersions(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when ListVersions: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>ListVersionsResponse</c>'s value.
        /// </example>
        public ListVersionsResponse ListVersions(ListVersionsRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "ListVersions begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "ListVersions", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName); LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CreateBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", HttpStatusCode.BadRequest);
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Delimiter: {0}", request.Delimiter));
                if (request.Encoding != null)
                {
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Encoding: {0}", request.Encoding.Value));
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("Key marker: {0}", request.KeyMarker));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Max keys: {0}", request.MaxKeys));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Prefix: {0}", request.Prefix));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Version Id marker: {0}", request.VersionIdMarker));

                ListVersionsResponse response = client.ListVersions(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "ListVersions", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "ListVersions end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "ListVersions", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  PutObject

        /// <summary>
        /// Adds an object to a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutObject service method.</param>
        /// 
        /// <returns>The response from the PutObject service method, as returned by Obs.</returns>
        /// 
        /// <example>Adds an object to a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PutObjectRequest request = new PutObjectRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "key";
        ///     PutObjectResponse response = client.PutObject(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when PutObject: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutObjectResponse</c>'s value.
        /// </example>
        public PutObjectResponse PutObject(PutObjectRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "PutObject begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "PutObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "PutObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", HttpStatusCode.BadRequest);
                }

                PutObjectResponse response = client.PutObject(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "PutObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "PutObject end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "PutObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  CopyObject

        /// <summary>
        /// Creates a copy of an object that is already stored in S3.
        /// </summary>
        /// <param name="sourceBucket">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="sourceKey">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="destinationBucket">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="destinationKey">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// 
        /// <returns>The response from the CopyObject service method, as returned by S3.</returns>
        internal CopyObjectResponse CopyObject(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey)
        {
            try
            {
                var request = new CopyObjectRequest();
                request.SourceBucket = sourceBucket;
                request.SourceKey = sourceKey;
                request.DestinationBucket = destinationBucket;
                request.DestinationKey = destinationKey;
                return CopyObject(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a copy of an object that is already stored in S3.
        /// </summary>
        /// <param name="sourceBucket">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="sourceKey">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="sourceVersionId">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="destinationBucket">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="destinationKey">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// 
        /// <returns>The response from the CopyObject service method, as returned by S3.</returns>
        internal CopyObjectResponse CopyObject(string sourceBucket, string sourceKey, string sourceVersionId, string destinationBucket, string destinationKey)
        {
            try
            {
                var request = new CopyObjectRequest();
                request.SourceBucket = sourceBucket;
                request.SourceKey = sourceKey;
                request.SourceVersionId = sourceVersionId;
                request.DestinationBucket = destinationBucket;
                request.DestinationKey = destinationKey;

                return CopyObject(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a copy of an object that is already stored in Obs S3.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the CopyObject service method.</param>
        /// 
        /// <returns>The response from the CopyObject service method, as returned by Obs.</returns>
        /// 
        /// <example>Creates a copy of an object that is already stored in Obs S3:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {
        ///     CopyObjectRequest request = new CopyObjectRequest();
        ///     request.SourceBucket = "sourceBucketName";
        ///     request.SourceKey = "sourceKey";
        ///     request.DestinationBucket = "destinationBucket";
        ///     request.DestinationKey = "destinationKey";
        ///     
        ///     CopyObjectResponse response = client.CopyObject(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when CopyObject: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>CopyObjectResponse</c>'s value.
        /// </example>
        public CopyObjectResponse CopyObject(CopyObjectRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "CopyObject begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (string.IsNullOrEmpty(request.SourceBucket) || string.IsNullOrEmpty(request.SourceBucket) || string.IsNullOrEmpty(request.DestinationBucket) || string.IsNullOrEmpty(request.DestinationBucket))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CopyObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.SourceKey) || string.IsNullOrEmpty(request.SourceKey) || string.IsNullOrEmpty(request.DestinationKey) || string.IsNullOrEmpty(request.DestinationKey))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CopyObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", System.Net.HttpStatusCode.BadRequest);
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("Source bucket: {0}", request.SourceBucket));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Source key: {0}", request.SourceKey));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Source version id: {0}", request.SourceVersionId));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Destination bucket: {0}", request.DestinationBucket));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Destination key: {0}", request.DestinationKey));

                LoggerMgr.Log_Run_Info(strProduct, string.Format("Website redirect location: {0}", request.WebsiteRedirectLocation));
                if (request.UnmodifiedSinceDate != null)
                {
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Unmodified since date: {0}", request.UnmodifiedSinceDate.ToShortDateString()));
                }

                if (request.CannedACL != null)
                {
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("CannedACL: {0}", request.CannedACL.Value));
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("ETag to match: {0}", request.ETagToMatch));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("ETag to not match: {0}", request.ETagToNotMatch));
                if (request.Headers != null)
                {
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Headers: {0}", request.Headers.ToString()));
                }

                CopyObjectResponse response = client.CopyObject(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "CopyObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "CopyObject end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CopyObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetObjectAcl

        /// <summary>
        /// Gets the access control policy for the bucket.
        /// </summary>
        /// <param name="bucketName">Bucket name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="versionId">Version id.</param>
        /// 
        /// <returns>The response from the GetObjectAcl service method, as returned by S3.</returns>
        internal GetACLResponse GetObjectAcl(string bucketName, string keyName, string versionId)
        {
            try
            {
                var request = new GetACLRequest();
                request.BucketName = bucketName;
                request.Key = keyName;
                request.VersionId = versionId;
                return GetObjectAcl(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the access control policy for the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetObjectAcl service method.</param>
        /// 
        /// <returns>The response from the GetObjectAcl service method, as returned by Obs.</returns>
        /// 
        /// <example>Gets the access control policy for the bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetACLRequest request = new GetACLRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "keyName";
        ///     GetACLResponse response = client.GetObjectAcl(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetObjectAcl: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetACLResponse</c>'s value.
        /// </example>
        public GetACLResponse GetObjectAcl(GetACLRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetObjectAcl begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetObjectAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetObjectAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Key name: {0}", request.Key));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Version Id: {0}", request.VersionId));

                GetACLResponse response = client.GetACL(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetObjectAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetObjectAcl end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetObjectAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  SetObjectAcl

        /// <summary>
        /// Sets the permissions on an object using access control lists (ACL).
        /// </summary>
        /// <param name="bucketName">Bucket name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="versionId">Version id.</param>
        /// <param name="acl">Access control lists (ACL)</param>
        /// <returns>The response from the SetBucketAcl service method, as returned by S3.</returns>
        internal PutACLResponse SetObjectAcl(string bucketName, string keyName, string versionId, S3AccessControlList acl)
        {
            try
            {
                PutACLRequest request = new PutACLRequest();
                request.BucketName = bucketName;
                request.AccessControlList = acl;
                return SetObjectAcl(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Sets the permissions on a bucket using access control lists (ACL).
        /// </summary>
        /// <param name="bucketName">A property of PutACLRequest used to execute the SetBucketAcl service method.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="versionId">Version id.</param>
        /// <param name="cannedACL">Canned ACL</param>
        /// <returns>The response from the SetBucketAcl service method, as returned by S3.</returns>
        internal PutACLResponse SetObjectAcl(string bucketName, string keyName, string versionId, S3CannedACL cannedACL)
        {
            try
            {
                PutACLRequest request = new PutACLRequest();
                request.BucketName = bucketName;
                request.CannedACL = cannedACL;
                return SetObjectAcl(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Sets the permissions on a object using access control lists (ACL).
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the SetObjectAcl service method.</param>
        /// 
        /// <returns>The response from the SetObjectAcl service method, as returned by S3.</returns>
        /// 
        /// <example>Sets the permissions on a object using ACL:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PutACLRequest request = new PutACLRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "keyName";
        ///     PutACLResponse response = client.SetObjectAcl(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when SetObjectAcl {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutACLResponse</c>'s value.
        /// </example>
        public PutACLResponse SetObjectAcl(PutACLRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetObjectAcl begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetObjectAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetObjectAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", HttpStatusCode.BadRequest);
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Key name: {0}", request.Key));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Version Id: {0}", request.VersionId));

                if (request.CannedACL != null)
                {
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Canned ACL: {0}", request.CannedACL.Value));
                }

                if (request.AccessControlList != null)
                {
                    foreach (S3Grant grant in request.AccessControlList.Grants)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Grant permission value: {0}", grant.Permission.Value));
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Grant grantee canonical user: {0}", grant.Grantee.CanonicalUser));
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Grant grantee display name: {0}", grant.Grantee.DisplayName));

                        LoggerMgr.Log_Run_Info(strProduct, string.Format("Grant grantee URI: {0}", grant.Grantee.URI));
                    }
                }

                PutACLResponse response = client.PutACL(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "SetObjectAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "SetObjectAcl end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetObjectAcl", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  UploadPart

        /// <summary>
        /// Uploads a part in a multipart upload.
        /// 
        ///  
        /// <para>
        /// <b>Note:</b> After you initiate multipart upload and upload one or more parts, you
        /// must either complete or abort multipart upload in order to stop getting charged for
        /// storage of the uploaded parts. Only after you either complete or abort multipart upload,
        /// Obs S3 frees up the parts storage and stops charging you for the parts storage.
        /// </para>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UploadPart service method.</param>
        /// 
        /// <returns>The response from the UploadPart service method, as returned by S3.</returns>
        /// 
        /// <example>Uploads a part in a multipart upload:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     UploadPartRequest request = new UploadPartRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "key";
        ///     request.FilePath = "filePath";
        ///     request.UploadId = "uploadId";
        ///     request.PartSize = 5 * 1024 * 1024;//5M
        /// 
        ///     UploadPartResponse response = client.UploadPart(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when UploadPart: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>UploadPartResponse</c>'s value.
        /// </example>
        public UploadPartResponse UploadPart(UploadPartRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "UploadPart begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "UploadPart", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "UploadPart", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("File path: {0}", request.FilePath));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("File position: {0}", request.FilePosition));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("IVSize: {0}", request.IVSize));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("key name: {0}", request.Key));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Part number: {0}", request.PartNumber));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Part size: {0}", request.PartSize));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Upload Id: {0}", request.UploadId));

                UploadPartResponse response = client.UploadPart(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "UploadPart", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "UploadPart end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "UploadPart", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  CopyPart

        /// <summary>
        /// Uploads a part by copying data from an existing object as data source.
        /// </summary>
        /// <param name="sourceBucket">A property of CopyPartRequest used to execute the CopyPart service method.</param>
        /// <param name="sourceKey">A property of CopyPartRequest used to execute the CopyPart service method.</param>
        /// <param name="destinationBucket">A property of CopyPartRequest used to execute the CopyPart service method.</param>
        /// <param name="destinationKey">A property of CopyPartRequest used to execute the CopyPart service method.</param>
        /// <param name="uploadId">Upload ID identifying the multipart upload whose part is being copied.</param>
        /// 
        /// <returns>The response from the CopyPart service method, as returned by S3.</returns>
        internal CopyPartResponse CopyPart(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey, string uploadId)
        {
            try
            {
                var request = new CopyPartRequest();
                request.SourceBucket = sourceBucket;
                request.SourceKey = sourceKey;

                request.DestinationBucket = destinationBucket;
                request.DestinationKey = destinationKey;
                request.UploadId = uploadId;

                return CopyPart(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Uploads a part by copying data from an existing object as data source.
        /// </summary>
        /// <param name="sourceBucket">A property of CopyPartRequest used to execute the CopyPart service method.</param>
        /// <param name="sourceKey">A property of CopyPartRequest used to execute the CopyPart service method.</param>
        /// <param name="sourceVersionId">A property of CopyPartRequest used to execute the CopyPart service method.</param>
        /// <param name="destinationBucket">A property of CopyPartRequest used to execute the CopyPart service method.</param>
        /// <param name="destinationKey">A property of CopyPartRequest used to execute the CopyPart service method.</param>
        /// <param name="uploadId">Upload ID identifying the multipart upload whose part is being copied.</param>
        /// 
        /// <returns>The response from the CopyPart service method, as returned by S3.</returns>
        internal CopyPartResponse CopyPart(string sourceBucket, string sourceKey, string sourceVersionId, string destinationBucket, string destinationKey, string uploadId)
        {
            try
            {
                var request = new CopyPartRequest();
                request.SourceBucket = sourceBucket;
                request.SourceKey = sourceKey;
                request.SourceVersionId = sourceVersionId;
                request.DestinationBucket = destinationBucket;
                request.DestinationKey = destinationKey;
                request.UploadId = uploadId;

                return CopyPart(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Uploads a part by copying data from an existing object as data source.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the CopyPart service method.</param>
        /// 
        /// <returns>The response from the CopyPart service method, as returned by Obs.</returns>
        /// 
        /// <example>Uploads a part by copying data from an existing object as data source:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {
        ///     CopyPartRequest request = new CopyPartRequest();
        ///     request.DestinationBucket = "bucketName";
        ///     request.DestinationKey = "keyName";
        ///     request.UploadId = "strUploadId";
        ///     request.FirstByte = 1;
        ///     request.LastByte = 2;
        ///     
        ///     request.PartNumber = 1;
        ///     request.SourceBucket = "sourceBucket";
        ///     request.SourceKey = "sourceKey";
        ///     
        ///     CopyPartResponse response = client.CopyPart(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when CopyPart: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>CopyPartResponse</c>'s value.
        /// </example>
        public CopyPartResponse CopyPart(CopyPartRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "CopyPart begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (string.IsNullOrEmpty(request.SourceBucket) || string.IsNullOrEmpty(request.SourceBucket) || string.IsNullOrEmpty(request.DestinationBucket) || string.IsNullOrEmpty(request.DestinationBucket))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CopyPart", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.SourceKey) || string.IsNullOrEmpty(request.SourceKey) || string.IsNullOrEmpty(request.DestinationKey) || string.IsNullOrEmpty(request.DestinationKey))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CopyPart", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", System.Net.HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Source bucket name: {0}", request.SourceBucket));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Source key name: {0}", request.SourceKey));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Source UploadId: {0}", request.UploadId));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Source version Id: {0}", request.SourceVersionId));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Destination bucket name: {0}", request.DestinationBucket));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Destination key name: {0}", request.DestinationKey));

                CopyPartResponse response = client.CopyPart(request);
                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("CopyPart response HttpStatus: {0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "CopyPart", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "CopyPart with request end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CopyPart", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  InitiateMultipartUpload

        /// <summary>
        /// Initiates a multipart upload and returns an upload ID.
        /// 
        ///  
        /// <para>
        /// <b>Note:</b> After you initiate multipart upload and upload one or more parts, you
        /// must either complete or abort multipart upload in order to stop getting charged for
        /// storage of the uploaded parts. Only after you either complete or abort multipart upload,
        /// S3 frees up the parts storage and stops charging you for the parts storage.
        /// </para>
        /// </summary>
        /// <param name="bucketName">A property of InitiateMultipartUploadRequest used to execute the InitiateMultipartUpload service method.</param>
        /// <param name="key">A property of InitiateMultipartUploadRequest used to execute the InitiateMultipartUpload service method.</param>
        /// 
        /// <returns>The response from the InitiateMultipartUpload service method, as returned by S3.</returns>
        internal InitiateMultipartUploadResponse InitiateMultipartUpload(string bucketName, string key)
        {
            try
            {
                var request = new InitiateMultipartUploadRequest();
                request.BucketName = bucketName;
                request.Key = key;

                return InitiateMultipartUpload(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Initiates a multipart upload and returns an upload ID.
        /// 
        ///  
        /// <para>
        /// <b>Note:</b> After you initiate multipart upload and upload one or more parts, you
        /// must either complete or abort multipart upload in order to stop getting charged for
        /// storage of the uploaded parts. Only after you either complete or abort multipart upload,
        /// Obs S3 frees up the parts storage and stops charging you for the parts storage.
        /// </para>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the InitiateMultipartUpload service method.</param>
        /// 
        /// <returns>The response from the InitiateMultipartUpload service method, as returned by S3.</returns>
        /// 
        /// <example>Initiates a multipart upload and returns an upload ID:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     var request = new InitiateMultipartUploadRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "key";
        ///     InitiateMultipartUploadResponse response = client.InitiateMultipartUpload(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when InitiateMultipartUpload: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>InitiateMultipartUploadResponse</c>'s value.
        /// </example>
        public InitiateMultipartUploadResponse InitiateMultipartUpload(InitiateMultipartUploadRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "InitiateMultipartUpload begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "InitiateMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "InitiateMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", System.Net.HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Key name: {0}", request.Key));
                if (request.CannedACL != null)
                {
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Canned ACL: {0}", request.CannedACL.Value));
                }

                InitiateMultipartUploadResponse response = client.InitiateMultipartUpload(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "InitiateMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "InitiateMultipartUpload end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "InitiateMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  CompleteMultipartUpload

        /// <summary>
        /// Completes a multipart upload by assembling previously uploaded parts.
        /// </summary>
        /// <param name="request">Container for the parameters to the CompleteMultipartUpload operation.</param>
        /// 
        /// <returns>The response from the CompleteMultipartUpload service method, as returned by Obs.</returns>
        /// 
        /// <example>Completes a multipart upload by assembling previously uploaded parts:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {
        ///     request.BucketName = "bucketName";
        ///     request.Key = "keyName";
        ///     request.UploadId = "strUploadId";
        ///     request.PartETags = partETag;
        ///     CompleteMultipartUploadResponse response = client.CompleteMultipartUpload(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when CompleteMultipartUpload: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>CompleteMultipartUploadResponse</c>'s value.
        /// </example>
        public CompleteMultipartUploadResponse CompleteMultipartUpload(CompleteMultipartUploadRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "CompleteMultipartUpload begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CompleteMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CompleteMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", System.Net.HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Key name: {0}", request.Key));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Upload Id: {0}", request.UploadId));

                CompleteMultipartUploadResponse response = client.CompleteMultipartUpload(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "CompleteMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "CompleteMultipartUpload end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "CompleteMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  AbortMultipartUpload

        /// <summary>
        /// Aborts a multipart upload.
        /// 
        ///  
        /// <para>
        /// To verify that all parts have been removed, so you don't get charged for the part
        /// storage, you should call the List Parts operation and ensure the parts list is empty.
        /// </para>
        /// </summary>
        /// <param name="bucketName">A property of AbortMultipartUploadRequest used to execute the AbortMultipartUpload service method.</param>
        /// <param name="key">A property of AbortMultipartUploadRequest used to execute the AbortMultipartUpload service method.</param>
        /// <param name="uploadId">A property of AbortMultipartUploadRequest used to execute the AbortMultipartUpload service method.</param>
        /// 
        /// <returns>The response from the AbortMultipartUpload service method, as returned by S3.</returns>
        internal AbortMultipartUploadResponse AbortMultipartUpload(string bucketName, string key, string uploadId)
        {
            try
            {
                var request = new AbortMultipartUploadRequest();
                request.BucketName = bucketName;
                request.Key = key;
                request.UploadId = uploadId;
                return AbortMultipartUpload(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Aborts a multipart upload.
        /// 
        ///  
        /// <para>
        /// To verify that all parts have been removed, so you don't get charged for the part
        /// storage, you should call the List Parts operation and ensure the parts list is empty.
        /// </para>
        /// </summary>
        /// <param name="request">The parameters to request an abort of a multipart upload.</param>
        /// 
        /// <returns>The response from the AbortMultipartUpload service method, as returned by Obs.</returns>
        /// 
        /// <example>Aborts a multipart upload:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {
        ///     AbortMultipartUploadRequest request = new AbortMultipartUploadRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "keyName";
        ///     request.UploadId = "strUploadId";
        ///     
        ///     AbortMultipartUploadResponse response = client.AbortMultipartUpload(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when AbortMultipartUpload: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>AbortMultipartUploadResponse</c>'s value.
        /// </example>
        public AbortMultipartUploadResponse AbortMultipartUpload(AbortMultipartUploadRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "AbortMultipartUpload begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "AbortMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "AbortMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Key name: {0}", request.Key));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Upload Id: {0}", request.UploadId));

                AbortMultipartUploadResponse response = client.AbortMultipartUpload(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "AbortMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "AbortMultipartUpload end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "AbortMultipartUpload", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  ListParts

        /// <summary>
        /// Lists the parts that have been uploaded for a specific multipart upload.
        /// </summary>
        /// <param name="bucketName">A property of ListPartsRequest used to execute the ListParts service method.</param>
        /// <param name="key">A property of ListPartsRequest used to execute the ListParts service method.</param>
        /// <param name="uploadId">Upload ID identifying the multipart upload whose parts are being listed.</param>
        /// 
        /// <returns>The response from the ListParts service method, as returned by S3.</returns>
        internal ListPartsResponse ListParts(string bucketName, string key, string uploadId)
        {
            try
            {
                var request = new ListPartsRequest();
                request.BucketName = bucketName;
                request.Key = key;
                request.UploadId = uploadId;
                return ListParts(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Lists the parts that have been uploaded for a specific multipart upload.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListParts service method.</param>
        /// 
        /// <returns>The response from the ListParts service method, as returned by Obs.</returns>
        /// 
        /// <example>Lists the parts that have been uploaded for a specific multipart upload:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     ListPartsRequest request = new ListPartsRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "key";
        ///     request.UploadId = "uploadId";
        ///     ListPartsResponse response = client.ListParts(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when ListParts: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>ListPartsResponse</c>'s value.
        /// </example>
        public ListPartsResponse ListParts(ListPartsRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "ListParts begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "ListParts", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "ListParts", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", System.Net.HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Key name: {0}", request.Key));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Upload Id: {0}", request.UploadId));

                ListPartsResponse response = client.ListParts(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "ListParts", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "ListParts end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "ListParts", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetObject

        /// <summary>
        /// Retrieves objects from OBS.
        /// </summary>
        /// <param name="bucketName">A property of GetObjectRequest used to execute the GetObject service method.</param>
        /// <param name="key">A property of GetObjectRequest used to execute the GetObject service method.</param>
        /// 
        /// <returns>The response from the GetObject service method, as returned by S3.</returns>
        internal GetObjectResponse GetObject(string bucketName, string key)
        {
            try
            {
                var request = new GetObjectRequest();
                request.BucketName = bucketName;
                request.Key = key;

                return GetObject(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves objects from OBS.
        /// </summary>
        /// <param name="bucketName">A property of GetObjectRequest used to execute the GetObject service method.</param>
        /// <param name="key">A property of GetObjectRequest used to execute the GetObject service method.</param>
        /// <param name="versionId">VersionId used to reference a specific version of the object.</param>
        /// 
        /// <returns>The response from the GetObject service method, as returned by S3.</returns>
        internal GetObjectResponse GetObject(string bucketName, string key, string versionId)
        {
            try
            {
                var request = new GetObjectRequest();
                request.BucketName = bucketName;
                request.Key = key;
                request.VersionId = versionId;
                return GetObject(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves objects from Obs S3.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetObject service method.</param>
        /// 
        /// <returns>The response from the GetObject service method, as returned by Obs.</returns>
        /// 
        /// <example>Returns the request storage information of a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetObjectRequest request = new GetObjectRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "key";
        ///     request.VersionId = "versionId";
        ///     GetObjectResponse response = client.GetObject(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetObject: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetObjectResponse</c>'s value.
        /// </example>
        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetObject begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", System.Net.HttpStatusCode.BadRequest);
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Key name: {0}", request.Key));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Etag to match: {0}", request.EtagToMatch));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Etag to not match: {0}", request.EtagToNotMatch));

                LoggerMgr.Log_Run_Info(strProduct, string.Format("Version Id: {0}", request.VersionId));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Modified since date: {0}", request.ModifiedSinceDate.ToShortDateString()));

                GetObjectResponse response = client.GetObject(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetObject end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetObjectMetadata

        /// <summary>
        /// The HEAD operation retrieves metadata from an object without returning the object
        /// itself. This operation is useful if you're only interested in an object's metadata.
        /// To use HEAD, you must have READ access to the object.
        /// </summary>
        /// <param name="bucketName">A property of GetObjectMetadataRequest used to execute the GetObjectMetadata service method.</param>
        /// <param name="key">A property of GetObjectMetadataRequest used to execute the GetObjectMetadata service method.</param>
        /// 
        /// <returns>The response from the GetObjectMetadata service method, as returned by S3.</returns>
        internal GetObjectMetadataResponse GetObjectMetadata(string bucketName, string key)
        {
            try
            {
                var request = new GetObjectMetadataRequest();
                request.BucketName = bucketName;
                request.Key = key;
                return GetObjectMetadata(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// The HEAD operation retrieves metadata from an object without returning the object
        /// itself. This operation is useful if you're only interested in an object's metadata.
        /// To use HEAD, you must have READ access to the object.
        /// </summary>
        /// <param name="bucketName">A property of GetObjectMetadataRequest used to execute the GetObjectMetadata service method.</param>
        /// <param name="key">A property of GetObjectMetadataRequest used to execute the GetObjectMetadata service method.</param>
        /// <param name="versionId">VersionId used to reference a specific version of the object.</param>
        /// 
        /// <returns>The response from the GetObjectMetadata service method, as returned by S3.</returns>
        internal GetObjectMetadataResponse GetObjectMetadata(string bucketName, string key, string versionId)
        {
            try
            {
                var request = new GetObjectMetadataRequest();
                request.BucketName = bucketName;
                request.Key = key;
                request.VersionId = versionId;
                return GetObjectMetadata(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// The HEAD operation retrieves metadata from an object without returning the object
        /// itself. This operation is useful if you're only interested in an object's metadata.
        /// To use HEAD, you must have READ access to the object.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetObjectMetadata service method.</param>
        /// 
        /// <returns>The response from the GetObjectMetadata service method, as returned by S3.</returns>
        /// 
        /// <example>Retrieves metadata from an object without returning the object itself:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetObjectMetadataRequest request = new GetObjectMetadataRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "key";
        ///     request.VersionId = "versionId";
        ///     GetObjectMetadataResponse response = client.GetObjectMetadata(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetObjectMetadata: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetObjectMetadataResponse</c>'s value.
        /// </example>
        public GetObjectMetadataResponse GetObjectMetadata(GetObjectMetadataRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetObjectMetadata begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetObjectMetadata", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetObjectMetadata", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", System.Net.HttpStatusCode.BadRequest);
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Key name: {0}", request.Key));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Version Id: {0}", request.VersionId));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Etag to match: {0}", request.EtagToMatch));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Etag to not match: {0}", request.EtagToNotMatch));

                GetObjectMetadataResponse response = client.GetObjectMetadata(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetObjectMetadata", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetObjectMetadata end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetObjectMetadata", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  DeleteObject

        /// <summary>
        /// Removes the null version (if there is one) of an object and inserts a delete marker,
        /// which becomes the latest version of the object. If there isn't a null version, 
        /// S3 does not remove any objects.
        /// </summary>
        /// <param name="bucketName">A property of DeleteObjectRequest used to execute the DeleteObject service method.</param>
        /// <param name="key">A property of DeleteObjectRequest used to execute the DeleteObject service method.</param>
        /// 
        /// <returns>The response from the DeleteObject service method, as returned by S3.</returns>
        internal DeleteObjectResponse DeleteObject(string bucketName, string key)
        {
            try
            {
                var request = new DeleteObjectRequest();
                request.BucketName = bucketName;
                request.Key = key;
                return DeleteObject(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the null version (if there is one) of an object and inserts a delete marker,
        /// which becomes the latest version of the object. If there isn't a null version, 
        /// S3 does not remove any objects.
        /// </summary>
        /// <param name="bucketName">A property of DeleteObjectRequest used to execute the DeleteObject service method.</param>
        /// <param name="key">A property of DeleteObjectRequest used to execute the DeleteObject service method.</param>
        /// <param name="versionId">VersionId used to reference a specific version of the object.</param>
        /// 
        /// <returns>The response from the DeleteObject service method, as returned by S3.</returns>
        internal DeleteObjectResponse DeleteObject(string bucketName, string key, string versionId)
        {
            try
            {
                var request = new DeleteObjectRequest();
                request.BucketName = bucketName;
                request.Key = key;
                request.VersionId = versionId;
                return DeleteObject(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the null version (if there is one) of an object and inserts a delete marker,
        /// which becomes the latest version of the object. If there isn't a null version, Obs
        /// S3 does not remove any objects.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteObject service method.</param>
        /// 
        /// <returns>The response from the DeleteObject service method, as returned by S3.</returns>
        /// 
        /// <example>Removes the null version (if there is one) of an object and inserts a delete marker:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     DeleteObjectRequest request = new DeleteObjectRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "key";
        ///     request.VersionId = "versionId";
        ///     DeleteObjectResponse response = client.DeleteObject(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when DeleteObject: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>DeleteObjectResponse</c>'s value.
        /// </example>
        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "DeleteObject begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", System.Net.HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Key name: {0}", request.Key));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Version Id: {0}", request.VersionId));

                DeleteObjectResponse response = client.DeleteObject(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "DeleteObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "DeleteObject end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  DeleteObjects

        /// <summary>
        /// This operation enables you to delete multiple objects from a bucket using a single
        /// HTTP request. You may specify up to 1000 keys.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteObjects service method.</param>
        /// 
        /// <returns>The response from the DeleteObjects service method, as returned by Obs.</returns>
        /// 
        /// <example>Delete multiple objects from a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     DeleteObjectRequest request = new DeleteObjectRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Quiet = true;
        ///     request.AddKey("key1Name");
        ///     request.AddKey("key2Name");
        ///     request.AddKey("key3Name");
        ///     DeleteObjectResponse response = client.DeleteObjects(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when DeleteObjects: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>DeleteObjectsResponse</c>'s value.
        /// </example>
        public DeleteObjectsResponse DeleteObjects(DeleteObjectsRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "DeleteObjects begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteObjects", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                foreach (KeyVersion keys in request.Objects)
                {
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Key: {0}", keys.Key));
                    LoggerMgr.Log_Run_Info(strProduct, string.Format("Version: {0}", keys.VersionId));
                }

                DeleteObjectsResponse response = client.DeleteObjects(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "DeleteObjects", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "DeleteObjects end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteObjects", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  Options bucket

        /// <summary>
        /// This operation enables you to check whether have rights.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the OptionsBucket service method.</param>
        /// <returns>The response from the OptionsBucketResponse service method, as returned by Obs.</returns>
        /// 
        /// <example>Options from a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     OptionsBucketRequest request = new OptionsBucketRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Origin = "www.huawei.com";
        ///     
        ///     request.AccessControlRequestMethod = new List<![CDATA[<string>]]>();
        ///     request.AccessControlRequestMethod.Add("PUT");
        ///     request.AccessControlRequestMethod.Add("GET");
        ///     request.AccessControlRequestMethod.Add("HEAD");
        ///     request.AccessControlRequestMethod.Add("DELETE");
        ///     
        ///     request.AccessControlRequestHeaders = new List<![CDATA[<string>]]>();
        ///     request.AccessControlRequestHeaders.Add("AllowedHeader_1");
        ///     request.AccessControlRequestHeaders.Add("AllowedHeader_2");
        ///     
        ///     OptionsBucketResponse response = client.OptionsBucket(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when OptionsBucket: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>OptionsBucketResponse</c>'s value.
        /// </example>
        public OptionsBucketResponse OptionsBucket(OptionsBucketRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "OptionsBucket begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "OptionsBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                OptionsBucketResponse response = client.OptionsBucket(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "OptionsBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "OptionsBucket end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "OptionsBucket", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region Options object

        /// <summary>
        /// This operation enables you to check whether have rights.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the OptionsObject service method.</param>
        /// 
        /// <returns>The response from the OptionsObjectResponse service method, as returned by Obs.</returns>
        /// 
        /// <example>Options from a object:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     OptionsObjectRequest request = new OptionsObjectRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "key";
        ///     request.Origin = "www.huawei.com";
        ///     
        ///     request.AccessControlRequestMethod = new List<![CDATA[<string>]]>();
        ///     request.AccessControlRequestMethod.Add("PUT");
        ///     request.AccessControlRequestMethod.Add("GET");
        ///     request.AccessControlRequestMethod.Add("HEAD");
        ///     request.AccessControlRequestMethod.Add("DELETE");
        ///     
        ///     request.AccessControlRequestHeaders = new List<![CDATA[<string>]]>();
        ///     request.AccessControlRequestHeaders.Add("AllowedHeader_1");
        ///     request.AccessControlRequestHeaders.Add("AllowedHeader_2");
        ///     
        ///     OptionsObjectResponse response = client.OptionsObject(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when OptionsObject: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>OptionsObjectResponse</c>'s value.
        /// </example>
        public OptionsObjectResponse OptionsObject(OptionsObjectRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "OptionsObject begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "OptionsObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, ErrorType.Sender, S3Constants.InvalidBucketName, "", HttpStatusCode.BadRequest);
                }

                //2015-6-10 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "OptionsObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, ErrorType.Sender, S3Constants.NoSuchKey, "", HttpStatusCode.BadRequest);
                }

                OptionsObjectResponse response = client.OptionsObject(request);

                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "OptionsObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "OptionsObject end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "OptionsObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }
        #endregion

        #region GetPreSignedURL
        /// <summary>
        /// This operation create a signed URL allowing access to a resource that would usually require authentication. 
        /// </summary>
        /// <param name="request">The GetPreSignedUrlRequest that defines the parameters of the operation.</param>
        /// 
        /// <returns>A string that is the signed http request.</returns>
        /// 
        /// <example>The following examples show how to create various different pre-signed URLs. 
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {        
        ///     GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
        ///     {
        ///         BucketName = "bucketName",
        ///         Key = "keyName",
        ///         Expires = DateTime.Now.AddMinutes(5)
        ///     };
        ///     
        ///     string strURL = client.GetPreSignedURL(request);        
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetPreSignedURL: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>string</c> value.
        /// </example>
        public GetPreSignedUrlResponse GetPreSignedURL(GetPreSignedUrlRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetPreSignedURL begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null)
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetPreSignedURL", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, ErrorType.Sender, S3Constants.InvalidBucketName, "", HttpStatusCode.BadRequest);
                }

                //2015-6-10 w00322557

                GetPreSignedUrlResponse response = client.GetPreSignedURL(request);
                string strResultOut = "GetPreSignedURL successfully!";
                //2015-5-30 w00322557   
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "GetPreSignedURL", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), strResultOut, "request");
                LoggerMgr.Log_Run_Info(strProduct, strResultOut);
                LoggerMgr.Log_Run_Info(strProduct, "GetPreSignedURL end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetPreSignedURL", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }
        #endregion
        #region RestoreObject
        /// <summary>
        /// The HEAD operation retrieves metadata from an object without returning the object
        /// itself. This operation is useful if you're only interested in an object's metadata.
        /// To use HEAD, you must have READ access to the object.
        /// </summary>
        /// <param name="bucketName">A property of GetObjectMetadataRequest used to execute the GetObjectMetadata service method.</param>
        /// <param name="key">A property of GetObjectMetadataRequest used to execute the GetObjectMetadata service method.</param>
        /// <param name="versionId">VersionId used to reference a specific version of the object.</param>
        /// 
        /// <returns>The response from the GetObjectMetadata service method, as returned by S3.</returns>
        internal RestoreObjectResponse RestoreObject(string bucketName, string key, string versionId)
        {
            try
            {
                var request = new RestoreObjectRequest();
                request.BucketName = bucketName;
                request.Key = key;
                request.VersionId = versionId;
                return RestoreObject(request);
            }
            catch (ObsS3Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// The HEAD operation retrieves metadata from an object without returning the object
        /// itself. This operation is useful if you're only interested in an object's metadata.
        /// To use HEAD, you must have READ access to the object.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetObjectMetadata service method.</param>
        /// 
        /// <returns>The response from the GetObjectMetadata service method, as returned by S3.</returns>
        /// 
        /// <example>Retrieves metadata from an object without returning the object itself:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetObjectMetadataRequest request = new GetObjectMetadataRequest();
        ///     request.BucketName = "bucketName";
        ///     request.Key = "key";
        ///     request.VersionId = "versionId";
        ///     GetObjectMetadataResponse response = client.GetObjectMetadata(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetObjectMetadata: {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetObjectMetadataResponse</c>'s value.
        /// </example>
        public RestoreObjectResponse RestoreObject(RestoreObjectRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "RestoreObject begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "RestoreObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                //2015-5-28 w00322557
                if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Key))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "RestoreObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), S3Constants.NoSuchKey);
                    throw new ObsS3Exception(S3Constants.NoSuchKeyMessage, OBS.Runtime.ErrorType.Sender, S3Constants.NoSuchKey, "", System.Net.HttpStatusCode.BadRequest);
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("Bucket name: {0}", request.BucketName));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Key name: {0}", request.Key));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Version Id: {0}", request.VersionId));
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Tier: {0}", request.Tier));

                RestoreObjectResponse response = client.RestoreObject(request);
                //2015-5-30 w00322557   
                LoggerMgr.Log_Run_Info(strProduct, string.Format("Response HttpStatusCode={0}", response.HttpStatusCode));
                LoggerMgr.Log_Interface_Info(strProduct, "1", "Rest", "RestoreObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), response.HttpStatusCode.ToString(), "request");
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "RestoreObject end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "RestoreObject", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  SetBucketTagging

        /// <summary>
        /// Sets the tagging for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketTaggingRequest used to execute the PutBucketTagging service method.</param>
        /// 
        /// <returns>The response from the SetBucketTagging service method, as returned by S3.</returns>
        internal PutBucketTaggingResponse SetBucketTagging(string bucketName)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetBucketTagging with bucketName begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                var request = new PutBucketTaggingRequest();
                request.BucketName = bucketName;
                LoggerMgr.Log_Run_Info(strProduct, "SetBucketTagging with bucket name end.");
                return SetBucketTagging(request);
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("SetBucketTagging exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketTagging", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Sets the tagging for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the SetBucketTagging service method.</param>
        /// 
        /// <returns>The response from the SetBucketTagging service method, as returned by S3.</returns>
        /// 
        /// <example>Sets the tagging for a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PutBucketTaggingRequest request = new PutBucketTaggingRequest();
        ///     request.BucketName = "bucketName";
        ///     PutBucketTaggingResponse response = client.SetBucketTagging(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when SetBucketTagging {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutBucketTaggingResponse</c>'s value.
        /// </example>
        public PutBucketTaggingResponse SetBucketTagging(PutBucketTaggingRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "SetBucketTagging begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketTagging", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("BucketName={0}", request.BucketName));

                PutBucketTaggingResponse response = client.PutBucketTagging(request);
                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("HttpStatusCode={0}", response.HttpStatusCode));
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "SetBucketTagging with request end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("SetBucketTagging exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "SetBucketTagging", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion

        #region  GetBucketTagging

        /// <summary>
        /// Getthe tagging for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketTaggingRequest used to execute the GetBucketTagging service method.</param>
        /// 
        /// <returns>The response from the GetBucketTagging service method, as returned by S3.</returns>
        internal GetBucketTaggingResponse GetBucketTagging(string bucketName)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketTagging with bucketName begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                var request = new GetBucketTaggingRequest();
                request.BucketName = bucketName;
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketTagging with bucket name end.");
                return GetBucketTagging(request);
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("GetBucketTagging exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GeteBucketTagging", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Get the tagging for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketTagging service method.</param>
        /// 
        /// <returns>The response from the GetBucketTagging service method, as returned by S3.</returns>
        /// 
        /// <example>Get the tagging for a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     GetBucketTaggingRequest request = new GetBucketTaggingRequest();
        ///     request.BucketName = "bucketName";
        ///     GetBucketTaggingResponse response = client.GetBucketTagging(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when GetBucketTagging {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>GetBucketTaggingResponse</c>'s value.
        /// </example>
        public GetBucketTaggingResponse GetBucketTagging(GetBucketTaggingRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "GetBucketTaggingRequest begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketTagging", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("BucketName={0}", request.BucketName));

                GetBucketTaggingResponse response = client.GetBucketTagging(request);
                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("HttpStatusCode={0}", response.HttpStatusCode));
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "GetBucketTaggingRequest with request end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("GetBucketTaggingRequest exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "GetBucketTaggingRequest", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion
        #region DeleteBucketTagging

        /// <summary>
        /// Delete the tagging for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of DeteBucketTaggingRequest used to execute the DeleteBucketTagging service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketTagging service method, as returned by S3.</returns>
        internal DeleteBucketTaggingResponse DeleteBucketTagging(string bucketName)
        {
            LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketTagging with bucketName begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                var request = new DeleteBucketTaggingRequest();
                request.BucketName = bucketName;
                LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketTagging with bucket name end.");
                return DeleteBucketTagging(request);
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("DeleteBucketTagging exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucketTagging", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Delete the tagging for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketTagging service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketTagging service method, as returned by S3.</returns>
        /// 
        /// <example>Delete the tagging for a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     DeleteBucketTaggingRequest request = new DeleteBucketTaggingRequest();
        ///     request.BucketName = "bucketName";
        ///     DeleteBucketTaggingResponse response = client.DeleteBucketTagging(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when DeleteBucketTagging {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>DeleteBucketTaggingResponse</c>'s value.
        /// </example>
        public DeleteBucketTaggingResponse DeleteBucketTagging(DeleteBucketTaggingRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketTagging begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucketTagging", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }

                LoggerMgr.Log_Run_Info(strProduct, string.Format("BucketName={0}", request.BucketName));

                DeleteBucketTaggingResponse response = client.DeleteBucketTagging(request);
                //2015-5-30 w00322557 
                LoggerMgr.Log_Run_Info(strProduct, string.Format("HttpStatusCode={0}", response.HttpStatusCode));
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "DeleteBucketTagging with request end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("DeleteBucketTagging exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "DeleteBucketTagging", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }

        #endregion
        #region PutBucketNotificationConfiguration
        /// <summary>
        /// Put the notificationconfiguration for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketNotificationConfiguration used to execute the PutBucketNotificationConfiguration service method.</param>
        /// 
        /// <returns>The response from the PutBucketNotificationConfiguration service method, as returned by S3.</returns>
        internal PutBucketNotificationResponse PutBucketNotificationConfiguration(string bucketName)
        {
            LoggerMgr.Log_Run_Info(strProduct, "PutBucketNotificationConfiguration with bucketName begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                var request = new PutBucketNotificationRequest();
                request.BucketName = bucketName;
                LoggerMgr.Log_Run_Info(strProduct, "PutBucketNotificationConfiguration with bucket name end.");
                return PutBucketNotificationConfiguration(request);
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("PutBucketNotificationConfiguration exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "PutBucketNotificationConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Put the notificationconfiguration for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutBucketNotificationConfiguration service method.</param>
        /// 
        /// <returns>The response from the PutBucketNotificationConfiguration service method, as returned by S3.</returns>
        /// 
        /// <example>Put the notificationconfigurationg for a bucket:
        /// <code>
        /// ObsClient client = new ObsClient("AccessKeyID", "SecretAccessKey", config);
        /// try
        /// {   
        ///     PutBucketNotificationRequest request = new PutBucketNotificationRequest();
        ///     request.BucketName = "bucketName";
        ///     PutBucketNotificationResponse response = client.PutBucketNotificationConfiguration(request);
        ///  }
        ///  catch (ObsS3Exception exception)
        ///  {
        ///     Console.WriteLine("An Error number: {0}, occurred when PutBucketNotificationConfiguration {1}", exception.ErrorCode, exception.Message);
        ///  }
        /// </code>
        /// results in <c>PutBucketNotificationResponse</c>'s value.
        /// </example>
        public PutBucketNotificationResponse PutBucketNotificationConfiguration(PutBucketNotificationRequest request)
        {
            LoggerMgr.Log_Run_Info(strProduct, "PutBucketNotificationConfiguration begin.");
            DateTime reqTime = System.DateTime.Now;
            try
            {
                if (request == null || string.IsNullOrEmpty(request.BucketName))
                {
                    LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "PutBucketNotificationConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), HttpStatusCode.BadRequest.ToString(), "BuckerName=" + S3Constants.InvalidBucketName);
                    throw new ObsS3Exception(S3Constants.InvalidBucketNameMessage, OBS.Runtime.ErrorType.Sender, S3Constants.InvalidBucketName, "", System.Net.HttpStatusCode.BadRequest);
                }
                LoggerMgr.Log_Run_Info(strProduct, string.Format("BucketName={0}", request.BucketName));
                PutBucketNotificationResponse response = client.PutBucketNotificationConfiguration(request);
                LoggerMgr.Log_Run_Info(strProduct, string.Format("HttpStatusCode={0}", response.HttpStatusCode));
                if (response.ResponseMetadata != null)
                {
                    LoggerMgr.Log_Run_Debug(strProduct, string.Format("RequestId={0}", response.ResponseMetadata.RequestId));
                    foreach (var head in response.ResponseMetadata.Metadata)
                    {
                        LoggerMgr.Log_Run_Info(strProduct, string.Format("ResponseMetadata key={0},value={1}", head.Key, head.Value));
                    }
                }
                LoggerMgr.Log_Run_Info(strProduct, "PutBucketNotificationConfiguration with request end.");
                return response;
            }
            catch (ObsS3Exception ex)
            {
                LoggerMgr.Log_Run_Error(strProduct, string.Format("PutBucketNotificationConfiguration exception code: {0}, with message: {1}", ex.ErrorCode, ex.Message));
                LoggerMgr.Log_Interface_Error(strProduct, "1", "Rest", "PutBucketNotificationConfiguration", "", string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", reqTime), string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", DateTime.Now), ex.ErrorCode, ex.Message);
                throw;
            }
        }
        #endregion
    }
}
        #endregion