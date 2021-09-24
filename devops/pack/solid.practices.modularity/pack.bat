cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../src/Bin/netstandard/Release netstandard2.0 Solid.Practices.Modularity.* /E
cd ../../
nuget pack contents/Solid.Practices.Modularity.nuspec -OutputDirectory ../../../output