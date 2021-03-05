call test-specs-single.bat Solid.Bootstrapping.Specs
call test-specs-single.bat Solid.Extensibility.Specs
call test-tests-single.bat Solid.Core.Tests
call test-specs-single.bat Solid.IoC.Adapters.BoDi.Specs
call test-specs-single.bat Solid.Practices.Composition.Container.Specs
call test-specs-single.bat Solid.Practices.Composition.Specs
call test-specs-single.bat Solid.Practices.Middleware.Specs
rem TODO: Provide more generic and reusable way
call test-tests-single.bat Solid.Practices.Composition.IntegrationTests.App
cd ../../tests/bin/composition
livingdoc test-assembly Solid.Practices.Composition.IntegrationTests.App.dll -t TestExecution.json --work-item-url-template https://github.com/godrose/Solid/issues/{id} --work-item-prefix WI
cd ../../../devops/test

