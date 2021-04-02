cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../Bin/netstandard/Release netstandard2.0 Solid.Practices.Composition.Client.* /E
cd ../../
nuget pack contents/solid.practices.composition.client.nuspec -OutputDirectory ../../../output