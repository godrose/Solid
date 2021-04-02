for /f "delims=" %%D in ('dir /a:d /b') do (
	cd %%~fD 
	call pack.bat
	cd ..
)