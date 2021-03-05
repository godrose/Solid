# Solid

The **Solid** library addresses different aspects in applications development.
The most prominent are: **Inversion-Of-Control**, **Modularity**, **Extensibility** via **Middleware** and **Composition**.

The **Inversion-Of-Control** aspect is addressed by defining an abstraction of an IoC container which can be implemented using any existing or even new container. It's important to stress that the **user is not limited to this abstract API**. The abstraction is created to support different bootstrapping and modular functionality. 

The **Modularity** aspect addresses the cases where there's need to add functionality during app initialization without having to include it into the initialization root thus preserving the encapsulation. There are various sorts of functionality that can be added using this approach: registering dependencies into the IoC container, initializing third-party engines, etc.

The **Extensibility** aspect is addressed by defining an abstraction for reusable piece of functionality that is attached to a certain object.This abstraction is represented by the following interface ```IMiddleware<T>``` which defined the ```Apply``` method in a fluent fashion.

The **Composition** aspect is the only one that has concrete implementation. In essense, it provides a unified way of composing application blocks, including assemblies and composition modules, for further consumption.

Additionally, the **Solid** library contains various interfaces and implementation for some of the Design Patterns, including ```IAcceptor``` for the **Visitor** pattern, ```IMemento<T>``` for the **Memento** pattern and so on. This saves a lot of duplicate code when such a pattern is used in the app development.

<img src=https://ci.appveyor.com/api/projects/status/github/godrose/solid>


## Inversion of Control

<img src=https://img.shields.io/nuget/dt/Solid.Practices.IoC>

[Living Documentation](https://ci.appveyor.com/api/projects/godrose/Solid/artifacts/Solid.IoC.Adapters.BoDi.Specs/bin/Release/LivingDoc.html)

[Get package](https://www.nuget.org/packages/Solid.Practices.IoC/)

## Middleware

<img src=https://img.shields.io/nuget/dt/Solid.Practices.Middleware?stylefor-the-badge>

[Living Documentation](https://ci.appveyor.com/api/projects/godrose/Solid/artifacts/Solid.Practices.Middleware.Specs/bin/Release/LivingDoc.html)

[Get package](https://www.nuget.org/packages/Solid.Practices.Middleware/)

## Extensibility

<img src=https://img.shields.io/nuget/dt/Solid.Extensibility?stylefor-the-badge>

[Living Documentation](https://ci.appveyor.com/api/projects/godrose/Solid/artifacts/Solid.Extensibility.Specs/bin/Release/LivingDoc.html)

[Get package](https://www.nuget.org/packages/Solid.Extensibility/)

## Composition

<img src=https://img.shields.io/nuget/dt/Solid.Practices.Composition.Core?stylefor-the-badge>

[Living Documentation](https://ci.appveyor.com/api/projects/godrose/Solid/artifacts/Solid.Practices.Composition.Specs/bin/Release/LivingDoc.html)

[Get package](https://www.nuget.org/packages/Solid.Practices.Composition.Core/)

## Bootstrapping

<img src=https://img.shields.io/nuget/dt/Solid.Bootstrapping?stylefor-the-badge>

[Living Documentation](https://ci.appveyor.com/api/projects/godrose/Solid/artifacts/Solid.Bootstrapping.Specs/bin/Release/LivingDoc.html)

[Get package](https://www.nuget.org/packages/Solid.Bootstrapping/)
