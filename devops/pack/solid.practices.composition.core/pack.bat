cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../src/Bin/netstandard/Release netstandard2.0 Solid.Practices.Composition.dll Solid.Practices.Composition.xml Solid.Practices.Composition.pdb Solid.Practices.Composition.deps.json /E
robocopy ../../../../../src/Bin/netstandard/Release netstandard2.0 Solid.Practices.Composition.Contracts.* /E
robocopy ../../../../../src/Bin/netstandard/Release netstandard2.0 Solid.Practices.Composition.Container.* /E

cd ../../
nuget pack contents/solid.practices.composition.core.nuspec -OutputDirectory ../../../output