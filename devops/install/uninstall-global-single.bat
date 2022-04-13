rem provide more generic way for drives case
SET current_dir=%cd%
echo %current_dir%
SET package_name=%1
SET package_version=%2
cd %UserProfile%/.nuget/packages/%package_name%
C:
rmdir /Q /S %package_version%
cd %current_dir%
Y: