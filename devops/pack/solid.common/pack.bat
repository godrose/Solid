cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir net461\
robocopy ../../../../../src/Bin/netframework/Release net461 Solid.Common.Platform.* /E
mkdir net5.0
robocopy ../../../../../src/Bin/net/Release net5.0 Solid.Common.Platform.* /E
cd net5.0
rmdir /Q /S ref
cd ..
mkdir netcoreapp3.1
robocopy ../../../../../src/Bin/netcore/Release netcoreapp3.1 Solid.Common.Platform.* /E
mkdir uap10.0
robocopy ../../../../../src/Bin/uwp/Release uap10.0 Solid.Common.Platform.* /E
cd ../../
nuget pack contents/solid.common.nuspec -OutputDirectory ../../../output