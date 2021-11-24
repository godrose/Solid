cd ../..
nuget restore
cd devops/publish
PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& '../build/build-all.ps1'"
SET package_name=%1
SET package_version=2.3.3
SET target=../../../packages/Tests-All
cd ../test
call test-all
cd ../pack
call pack-single %package_name%
cd ../../publish
call copy-single %package_name% %package_version% %target%
cd ../install
call uninstall-global-single.bat %package_name% %package_version%
cd ..