/*
 * Copyright 2010-2014 OBS.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */

/*
 * Do not modify this file. This file is generated from the s3-2006-03-01.normal.json service model.
 */


using System;
using System.Collections.Generic;

using OBS.S3.Model;

namespace OBS.S3
{
    /// <summary>
    /// Interface for accessing S3
    ///
    /// 
    /// </summary>
    public partial interface IObsS3 : IDisposable
    {

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
        AbortMultipartUploadResponse AbortMultipartUpload(string bucketName, string key, string uploadId);

        /// <summary>
        /// Aborts a multipart upload.
        /// 
        ///  
        /// <para>
        /// To verify that all parts have been removed, so you don't get charged for the part
        /// storage, you should call the List Parts operation and ensure the parts list is empty.
        /// </para>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the AbortMultipartUpload service method.</param>
        /// 
        /// <returns>The response from the AbortMultipartUpload service method, as returned by S3.</returns>
        AbortMultipartUploadResponse AbortMultipartUpload(AbortMultipartUploadRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the AbortMultipartUpload operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the AbortMultipartUpload operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndAbortMultipartUpload
        ///         operation.</returns>
        IAsyncResult BeginAbortMultipartUpload(AbortMultipartUploadRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  AbortMultipartUpload operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginAbortMultipartUpload.</param>
        /// 
        /// <returns>Returns a  AbortMultipartUploadResult from S3.</returns>
        AbortMultipartUploadResponse EndAbortMultipartUpload(IAsyncResult asyncResult);

        #endregion

        #region  CompleteMultipartUpload

        /// <summary>
        /// Completes a multipart upload by assembling previously uploaded parts.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the CompleteMultipartUpload service method.</param>
        /// 
        /// <returns>The response from the CompleteMultipartUpload service method, as returned by S3.</returns>
        CompleteMultipartUploadResponse CompleteMultipartUpload(CompleteMultipartUploadRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the CompleteMultipartUpload operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CompleteMultipartUpload operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndCompleteMultipartUpload
        ///         operation.</returns>
        IAsyncResult BeginCompleteMultipartUpload(CompleteMultipartUploadRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  CompleteMultipartUpload operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginCompleteMultipartUpload.</param>
        /// 
        /// <returns>Returns a  CompleteMultipartUploadResult from S3.</returns>
        CompleteMultipartUploadResponse EndCompleteMultipartUpload(IAsyncResult asyncResult);

        #endregion

        #region  CopyObject

        /// <summary>
        /// Creates a copy of an object that is already stored in Amazon S3.
        /// </summary>
        /// <param name="sourceBucket">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="sourceKey">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="destinationBucket">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="destinationKey">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// 
        /// <returns>The response from the CopyObject service method, as returned by S3.</returns>
        CopyObjectResponse CopyObject(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey);

        /// <summary>
        /// Creates a copy of an object that is already stored in Amazon S3.
        /// </summary>
        /// <param name="sourceBucket">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="sourceKey">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="sourceVersionId">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="destinationBucket">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// <param name="destinationKey">A property of CopyObjectRequest used to execute the CopyObject service method.</param>
        /// 
        /// <returns>The response from the CopyObject service method, as returned by S3.</returns>
        CopyObjectResponse CopyObject(string sourceBucket, string sourceKey, string sourceVersionId, string destinationBucket, string destinationKey);

        /// <summary>
        /// Creates a copy of an object that is already stored in Amazon S3.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the CopyObject service method.</param>
        /// 
        /// <returns>The response from the CopyObject service method, as returned by S3.</returns>
        CopyObjectResponse CopyObject(CopyObjectRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the CopyObject operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CopyObject operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndCopyObject
        ///         operation.</returns>
        IAsyncResult BeginCopyObject(CopyObjectRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  CopyObject operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginCopyObject.</param>
        /// 
        /// <returns>Returns a  CopyObjectResult from S3.</returns>
        CopyObjectResponse EndCopyObject(IAsyncResult asyncResult);

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
        CopyPartResponse CopyPart(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey, string uploadId);

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
        CopyPartResponse CopyPart(string sourceBucket, string sourceKey, string sourceVersionId, string destinationBucket, string destinationKey, string uploadId);

        /// <summary>
        /// Uploads a part by copying data from an existing object as data source.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the CopyPart service method.</param>
        /// 
        /// <returns>The response from the CopyPart service method, as returned by S3.</returns>
        CopyPartResponse CopyPart(CopyPartRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the CopyPart operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CopyPart operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndCopyPart
        ///         operation.</returns>
        IAsyncResult BeginCopyPart(CopyPartRequest request, AsyncCallback callback, object state);
        
        /// <summary>
        /// Finishes the asynchronous execution of the  CopyPart operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginCopyPart.</param>
        /// 
        /// <returns>Returns a  CopyPartResult from S3.</returns>
        CopyPartResponse EndCopyPart(IAsyncResult asyncResult);

        #endregion

        #region  DeleteBucket

        /// <summary>
        /// Deletes the bucket. All objects (including all object versions and Delete Markers)
        /// in the bucket must be deleted before the bucket itself can be deleted.
        /// </summary>
        /// <param name="bucketName">A property of DeleteBucketRequest used to execute the DeleteBucket service method.</param>
        /// 
        /// <returns>The response from the DeleteBucket service method, as returned by S3.</returns>
        DeleteBucketResponse DeleteBucket(string bucketName);

        /// <summary>
        /// Deletes the bucket. All objects (including all object versions and Delete Markers)
        /// in the bucket must be deleted before the bucket itself can be deleted.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucket service method.</param>
        /// 
        /// <returns>The response from the DeleteBucket service method, as returned by S3.</returns>
        DeleteBucketResponse DeleteBucket(DeleteBucketRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteBucket operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucket operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndDeleteBucket
        ///         operation.</returns>
        IAsyncResult BeginDeleteBucket(DeleteBucketRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  DeleteBucket operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginDeleteBucket.</param>
        /// 
        /// <returns>Returns a  DeleteBucketResult from S3.</returns>
        DeleteBucketResponse EndDeleteBucket(IAsyncResult asyncResult);

        #endregion

        #region  DeleteBucketPolicy

        /// <summary>
        /// Deletes the policy from the bucket.
        /// </summary>
        /// <param name="bucketName">A property of DeleteBucketPolicyRequest used to execute the DeleteBucketPolicy service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketPolicy service method, as returned by S3.</returns>
        DeleteBucketPolicyResponse DeleteBucketPolicy(string bucketName);

        /// <summary>
        /// Deletes the policy from the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketPolicy service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketPolicy service method, as returned by S3.</returns>
        DeleteBucketPolicyResponse DeleteBucketPolicy(DeleteBucketPolicyRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteBucketPolicy operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketPolicy operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndDeleteBucketPolicy
        ///         operation.</returns>
        IAsyncResult BeginDeleteBucketPolicy(DeleteBucketPolicyRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  DeleteBucketPolicy operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginDeleteBucketPolicy.</param>
        /// 
        /// <returns>Returns a  DeleteBucketPolicyResult from S3.</returns>
        DeleteBucketPolicyResponse EndDeleteBucketPolicy(IAsyncResult asyncResult);

        #endregion

        #region  DeleteBucketWebsite

        /// <summary>
        /// This operation removes the website configuration from the bucket.
        /// </summary>
        /// <param name="bucketName">A property of DeleteBucketWebsiteRequest used to execute the DeleteBucketWebsite service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketWebsite service method, as returned by S3.</returns>
        DeleteBucketWebsiteResponse DeleteBucketWebsite(string bucketName);

        /// <summary>
        /// This operation removes the website configuration from the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketWebsite service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketWebsite service method, as returned by S3.</returns>
        DeleteBucketWebsiteResponse DeleteBucketWebsite(DeleteBucketWebsiteRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteBucketWebsite operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketWebsite operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndDeleteBucketWebsite
        ///         operation.</returns>
        IAsyncResult BeginDeleteBucketWebsite(DeleteBucketWebsiteRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  DeleteBucketWebsite operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginDeleteBucketWebsite.</param>
        /// 
        /// <returns>Returns a  DeleteBucketWebsiteResult from S3.</returns>
        DeleteBucketWebsiteResponse EndDeleteBucketWebsite(IAsyncResult asyncResult);

        #endregion
        
        #region  DeleteCORSConfiguration

        /// <summary>
        /// Deletes the cors configuration information set for the bucket.
        /// </summary>
        /// <param name="bucketName">A property of DeleteCORSConfigurationRequest used to execute the DeleteCORSConfiguration service method.</param>
        /// 
        /// <returns>The response from the DeleteCORSConfiguration service method, as returned by S3.</returns>
        DeleteCORSConfigurationResponse DeleteCORSConfiguration(string bucketName);

        /// <summary>
        /// Deletes the cors configuration information set for the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteCORSConfiguration service method.</param>
        /// 
        /// <returns>The response from the DeleteCORSConfiguration service method, as returned by S3.</returns>
        DeleteCORSConfigurationResponse DeleteCORSConfiguration(DeleteCORSConfigurationRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteCORSConfiguration operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteCORSConfiguration operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndDeleteCORSConfiguration
        ///         operation.</returns>
        IAsyncResult BeginDeleteCORSConfiguration(DeleteCORSConfigurationRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  DeleteCORSConfiguration operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginDeleteCORSConfiguration.</param>
        /// 
        /// <returns>Returns a  DeleteCORSConfigurationResult from S3.</returns>
        DeleteCORSConfigurationResponse EndDeleteCORSConfiguration(IAsyncResult asyncResult);

        #endregion

        #region  DeleteLifecycleConfiguration

        /// <summary>
        /// Deletes the lifecycle configuration from the bucket.
        /// </summary>
        /// <param name="bucketName">A property of DeleteLifecycleConfigurationRequest used to execute the DeleteLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the DeleteLifecycleConfiguration service method, as returned by S3.</returns>
        DeleteLifecycleConfigurationResponse DeleteLifecycleConfiguration(string bucketName);

        /// <summary>
        /// Deletes the lifecycle configuration from the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the DeleteLifecycleConfiguration service method, as returned by S3.</returns>
        DeleteLifecycleConfigurationResponse DeleteLifecycleConfiguration(DeleteLifecycleConfigurationRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteLifecycleConfiguration operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteLifecycleConfiguration operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndDeleteLifecycleConfiguration
        ///         operation.</returns>
        IAsyncResult BeginDeleteLifecycleConfiguration(DeleteLifecycleConfigurationRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  DeleteLifecycleConfiguration operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginDeleteLifecycleConfiguration.</param>
        /// 
        /// <returns>Returns a  DeleteLifecycleConfigurationResult from S3.</returns>
        DeleteLifecycleConfigurationResponse EndDeleteLifecycleConfiguration(IAsyncResult asyncResult);

        #endregion

        #region  DeleteObject

        /// <summary>
        /// Removes the null version (if there is one) of an object and inserts a delete marker,
        /// which becomes the latest version of the object. If there isn't a null version, Amazon
        /// S3 does not remove any objects.
        /// </summary>
        /// <param name="bucketName">A property of DeleteObjectRequest used to execute the DeleteObject service method.</param>
        /// <param name="key">A property of DeleteObjectRequest used to execute the DeleteObject service method.</param>
        /// 
        /// <returns>The response from the DeleteObject service method, as returned by S3.</returns>
        DeleteObjectResponse DeleteObject(string bucketName, string key);

        /// <summary>
        /// Removes the null version (if there is one) of an object and inserts a delete marker,
        /// which becomes the latest version of the object. If there isn't a null version, Amazon
        /// S3 does not remove any objects.
        /// </summary>
        /// <param name="bucketName">A property of DeleteObjectRequest used to execute the DeleteObject service method.</param>
        /// <param name="key">A property of DeleteObjectRequest used to execute the DeleteObject service method.</param>
        /// <param name="versionId">VersionId used to reference a specific version of the object.</param>
        /// 
        /// <returns>The response from the DeleteObject service method, as returned by S3.</returns>
        DeleteObjectResponse DeleteObject(string bucketName, string key, string versionId);

        /// <summary>
        /// Removes the null version (if there is one) of an object and inserts a delete marker,
        /// which becomes the latest version of the object. If there isn't a null version, Amazon
        /// S3 does not remove any objects.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteObject service method.</param>
        /// 
        /// <returns>The response from the DeleteObject service method, as returned by S3.</returns>
        DeleteObjectResponse DeleteObject(DeleteObjectRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteObject operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteObject operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndDeleteObject
        ///         operation.</returns>
        IAsyncResult BeginDeleteObject(DeleteObjectRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  DeleteObject operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginDeleteObject.</param>
        /// 
        /// <returns>Returns a  DeleteObjectResult from S3.</returns>
        DeleteObjectResponse EndDeleteObject(IAsyncResult asyncResult);

        #endregion

        #region  DeleteObjects

        /// <summary>
        /// This operation enables you to delete multiple objects from a bucket using a single
        /// HTTP request. You may specify up to 1000 keys.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteObjects service method.</param>
        /// 
        /// <returns>The response from the DeleteObjects service method, as returned by S3.</returns>
        DeleteObjectsResponse DeleteObjects(DeleteObjectsRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteObjects operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteObjects operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndDeleteObjects
        ///         operation.</returns>
        IAsyncResult BeginDeleteObjects(DeleteObjectsRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  DeleteObjects operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginDeleteObjects.</param>
        /// 
        /// <returns>Returns a  DeleteObjectsResult from S3.</returns>
        DeleteObjectsResponse EndDeleteObjects(IAsyncResult asyncResult);

        #endregion

        #region  GetACL

        /// <summary>
        /// Gets the access control policy for the bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetACLRequest used to execute the GetACL service method.</param>
        /// 
        /// <returns>The response from the GetACL service method, as returned by S3.</returns>
        GetACLResponse GetACL(string bucketName);

        /// <summary>
        /// Gets the access control policy for the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetACL service method.</param>
        /// 
        /// <returns>The response from the GetACL service method, as returned by S3.</returns>
        GetACLResponse GetACL(GetACLRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetACL operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetACL operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetACL
        ///         operation.</returns>
        IAsyncResult BeginGetACL(GetACLRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  GetACL operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetACL.</param>
        /// 
        /// <returns>Returns a  GetACLResult from S3.</returns>
        GetACLResponse EndGetACL(IAsyncResult asyncResult);

        #endregion

        #region  GetBucketLocation

        /// <summary>
        /// Returns the region the bucket resides in.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketLocationRequest used to execute the GetBucketLocation service method.</param>
        /// 
        /// <returns>The response from the GetBucketLocation service method, as returned by S3.</returns>
        GetBucketLocationResponse GetBucketLocation(string bucketName);

        /// <summary>
        /// Returns the region the bucket resides in.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketLocation service method.</param>
        /// 
        /// <returns>The response from the GetBucketLocation service method, as returned by S3.</returns>
        GetBucketLocationResponse GetBucketLocation(GetBucketLocationRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetBucketLocation operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetBucketLocation operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetBucketLocation
        ///         operation.</returns>
        IAsyncResult BeginGetBucketLocation(GetBucketLocationRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  GetBucketLocation operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetBucketLocation.</param>
        /// 
        /// <returns>Returns a  GetBucketLocationResult from S3.</returns>
        GetBucketLocationResponse EndGetBucketLocation(IAsyncResult asyncResult);

        #endregion

        #region  GetBucketLogging

        /// <summary>
        /// Returns the logging status of a bucket and the permissions users have to view and
        /// modify that status. To use GET, you must be the bucket owner.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketLoggingRequest used to execute the GetBucketLogging service method.</param>
        /// 
        /// <returns>The response from the GetBucketLogging service method, as returned by S3.</returns>
        GetBucketLoggingResponse GetBucketLogging(string bucketName);

        /// <summary>
        /// Returns the logging status of a bucket and the permissions users have to view and
        /// modify that status. To use GET, you must be the bucket owner.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketLogging service method.</param>
        /// 
        /// <returns>The response from the GetBucketLogging service method, as returned by S3.</returns>
        GetBucketLoggingResponse GetBucketLogging(GetBucketLoggingRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetBucketLogging operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetBucketLogging operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetBucketLogging
        ///         operation.</returns>
        IAsyncResult BeginGetBucketLogging(GetBucketLoggingRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  GetBucketLogging operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetBucketLogging.</param>
        /// 
        /// <returns>Returns a  GetBucketLoggingResult from S3.</returns>
        GetBucketLoggingResponse EndGetBucketLogging(IAsyncResult asyncResult);

        #endregion
        

        #region  GetBucketPolicy

        /// <summary>
        /// Returns the policy of a specified bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketPolicyRequest used to execute the GetBucketPolicy service method.</param>
        /// 
        /// <returns>The response from the GetBucketPolicy service method, as returned by S3.</returns>
        GetBucketPolicyResponse GetBucketPolicy(string bucketName);

        /// <summary>
        /// Returns the policy of a specified bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketPolicy service method.</param>
        /// 
        /// <returns>The response from the GetBucketPolicy service method, as returned by S3.</returns>
        GetBucketPolicyResponse GetBucketPolicy(GetBucketPolicyRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetBucketPolicy operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetBucketPolicy operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetBucketPolicy
        ///         operation.</returns>
        IAsyncResult BeginGetBucketPolicy(GetBucketPolicyRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  GetBucketPolicy operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetBucketPolicy.</param>
        /// 
        /// <returns>Returns a  GetBucketPolicyResult from S3.</returns>
        GetBucketPolicyResponse EndGetBucketPolicy(IAsyncResult asyncResult);

        #endregion

        #region  GetBucketQuota

        /// <summary>
        /// Returns the quota associated with the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketQuota service method.</param>
        /// 
        /// <returns>The response from the GetBucketQuota service method, as returned by S3.</returns>
        GetBucketQuotaResponse GetBucketQuota(GetBucketQuotaRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetBucketQuota operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetBucketQuota operation on ObsS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetBucketQuota
        ///         operation.</returns>
        IAsyncResult BeginGetBucketQuota(GetBucketQuotaRequest request, AsyncCallback callback, object state);

        /// <summary>
        /// Finishes the asynchronous execution of the  GetBucketQuota operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetBucketQuota.</param>
        /// 
        /// <returns>Returns a  GetBucketQuotaResult from S3.</returns>
        GetBucketQuotaResponse EndGetBucketQuota(IAsyncResult asyncResult);

        #endregion           

        #region  GetBucketStorageInfo

        /// <summary>
        /// Returns the storage information associated with the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketStorageInfo service method.</param>
        /// 
        /// <returns>The response from the GetBucketStorageInfo service method, as returned by S3.</returns>
        GetBucketStorageInfoResponse GetBucketStorageInfo(GetBucketStorageInfoRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetBucketStorageInfo operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetBucketStorageInfo operation on ObsS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetBucketStorageInfo
        ///         operation.</returns>
        IAsyncResult BeginGetBucketStorageInfo(GetBucketStorageInfoRequest request, AsyncCallback callback, object state);

        /// <summary>
        /// Finishes the asynchronous execution of the  GetBucketStorageInfo operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetBucketStorageInfo.</param>
        /// 
        /// <returns>Returns a  GetBucketStorageInfoResult from S3.</returns>
        GetBucketStorageInfoResponse EndGetBucketStorageInfo(IAsyncResult asyncResult);

        #endregion
        
        #region  GetBucketVersioning

        /// <summary>
        /// Returns the versioning state of a bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketVersioningRequest used to execute the GetBucketVersioning service method.</param>
        /// 
        /// <returns>The response from the GetBucketVersioning service method, as returned by S3.</returns>
        GetBucketVersioningResponse GetBucketVersioning(string bucketName);

        /// <summary>
        /// Returns the versioning state of a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketVersioning service method.</param>
        /// 
        /// <returns>The response from the GetBucketVersioning service method, as returned by S3.</returns>
        GetBucketVersioningResponse GetBucketVersioning(GetBucketVersioningRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetBucketVersioning operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetBucketVersioning operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetBucketVersioning
        ///         operation.</returns>
        IAsyncResult BeginGetBucketVersioning(GetBucketVersioningRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  GetBucketVersioning operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetBucketVersioning.</param>
        /// 
        /// <returns>Returns a  GetBucketVersioningResult from S3.</returns>
        GetBucketVersioningResponse EndGetBucketVersioning(IAsyncResult asyncResult);

        #endregion

        #region  GetBucketWebsite

        /// <summary>
        /// Returns the website configuration for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetBucketWebsiteRequest used to execute the GetBucketWebsite service method.</param>
        /// 
        /// <returns>The response from the GetBucketWebsite service method, as returned by S3.</returns>
        GetBucketWebsiteResponse GetBucketWebsite(string bucketName);

        /// <summary>
        /// Returns the website configuration for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketWebsite service method.</param>
        /// 
        /// <returns>The response from the GetBucketWebsite service method, as returned by S3.</returns>
        GetBucketWebsiteResponse GetBucketWebsite(GetBucketWebsiteRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetBucketWebsite operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetBucketWebsite operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetBucketWebsite
        ///         operation.</returns>
        IAsyncResult BeginGetBucketWebsite(GetBucketWebsiteRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  GetBucketWebsite operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetBucketWebsite.</param>
        /// 
        /// <returns>Returns a  GetBucketWebsiteResult from S3.</returns>
        GetBucketWebsiteResponse EndGetBucketWebsite(IAsyncResult asyncResult);

        #endregion
        
        #region  GetCORSConfiguration

        /// <summary>
        /// Returns the cors configuration for the bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetCORSConfigurationRequest used to execute the GetCORSConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetCORSConfiguration service method, as returned by S3.</returns>
        GetCORSConfigurationResponse GetCORSConfiguration(string bucketName);

        /// <summary>
        /// Returns the cors configuration for the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetCORSConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetCORSConfiguration service method, as returned by S3.</returns>
        GetCORSConfigurationResponse GetCORSConfiguration(GetCORSConfigurationRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetCORSConfiguration operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetCORSConfiguration operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetCORSConfiguration
        ///         operation.</returns>
        IAsyncResult BeginGetCORSConfiguration(GetCORSConfigurationRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  GetCORSConfiguration operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetCORSConfiguration.</param>
        /// 
        /// <returns>Returns a  GetCORSConfigurationResult from S3.</returns>
        GetCORSConfigurationResponse EndGetCORSConfiguration(IAsyncResult asyncResult);

        #endregion
        
        #region  GetLifecycleConfiguration

        /// <summary>
        /// Returns the lifecycle configuration information set on the bucket.
        /// </summary>
        /// <param name="bucketName">A property of GetLifecycleConfigurationRequest used to execute the GetLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetLifecycleConfiguration service method, as returned by S3.</returns>
        GetLifecycleConfigurationResponse GetLifecycleConfiguration(string bucketName);

        /// <summary>
        /// Returns the lifecycle configuration information set on the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the GetLifecycleConfiguration service method, as returned by S3.</returns>
        GetLifecycleConfigurationResponse GetLifecycleConfiguration(GetLifecycleConfigurationRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetLifecycleConfiguration operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetLifecycleConfiguration operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetLifecycleConfiguration
        ///         operation.</returns>
        IAsyncResult BeginGetLifecycleConfiguration(GetLifecycleConfigurationRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  GetLifecycleConfiguration operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetLifecycleConfiguration.</param>
        /// 
        /// <returns>Returns a  GetLifecycleConfigurationResult from S3.</returns>
        GetLifecycleConfigurationResponse EndGetLifecycleConfiguration(IAsyncResult asyncResult);

        #endregion

        #region  GetObject

        /// <summary>
        /// Retrieves objects from Amazon S3.
        /// </summary>
        /// <param name="bucketName">A property of GetObjectRequest used to execute the GetObject service method.</param>
        /// <param name="key">A property of GetObjectRequest used to execute the GetObject service method.</param>
        /// 
        /// <returns>The response from the GetObject service method, as returned by S3.</returns>
        GetObjectResponse GetObject(string bucketName, string key);

        /// <summary>
        /// Retrieves objects from Amazon S3.
        /// </summary>
        /// <param name="bucketName">A property of GetObjectRequest used to execute the GetObject service method.</param>
        /// <param name="key">A property of GetObjectRequest used to execute the GetObject service method.</param>
        /// <param name="versionId">VersionId used to reference a specific version of the object.</param>
        /// 
        /// <returns>The response from the GetObject service method, as returned by S3.</returns>
        GetObjectResponse GetObject(string bucketName, string key, string versionId);

        /// <summary>
        /// Retrieves objects from Amazon S3.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetObject service method.</param>
        /// 
        /// <returns>The response from the GetObject service method, as returned by S3.</returns>
        GetObjectResponse GetObject(GetObjectRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetObject operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetObject operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetObject
        ///         operation.</returns>
        IAsyncResult BeginGetObject(GetObjectRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  GetObject operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetObject.</param>
        /// 
        /// <returns>Returns a  GetObjectResult from S3.</returns>
        GetObjectResponse EndGetObject(IAsyncResult asyncResult);

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
        GetObjectMetadataResponse GetObjectMetadata(string bucketName, string key);

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
        GetObjectMetadataResponse GetObjectMetadata(string bucketName, string key, string versionId);

        /// <summary>
        /// The HEAD operation retrieves metadata from an object without returning the object
        /// itself. This operation is useful if you're only interested in an object's metadata.
        /// To use HEAD, you must have READ access to the object.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetObjectMetadata service method.</param>
        /// 
        /// <returns>The response from the GetObjectMetadata service method, as returned by S3.</returns>
        GetObjectMetadataResponse GetObjectMetadata(GetObjectMetadataRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the GetObjectMetadata operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetObjectMetadata operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetObjectMetadata
        ///         operation.</returns>
        IAsyncResult BeginGetObjectMetadata(GetObjectMetadataRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  GetObjectMetadata operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetObjectMetadata.</param>
        /// 
        /// <returns>Returns a  GetObjectMetadataResult from S3.</returns>
        GetObjectMetadataResponse EndGetObjectMetadata(IAsyncResult asyncResult);

        #endregion

        
        #region  HeadBucket

        /// <summary>
        /// This operation is useful to determine if a bucket exists and you have permission to
        /// access it.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the HeadBucket service method.</param>
        /// 
        /// <returns>The response from the HeadBucket service method, as returned by S3.</returns>
        HeadBucketResponse HeadBucket(HeadBucketRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the HeadBucket operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the HeadBucket operation on ObsS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndHeadBucket
        ///         operation.</returns>
        IAsyncResult BeginHeadBucket(HeadBucketRequest request, AsyncCallback callback, object state);

        /// <summary>
        /// Finishes the asynchronous execution of the  HeadBucket operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginHeadBucket.</param>
        /// 
        /// <returns>Returns a  HeadBucketResult from S3.</returns>
        HeadBucketResponse EndHeadBucket(IAsyncResult asyncResult);

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
        /// Amazon S3 frees up the parts storage and stops charging you for the parts storage.
        /// </para>
        /// </summary>
        /// <param name="bucketName">A property of InitiateMultipartUploadRequest used to execute the InitiateMultipartUpload service method.</param>
        /// <param name="key">A property of InitiateMultipartUploadRequest used to execute the InitiateMultipartUpload service method.</param>
        /// 
        /// <returns>The response from the InitiateMultipartUpload service method, as returned by S3.</returns>
        InitiateMultipartUploadResponse InitiateMultipartUpload(string bucketName, string key);

        /// <summary>
        /// Initiates a multipart upload and returns an upload ID.
        /// 
        ///  
        /// <para>
        /// <b>Note:</b> After you initiate multipart upload and upload one or more parts, you
        /// must either complete or abort multipart upload in order to stop getting charged for
        /// storage of the uploaded parts. Only after you either complete or abort multipart upload,
        /// Amazon S3 frees up the parts storage and stops charging you for the parts storage.
        /// </para>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the InitiateMultipartUpload service method.</param>
        /// 
        /// <returns>The response from the InitiateMultipartUpload service method, as returned by S3.</returns>
        InitiateMultipartUploadResponse InitiateMultipartUpload(InitiateMultipartUploadRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the InitiateMultipartUpload operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the InitiateMultipartUpload operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndInitiateMultipartUpload
        ///         operation.</returns>
        IAsyncResult BeginInitiateMultipartUpload(InitiateMultipartUploadRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  InitiateMultipartUpload operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginInitiateMultipartUpload.</param>
        /// 
        /// <returns>Returns a  InitiateMultipartUploadResult from S3.</returns>
        InitiateMultipartUploadResponse EndInitiateMultipartUpload(IAsyncResult asyncResult);

        #endregion

        #region  ListBuckets

        /// <summary>
        /// Returns a list of all buckets owned by the authenticated sender of the request.
        /// </summary>
        /// 
        /// <returns>The response from the ListBuckets service method, as returned by S3.</returns>
        ListBucketsResponse ListBuckets();

        /// <summary>
        /// Returns a list of all buckets owned by the authenticated sender of the request.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListBuckets service method.</param>
        /// 
        /// <returns>The response from the ListBuckets service method, as returned by S3.</returns>
        ListBucketsResponse ListBuckets(ListBucketsRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the ListBuckets operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListBuckets operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndListBuckets
        ///         operation.</returns>
        IAsyncResult BeginListBuckets(ListBucketsRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  ListBuckets operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginListBuckets.</param>
        /// 
        /// <returns>Returns a  ListBucketsResult from S3.</returns>
        ListBucketsResponse EndListBuckets(IAsyncResult asyncResult);

        #endregion

        #region  ListMultipartUploads

        /// <summary>
        /// This operation lists in-progress multipart uploads.
        /// </summary>
        /// <param name="bucketName">A property of ListMultipartUploadsRequest used to execute the ListMultipartUploads service method.</param>
        /// 
        /// <returns>The response from the ListMultipartUploads service method, as returned by S3.</returns>
        ListMultipartUploadsResponse ListMultipartUploads(string bucketName);

        /// <summary>
        /// This operation lists in-progress multipart uploads.
        /// </summary>
        /// <param name="bucketName">A property of ListMultipartUploadsRequest used to execute the ListMultipartUploads service method.</param>
        /// <param name="prefix">Lists in-progress uploads only for those keys that begin with the specified prefix.</param>
        /// 
        /// <returns>The response from the ListMultipartUploads service method, as returned by S3.</returns>
        ListMultipartUploadsResponse ListMultipartUploads(string bucketName, string prefix);

        /// <summary>
        /// This operation lists in-progress multipart uploads.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListMultipartUploads service method.</param>
        /// 
        /// <returns>The response from the ListMultipartUploads service method, as returned by S3.</returns>
        ListMultipartUploadsResponse ListMultipartUploads(ListMultipartUploadsRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the ListMultipartUploads operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListMultipartUploads operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndListMultipartUploads
        ///         operation.</returns>
        IAsyncResult BeginListMultipartUploads(ListMultipartUploadsRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  ListMultipartUploads operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginListMultipartUploads.</param>
        /// 
        /// <returns>Returns a  ListMultipartUploadsResult from S3.</returns>
        ListMultipartUploadsResponse EndListMultipartUploads(IAsyncResult asyncResult);

        #endregion

        #region  ListObjects

        /// <summary>
        /// Returns some or all (up to 1000) of the objects in a bucket. You can use the request
        /// parameters as selection criteria to return a subset of the objects in a bucket.
        /// </summary>
        /// <param name="bucketName">A property of ListObjectsRequest used to execute the ListObjects service method.</param>
        /// 
        /// <returns>The response from the ListObjects service method, as returned by S3.</returns>
        ListObjectsResponse ListObjects(string bucketName);

        /// <summary>
        /// Returns some or all (up to 1000) of the objects in a bucket. You can use the request
        /// parameters as selection criteria to return a subset of the objects in a bucket.
        /// </summary>
        /// <param name="bucketName">A property of ListObjectsRequest used to execute the ListObjects service method.</param>
        /// <param name="prefix">Limits the response to keys that begin with the specified prefix.</param>
        /// 
        /// <returns>The response from the ListObjects service method, as returned by S3.</returns>
        ListObjectsResponse ListObjects(string bucketName, string prefix);

        /// <summary>
        /// Returns some or all (up to 1000) of the objects in a bucket. You can use the request
        /// parameters as selection criteria to return a subset of the objects in a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListObjects service method.</param>
        /// 
        /// <returns>The response from the ListObjects service method, as returned by S3.</returns>
        ListObjectsResponse ListObjects(ListObjectsRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the ListObjects operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListObjects operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndListObjects
        ///         operation.</returns>
        IAsyncResult BeginListObjects(ListObjectsRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  ListObjects operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginListObjects.</param>
        /// 
        /// <returns>Returns a  ListObjectsResult from S3.</returns>
        ListObjectsResponse EndListObjects(IAsyncResult asyncResult);

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
        ListPartsResponse ListParts(string bucketName, string key, string uploadId);

        /// <summary>
        /// Lists the parts that have been uploaded for a specific multipart upload.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListParts service method.</param>
        /// 
        /// <returns>The response from the ListParts service method, as returned by S3.</returns>
        ListPartsResponse ListParts(ListPartsRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the ListParts operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListParts operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndListParts
        ///         operation.</returns>
        IAsyncResult BeginListParts(ListPartsRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  ListParts operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginListParts.</param>
        /// 
        /// <returns>Returns a  ListPartsResult from S3.</returns>
        ListPartsResponse EndListParts(IAsyncResult asyncResult);

        #endregion

        #region  ListVersions

        /// <summary>
        /// Returns metadata about all of the versions of objects in a bucket.
        /// </summary>
        /// <param name="bucketName">A property of ListVersionsRequest used to execute the ListVersions service method.</param>
        /// 
        /// <returns>The response from the ListVersions service method, as returned by S3.</returns>
        ListVersionsResponse ListVersions(string bucketName);

        /// <summary>
        /// Returns metadata about all of the versions of objects in a bucket.
        /// </summary>
        /// <param name="bucketName">A property of ListVersionsRequest used to execute the ListVersions service method.</param>
        /// <param name="prefix">Limits the response to keys that begin with the specified prefix.</param>
        /// 
        /// <returns>The response from the ListVersions service method, as returned by S3.</returns>
        ListVersionsResponse ListVersions(string bucketName, string prefix);

        /// <summary>
        /// Returns metadata about all of the versions of objects in a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListVersions service method.</param>
        /// 
        /// <returns>The response from the ListVersions service method, as returned by S3.</returns>
        ListVersionsResponse ListVersions(ListVersionsRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the ListVersions operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListVersions operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndListVersions
        ///         operation.</returns>
        IAsyncResult BeginListVersions(ListVersionsRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  ListVersions operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginListVersions.</param>
        /// 
        /// <returns>Returns a  ListVersionsResult from S3.</returns>
        ListVersionsResponse EndListVersions(IAsyncResult asyncResult);

        #endregion
        
        #region  PostDeleteBucket

        /// <summary>
        /// Delete bucket using bucket name.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PostDeleteBucket service method.</param>
        /// 
        /// <returns>The response from the PostDeleteBucket service method, as returned by S3.</returns>
        PostDeleteBucketResponse PostDeleteBucket(PostDeleteBucketRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PostDeleteBucket operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PostDeleteBucket operation on ObsS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPostDeleteBucket
        ///         operation.</returns>
        IAsyncResult BeginPostDeleteBucket(PostDeleteBucketRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  PostDeleteBucket operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPostDeleteBucket.</param>
        /// 
        /// <returns>Returns a  PostDeleteBucketResult from S3.</returns>
        PostDeleteBucketResponse EndPostDeleteBucket(IAsyncResult asyncResult);

        #endregion

        #region  PutACL

        /// <summary>
        /// Sets the permissions on a bucket using access control lists (ACL).
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutACL service method.</param>
        /// 
        /// <returns>The response from the PutACL service method, as returned by S3.</returns>
        PutACLResponse PutACL(PutACLRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutACL operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutACL operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutACL
        ///         operation.</returns>
        IAsyncResult BeginPutACL(PutACLRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  PutACL operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutACL.</param>
        /// 
        /// <returns>Returns a  PutACLResult from S3.</returns>
        PutACLResponse EndPutACL(IAsyncResult asyncResult);

        #endregion

        #region  PutBucket

        /// <summary>
        /// Creates a new bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketRequest used to execute the PutBucket service method.</param>
        /// 
        /// <returns>The response from the PutBucket service method, as returned by S3.</returns>
        PutBucketResponse PutBucket(string bucketName);
        /// <summary>
        /// Creates a new bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketRequest used to execute the PutBucket service method.</param>
        /// <param name="storageClass">A property of PutBucketRequest used to execute the PutBucket service method.</param>
        /// <returns>The response from the PutBucket service method, as returned by S3.</returns>
        PutBucketResponse PutBucket(string bucketName, S3StorageClass storageClass);
        /// <summary>
        /// Creates a new bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutBucket service method.</param>
        /// 
        /// <returns>The response from the PutBucket service method, as returned by S3.</returns>
        PutBucketResponse PutBucket(PutBucketRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutBucket operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutBucket operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutBucket
        ///         operation.</returns>
        IAsyncResult BeginPutBucket(PutBucketRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  PutBucket operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutBucket.</param>
        /// 
        /// <returns>Returns a  PutBucketResult from S3.</returns>
        PutBucketResponse EndPutBucket(IAsyncResult asyncResult);

        #endregion

        #region  PutBucketLogging

        /// <summary>
        /// Set the logging parameters for a bucket and to specify permissions for who can view
        /// and modify the logging parameters. To set the logging status of a bucket, you must
        /// be the bucket owner.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutBucketLogging service method.</param>
        /// 
        /// <returns>The response from the PutBucketLogging service method, as returned by S3.</returns>
        PutBucketLoggingResponse PutBucketLogging(PutBucketLoggingRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutBucketLogging operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutBucketLogging operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutBucketLogging
        ///         operation.</returns>
        IAsyncResult BeginPutBucketLogging(PutBucketLoggingRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  PutBucketLogging operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutBucketLogging.</param>
        /// 
        /// <returns>Returns a  PutBucketLoggingResult from S3.</returns>
        PutBucketLoggingResponse EndPutBucketLogging(IAsyncResult asyncResult);

        #endregion


        #region  PutBucketPolicy

        /// <summary>
        /// Replaces a policy on a bucket. If the bucket already has a policy, the one in this
        /// request completely replaces it.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketPolicyRequest used to execute the PutBucketPolicy service method.</param>
        /// <param name="policy">The bucket policy as a JSON document.</param>
        /// 
        /// <returns>The response from the PutBucketPolicy service method, as returned by S3.</returns>
        PutBucketPolicyResponse PutBucketPolicy(string bucketName, string policy);

        /// <summary>
        /// Replaces a policy on a bucket. If the bucket already has a policy, the one in this
        /// request completely replaces it.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketPolicyRequest used to execute the PutBucketPolicy service method.</param>
        /// <param name="policy">The bucket policy as a JSON document.</param>
        /// <param name="contentMD5">A property of PutBucketPolicyRequest used to execute the PutBucketPolicy service method.</param>
        /// 
        /// <returns>The response from the PutBucketPolicy service method, as returned by S3.</returns>
        PutBucketPolicyResponse PutBucketPolicy(string bucketName, string policy, string contentMD5);

        /// <summary>
        /// Replaces a policy on a bucket. If the bucket already has a policy, the one in this
        /// request completely replaces it.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutBucketPolicy service method.</param>
        /// 
        /// <returns>The response from the PutBucketPolicy service method, as returned by S3.</returns>
        PutBucketPolicyResponse PutBucketPolicy(PutBucketPolicyRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutBucketPolicy operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutBucketPolicy operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutBucketPolicy
        ///         operation.</returns>
        IAsyncResult BeginPutBucketPolicy(PutBucketPolicyRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  PutBucketPolicy operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutBucketPolicy.</param>
        /// 
        /// <returns>Returns a  PutBucketPolicyResult from S3.</returns>
        PutBucketPolicyResponse EndPutBucketPolicy(IAsyncResult asyncResult);

        #endregion

        #region  PutBucketQuota

        /// <summary>
        /// Sets the quota for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketQuotaRequest used to execute the PutBucketQuota service method.</param>
        /// <param name="storageQuota">A property of PutBucketQuotaRequest used to execute the PutBucketQuota service method.</param>
        /// 
        /// <returns>The response from the PutBucketQuota service method, as returned by S3.</returns>
        PutBucketQuotaResponse PutBucketQuota(string bucketName, string storageQuota);

        /// <summary>
        /// Sets the quota for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutBucketQuota service method.</param>
        /// 
        /// <returns>The response from the PutBucketQuota service method, as returned by S3.</returns>
        PutBucketQuotaResponse PutBucketQuota(PutBucketQuotaRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutBucketQuota operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutBucketQuota operation on ObsS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutBucketQuota
        ///         operation.</returns>
        IAsyncResult BeginPutBucketQuota(PutBucketQuotaRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  PutBucketQuota operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutBucketQuota.</param>
        /// 
        /// <returns>Returns a  PutBucketQuotaResult from S3.</returns>
        PutBucketQuotaResponse EndPutBucketQuota(IAsyncResult asyncResult);

        #endregion

        #region  PutBucketVersioning

        /// <summary>
        /// Sets the versioning state of an existing bucket. To set the versioning state, you
        /// must be the bucket owner.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutBucketVersioning service method.</param>
        /// 
        /// <returns>The response from the PutBucketVersioning service method, as returned by S3.</returns>
        PutBucketVersioningResponse PutBucketVersioning(PutBucketVersioningRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutBucketVersioning operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutBucketVersioning operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutBucketVersioning
        ///         operation.</returns>
        IAsyncResult BeginPutBucketVersioning(PutBucketVersioningRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  PutBucketVersioning operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutBucketVersioning.</param>
        /// 
        /// <returns>Returns a  PutBucketVersioningResult from S3.</returns>
        PutBucketVersioningResponse EndPutBucketVersioning(IAsyncResult asyncResult);

        #endregion

        #region  PutBucketWebsite

        /// <summary>
        /// Set the website configuration for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketWebsiteRequest used to execute the PutBucketWebsite service method.</param>
        /// <param name="websiteConfiguration">A property of PutBucketWebsiteRequest used to execute the PutBucketWebsite service method.</param>
        /// 
        /// <returns>The response from the PutBucketWebsite service method, as returned by S3.</returns>
        PutBucketWebsiteResponse PutBucketWebsite(string bucketName, WebsiteConfiguration websiteConfiguration);

        /// <summary>
        /// Set the website configuration for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutBucketWebsite service method.</param>
        /// 
        /// <returns>The response from the PutBucketWebsite service method, as returned by S3.</returns>
        PutBucketWebsiteResponse PutBucketWebsite(PutBucketWebsiteRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutBucketWebsite operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutBucketWebsite operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutBucketWebsite
        ///         operation.</returns>
        IAsyncResult BeginPutBucketWebsite(PutBucketWebsiteRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  PutBucketWebsite operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutBucketWebsite.</param>
        /// 
        /// <returns>Returns a  PutBucketWebsiteResult from S3.</returns>
        PutBucketWebsiteResponse EndPutBucketWebsite(IAsyncResult asyncResult);

        #endregion
        
        #region  PutCORSConfiguration

        /// <summary>
        /// Sets the cors configuration for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutCORSConfigurationRequest used to execute the PutCORSConfiguration service method.</param>
        /// <param name="configuration">A property of PutCORSConfigurationRequest used to execute the PutCORSConfiguration service method.</param>
        /// 
        /// <returns>The response from the PutCORSConfiguration service method, as returned by S3.</returns>
        PutCORSConfigurationResponse PutCORSConfiguration(string bucketName, CORSConfiguration configuration);

        /// <summary>
        /// Sets the cors configuration for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutCORSConfiguration service method.</param>
        /// 
        /// <returns>The response from the PutCORSConfiguration service method, as returned by S3.</returns>
        PutCORSConfigurationResponse PutCORSConfiguration(PutCORSConfigurationRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutCORSConfiguration operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutCORSConfiguration operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutCORSConfiguration
        ///         operation.</returns>
        IAsyncResult BeginPutCORSConfiguration(PutCORSConfigurationRequest request, AsyncCallback callback, object state);

        /// <summary>
        /// Finishes the asynchronous execution of the  PutCORSConfiguration operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutCORSConfiguration.</param>
        /// 
        /// <returns>Returns a  PutCORSConfigurationResult from S3.</returns>
        PutCORSConfigurationResponse EndPutCORSConfiguration(IAsyncResult asyncResult);

        #endregion
        
        #region  PutLifecycleConfiguration

        /// <summary>
        /// Sets lifecycle configuration for your bucket. If a lifecycle configuration exists,
        /// it replaces it.
        /// </summary>
        /// <param name="bucketName">A property of PutLifecycleConfigurationRequest used to execute the PutLifecycleConfiguration service method.</param>
        /// <param name="configuration">A property of PutLifecycleConfigurationRequest used to execute the PutLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the PutLifecycleConfiguration service method, as returned by S3.</returns>
        PutLifecycleConfigurationResponse PutLifecycleConfiguration(string bucketName, LifecycleConfiguration configuration);

        /// <summary>
        /// Sets lifecycle configuration for your bucket. If a lifecycle configuration exists,
        /// it replaces it.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutLifecycleConfiguration service method.</param>
        /// 
        /// <returns>The response from the PutLifecycleConfiguration service method, as returned by S3.</returns>
        PutLifecycleConfigurationResponse PutLifecycleConfiguration(PutLifecycleConfigurationRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutLifecycleConfiguration operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutLifecycleConfiguration operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutLifecycleConfiguration
        ///         operation.</returns>
        IAsyncResult BeginPutLifecycleConfiguration(PutLifecycleConfigurationRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  PutLifecycleConfiguration operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutLifecycleConfiguration.</param>
        /// 
        /// <returns>Returns a  PutLifecycleConfigurationResult from S3.</returns>
        PutLifecycleConfigurationResponse EndPutLifecycleConfiguration(IAsyncResult asyncResult);

        #endregion

        #region  PutObject

        /// <summary>
        /// Adds an object to a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutObject service method.</param>
        /// 
        /// <returns>The response from the PutObject service method, as returned by S3.</returns>
        PutObjectResponse PutObject(PutObjectRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutObject operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutObject operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutObject
        ///         operation.</returns>
        IAsyncResult BeginPutObject(PutObjectRequest request, AsyncCallback callback, object state);



        /// <summary>
        /// Finishes the asynchronous execution of the  PutObject operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutObject.</param>
        /// 
        /// <returns>Returns a  PutObjectResult from S3.</returns>
        PutObjectResponse EndPutObject(IAsyncResult asyncResult);

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
        /// Amazon S3 frees up the parts storage and stops charging you for the parts storage.
        /// </para>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UploadPart service method.</param>
        /// 
        /// <returns>The response from the UploadPart service method, as returned by S3.</returns>
        UploadPartResponse UploadPart(UploadPartRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the UploadPart operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the UploadPart operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndUploadPart
        ///         operation.</returns>
        IAsyncResult BeginUploadPart(UploadPartRequest request, AsyncCallback callback, object state);
        
        /// <summary>
        /// Finishes the asynchronous execution of the  UploadPart operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginUploadPart.</param>
        /// 
        /// <returns>Returns a  UploadPartResult from S3.</returns>
        UploadPartResponse EndUploadPart(IAsyncResult asyncResult);

        #endregion


        #region  Options bucket

        /// <summary>
        /// Wether has right to operator.
        /// 
        ///  
        /// <para>
        /// <b>Note:</b> Wether has right to operator
        /// </para>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UploadPart service method.</param>
        /// 
        /// <returns>The response from the UploadPart service method, as returned by S3.</returns>
        OptionsBucketResponse OptionsBucket(OptionsBucketRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the UploadPart operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the UploadPart operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndUploadPart
        ///         operation.</returns>
        IAsyncResult BeginOptionsBucket(OptionsBucketRequest request, AsyncCallback callback, object state);

        /// <summary>
        /// Finishes the asynchronous execution of the  UploadPart operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginUploadPart.</param>
        /// 
        /// <returns>Returns a  UploadPartResult from S3.</returns>
        OptionsBucketResponse EndOptionsBucket(IAsyncResult asyncResult);

        #endregion

        #region  Options object

        /// <summary>
        /// Wether has right to operator.
        /// 
        ///  
        /// <para>
        /// <b>Note:</b> Wether has right to operator
        /// </para>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UploadPart service method.</param>
        /// 
        /// <returns>The response from the UploadPart service method, as returned by S3.</returns>
        OptionsObjectResponse OptionsObject(OptionsObjectRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the UploadPart operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the UploadPart operation on AmazonS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndUploadPart
        ///         operation.</returns>
        IAsyncResult BeginOptionsObject(OptionsObjectRequest request, AsyncCallback callback, object state);

        /// <summary>
        /// Finishes the asynchronous execution of the  UploadPart operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginUploadPart.</param>
        /// 
        /// <returns>Returns a  UploadPartResult from S3.</returns>
        OptionsObjectResponse EndOptionsObject(IAsyncResult asyncResult);

        #endregion
        #region  GetBucketMetadata
        GetBucketMetadataResponse GetBucketMetadata(GetBucketMetadataRequest request);
        IAsyncResult BeginGetBucketMetadata(GetBucketMetadataRequest request, AsyncCallback callback, object state);
        GetBucketMetadataResponse EndGetBucketMetadata(IAsyncResult asyncResult);
        #endregion

        #region  RestoreObject
        RestoreObjectResponse RestoreObject(string bucketName, String key, String versionId);
        RestoreObjectResponse RestoreObject(RestoreObjectRequest request);
        IAsyncResult BeginRestoreObject(RestoreObjectRequest request, AsyncCallback callback, object state);
        RestoreObjectResponse EndRestoreObject(IAsyncResult asyncResult);
        #endregion

        #region  PutBucketTagging
        /// <summary>
        /// Deletes the policy from the bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketTaggingRequest used to execute the PutBucketTaggingRequest service method.</param>
        /// 
        /// <returns>The response from the PutBucketTaggingRequest service method, as returned by S3.</returns>
        PutBucketTaggingResponse PutBucketTagging(string bucketName);

        /// <summary>
        /// Sets the tagging for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutBucketTagging service method.</param>
        /// 
        /// <returns>The response from the PutBucketTagging service method, as returned by S3.</returns>
        PutBucketTaggingResponse PutBucketTagging(PutBucketTaggingRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutBucketTagging operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutBucketTagging operation on ObsS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutBucketTagging
        ///         operation.</returns>
        IAsyncResult BeginPutBucketTagging(PutBucketTaggingRequest request, AsyncCallback callback, object state);

        /// <summary>
        /// Finishes the asynchronous execution of the  PutBucketTagging operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutBucketTagging.</param>
        /// 
        /// <returns>Returns a  PutBucketTaggingResult from S3.</returns>
        PutBucketTaggingResponse EndPutBucketTagging(IAsyncResult asyncResult);

        #endregion

        #region  GetBucketTagging
        /// <summary>
        /// Get the tagging for a bucket.
        /// </summary>
        /// <param name="bucketName">the name of bucket.</param>
        /// 
        /// <returns>The response from the GetBucketTagging service method, as returned by S3.</returns>
        GetBucketTaggingResponse GetBucketTagging(string bucketName);
        /// <summary>
        /// Get the tagging for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetBucketTagging service method.</param>
        /// 
        /// <returns>The response from the GetBucketTagging service method, as returned by S3.</returns>
        GetBucketTaggingResponse GetBucketTagging(GetBucketTaggingRequest request);
        /// <summary>
        /// Initiates the asynchronous execution of the GetBucketTagging operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetBucketTagging operation on ObsS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndGetBucketTagging
        ///         operation.</returns>
        IAsyncResult BeginGetBucketTagging(GetBucketTaggingRequest request, AsyncCallback callback, object state);
        /// <summary>
        /// Finishes the asynchronous execution of the  GetBucketTagging operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginGetBucketTagging.</param>
        /// 
        /// <returns>Returns a  GetBucketTaggingResult from S3.</returns>
        GetBucketTaggingResponse EndGetBucketTagging(IAsyncResult asyncResult);


        #endregion

        #region  DeleteBucketTagging
        /// <summary>
        /// Deletes the tagging from the bucket.
        /// </summary>
        /// <param name="bucketName">A property of DeleteBucketTaggingRequest used to execute the DeleteBucketTaggingRequest service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketTaggingRequest service method, as returned by S3.</returns>
        DeleteBucketTaggingResponse DeleteBucketTagging(string bucketName);

        /// <summary>
        /// Delete the tagging for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketTagging service method.</param>
        /// 
        /// <returns>The response from the DeleteBucketTagging service method, as returned by S3.</returns>
        DeleteBucketTaggingResponse DeleteBucketTagging(DeleteBucketTaggingRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteBucketTagging operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteBucketTagging operation on ObsS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndDeleteBucketTagging
        ///         operation.</returns>
        IAsyncResult BeginDeleteBucketTagging(DeleteBucketTaggingRequest request, AsyncCallback callback, object state);

        /// <summary>
        /// Finishes the asynchronous execution of the  DeleteBucketTagging operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginDeleteBucketTagging.</param>
        /// 
        /// <returns>Returns a  DeleteBucketTaggingResult from S3.</returns>
        DeleteBucketTaggingResponse EndDeleteBucketTagging(IAsyncResult asyncResult);

        #endregion
        #region  PutBucketNotificationConfiguration
        /// <summary>
        /// Put the notificationconfiguration for a bucket.
        /// </summary>
        /// <param name="bucketName">A property of PutBucketNotificationConfiguration used to execute the PutBucketNotificationConfiguration service method.</param>
        /// 
        /// <returns>The response from the PutBucketTaggingRequest service method, as returned by S3.</returns>
        PutBucketNotificationResponse PutBucketNotificationConfiguration(string bucketName);

        /// <summary>
        /// Put the notificationconfiguration for a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutBucketNotificationConfiguration service method.</param>
        /// 
        /// <returns>The response from the PutBucketNotificationConfiguration service method, as returned by S3.</returns>
        PutBucketNotificationResponse PutBucketNotificationConfiguration(PutBucketNotificationRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the PutBucketNotificationConfiguration operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutBucketNotificationConfiguration operation on ObsS3Client.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes.</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// 
        /// <returns>An IAsyncResult that can be used to poll or wait for results, or both; this value is also needed when invoking EndPutBucketTagging
        ///         operation.</returns>
        IAsyncResult BeginPutBucketNotificationConfiguration(PutBucketNotificationRequest request, AsyncCallback callback, object state);

        /// <summary>
        /// Finishes the asynchronous execution of the  PutBucketTagging operation.
        /// </summary>
        /// 
        /// <param name="asyncResult">The IAsyncResult returned by the call to BeginPutBucketNotificationConfiguration.</param>
        /// 
        /// <returns>Returns a  PutBucketNotificationConfigurationResult from S3.</returns>
        PutBucketNotificationResponse EndPutBucketNotificationConfiguration(IAsyncResult asyncResult);

        #endregion
    }
}