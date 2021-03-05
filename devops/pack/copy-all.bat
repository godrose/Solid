SET package_version=%1
SET target=../../../packages/Tests-All
for /d %%d in (*.*) do (
	echo %%d 
	call copy-single.bat %%d %package_version% %target%	
)