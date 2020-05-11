REM %1 - Package name
REM %2 - Package version
REM TODO: read from csproj or set during pack process
set package_file_name=%1.%2.nupkg
echo %package_file_name%

REM Uninstall package
dotnet .\utils\UninstallTemplate.dll -d %1

if %ERRORLEVEL% NEQ 0 ( 
	goto EXIT
)

cd ..\output
dotnet new -i %package_file_name%

if %ERRORLEVEL% NEQ 0 ( 
	goto EXIT
)

cd ..\..\devops

:EXIT
EXIT /B %ERRORLEVEL%