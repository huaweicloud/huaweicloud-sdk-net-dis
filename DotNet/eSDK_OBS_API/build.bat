::关闭回显
@echo off


set CUR_PATH=%cd%
set OBS_BUILD_PATH=eSDK_OBS_API_.Net
set OBS_API_PATH=sdk
set OBS_DEMO_PATH=demo
set LOG4NET_SRC_PATH=%CUR_PATH%\..\third_party_dlls\log4net
set DEMO_SRC_PATH=%CUR_PATH%\..\demo


del /F /S /Q %OBS_BUILD_PATH% %OBS_BUILD_PATH%.zip 
rd /Q /S 	%OBS_BUILD_PATH%

mkdir %OBS_BUILD_PATH%
mkdir %OBS_BUILD_PATH%\%OBS_API_PATH%
mkdir %OBS_BUILD_PATH%\%OBS_DEMO_PATH%



::获得当前时间，作为生成版本的目录名
for /F "tokens=1-4 delims=-/ " %%i in ('date /t') do (
   set Year=%%i
   set Month=%%j
   set Day=%%k
   set DayOfWeek=%%l
)
for /F "tokens=1-2 delims=: " %%i in ('time /t') do (
   set Hour=%%i
   set Minute=%%j
)

::设置各变量名
set   	DateTime=%Year%-%Month%-%Day%-%Hour%-%Minute%

@echo off
echo %DateTime%
@echo .

echo.
echo －－－－－－－－－－－－编译 log4net release版本 －－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－

echo －－－－－－－－－－－－解压日志源码文件－－－－－－－－－－－－－

call "C:\Program Files (x86)\WinRAR\WinRAR.exe"  x  %LOG4NET_SRC_PATH%\log4net-2.0.8-src.zip  %LOG4NET_SRC_PATH%

echo －－－－－－－－－－－－执行日志编译脚本－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－
call "%LOG4NET_SRC_PATH%\log4net-2.0.8\build.cmd"
xcopy /y /i /r /s "%LOG4NET_SRC_PATH%\log4net-2.0.8\bin\net\4.5\release\log4net.dll"						"%CUR_PATH%\bin\Release"
echo －－－－－－－－－－－－编译 eSDK_OBS_API release版本 －－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－
"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe" .\eSDK_OBS_API.sln /Clean
@"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe" .\eSDK_OBS_API.sln /Rebuild "release|AnyCPU" /out output.txt
echo.
echo －－－－－－－－－－－－编译 eSDK_OBS_API release版本成功－－－－－－－


@echo .
@echo －－－－－－－－－－－－－－开始拷贝版本－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－	
@echo .

echo －－－－－－－－－－－－拷贝 eSDK_OBS_API/日志库/日志配置 －－－－－－－－－
xcopy /y /i /r /s "%CUR_PATH%\bin\Release\eSDK_OBS_API_.NET.dll"											"%OBS_BUILD_PATH%\%OBS_API_PATH%"
xcopy /y /i /r /s "%CUR_PATH%\bin\Release\log4net.dll"						"%OBS_BUILD_PATH%\%OBS_API_PATH%"
xcopy /y /i /r /s "%CUR_PATH%\bin\Release\Log4Net.config"						"%OBS_BUILD_PATH%\%OBS_API_PATH%"									

echo －－－－－－－－－－－－拷贝 demo －－－－－－－－－-----------------------								
xcopy /y /i /r /s "%DEMO_SRC_PATH%\App.config"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\assemblyinfo.cs"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\BucketOperationsSample.cs"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\ObjectOperationsSample.cs"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\ObsSample.csproj"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\ObsSample.sln"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\TrustAllCertificatePolicy.cs"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%



cd "%OBS_BUILD_PATH%"

echo －－－－－－－－－－－－压缩文件 sdk/demo/doc－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－
call "C:\Program Files (x86)\WinRAR\WinRAR.exe" a -r %OBS_API_PATH%.zip %OBS_API_PATH%
call "C:\Program Files (x86)\WinRAR\WinRAR.exe" a -r %OBS_DEMO_PATH%.zip %OBS_DEMO_PATH%


cd %CUR_PATH%
echo －－－－－－－－－－－－压缩文件 %OBS_BUILD_PATH%.zip－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－
call "C:\Program Files (x86)\WinRAR\WinRAR.exe" a -r %OBS_BUILD_PATH%.zip .\%OBS_BUILD_PATH%\*.zip
rd /Q /S "%CUR_PATH%\%OBS_BUILD_PATH%"

