cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../Bin/netstandard/Release netstandard2.0 Solid.Practices.Scheduling.* /E
cd ../../
nuget pack contents/Solid.Practices.Scheduling.nuspec -OutputDirectory ../../../output