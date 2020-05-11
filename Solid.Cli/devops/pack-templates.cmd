call copy-template.cmd Solid.Templates --use-common

if %ERRORLEVEL% NEQ 0 ( 
	goto EXIT
)

cd ..\
dotnet pack -o output

if %ERRORLEVEL% NEQ 0 ( 
	goto EXIT
)

cd devops

:EXIT
EXIT /B %ERRORLEVEL%