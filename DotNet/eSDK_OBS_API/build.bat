::�رջ���
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



::��õ�ǰʱ�䣬��Ϊ���ɰ汾��Ŀ¼��
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

::���ø�������
set   	DateTime=%Year%-%Month%-%Day%-%Hour%-%Minute%

@echo off
echo %DateTime%
@echo .

echo.
echo ���������������������������� log4net release�汾 ������������������������������������������������������������������������������������������

echo ��������������������������ѹ��־Դ���ļ���������������������������

call "C:\Program Files (x86)\WinRAR\WinRAR.exe"  x  %LOG4NET_SRC_PATH%\log4net-2.0.8-src.zip  %LOG4NET_SRC_PATH%

echo ������������������������ִ����־����ű���������������������������������������������������������������������������������������������������
call "%LOG4NET_SRC_PATH%\log4net-2.0.8\build.cmd"
xcopy /y /i /r /s "%LOG4NET_SRC_PATH%\log4net-2.0.8\bin\net\4.5\release\log4net.dll"						"%CUR_PATH%\bin\Release"
echo ���������������������������� eSDK_OBS_API release�汾 ������������������������������������������������������������������������������������
"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe" .\eSDK_OBS_API.sln /Clean
@"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe" .\eSDK_OBS_API.sln /Rebuild "release|AnyCPU" /out output.txt
echo.
echo ���������������������������� eSDK_OBS_API release�汾�ɹ���������������


@echo .
@echo ������������������������������ʼ�����汾��������������������������������������������������������������������������������������������������	
@echo .

echo ���������������������������� eSDK_OBS_API/��־��/��־���� ������������������
xcopy /y /i /r /s "%CUR_PATH%\bin\Release\eSDK_OBS_API_.NET.dll"											"%OBS_BUILD_PATH%\%OBS_API_PATH%"
xcopy /y /i /r /s "%CUR_PATH%\bin\Release\log4net.dll"						"%OBS_BUILD_PATH%\%OBS_API_PATH%"
xcopy /y /i /r /s "%CUR_PATH%\bin\Release\Log4Net.config"						"%OBS_BUILD_PATH%\%OBS_API_PATH%"									

echo ���������������������������� demo ������������������-----------------------								
xcopy /y /i /r /s "%DEMO_SRC_PATH%\App.config"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\assemblyinfo.cs"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\BucketOperationsSample.cs"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\ObjectOperationsSample.cs"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\ObsSample.csproj"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\ObsSample.sln"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%
xcopy /y /i /r /s "%DEMO_SRC_PATH%\TrustAllCertificatePolicy.cs"						%OBS_BUILD_PATH%\%OBS_DEMO_PATH%



cd "%OBS_BUILD_PATH%"

echo ������������������������ѹ���ļ� sdk/demo/doc����������������������������������������������������������������������������������������������������������
call "C:\Program Files (x86)\WinRAR\WinRAR.exe" a -r %OBS_API_PATH%.zip %OBS_API_PATH%
call "C:\Program Files (x86)\WinRAR\WinRAR.exe" a -r %OBS_DEMO_PATH%.zip %OBS_DEMO_PATH%


cd %CUR_PATH%
echo ������������������������ѹ���ļ� %OBS_BUILD_PATH%.zip����������������������������������������������������������������������������������������
call "C:\Program Files (x86)\WinRAR\WinRAR.exe" a -r %OBS_BUILD_PATH%.zip .\%OBS_BUILD_PATH%\*.zip
rd /Q /S "%CUR_PATH%\%OBS_BUILD_PATH%"

