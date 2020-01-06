using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using Google.Protobuf;
using HuaweiCloud.DIS.Api.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetRecordsResult = Com.Bigdata.Dis.Sdk.DISCommon.Model.GetRecordsResult;
using Record = Com.Bigdata.Dis.Sdk.DISCommon.Model.Record;
using PutRecordsResult = Com.Bigdata.Dis.Sdk.DISCommon.Model.PutRecordsResult;
using PutRecordsResultEntry = Com.Bigdata.Dis.Sdk.DISCommon.Model.PutRecordsResultEntry;
using PutRecordsRequest = Com.Bigdata.Dis.Sdk.DISCommon.Model.PutRecordsRequest;
using PutRecordsRequestEntry = Com.Bigdata.Dis.Sdk.DISCommon.Model.PutRecordsRequestEntry;
using PutRecordsRequestEntryExtendedInfo = Com.Bigdata.Dis.Sdk.DISCommon.Model.PutRecordsRequestEntryExtendedInfo;

namespace DISClient_4._5.Protobuf
{
    public class ProtobufUtils
    {
        /// <summary>
        /// 从protobuf类型的上传数据响应，转换为标准的响应类型
        /// </summary>
        /// <param name="putRecordsResult"></param>
        /// <returns></returns>
        public static PutRecordsResult ToPutRecordsResult(HuaweiCloud.DIS.Api.Protobuf.PutRecordsResult putRecordsResult)
        {
            PutRecordsResult result = new PutRecordsResult();
            result.FailedRecordCount = putRecordsResult.FailedRecordCount;

            List<PutRecordsResultEntry> records = new List<PutRecordsResultEntry>();

            foreach (HuaweiCloud.DIS.Api.Protobuf.PutRecordsResultEntry protoEntry in putRecordsResult.Records)
            {
                PutRecordsResultEntry entry = new PutRecordsResultEntry();
                entry.ErrorCode = string.IsNullOrEmpty(protoEntry.ErrorCode) ? null : protoEntry.ErrorCode;
                entry.SequenceNumber = string.IsNullOrEmpty(protoEntry.SequenceNumber) ? null : protoEntry.SequenceNumber;
                entry.SequenceNumber = protoEntry.SequenceNumber;
                entry.ShardId = protoEntry.ShardId;
                records.Add(entry);
            }
            result.Records = records;
            return result;
        }

        /// <summary>
        /// 将标准请求类型的对象转换为protobuf的请求参数类型
        /// </summary>
        /// <param name="putRecordsParam"></param>
        /// <returns></returns>
        public static HuaweiCloud.DIS.Api.Protobuf.PutRecordsRequest ToProtobufPutRecordsRequest(PutRecordsRequest putRecordsParam)
        {
            HuaweiCloud.DIS.Api.Protobuf.PutRecordsRequest protobufRequest = new HuaweiCloud.DIS.Api.Protobuf.PutRecordsRequest();
            protobufRequest.StreamName = putRecordsParam.StreamName;

            List<HuaweiCloud.DIS.Api.Protobuf.PutRecordsRequestEntry> protobufRecordsList = new List<HuaweiCloud.DIS.Api.Protobuf.PutRecordsRequestEntry>();

            foreach (PutRecordsRequestEntry putRecordsRequestEntry in putRecordsParam.Records)
            {
                HuaweiCloud.DIS.Api.Protobuf.PutRecordsRequestEntry protobufReqEntry = new HuaweiCloud.DIS.Api.Protobuf.PutRecordsRequestEntry();
                
                if (putRecordsRequestEntry.Data != null)
                {
                    protobufReqEntry.Data = ByteString.CopyFrom(putRecordsRequestEntry.Data.ToArray());
                }
                if (putRecordsRequestEntry.PartitionKey != null)
                {
                    protobufReqEntry.PartitionKey = putRecordsRequestEntry.PartitionKey;
                }
                if (putRecordsRequestEntry.ExplicitHashKey != null)
                {
                    protobufReqEntry.ExplicitHashKey = putRecordsRequestEntry.ExplicitHashKey;
                }
                if (putRecordsRequestEntry.PartitionId != null)
                {
                    protobufReqEntry.PartitionId = putRecordsRequestEntry.PartitionId;
                }


                PutRecordsRequestEntryExtendedInfo putRecordsRequestEntryExtendedInfo = putRecordsRequestEntry.ExtenedInfo;
                if (putRecordsRequestEntryExtendedInfo != null)
                {
                    HuaweiCloud.DIS.Api.Protobuf.PutRecordsRequestEntryExtendedInfo ProtobufExtendedInfo = new HuaweiCloud.DIS.Api.Protobuf.PutRecordsRequestEntryExtendedInfo();

                    if (putRecordsRequestEntryExtendedInfo.DeliverDataId != null)
                    {
                        ProtobufExtendedInfo.DeliverDataId = putRecordsRequestEntryExtendedInfo.DeliverDataId;
                    }

                    if (putRecordsRequestEntryExtendedInfo.FileName != null)
                    {
                        ProtobufExtendedInfo.FileName = putRecordsRequestEntryExtendedInfo.FileName;
                    }

                    ProtobufExtendedInfo.EndFlag = putRecordsRequestEntryExtendedInfo.EndFlag;

                    ProtobufExtendedInfo.SeqNum = putRecordsRequestEntryExtendedInfo.SequenceNumber;

                    protobufReqEntry.ExtendedInfo = ProtobufExtendedInfo;
                }

                protobufRecordsList.Add(protobufReqEntry);
            }

            protobufRequest.Records.Add(protobufRecordsList);
            return protobufRequest;
        }

        /// <summary>
        /// 将protobuf类型的下载数据响应，转换为标准的响应类型
        /// </summary>
        /// <param name="protoResult"></param>
        /// <returns></returns>
        public static GetRecordsResult ToGetRecordsResult(HuaweiCloud.DIS.Api.Protobuf.GetRecordsResult protoResult)
        {
            GetRecordsResult result = new GetRecordsResult();
            result.NextShardIterator = protoResult.NextShardIterator;

            List<Record> records = new List<Record>();
            foreach (HuaweiCloud.DIS.Api.Protobuf.Record protoRecord in protoResult.Records)
            {
                Record record = new Record();

                record.SequenceNumber = protoRecord.SequenceNumber;
                record.PartitionKey = protoRecord.PartitionKey;
                if (protoRecord.ToByteArray() != null)
                {
                    record.Data = protoRecord.Data.ToByteArray();
                }
                record.Timestamp = protoRecord.Timestamp;
                if (!string.IsNullOrEmpty(protoRecord.TimestampType))
                {
                    record.TimestampType = protoRecord.TimestampType;
                }

                records.Add(record);
            }
            result.Records = records;

            return result;
        }

    }
}
