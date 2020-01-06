using System; using eSDK_OBS_API.OBS.Util;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)] 
namespace eSDK_OBS_API.OBS.Util
{
    // 摘要: 
    //     LoggerMgr类，实现C++ eSDK_LOG_API接口,对外开放日志接口
    public class LoggerMgr
    {

        private static readonly ILog _logger = LogManager.GetLogger("LoggerMgr");

        // 摘要: 
        //     该函数用于写interface下的error日志
        //
        // 参数: 
        //   product:
        //     填写接口所属的产品，如UC的接口填写UC。包括UC、IVS、TP、FusionSphere、 Storage等
        //
        //   interfaceType:
        //     接口类型，值为1和2：其中1标识为北向接口；2标识为南向接口
        //
        //   protocolType:
        //     接口类型，值为SOAP（细分ParlayX）、Rest、COM、Native、HTTP+XML，SMPP
        //
        //   interfaceName:
        //     接口名称
        //
        //   TransactionID:
        //     唯一标识接口消息所属事务，不存在时为空
        //
        //   reqTime:
        //     请求时间
        //
        //   RespTime:
        //     应答时间
        //
        //   resultCode:
        //     接口返回结果码
        //
        //   param:
        //     请求和响应参数，格式为“paramname=value”等,关键字需要用*替换
        //
        //   otherParams:
        //     可变参数
        public static void Log_Interface_Error(string product, string interfaceType, string protocolType, string interfaceName, string TransactionID, string reqTime, string RespTime, string resultCode, string param, params object[] otherParams)
        {
            StringBuilder sb = new StringBuilder(product);
            sb.Append("|");
            sb.Append(interfaceType);
            sb.Append("|");
            sb.Append(protocolType);
            sb.Append("|");
            sb.Append(interfaceName);
            sb.Append("|");
            sb.Append(TransactionID);
            sb.Append("|");
            sb.Append(reqTime);
            sb.Append("|");
            sb.Append(RespTime);
            sb.Append("|");
            sb.Append(resultCode);
            sb.Append("|");
            sb.Append(param);
            sb.Append("|");
            if (otherParams.Count() > 0)
            {
                for (int i = 0; i < otherParams.Count();i++ )
                {
                    sb.Append(otherParams[i]);
                    if (i != otherParams.Count() - 1)
                    {
                        sb.Append("|");
                    }
                }
            }
            _logger.Error(sb.ToString());
        }
        //
        // 摘要: 
        //     该函数用于写interface下的info日志
        //
        // 参数: 
        //   product:
        //     填写接口所属的产品，如UC的接口填写UC。包括UC、IVS、TP、FusionSphere、 Storage等
        //
        //   interfaceType:
        //     接口类型，值为1和2：其中1标识为北向接口；2标识为南向接口
        //
        //   protocolType:
        //     接口类型，值为SOAP（细分ParlayX）、Rest、COM、Native、HTTP+XML，SMPP
        //
        //   interfaceName:
        //     接口名称
        //
        //   TransactionID:
        //     唯一标识接口消息所属事务，不存在时为空
        //
        //   reqTime:
        //     请求时间
        //
        //   RespTime:
        //     应答时间
        //
        //   resultCode:
        //     接口返回结果码
        //
        //   param:
        //     请求和响应参数，格式为“paramname=value”等,关键字需要用*替换
        //
        //   otherParams:
        //     可变参数
        public static void Log_Interface_Info(string product, string interfaceType, string protocolType, string interfaceName, string TransactionID, string reqTime, string RespTime, string resultCode, string param, params object[] otherParams)
        {
            StringBuilder sb = new StringBuilder(product);
            sb.Append("|");
            sb.Append(interfaceType);
            sb.Append("|");
            sb.Append(protocolType);
            sb.Append("|");
            sb.Append(interfaceName);
            sb.Append("|");
            sb.Append(TransactionID);
            sb.Append("|");
            sb.Append(reqTime);
            sb.Append("|");
            sb.Append(RespTime);
            sb.Append("|");
            sb.Append(resultCode);
            sb.Append("|");
            sb.Append(param);
            sb.Append("|");
            if (otherParams.Count() > 0)
            {
                for (int i = 0; i < otherParams.Count(); i++)
                {
                    sb.Append(otherParams[i]);
                    if (i != otherParams.Count() - 1)
                    {
                        sb.Append("|");
                    }
                }
            }
            _logger.Info(sb.ToString());
        }
        //
        // 摘要: 
        //     该函数用于写operate下的Debug日志
        //
        // 参数: 
        //   product:
        //     使用日志模块的产品名字，同进程中的唯一标示
        //
        //   moduleName:
        //     内部模块名称，暂时分为：login、config、log、version
        //
        //   userName:
        //     操作用户名
        //
        //   clientFlag:
        //     操作客户端标识，一般为客户端IP
        //
        //   resultCode:
        //     操作结果码
        //
        //   keyInfo:
        //     关键描述信息：查询类操作，需要包括查询对象标识、名称、相关属性名称和属性值。设置类操作，需要包括设置对象标识、名称、相关属性名称和属性新值和旧值。创建类操作，需要包括创建涉及对象标识、名称。删除类操作，需要包括删除涉及对象标识、名称。
        //
        //   param:
        //     无
        //
        //   otherParams:
        //     可变参数
        public static void Log_Operate_Debug(string product, string moduleName, string userName, string clientFlag, string resultCode, string keyInfo, string param, params object[] otherParams)
        {
            StringBuilder sb = new StringBuilder(product);
            sb.Append("|");
            sb.Append(moduleName);
            sb.Append("|");
            sb.Append(userName);
            sb.Append("|");
            sb.Append(clientFlag);
            sb.Append("|");
            sb.Append(resultCode);
            sb.Append("|");
            sb.Append(keyInfo);
            sb.Append("|");
            sb.Append(param);
            sb.Append("|");
            if (otherParams.Count() > 0)
            {
                for (int i = 0; i < otherParams.Count(); i++)
                {
                    sb.Append(otherParams[i]);
                    if (i != otherParams.Count() - 1)
                    {
                        sb.Append("|");
                    }
                }
            }
            _logger.Debug(sb.ToString());
        }
        //
        // 摘要: 
        //     该函数用于写operate下的Error日志
        //
        // 参数: 
        //   product:
        //     使用日志模块的产品名字，同进程中的唯一标示
        //
        //   moduleName:
        //     内部模块名称，暂时分为：login、config、log、version
        //
        //   userName:
        //     操作用户名
        //
        //   clientFlag:
        //     操作客户端标识，一般为客户端IP
        //
        //   resultCode:
        //     操作结果码
        //
        //   keyInfo:
        //     关键描述信息：查询类操作，需要包括查询对象标识、名称、相关属性名称和属性值。设置类操作，需要包括设置对象标识、名称、相关属性名称和属性新值和旧值。创建类操作，需要包括创建涉及对象标识、名称。删除类操作，需要包括删除涉及对象标识、名称。
        //
        //   param:
        //     无
        //
        //   otherParams:
        //     可变参数
        public static void Log_Operate_Error(string product, string moduleName, string userName, string clientFlag, string resultCode, string keyInfo, string param, params object[] otherParams)
        {
            StringBuilder sb = new StringBuilder(product);
            sb.Append("|");
            sb.Append(moduleName);
            sb.Append("|");
            sb.Append(userName);
            sb.Append("|");
            sb.Append(clientFlag);
            sb.Append("|");
            sb.Append(resultCode);
            sb.Append("|");
            sb.Append(keyInfo);
            sb.Append("|");
            sb.Append(param);
            sb.Append("|");
            if (otherParams.Count() > 0)
            {
                for (int i = 0; i < otherParams.Count(); i++)
                {
                    sb.Append(otherParams[i]);
                    if (i != otherParams.Count() - 1)
                    {
                        sb.Append("|");
                    }
                }
            }
            _logger.Error(sb.ToString());
        }
        //
        // 摘要: 
        //     该函数用于写operate下的Info日志
        //
        // 参数: 
        //   product:
        //     使用日志模块的产品名字，同进程中的唯一标示
        //
        //   moduleName:
        //     内部模块名称，暂时分为：login、config、log、version
        //
        //   userName:
        //     操作用户名
        //
        //   clientFlag:
        //     操作客户端标识，一般为客户端IP
        //
        //   resultCode:
        //     操作结果码
        //
        //   keyInfo:
        //     关键描述信息：查询类操作，需要包括查询对象标识、名称、相关属性名称和属性值。设置类操作，需要包括设置对象标识、名称、相关属性名称和属性新值和旧值。创建类操作，需要包括创建涉及对象标识、名称。删除类操作，需要包括删除涉及对象标识、名称。
        //
        //   param:
        //     无
        //
        //   otherParams:
        //     可变参数
        public static void Log_Operate_Info(string product, string moduleName, string userName, string clientFlag, string resultCode, string keyInfo, string param, params object[] otherParams)
        {
            StringBuilder sb = new StringBuilder(product);
            sb.Append("|");
            sb.Append(moduleName);
            sb.Append("|");
            sb.Append(userName);
            sb.Append("|");
            sb.Append(clientFlag);
            sb.Append("|");
            sb.Append(resultCode);
            sb.Append("|");
            sb.Append(keyInfo);
            sb.Append("|");
            sb.Append(param);
            sb.Append("|");
            if (otherParams.Count() > 0)
            {
                for (int i = 0; i < otherParams.Count(); i++)
                {
                    sb.Append(otherParams[i]);
                    if (i != otherParams.Count() - 1)
                    {
                        sb.Append("|");
                    }
                }
            }
            _logger.Info(sb.ToString());
        }
        //
        // 摘要: 
        //     该函数用于写operate下的Warn日志
        //
        // 参数: 
        //   product:
        //     使用日志模块的产品名字，同进程中的唯一标示
        //
        //   moduleName:
        //     内部模块名称，暂时分为：login、config、log、version
        //
        //   userName:
        //     操作用户名
        //
        //   clientFlag:
        //     操作客户端标识，一般为客户端IP
        //
        //   resultCode:
        //     操作结果码
        //
        //   keyInfo:
        //     关键描述信息：查询类操作，需要包括查询对象标识、名称、相关属性名称和属性值。设置类操作，需要包括设置对象标识、名称、相关属性名称和属性新值和旧值。创建类操作，需要包括创建涉及对象标识、名称。删除类操作，需要包括删除涉及对象标识、名称。
        //
        //   param:
        //     无
        //
        //   otherParams:
        //     可变参数
        public static void Log_Operate_Warn(string product, string moduleName, string userName, string clientFlag, string resultCode, string keyInfo, string param, params object[] otherParams)
        {
            StringBuilder sb = new StringBuilder(product);
            sb.Append("|");
            sb.Append(moduleName);
            sb.Append("|");
            sb.Append(userName);
            sb.Append("|");
            sb.Append(clientFlag);
            sb.Append("|");
            sb.Append(resultCode);
            sb.Append("|");
            sb.Append(keyInfo);
            sb.Append("|");
            sb.Append(param);
            sb.Append("|");
            if (otherParams.Count() > 0)
            {
                for (int i = 0; i < otherParams.Count(); i++)
                {
                    sb.Append(otherParams[i]);
                    if (i != otherParams.Count() - 1)
                    {
                        sb.Append("|");
                    }
                }
            }
            _logger.Warn(sb.ToString());
        }
        //
        // 摘要: 
        //     该函数用于写run下的Debug日志
        //
        // 参数: 
        //   product:
        //     使用日志模块的产品名字，同进程中的唯一标示
        //
        //   param:
        //     param
        public static void Log_Run_Debug(string product, string param)
        {
            StringBuilder sb = new StringBuilder(product);
            sb.Append("|");
            sb.Append(param);
            sb.Append("|");
            _logger.Debug(sb.ToString());
        }
        //
        // 摘要: 
        //     该函数用于写run下的Error日志
        //
        // 参数: 
        //   product:
        //     使用日志模块的产品名字，同进程中的唯一标示
        //
        //   param:
        //     param
        public static void Log_Run_Error(string product, string param)
        {
            StringBuilder sb = new StringBuilder(product);
            sb.Append("|");
            sb.Append(param);
            sb.Append("|");
            _logger.Error(sb.ToString());
        }
        //
        // 摘要: 
        //     该函数用于写run下的Info日志
        //
        // 参数: 
        //   product:
        //     使用日志模块的产品名字，同进程中的唯一标示
        //
        //   param:
        //     param
        public static void Log_Run_Info(string product, string param)
        {
            StringBuilder sb = new StringBuilder(product);
            sb.Append("|");
            sb.Append(param);
            sb.Append("|");
            _logger.Info(sb.ToString());
        }
        //
        // 摘要: 
        //     该函数用于写run下的Warn日志
        //
        // 参数: 
        //   product:
        //     使用日志模块的产品名字，同进程中的唯一标示
        //
        //   param:
        //     param
        public static void Log_Run_Warn(string product, string param)
        {
            StringBuilder sb = new StringBuilder(product);
            sb.Append("|");
            sb.Append(param);
            sb.Append("|");
            _logger.Warn(sb.ToString());
        }
        //
        // 摘要: 
        //     该函数用于进程不使用本API后，去初始化数据
        //
        // 参数: 
        //   product:
        //     使用日志模块的产品名字，同进程中的唯一标示
        //
        // 返回结果: 
        //     非0失败（请参考错误返回码枚举类型RetCode）
        public static int LogFini(string product)
        {
            return 0;
        }
        //
        // 摘要: 
        //     该函数用于初始化数据
        //
        // 参数: 
        //   product:
        //     使用日志模块的产品名字，同进程中的唯一标示
        //
        //   iniFile:
        //     日志配置文件路径（包括配置文件名，如：D:\eSDKClientLogCfg.ini）
        //
        //   logLevel:
        //     logLevel[0]接口日志级别, logLevel[1]操作日志级别, logLevel[2]运行日志级别，参考枚举LOGLEVEL
        //
        //   logPath:
        //     日志保存路径（如：D:\log\），必须是绝对路径，如要使用默认配置请用INVALID_LOG_LEVEL
        //
        // 返回结果: 
        //     非0失败（请参考错误返回码枚举类型RetCode）
        public static int logInit(string product, string inifile, int[] loglevel, string logpath)
        {
            return 0;
        }
    }
}
