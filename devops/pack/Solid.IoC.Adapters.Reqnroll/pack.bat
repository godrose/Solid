cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../src/Bin/netstandard/Release netstandard2.0 Solid.IoC.Adapters.Reqnroll.* /E
cd ../../
nuget pack contents/Solid.IoC.Adapters.Reqnroll.nuspec -OutputDirectory ../../../output