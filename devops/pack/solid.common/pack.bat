cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir net462\
robocopy ../../../../../src/Bin/netframework/Release net462 Solid.Common.Platform.* /E
mkdir net6.0
robocopy ../../../../../src/Bin/net/Release net6.0 Solid.Common.Platform.* /E
cd net6.0
rmdir /Q /S ref
cd ..
mkdir netcoreapp3.1
robocopy ../../../../../src/Bin/netcore/Release netcoreapp3.1 Solid.Common.Platform.* /E
cd ../../
nuget pack contents/solid.common.nuspec -OutputDirectory ../../../output