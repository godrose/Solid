SET package_version=%1
SET target=../../../packages/Tests-All
for /d %%d in (../pack/*.*) do (
	echo %%d 
	call copy-single.bat %%d %package_version% %target%	
)