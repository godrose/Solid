REM TODO: read from csproj or set during pack process
set package_name=Solid.Templates
REM TODO: read from csproj or set during pack process
set package_version=2.2.0-rc1

call pack-templates.cmd

if %ERRORLEVEL% NEQ 0 ( 
	goto EXIT
)

call install-package.cmd %package_name% %package_version%

cd ..

if exist output (
	rmdir output /s /q
)

if exist bin (
	rmdir bin /s /q
)

if exist obj (
	rmdir obj /s /q
)

:EXIT
EXIT /B %ERRORLEVEL%