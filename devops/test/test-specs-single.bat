rem TODO: Provide more generic and reusable way
cd ../../src/%1
dotnet test %1.csproj -c Release
rem TODO: Provide more generic and reusable way
cd ./Bin/Release
livingdoc test-assembly %1.dll -t TestExecution.json --work-item-url-template https://github.com/godrose/Solid/issues/{id} --work-item-prefix WI
xcopy LivingDoc.html "../../../../output/%1.LivingDoc.html*" /Y
cd ../../../../devops/test