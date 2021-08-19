cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../Bin/netstandard/Release netstandard2.0 Solid.IoC.Registration.* /E
cd ../../
nuget pack contents/Solid.IoC.Registration.nuspec -OutputDirectory ../../../output