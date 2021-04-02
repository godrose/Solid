cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../Bin/netstandard/Release netstandard2.0 Solid.Practices.Middleware.* /E
cd ../../
nuget pack contents/Solid.Practices.Middleware.nuspec -OutputDirectory ../../../output