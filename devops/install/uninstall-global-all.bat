SET package_version=%1
for /d %%d in (../pack/*.*) do (
	echo %%d 
	call uninstall-global-single.bat %%d %package_version%
)