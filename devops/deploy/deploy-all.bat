rem TODO: Use common source for all version instances
SET version=2.3.6-rc1
rem TODO: Refactor using loop and automatic discovery
call deploy-single.bat Solid.Bootstrapping %version% 
call deploy-single.bat Solid.Common %version% 
call deploy-single.bat Solid.Common.Core %version% 
call deploy-single.bat Solid.Core %version% 
call deploy-single.bat Solid.Extensibility %version% 
call deploy-single.bat Solid.IoC.Adapters.BoDi %version%
call deploy-single.bat Solid.IoC.Registration %version%  
call deploy-single.bat Solid.Patterns.Builder %version% 
call deploy-single.bat Solid.Patterns.ChainOfResponsibility %version% 
call deploy-single.bat Solid.Patterns.Memento %version% 
call deploy-single.bat Solid.Patterns.Visitor %version% 
call deploy-single.bat Solid.Practices.Composition.Client %version% 
call deploy-single.bat Solid.Practices.Composition.Core %version% 
call deploy-single.bat Solid.Practices.Composition.Web %version% 
call deploy-single.bat Solid.Practices.IoC %version% 
call deploy-single.bat Solid.Practices.Middleware %version% 
call deploy-single.bat Solid.Practices.Modularity %version% 
call deploy-single.bat Solid.Practices.Scheduling %version% 