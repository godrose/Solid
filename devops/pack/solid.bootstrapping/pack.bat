cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../src/Bin/netstandard/Release netstandard2.0 Solid.Bootstrapping.* /E
cd ../../
nuget pack contents/Solid.Bootstrapping.nuspec -OutputDirectory ../../../output