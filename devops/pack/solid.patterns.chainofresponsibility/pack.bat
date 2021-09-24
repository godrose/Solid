cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../src/Bin/netstandard/Release netstandard2.0 Solid.Patterns.ChainOfResponsibility.* /E
cd ../../
nuget pack contents/Solid.Patterns.ChainOfResponsibility.nuspec -OutputDirectory ../../../output