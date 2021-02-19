rmdir /Q /S contents/lib
cd contents
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../Bin/netstandard/Release netstandard2.0 Solid.Core.* /E
cd ../../
nuget pack contents/solid.core.nuspec -OutputDirectory ../../../output