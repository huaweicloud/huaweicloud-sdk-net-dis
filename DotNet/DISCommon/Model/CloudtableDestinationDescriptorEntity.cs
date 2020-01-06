using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class CloudtableDestinationDescriptorEntity
    {
        /// <summary>
        /// 偏移量。
        /// LATEST：最大偏移量，即获取最新的数据。
        /// EARLIEST：最小偏移量，即读取最早的数据。
        /// 缺省值:LATEST
        /// </summary>
        [JsonProperty("consumer_strategy", NullValueHandling = NullValueHandling.Ignore)]
        public string ConsumerStrategy { get; set; }

        /// <summary>
        /// 存储该通道数据的CloudTable集群名称。
        /// </summary>
        [JsonProperty("cloudtable_cluster_name", NullValueHandling = NullValueHandling.Ignore)]
        public string CloudTableClusterName { get; set; }

        /// <summary>
        /// 存储该通道数据的CloudTable集群ID。
        /// </summary>
        [JsonProperty("cloudtable_cluster_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CloudtableClusterId { get; set; }

        /// <summary>
        /// 转储HBase时必选，表示存储该通道数据的CloudTable集群HBase表名称。
        /// </summary>
        [JsonProperty("cloudtable_table_name", NullValueHandling = NullValueHandling.Ignore)]
        public string CloudtableTable_name { get; set; }

        /// <summary>
        /// 转储HBase时必选，与“opentsdb_schema”二选一，表示CloudTable集群HBase数据的Schema配置。用于将通道内的JSON数据进行格式转换并导入Cloudtable的HBase表中。
        /// </summary>
        [JsonProperty("cloudtable_schema", NullValueHandling = NullValueHandling.Ignore)]
        public CloudtableSchema CloudtableSchema { get; set; }

        /// <summary>
        /// 转储HBase的rowkey分隔符，用于分隔生成rowKey的用户数据。取值范围：”, ”、 ”. ”、 ”|”、 ”; ”、 ”\”、 ”-”、 ”_”、 ”~”
        /// 缺省值：”.”
        /// </summary>
        [JsonProperty("cloudtable_row_key_delimiter", NullValueHandling = NullValueHandling.Ignore)]
        public CloudtableSchema CloudtableRowKeyDelimiter { get; set; }

        /// <summary>
        /// 转储OpenTSDB时必选，与“cloudtable_schema”二选一，表示CloudTable集群OpenTSDB数据的Schema配置。用于将通道内的JSON数据进行格式转换并导入Cloudtable的OpenTSDB。
        /// </summary>
        [JsonProperty("opentsdb_schema", NullValueHandling = NullValueHandling.Ignore)]
        public List<OpentsdbSchema> OpentsdbSchema { get; set; }

        /// <summary>
        /// 用户数据转储CloudTable服务失败时，可选择将转储失败的数据备份至OBS服务，此参数为OBS服务的桶名称
        /// </summary>
        [JsonProperty("obs_backup_bucket_path", NullValueHandling = NullValueHandling.Ignore)]
        public string ObsBackupBucketPath { get; set; }

        /// <summary>
        /// 用户数据转储CloudTable服务失败时，可选择将转储失败的数据备份至OBS服务，此参数为OBS桶下的自定义目录，多级目录可用“/”进行分隔，不可以“/”开头
        /// </summary>
        [JsonProperty("backup_file_prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string BackupFilePrefix { get; set; }

        /// <summary>
        /// 用户数据导入CloudTable服务失败的重试失效时间
        /// </summary>
        [JsonProperty("retry_duration", NullValueHandling = NullValueHandling.Ignore)]
        public string RetryDuration { get; set; }
    }


    public class CloudtableSchema
    {
        [JsonProperty("row_key", NullValueHandling = NullValueHandling.Ignore)]
        public List<Row> RowKey { get; set; }

        [JsonProperty("columns", NullValueHandling = NullValueHandling.Ignore)]
        public List<Column> Columns { get; set; }
    }

    public class Row
    {
        /// <summary>
        /// 通道内JSON数据的JSON属性名，用于生成HBase数据的rowkey。
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        /// 通道内JSON数据的JSON属性的类型名称。
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }

    public class Column
    {
        /// <summary>
        /// 存储该通道数据的HBase表数据的列族名称。
        /// </summary>
        [JsonProperty("column_family_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ColumnFamilyName { get; set; }

        /// <summary>
        /// 存储该通道数据的HBase表数据的列名称。
        /// </summary>
        [JsonProperty("column_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ColumnName { get; set; }

        /// <summary>
        /// 通道内JSON数据的JSON属性名，用于生成HBase数据的列值。
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        /// 通道内JSON数据的JSON属性的类型名称。
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }

    public class OpentsdbSchema
    {
        /// <summary>
        /// CloudTable集群OpenTSDB数据metric的Schema配置，用于将通道内的JSON数据进行格式转换生成OpenTSDB数据的metric。
        /// </summary>
        [JsonProperty("metric", NullValueHandling = NullValueHandling.Ignore)]
        public List<Metric> Metrics { get; set; }

        /// <summary>
        /// CloudTable集群OpenTSDB 数据timestamp的Schema配置，用于将通道内的JSON数据进行格式转换生成OpenTSDB数据的timestamp。
        /// </summary>
        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public Timestamp Timestamp { get; set; }

        /// <summary>
        /// CloudTable集群OpenTSDB 数据value的Schema配置，用于将通道内的JSON数据进行格式转换生成OpenTSDB 数据的value。
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public ValueEntity ValueEntity { get; set; }

        /// <summary>
        /// CloudTable集群OpenTSDB数据tags的Schema配置，用于将通道内的JSON数据进行格式转换生成OpenTSDB数据的tags。
        /// </summary>
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<Tags> Tags { get; set; }
    }

    public class Metric
    {
        /// <summary>
        /// 常量或通道内用户数据的JSON属性名称。
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        /// Constant表示metric为常量value的值。
        /// String表示metric为通道内用户数据对应JSON属性的取值，且该JOSN属性的取值为String。
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }

    public class Timestamp
    {
        /// <summary>
        /// Timestamp类型表示通道内用户数据对应JSON属性的取值为Timestamp类型，不需要进行数据格式转换就可以生成OpenTSDB的timestamp。
        /// String类型表示通道内用户数据对应JSON属性的取值为Date格式，需要进行数据格式转换才能生成OpenTSDB的timestamp。
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        /// 通道内用户数据的JSON属性名称。
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        /// “type”为“String”类型时必选。表示通道内用户数据对应JSON属性的取值为Date格式，需要根据format字段进行数据格式转换生成OpenTSDB的timestamp。
        /// </summary>
        [JsonProperty("format", NullValueHandling = NullValueHandling.Ignore)]
        public string Format { get; set; }
    }

    public class ValueEntity
    {
        /// <summary>
        /// 通道内用户JSON数据对应JSON属性的类型名称。
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        /// 通道内用户数据的JSON属性名称。
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

    public class Tags
    {
        /// <summary>
        /// 存储该通道数据的OpenTSDB数据的tag名称。
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// 通道内用户JSON数据对应JSON属性的类型名称。
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        /// 常量或通道内用户数据的JSON属性名称。
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

}
