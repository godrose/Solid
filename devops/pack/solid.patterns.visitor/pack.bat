cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../src/Bin/netstandard/Release netstandard2.0 Solid.Patterns.Visitor.* /E
cd ../../
nuget pack contents/Solid.Patterns.Visitor.nuspec -OutputDirectory ../../../output