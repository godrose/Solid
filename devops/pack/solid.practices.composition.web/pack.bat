cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../src/Bin/netstandard/Release netstandard2.0 Solid.Practices.Composition.Web.* /E
cd ../../
nuget pack contents/solid.practices.composition.web.nuspec -OutputDirectory ../../../output