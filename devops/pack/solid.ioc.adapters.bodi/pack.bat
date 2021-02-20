cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../Bin/netstandard/Release netstandard2.0 Solid.IoC.Adapters.BoDi.* /E
cd ../../
nuget pack contents/Solid.IoC.Adapters.BoDi.nuspec -OutputDirectory ../../../output