/*
 * Copyright 2010-2013 OBS.com, Inc. or its affiliates. All Rights Reserved.
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

using System.Diagnostics.CodeAnalysis;


// Suppressions for empty response types
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.SimpleEmail.Model.Internal.MarshallTransformations.DeleteIdentityResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.SimpleEmail.Model.DeleteIdentityResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.SimpleEmail.Model.Internal.MarshallTransformations.SetIdentityFeedbackForwardingEnabledResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.SimpleEmail.Model.SetIdentityFeedbackForwardingEnabledResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.SimpleEmail.Model.Internal.MarshallTransformations.SetIdentityNotificationTopicResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.SimpleEmail.Model.SetIdentityNotificationTopicResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.SimpleEmail.Model.Internal.MarshallTransformations.VerifyEmailIdentityResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.SimpleEmail.Model.VerifyEmailIdentityResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.SimpleEmail.Model.Internal.MarshallTransformations.SetIdentityDkimEnabledResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.SimpleEmail.Model.SetIdentityDkimEnabledResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticTranscoder.Model.Internal.MarshallTransformations.DeletePresetResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.ElasticTranscoder.Model.DeletePresetResponse)", MessageId = "context")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticTranscoder.Model.Internal.MarshallTransformations.DeletePresetResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.ElasticTranscoder.Model.DeletePresetResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticTranscoder.Model.Internal.MarshallTransformations.DeletePipelineResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.ElasticTranscoder.Model.DeletePipelineResponse)", MessageId = "context")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticTranscoder.Model.Internal.MarshallTransformations.DeletePipelineResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.ElasticTranscoder.Model.DeletePipelineResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticTranscoder.Model.Internal.MarshallTransformations.CancelJobResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.ElasticTranscoder.Model.CancelJobResponse)", MessageId = "context")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticTranscoder.Model.Internal.MarshallTransformations.CancelJobResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.ElasticTranscoder.Model.CancelJobResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.S3.Model.Internal.MarshallTransformations.PutBucketResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.S3.Model.PutBucketResponse)", MessageId = "context")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.S3.Model.Internal.MarshallTransformations.PutBucketResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.S3.Model.PutBucketResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.S3.Model.Internal.MarshallTransformations.S3Transforms.#ToURLEncodedValue(System.Int32,System.Boolean)", MessageId = "path")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.S3.Model.Internal.MarshallTransformations.S3Transforms.#ToURLEncodedValue(System.DateTime,System.Boolean)", MessageId = "path")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.Route53.Model.Internal.MarshallTransformations.DeleteHealthCheckResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.Route53.Model.DeleteHealthCheckResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticLoadBalancing.Model.Internal.MarshallTransformations.DeleteLoadBalancerListenersResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.ElasticLoadBalancing.Model.DeleteLoadBalancerListenersResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticLoadBalancing.Model.Internal.MarshallTransformations.DeleteLoadBalancerPolicyResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.ElasticLoadBalancing.Model.DeleteLoadBalancerPolicyResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticLoadBalancing.Model.Internal.MarshallTransformations.DeleteLoadBalancerResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.ElasticLoadBalancing.Model.DeleteLoadBalancerResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticLoadBalancing.Model.Internal.MarshallTransformations.SetLoadBalancerPoliciesForBackendServerResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.ElasticLoadBalancing.Model.SetLoadBalancerPoliciesForBackendServerResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticLoadBalancing.Model.Internal.MarshallTransformations.SetLoadBalancerListenerSSLCertificateResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.ElasticLoadBalancing.Model.SetLoadBalancerListenerSSLCertificateResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticLoadBalancing.Model.Internal.MarshallTransformations.SetLoadBalancerPoliciesOfListenerResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.ElasticLoadBalancing.Model.SetLoadBalancerPoliciesOfListenerResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticLoadBalancing.Model.Internal.MarshallTransformations.CreateLoadBalancerListenersResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.ElasticLoadBalancing.Model.CreateLoadBalancerListenersResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticLoadBalancing.Model.Internal.MarshallTransformations.CreateAppCookieStickinessPolicyResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.ElasticLoadBalancing.Model.CreateAppCookieStickinessPolicyResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticLoadBalancing.Model.Internal.MarshallTransformations.CreateLoadBalancerPolicyResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.ElasticLoadBalancing.Model.CreateLoadBalancerPolicyResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticLoadBalancing.Model.Internal.MarshallTransformations.CreateLBCookieStickinessPolicyResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.ElasticLoadBalancing.Model.CreateLBCookieStickinessPolicyResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.DataPipeline.Model.Internal.MarshallTransformations.SetTaskStatusResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.DataPipeline.Model.SetTaskStatusResponse)", MessageId = "context")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.DataPipeline.Model.Internal.MarshallTransformations.SetTaskStatusResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.DataPipeline.Model.SetTaskStatusResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.DataPipeline.Model.Internal.MarshallTransformations.ActivatePipelineResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.DataPipeline.Model.ActivatePipelineResponse)", MessageId = "context")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.DataPipeline.Model.Internal.MarshallTransformations.ActivatePipelineResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.DataPipeline.Model.ActivatePipelineResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticLoadBalancing.Model.Internal.MarshallTransformations.ModifyLoadBalancerAttributesResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.ElasticLoadBalancing.Model.ModifyLoadBalancerAttributesResponse)", MessageId = "response")]

// Suppressions for identifiers in base classes
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "type", Target = "OBS.DataPipeline.Model.Operator", MessageId = "Operator")]
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "type", Target = "OBS.DynamoDBv2.Select", MessageId = "Select")]
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "type", Target = "OBS.ElastiCache.Model.Event", MessageId = "Event")]
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "type", Target = "OBS.RDS.Model.Event", MessageId = "Event")]
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "type", Target = "OBS.RDS.Model.Option", MessageId = "Option")]
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "type", Target = "OBS.Redshift.Model.Event", MessageId = "Event")]
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "member", Target = "OBS.SimpleDB.IObsSimpleDB.#Select(OBS.SimpleDB.Model.SelectRequest)", MessageId = "Select")]
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "type", Target = "OBS.ElasticMapReduce.Model.Step", MessageId = "Step")]
[module: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "member", Target = "OBS.Runtime.Internal.Util.ILogger.#Error(System.Exception,System.String,System.Object[])", MessageId = "Error")]

// Identifiers should have correct prefix
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "OBS.ElasticMapReduce.Model.ResizeAction", MessageId = "I")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "member", Target = "OBS.Runtime.ObsWebServiceClient.#Invoke`2(!!1,System.AsyncCallback,System.Object,System.Boolean,OBS.Runtime.Internal.Transform.IMarshaller`2<!!0,!!1>,OBS.Runtime.Internal.Transform.ResponseUnmarshaller,OBS.Runtime.Internal.Auth.AbstractAWSSigner)", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "OBS.Runtime.Internal.Transform.IResponseUnmarshaller`2", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "OBS.Runtime.Internal.Transform.IUnmarshaller`2", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "OBS.Runtime.Internal.Transform.DictionaryUnmarshaller`4", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "OBS.Runtime.Internal.Transform.IMarshaller`2", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "OBS.Runtime.Internal.Transform.ListUnmarshaller`2", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "OBS.Runtime.Internal.Transform.KeyValueUnmarshaller`4", MessageId = "T")]
[module: SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", Scope = "type", Target = "OBS.Runtime.Internal.Transform.IRequestMarshaller`2", MessageId = "T")]

// Identifiers should not contain type names
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.DynamoDBEntry.#AsInt()", MessageId = "int")]
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.DynamoDBEntry.#AsLong()", MessageId = "long")]
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.DynamoDBEntry.#AsShort()", MessageId = "short")]
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.DynamoDBEntry.#AsUInt()", MessageId = "uint")]
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.DynamoDBEntry.#AsULong()", MessageId = "ulong")]
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.DynamoDBEntry.#AsUShort()", MessageId = "ushort")]
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "OBS.SimpleDB.Util.ObsSimpleDBUtil.#DecodeRealNumberRangeFloat(System.String,System.Int32,System.Int32)", MessageId = "float")]
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "OBS.SimpleDB.Util.ObsSimpleDBUtil.#DecodeRealNumberRangeInt(System.String,System.Int32)", MessageId = "int")]
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "OBS.SimpleDB.Util.ObsSimpleDBUtil.#DecodeZeroPaddingFloat(System.String)", MessageId = "float")]
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "OBS.SimpleDB.Util.ObsSimpleDBUtil.#DecodeZeroPaddingInt(System.String)", MessageId = "int")]
[module: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Scope = "member", Target = "OBS.DynamoDBv2.Model.AttributeValue.#BOOL", MessageId = "bool")]

// General suppressions for marshallers
[module: SuppressMessage("Microsoft.Performance", "CA1809:AvoidExcessiveLocals", Scope = "member", Target = "OBS.CloudFront.Model.Internal.MarshallTransformations.CreateDistributionRequestMarshaller.#Marshall(OBS.CloudFront.Model.CreateDistributionRequest)")]
[module: SuppressMessage("Microsoft.Performance", "CA1809:AvoidExcessiveLocals", Scope = "member", Target = "OBS.CloudFront.Model.Internal.MarshallTransformations.UpdateDistributionRequestMarshaller.#Marshall(OBS.CloudFront.Model.UpdateDistributionRequest)")]

// Array properties/fields
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.Runtime.Internal.Util.HashStream.#CalculatedHash")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.Runtime.Internal.Util.HashStream.#ExpectedHash")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.Auth.AccessControlPolicy.Condition.#Values")]
[module: SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly", Scope = "member", Target = "OBS.Util.TypeFactory.#EmptyTypes")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBLocalSecondaryIndexRangeKeyAttribute.#IndexNames")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.QueryCondition.#Values")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.ScanCondition.#Values")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.Runtime.Internal.IRequest.#Content")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.S3.Util.S3PostUploadError.#elements")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.Runtime.Internal.Util.LogMessage.#Args")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4SigningResult.#SignatureBytes")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4SigningResult.#SigningKey")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBGlobalSecondaryIndexHashKeyAttribute.#IndexNames")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBGlobalSecondaryIndexRangeKeyAttribute.#IndexNames")]

// Nested generic types
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.DynamoDBv2.Model.BatchGetItemResult.#Responses")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.DynamoDBv2.Model.BatchWriteItemRequest.#RequestItems")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.DynamoDBv2.Model.BatchWriteItemResult.#ItemCollectionMetrics")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.DynamoDBv2.Model.BatchWriteItemResult.#UnprocessedItems")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.DynamoDBv2.Model.KeysAndAttributes.#Keys")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.DynamoDBv2.Model.QueryResult.#Items")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.DynamoDBv2.Model.ScanResult.#Items")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.OpsWorks.Model.DeploymentCommand.#Args")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.Runtime.IRequestMetrics.#Properties")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.Runtime.IRequestMetrics.#Timings")]

// Suppressions for exceptions in setters
[module: SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Scope = "member", Target = "OBS.ElasticMapReduce.Model.AddInstanceGroup.#get_Args()")]
[module: SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Scope = "member", Target = "OBS.ElasticMapReduce.Model.ModifyInstanceGroup.#get_Args()")]

[module: SuppressMessage("Microsoft.Usage", "CA2219:DoNotRaiseExceptionsInExceptionClauses", Scope = "member", Target = "OBS.Runtime.Internal.HttpRequest.#WriteToRequestBody(System.IO.Stream,System.IO.Stream,System.Collections.Generic.IDictionary`2<System.String,System.String>,OBS.Runtime.IRequestContext)")]

// Suppressions for DynamoDBContext generic methods
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#BeginDelete`1(System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#BeginDelete`1(System.Object,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#BeginDelete`1(System.Object,System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#BeginDelete`1(System.Object,System.Object,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#BeginLoad`1(System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#BeginLoad`1(System.Object,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#BeginLoad`1(System.Object,System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#BeginLoad`1(System.Object,System.Object,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#Delete`1(System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#Delete`1(System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#Delete`1(System.Object,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#Delete`1(System.Object,System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#GetTargetTable`1()")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#GetTargetTable`1(OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig)")]

// Suppressions for IDynamoDBContext generic methods
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#Delete`1(System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#BeginLoad`1(System.Object,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#BeginDelete`1(System.Object,System.Object,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#BeginDelete`1(System.Object,System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#Delete`1(System.Object,System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#BeginDelete`1(System.Object,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#BeginLoad`1(System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#GetTargetTable`1()")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#GetTargetTable`1(OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#BeginLoad`1(System.Object,System.Object,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#Delete`1(System.Object,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#BeginLoad`1(System.Object,System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#Delete`1(System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.IDynamoDBContext.#BeginDelete`1(System.Object,OBS.DynamoDBv2.DataModel.DynamoDBOperationConfig,System.AsyncCallback,System.Object)")]

// Serialization-only classes
[module: SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type", Target = "OBS.Runtime.InstanceProfileAWSCredentials+SecurityCredentials")]
[module: SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Scope = "type", Target = "OBS.Runtime.InstanceProfileAWSCredentials+SecurityInfo")]

// Sealing attributes, attributes are extended elsewhere
[module: SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Scope = "type", Target = "OBS.DynamoDBv2.DataModel.DynamoDBPropertyAttribute")]
[module: SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Scope = "type", Target = "OBS.DynamoDBv2.DataModel.DynamoDBRangeKeyAttribute")]
[module: SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Scope = "type", Target = "OBS.DynamoDBv2.DataModel.DynamoDBGlobalSecondaryIndexHashKeyAttribute")]
[module: SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Scope = "type", Target = "OBS.DynamoDBv2.DataModel.DynamoDBGlobalSecondaryIndexRangeKeyAttribute")]
[module: SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Scope = "type", Target = "OBS.DynamoDBv2.DataModel.DynamoDBHashKeyAttribute")]

// Passing base types
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "OBS.S3.Model.S3AccessControlList.#RemoveGrant(OBS.S3.Model.S3Grantee,OBS.S3.S3Permission)")]
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "OBS.S3.Model.S3BucketLoggingConfig.#RemoveGrant(OBS.S3.Model.S3Grantee,OBS.S3.S3Permission)")]
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#SignUrlCanned(System.String,System.String,System.IO.FileInfo,System.DateTime)")]
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#GetCustomSignedURL(OBS.CloudFront.ObsCloudFrontUrlSigner+Protocol,System.String,System.IO.FileInfo,System.String,System.String,System.DateTime,System.DateTime,System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#SignUrl(System.String,System.String,System.IO.FileInfo,System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#GetCannedSignedURL(OBS.CloudFront.ObsCloudFrontUrlSigner+Protocol,System.String,System.IO.FileInfo,System.String,System.String,System.DateTime)")]
[module: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "OBS.Auth.AccessControlPolicy.ConditionFactory.#NewCannedACLCondition(OBS.S3.S3CannedACL)")]

// Casting
[module: SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "OBS.Runtime.Internal.Util.WrapperStream.#SearchWrappedStream(System.Func`2<System.IO.Stream,System.Boolean>)")]
[module: SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "OBS.Runtime.Internal.Transform.JsonUnmarshallerContext.#ReadText()")]

// Identifier suffix
[module: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "OBS.S3.Model.TransferProgressArgs")]
[module: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "OBS.S3.Model.WriteObjectProgressArgs")]
[module: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "OBS.Runtime.StreamTransferProgressArgs")]
[module: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "OBS.S3.Transfer.UploadProgressArgs")]
[module: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "OBS.S3.Transfer.DownloadDirectoryProgressArgs")]
[module: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "OBS.S3.Transfer.UploadDirectoryProgressArgs")]
[module: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "OBS.S3.Transfer.UploadDirectoryFileRequestArgs")]
[module: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "OBS.DynamoDBv2.DocumentModel.Document")]

// Interface methods not callable by child types
[module: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "OBS.Runtime.ObsWebServiceRequest.#OBS.Runtime.Internal.IRequestEvents.FireBeforeRequestEvent(System.Object,OBS.Runtime.RequestEventArgs)")]
[module: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "OBS.Runtime.ObsWebServiceRequest.#OBS.Runtime.Internal.IRequestEvents.AddBeforeRequestHandler(OBS.Runtime.RequestEventHandler)")]

// General exception types

[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.S3.Transfer.Internal.MultipartUploadCommand.#AbortMultipartUpload(System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.Util.InternalLog4netLogger.#loadStatics()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.Util.BackgroundDispatcher`1.#HandleInvoked()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.FallbackCredentialsFactory.#GetCredentials(System.Boolean)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.EC2.Util.VPCUtilities.#WaitTillTrue(System.Func`1<System.Boolean>)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.EC2.Util.EC2Metadata.#FetchData(System.String,System.Boolean)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.EC2.Util.EC2Metadata.#get_IAMSecurityCredentials()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.EC2.Util.EC2Metadata.#get_IAMInstanceProfileInfo()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.EC2.Util.ImageUtilities.#LoadDefinitionsFromWeb()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.EC2.Util.ImageUtilities.#ParseAMIDefinitions(System.IO.StreamReader)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.Util.MetricError.#.ctor(OBS.Runtime.Internal.Util.Metric,System.Exception,System.String,System.Object[])")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.S3.Transfer.Internal.MultipartUploadCommand.#shutdown(System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.S3.Transfer.Internal.MultipartUploadCommand+UploadPartInvoker.#Execute()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.S3.Model.Internal.MarshallTransformations.S3ErrorResponseUnmarshaller.#Unmarshall(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.ObsWebServiceClient.#getRequestStreamCallback(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.ObsWebServiceClient.#getResponseCallback(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.InstanceProfileAWSCredentials.#GenerateNewCredentials()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.EC2.ObsEC2Client+DryRunInfo.#DryRun(OBS.EC2.ObsEC2Client,OBS.EC2.ObsEC2Request,OBS.EC2.Model.DryRunResponse&)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.DynamoDBv2.DynamoDBAsyncExecutor.#Execute(OBS.DynamoDBv2.AsyncCall,OBS.DynamoDBv2.DynamoDBAsyncResult)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Glacier.Transfer.Internal.DownloadJobCommand.#Execute()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Util.CryptoUtilFactory+CryptoUtil.#get_SHA256HashAlgorithmInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Util.AWSSDKUtils.#PreserveStackTrace(System.Exception)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Util.AWSSDKUtils.#ForceCanonicalPathAndQuery(System.Uri)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Util.AWSSDKUtils.#DetermineFramework()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.S3.Util.ObsS3Util.#InvokeDeleteS3BucketWithObjects(System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.S3.Util.S3PostUploadException.#FromResponse(System.Net.HttpWebResponse)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.Util.EventStream.#BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.S3.Transfer.Executer.#Execute()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Table.#TryLoadTable(OBS.DynamoDBv2.IObsDynamoDB,System.String,OBS.DynamoDBv2.DocumentModel.Table&)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Table.#TryLoadTable(OBS.DynamoDBv2.IObsDynamoDB,System.String,OBS.DynamoDBv2.DocumentModel.Table+DynamoDBConsumer,OBS.DynamoDBv2.DynamoDBEntryConversion,OBS.DynamoDBv2.DocumentModel.Table&)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Table.#TryLoadTable(OBS.DynamoDBv2.IObsDynamoDB,System.String,OBS.DynamoDBv2.DynamoDBEntryConversion,OBS.DynamoDBv2.DocumentModel.Table&)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.AWSSection.#Serialize(System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.AbstractWebServiceClient.#DontUnescapePathDotsAndSlashes(System.Uri)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.EC2.Import.DiskImageImporter.#UploadImageFilePart(System.Object)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.EC2.Import.DiskImageImporter.#DetermineRemainingUploads()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.EC2.Import.DiskImageImporter.#RemoveUploadedArtifacts(System.String,System.Collections.Generic.IEnumerable`1<OBS.EC2.Util.ImageFilePart>)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.EC2.Import.ImageFileParts.#FetchNextPartForUpload(System.IO.Stream,System.Byte[]&)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.RegionEndpoint.#TryLoadEndpointDefinitionsFromAssemblyDir()")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.HttpHandler`1.#DontUnescapePathDotsAndSlashes(System.Uri)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.HttpHandler`1.#GetResponseCallback(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.HttpHandler`1.#GetRequestStreamCallback(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.PipelineHandler.#AsyncCallback(OBS.Runtime.IAsyncExecutionContext)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.HttpErrorResponseExceptionHandler.#HandleSuppressed404(OBS.Runtime.IExecutionContext,OBS.Runtime.Internal.Transform.IWebResponseData)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.ErrorHandler.#InvokeAsyncCallback(OBS.Runtime.IAsyncExecutionContext)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.ObsServiceClient.#DontUnescapePathDotsAndSlashes(System.Uri)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.DynamoDBContext.#TryToScalar(System.Object,System.Type,OBS.DynamoDBv2.DataModel.DynamoDBFlatConfig,OBS.DynamoDBv2.DocumentModel.DynamoDBEntry&)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.JsonUtils.#TryDecodeBase64(System.String,System.Byte[]&)")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.Util.MetricError.#.ctor(OBS.Runtime.Metric,System.Exception,System.String,System.Object[])")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OBS.Runtime.Internal.Util.RequestMetrics.#ToString()")]

// Overflow operations
[module: SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Scope = "member", Target = "OBS.DynamoDBv2.ObsDynamoDBClient.#pauseExponentially(System.Int32)", MessageId = "retries-1")]
[module: SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Scope = "member", Target = "OBS.Runtime.Internal.Util.EncryptStream.#Read(System.Byte[],System.Int32,System.Int32)", MessageId = "offset+16")]
[module: SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Scope = "member", Target = "OBS.Runtime.Internal.Util.EncryptUploadPartStream.#Read(System.Byte[],System.Int32,System.Int32)", MessageId = "offset+16")]

// Atrribute arguments, breaking change
[module: SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Scope = "type", Target = "OBS.DynamoDBv2.DataModel.DynamoDBRenamableAttribute")]
[module: SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Scope = "type", Target = "OBS.DynamoDBv2.DataModel.DynamoDBLocalSecondaryIndexRangeKeyAttribute")]
[module: SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Scope = "type", Target = "OBS.DynamoDBv2.DataModel.DynamoDBPropertyAttribute")]
[module: SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Scope = "type", Target = "OBS.DynamoDBv2.DataModel.DynamoDBTableAttribute")]
[module: SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Scope = "type", Target = "OBS.DynamoDBv2.DataModel.DynamoDBGlobalSecondaryIndexHashKeyAttribute")]
[module: SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Scope = "type", Target = "OBS.DynamoDBv2.DataModel.DynamoDBGlobalSecondaryIndexRangeKeyAttribute")]

// Nested types, breaking change
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.Auth.AccessControlPolicy.ConditionFactory+ArnComparisonType")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.Auth.AccessControlPolicy.ConditionFactory+DateComparisonType")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.Auth.AccessControlPolicy.ConditionFactory+IpAddressComparisonType")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.Auth.AccessControlPolicy.ConditionFactory+NumericComparisonType")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.Auth.AccessControlPolicy.ConditionFactory+StringComparisonType")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.Auth.AccessControlPolicy.Statement+StatementEffect")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner+Protocol")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.EC2.Util.VPCUtilities+Progress")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.EC2.Util.ImageUtilities+ImageDescriptor")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.ElasticMapReduce.Model.StepFactory+HiveVersion")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.RegionEndpoint+Endpoint")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.Runtime.Internal.AsyncResult+AsyncRequestState")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.S3.Util.S3EventNotification+RequestParametersEntity")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.S3.Util.S3EventNotification+ResponseElementsEntity")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.S3.Util.S3EventNotification+S3BucketEntity")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.S3.Util.S3EventNotification+S3Entity")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.S3.Util.S3EventNotification+S3EventNotificationRecord")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.S3.Util.S3EventNotification+S3ObjectEntity")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "OBS.S3.Util.S3EventNotification+UserIdentityEntity")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "")]
[module: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "")]

// Normalize strings to uppercase, breaking change
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Auth.AccessControlPolicy.ConditionFactory.#NewCondition(System.String,System.Boolean)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.CloudFront.Model.Internal.MarshallTransformations.CreateDistributionRequestMarshaller.#Marshall(OBS.CloudFront.Model.CreateDistributionRequest)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.CloudFront.Model.Internal.MarshallTransformations.CreateStreamingDistributionRequestMarshaller.#Marshall(OBS.CloudFront.Model.CreateStreamingDistributionRequest)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.CloudFront.Model.Internal.MarshallTransformations.UpdateDistributionRequestMarshaller.#Marshall(OBS.CloudFront.Model.UpdateDistributionRequest)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.CloudFront.Model.Internal.MarshallTransformations.UpdateStreamingDistributionRequestMarshaller.#Marshall(OBS.CloudFront.Model.UpdateStreamingDistributionRequest)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.ElasticMapReduce.Model.ConfigureDaemons.#AddHeapSize(OBS.ElasticMapReduce.Model.Daemon,System.Int32)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.ElasticMapReduce.Model.ConfigureDaemons.#AddOpts(OBS.ElasticMapReduce.Model.Daemon,System.String)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Glacier.TreeHashGenerator.#CalculateTreeHash(System.Collections.Generic.IEnumerable`1<System.String>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Glacier.TreeHashGenerator.#CalculateTreeHash(System.IO.Stream)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.RegionEndpoint.#GetBySystemName(System.String)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Route53.Model.Internal.MarshallTransformations.ChangeResourceRecordSetsRequestMarshaller.#Marshall(OBS.Route53.Model.ChangeResourceRecordSetsRequest)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS3Signer.#GetCanonicalizedHeadersForStringToSign(OBS.Runtime.Internal.IRequest)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS3Signer.#GetHeadersForStringToSign(OBS.Runtime.Internal.IRequest)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4Signer.#CalculateSignature(System.Collections.Generic.IDictionary`2<System.String,System.String>,System.Collections.Generic.IDictionary`2<System.String,System.String>,System.String,System.String,System.String,System.String,OBS.Runtime.ImmutableCredentials)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4Signer.#DetermineRegion(OBS.Runtime.ClientConfig)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4Signer.#DetermineService(OBS.Runtime.ClientConfig)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4Signer.#GetCanonicalizedHeaders(System.Collections.Generic.List`1<System.String>,System.Collections.Generic.IDictionary`2<System.String,System.String>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4Signer.#GetSignedHeaders(System.Collections.Generic.List`1<System.String>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.S3Signer.#buildCanonicalizedHeaders(System.Collections.Generic.IDictionary`2<System.String,System.String>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Util.StringUtils.#FromBool(System.Boolean)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.S3.Model.Internal.MarshallTransformations.DeleteObjectsRequestMarshaller.#Marshall(OBS.S3.Model.DeleteObjectsRequest)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.SQS.ObsSQSClient.#CalculateMD5(System.String)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4Signer.#CanonicalizeHeaderNames(System.Collections.Generic.IEnumerable`1<System.Collections.Generic.KeyValuePair`2<System.String,System.String>>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4Signer.#CanonicalizeHeaders(System.Collections.Generic.ICollection`1<System.Collections.Generic.KeyValuePair`2<System.String,System.String>>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.SQS.ObsSQSClient.#CalculateMD5(System.Byte[])")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.S3Signer.#BuildCanonicalizedHeaders(System.Collections.Generic.IDictionary`2<System.String,System.String>)")]
[module: SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4Signer.#DetermineSigningRegion(OBS.Runtime.ClientConfig,System.String,OBS.RegionEndpoint,OBS.Runtime.Internal.IRequest)")]

// Types names matching namespaces
[module: SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Scope = "type", Target = "OBS.Auth.AccessControlPolicy.Policy")]
[module: SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Scope = "type", Target = "OBS.Auth.AccessControlPolicy.Principal")]
[module: SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Scope = "type", Target = "OBS.OpsWorks.Model.Deployment")]
[module: SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Scope = "type", Target = "OBS.ElasticTranscoder.Model.Encryption")]

// Uri properties should not be strings
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFormation.Util.ObsCloudFormationUtil.#SignalWaitCondition(System.String,System.String,System.String,System.String,System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#SignUrl(System.String,System.String,System.IO.FileInfo,System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#SignUrl(System.String,System.String,System.IO.StreamReader,System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#SignUrlCanned(System.String,System.String,System.IO.FileInfo,System.DateTime)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#SignUrlCanned(System.String,System.String,System.IO.StreamReader,System.DateTime)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SimpleNotificationService.ObsSimpleNotificationServiceClient.#SubscribeQueue(System.String,OBS.SQS.IObsSQS,System.String)", MessageId = "2#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope="member", Target="OBS.SQS.IObsSQS.#AuthorizeS3ToSendMessage(System.String,System.String)", MessageId="0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.Util.AWSSDKUtils.#DetermineRegion(System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.Util.AWSSDKUtils.#DetermineService(System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFormation.Model.CreateStackRequest.#TemplateURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFormation.Model.EstimateTemplateCostRequest.#TemplateURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFormation.Model.EstimateTemplateCostResult.#Url")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFormation.Model.UpdateStackRequest.#TemplateURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFormation.Model.ValidateTemplateRequest.#TemplateURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFormation.Model.GetTemplateSummaryRequest.#TemplateURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.EC2.Model.DiskImageDescription.#ImportManifestUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.EC2.Model.DiskImageDetail.#ImportManifestUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.ElasticBeanstalk.Model.CreateEnvironmentResult.#EndpointURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.ElasticBeanstalk.Model.EnvironmentDescription.#EndpointURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.ElasticBeanstalk.Model.TerminateEnvironmentResult.#EndpointURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.ElasticBeanstalk.Model.UpdateEnvironmentResult.#EndpointURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.ElasticMapReduce.Model.Cluster.#LogUri")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.ElasticMapReduce.Model.JobFlowDetail.#LogUri")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.ElasticMapReduce.Model.RunJobFlowRequest.#LogUri")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.ElasticTranscoder.Model.HlsContentProtection.#LicenseAcquisitionUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.ImportExport.Model.Artifact.#URL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.OpsWorks.Model.Command.#LogUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.OpsWorks.Model.Source.#Url")]

[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.OpsWorks.Model.DescribeStackProvisioningParametersResult.#AgentInstallerUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.Runtime.ClientConfig.#ServiceURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.S3.Model.S3Grantee.#URI")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.AddPermissionRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.ChangeMessageVisibilityBatchRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.ChangeMessageVisibilityRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.CreateQueueResult.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.DeleteMessageBatchRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.DeleteMessageRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.DeleteQueueRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.GetQueueAttributesRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.GetQueueUrlResult.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.ReceiveMessageRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.RemovePermissionRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.SendMessageBatchRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.SendMessageRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.SetQueueAttributesRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope="member", Target="OBS.SQS.Model.PurgeQueueRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope="member", Target="OBS.SQS.IObsSQS.#PurgeQueue(System.String)", MessageId="0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SimpleNotificationService.IObsSimpleNotificationService.#SubscribeQueue(System.String,OBS.SQS.IObsSQS,System.String)", MessageId = "2#")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.ElasticBeanstalk.Model.Queue.#URL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.ListDeadLetterSourceQueuesRequest.#QueueUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFormation.Model.UpdateStackRequest.#StackPolicyDuringUpdateURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFormation.Model.UpdateStackRequest.#StackPolicyURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFormation.Model.CreateStackRequest.#StackPolicyURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFormation.Model.SetStackPolicyRequest.#StackPolicyURL")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.EC2.Import.ImportManifestRoot.#SelfDestructUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.EC2.Import.ImageFilePart.#HeadUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.EC2.Import.ImageFilePart.#DeleteUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.EC2.Import.ImageFilePart.#GetUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.EC2.Model.CopySnapshotRequest.#PresignedUrl")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.IdentityManagement.Model.GetOpenIDConnectProviderResult.#Url")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.IdentityManagement.Model.CreateOpenIDConnectProviderRequest.#Url")]
[module: SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "OBS.ImportExport.Model.GetShippingLabelResult.#ShippingLabelURL")]

[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#BuildPolicyForSignedUrl(System.String,System.DateTime,System.String,System.DateTime)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#GetCannedSignedURL(OBS.CloudFront.ObsCloudFrontUrlSigner+Protocol,System.String,System.IO.FileInfo,System.String,System.String,System.DateTime)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#GetCannedSignedURL(OBS.CloudFront.ObsCloudFrontUrlSigner+Protocol,System.String,System.IO.StreamReader,System.String,System.String,System.DateTime)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#GetCustomSignedURL(OBS.CloudFront.ObsCloudFrontUrlSigner+Protocol,System.String,System.IO.FileInfo,System.String,System.String,System.DateTime,System.DateTime,System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#GetCustomSignedURL(OBS.CloudFront.ObsCloudFrontUrlSigner+Protocol,System.String,System.IO.StreamReader,System.String,System.String,System.DateTime,System.DateTime,System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#SignUrl(System.String,System.String,System.IO.FileInfo,System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#SignUrl(System.String,System.String,System.IO.StreamReader,System.String)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#SignUrlCanned(System.String,System.String,System.IO.FileInfo,System.DateTime)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.ObsCloudFrontUrlSigner.#SignUrlCanned(System.String,System.String,System.IO.StreamReader,System.DateTime)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.CloudFront.Util.ObsCloudFrontUtil.#UrlEncode(System.String,System.Boolean)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.S3.IObsS3.#GetPreSignedURL(OBS.S3.Model.GetPreSignedUrlRequest)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.S3.Util.ObsS3Util.#UrlEncode(System.String,System.Boolean)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.Util.AWSSDKUtils.#UrlEncode(System.String,System.Boolean)")]
[module: SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.S3Link.#GetPreSignedURL(System.DateTime)")]

// Use properties, breaking changes
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Document.#GetAttributeNames()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Search.#GetNextSet()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Search.#GetRemaining()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.AWSCredentials.#GetCredentials()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.InstanceProfileAWSCredentials.#GetAvailableRoles()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.RequestMetrics.#GetErrors()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.S3.Util.S3PostUploadSignedPolicy.#GetReadablePolicy()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.AWSCredentials.#GetCredentialsAsync()")]

// Use properties, huge change
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.S3.Model.Internal.MarshallTransformations.S3ErrorResponseUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.ResponseMetadataUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.MemoryStreamUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.StringUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.LongUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.IntUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.ErrorResponseUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.FloatUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.JsonErrorResponseUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.DateTimeUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.ByteUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.DoubleUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.BoolUnmarshaller.#GetInstance()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Transform.DateTimeEpochLongMillisecondsUnmarshaller.#GetInstance()")]

// Use properties, design decision to suppress
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.Util.WrapperStream.#GetSeekableBaseStream()")]

// Flag enums should have plural names, breaking changes
[module: SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames", Scope = "type", Target = "OBS.DynamoDBv2.DocumentModel.ReturnValues")]
[module: SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames", Scope = "type", Target = "OBS.DynamoDBv2.DocumentModel.SelectValues")]
[module: SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames", Scope = "type", Target = "OBS.DynamoDBv2.DocumentModel.ConditionalOperatorValues")]

// Unused parameters
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Table.#UserAgentRequestEventHandler(System.Object,OBS.Runtime.RequestEventArgs,System.Boolean)", MessageId = "sender")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.Runtime.ObsWebServiceClient.#LogResponse(OBS.Runtime.Internal.Util.RequestMetrics,OBS.Runtime.Internal.IRequest,System.Net.HttpStatusCode)", MessageId = "request")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.Runtime.ConstantClass.#ToString(System.IFormatProvider)", MessageId = "provider")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticMapReduce.Model.Internal.MarshallTransformations.AddTagsResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.ElasticMapReduce.Model.AddTagsResponse)", MessageId = "context")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticMapReduce.Model.Internal.MarshallTransformations.AddTagsResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.ElasticMapReduce.Model.AddTagsResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticMapReduce.Model.Internal.MarshallTransformations.RemoveTagsResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.ElasticMapReduce.Model.RemoveTagsResponse)", MessageId = "context")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.ElasticMapReduce.Model.Internal.MarshallTransformations.RemoveTagsResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.JsonUnmarshallerContext,OBS.ElasticMapReduce.Model.RemoveTagsResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.SQS.Model.Internal.MarshallTransformations.AddPermissionResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.SQS.Model.AddPermissionResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.SQS.Model.Internal.MarshallTransformations.ChangeMessageVisibilityResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.SQS.Model.ChangeMessageVisibilityResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.SQS.Model.Internal.MarshallTransformations.DeleteMessageResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.SQS.Model.DeleteMessageResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.SQS.Model.Internal.MarshallTransformations.DeleteQueueResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.SQS.Model.DeleteQueueResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.SQS.Model.Internal.MarshallTransformations.RemovePermissionResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.SQS.Model.RemovePermissionResponse)", MessageId = "response")]
[module: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "member", Target = "OBS.SQS.Model.Internal.MarshallTransformations.SetQueueAttributesResponseUnmarshaller.#UnmarshallResult(OBS.Runtime.Internal.Transform.XmlUnmarshallerContext,OBS.SQS.Model.SetQueueAttributesResponse)", MessageId = "response")]

// Mark members as static, breaking changes
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.MultiTableBatchWrite.#EndExecute(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.BatchWrite.#EndExecute(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.MultiTableBatchGet.#EndExecute(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DataModel.BatchGet.#EndExecute(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.MultiTableDocumentBatchGet.#EndExecute(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Search.#EndGetRemaining(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Search.#EndGetNextSet(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.DocumentBatchWrite.#EndExecute(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Table.#EndGetItem(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Table.#EndUpdateItem(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Table.#EndDeleteItem(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.Table.#EndPutItem(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.MultiTableDocumentBatchWrite.#EndExecute(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.DocumentBatchGet.#EndExecute(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.S3.ObsS3Client.#EndHeadBucket(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.S3.Transfer.TransferUtility.#EndUploadDirectory(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.S3.Transfer.TransferUtility.#EndDownload(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.S3.Transfer.TransferUtility.#EndUpload(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.S3.Transfer.TransferUtility.#EndOpenStream(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.S3.Transfer.TransferUtility.#EndDownloadDirectory(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.S3.Transfer.TransferUtility.#EndAbortMultipartUploads(System.IAsyncResult)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4Signer.#SignRequest(OBS.Runtime.Internal.IRequest,OBS.Runtime.ClientConfig,OBS.Runtime.Internal.Util.RequestMetrics,System.String,System.String)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.Runtime.Internal.Auth.AWS4PreSignedUrlSigner.#SignRequest(OBS.Runtime.Internal.IRequest,OBS.Runtime.ClientConfig,OBS.Runtime.Internal.Util.RequestMetrics,System.String,System.String)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.Runtime.Internal.Auth.S3Signer.#SignRequest(OBS.Runtime.Internal.IRequest,OBS.Runtime.ClientConfig,OBS.Runtime.Internal.Util.RequestMetrics,System.String,System.String)")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OBS.Runtime.Internal.Auth.S3Signer.#SignRequest(OBS.Runtime.Internal.IRequest,OBS.Runtime.Internal.Util.RequestMetrics,System.String,System.String)")]

// Link demand
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "OBS.Runtime.Internal.Util.TraceSourceUtil.#GetTraceSourceWithListeners(System.String,System.Diagnostics.SourceLevels)")]

// Initialize reference type static fields inline
[module: SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "OBS.EC2.Util.ImageUtilities.#.cctor()")]

// Initialize reference type static fields inline
[module: SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "OBS.Util.AWSSDKUtils.#.cctor()")]

// Abstract types should not have constructors
[module: SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors", Scope = "type", Target = "OBS.Runtime.ClientConfig")]

// Avoid uncalled private code, properties used in serialization
[module: SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "OBS.Runtime.InstanceProfileAWSCredentials+SecurityBase.#set_Code(System.String)")]
[module: SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "OBS.Runtime.InstanceProfileAWSCredentials+SecurityBase.#get_LastUpdated()")]
[module: SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "OBS.Runtime.InstanceProfileAWSCredentials+SecurityBase.#set_LastUpdated(System.DateTime)")]
[module: SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "OBS.Runtime.InstanceProfileAWSCredentials+SecurityBase.#set_Message(System.String)")]

// Do not call overridable methods in constructors
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "OBS.Runtime.AbstractWebServiceClient.#.ctor(OBS.Runtime.AWSCredentials,OBS.Runtime.ClientConfig,OBS.Runtime.AbstractWebServiceClient+AuthenticationTypes)")]
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "OBS.Runtime.ClientConfig.#.ctor()")]
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "OBS.Runtime.ObsServiceClient.#.ctor(OBS.Runtime.AWSCredentials,OBS.Runtime.ClientConfig)")]
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "OBS.Runtime.Internal.ErrorHandler.#.ctor(OBS.Runtime.Internal.Util.ILogger)")]

// Events should have before or after prefix
[module: SuppressMessage("Microsoft.Naming", "CA1713:EventsShouldNotHaveBeforeOrAfterPrefix", Scope = "member", Target = "OBS.Runtime.AbstractWebServiceClient.#BeforeRequestEvent")]
[module: SuppressMessage("Microsoft.Naming", "CA1713:EventsShouldNotHaveBeforeOrAfterPrefix", Scope = "member", Target = "OBS.Runtime.AbstractWebServiceClient.#AfterResponseEvent")]
[module: SuppressMessage("Microsoft.Naming", "CA1713:EventsShouldNotHaveBeforeOrAfterPrefix", Scope = "member", Target = "OBS.Runtime.ObsServiceClient.#BeforeRequestEvent")]
[module: SuppressMessage("Microsoft.Naming", "CA1713:EventsShouldNotHaveBeforeOrAfterPrefix", Scope = "member", Target = "OBS.Runtime.ObsServiceClient.#AfterResponseEvent")]

// ISerializable attribute
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "OBS.Runtime.ObsServiceException")]
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "OBS.Runtime.SignatureException")]
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "OBS.Runtime.ObsClientException")]
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "OBS.Runtime.Internal.Auth.SignatureException")]
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "OBS.S3.Util.S3PostUploadException")]
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "OBS.EC2.Import.DiskImageImporterException")]
[module: SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Scope = "type", Target = "OBS.Runtime.Internal.HttpErrorResponseException")]

// Use generic event handler instances, breaking change
[module: SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances", Scope = "type", Target = "OBS.Runtime.RequestEventHandler")]
[module: SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances", Scope = "type", Target = "OBS.Runtime.ExceptionEventHandler")]
[module: SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances", Scope = "type", Target = "OBS.Runtime.ResponseEventHandler")]
[module: SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances", Scope = "type", Target = "OBS.Runtime.PreRequestEventHandler")]

// Declare event handlers correctly, breaking change
[module: SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Scope = "member", Target = "OBS.Runtime.AbstractWebServiceClient.#BeforeRequestEvent")]
[module: SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Scope = "member", Target = "OBS.Runtime.AbstractWebServiceClient.#ExceptionEvent")]
[module: SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Scope = "member", Target = "OBS.Runtime.AbstractWebServiceClient.#AfterResponseEvent")]

// Use events where appropriate
[module: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "OBS.Runtime.Internal.IRequestEvents.#FireBeforeRequestEvent(System.Object,OBS.Runtime.RequestEventArgs)")]

// Override methods on comparable types
[module: SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Scope = "type", Target = "OBS.S3.Model.PartETag")]

// Do not call overridable methods in constructors
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "OBS.Runtime.Internal.Util.EncryptionWrapper.#.ctor()")]
[module: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "OBS.Runtime.Internal.Util.DecryptionWrapper.#.ctor()")]


// Suppressions for Runtime generic methods
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.Runtime.Internal.RuntimePipeline.#RemoveHandler`1()")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.Runtime.Internal.RuntimePipeline.#AddHandlerAfter`1(OBS.Runtime.IPipelineHandler)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.Runtime.Internal.RuntimePipeline.#ReplaceHandler`1(OBS.Runtime.IPipelineHandler)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.Runtime.Internal.RuntimePipeline.#AddHandlerBefore`1(OBS.Runtime.IPipelineHandler)")]
[module: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "OBS.Runtime.Internal.DefaultRetryPolicy.#IsInnerException`1(System.Exception)")]

[module: SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Scope = "member", Target = "OBS.Runtime.Internal.DefaultRetryPolicy.#IsInnerException`1(System.Exception,!!0&)", MessageId = "1#")]

// Suppression for methods on IHttpRequest
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.IHttpRequest`1.#GetRequestContent()")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "OBS.Runtime.IHttpRequest`1.#GetResponse()")]

// Suppression for HttpWebRequestResponseData, HttpWebRequestResponseData.ResponseBody is disposed on it's own.
[module: SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type", Target = "OBS.Runtime.Internal.Transform.HttpWebRequestResponseData")]

// Suppressions for SQS QueueUrl param being string.

[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.ChangeMessageVisibilityRequest.#.ctor(System.String,System.String,System.Int32)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.SendMessageBatchRequest.#.ctor(System.String,System.Collections.Generic.List`1<OBS.SQS.Model.SendMessageBatchRequestEntry>)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.SendMessageRequest.#.ctor(System.String,System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.ChangeMessageVisibilityBatchRequest.#.ctor(System.String,System.Collections.Generic.List`1<OBS.SQS.Model.ChangeMessageVisibilityBatchRequestEntry>)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.SetQueueAttributesRequest.#.ctor(System.String,System.Collections.Generic.Dictionary`2<System.String,System.String>)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.ReceiveMessageRequest.#.ctor(System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.GetQueueAttributesRequest.#.ctor(System.String,System.Collections.Generic.List`1<System.String>)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.AddPermissionRequest.#.ctor(System.String,System.String,System.Collections.Generic.List`1<System.String>,System.Collections.Generic.List`1<System.String>)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.DeleteQueueRequest.#.ctor(System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.DeleteMessageRequest.#.ctor(System.String,System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.RemovePermissionRequest.#.ctor(System.String,System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.Model.DeleteMessageBatchRequest.#.ctor(System.String,System.Collections.Generic.List`1<OBS.SQS.Model.DeleteMessageBatchRequestEntry>)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#SendMessage(System.String,System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#AddPermission(System.String,System.String,System.Collections.Generic.List`1<System.String>,System.Collections.Generic.List`1<System.String>)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#SendMessageBatch(System.String,System.Collections.Generic.List`1<OBS.SQS.Model.SendMessageBatchRequestEntry>)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#DeleteMessageBatch(System.String,System.Collections.Generic.List`1<OBS.SQS.Model.DeleteMessageBatchRequestEntry>)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#RemovePermission(System.String,System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#ChangeMessageVisibility(System.String,System.String,System.Int32)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#DeleteQueue(System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#ReceiveMessage(System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#GetQueueAttributes(System.String,System.Collections.Generic.List`1<System.String>)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#DeleteMessage(System.String,System.String)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#ChangeMessageVisibilityBatch(System.String,System.Collections.Generic.List`1<OBS.SQS.Model.ChangeMessageVisibilityBatchRequestEntry>)", MessageId = "0#")]
[module: SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Scope = "member", Target = "OBS.SQS.IObsSQS.#SetQueueAttributes(System.String,System.Collections.Generic.Dictionary`2<System.String,System.String>)", MessageId = "0#")]

// Suppressions for DynamoDB Nested Generic types

[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.DynamoDBv2.IObsDynamoDB.#BatchWriteItem(System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<OBS.DynamoDBv2.Model.WriteRequest>>)")]
[module: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "OBS.DynamoDBv2.Model.BatchWriteItemRequest.#.ctor(System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<OBS.DynamoDBv2.Model.WriteRequest>>)")]

// Suppression for base and subclass conversions
[module: SuppressMessage("Microsoft.Design", "CA1061:DoNotHideBaseClassMethods", Scope = "member", Target = "OBS.DynamoDBv2.DocumentModel.DynamoDBList.#op_Implicit(OBS.DynamoDBv2.DocumentModel.DynamoDBEntry[]):OBS.DynamoDBv2.DocumentModel.DynamoDBList")]