PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& '../build/build-all.ps1'"
SET package_version=2.3.0-rc1
cd ../test
call test-all
cd ../pack
call pack-all
cd ../publish
call copy-all %package_version%
