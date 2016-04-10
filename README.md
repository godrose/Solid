# Solid

The Solid library addresses different aspects in applications development.
The most prominent are: Inversion-Of-Control, Modularity, Extensibility via Middleware and Composition.

The Inversion-Of-Control aspect is addressed by defining an abstraction of an IoC container which can be implemented using any existing or even new container. It's important to stress that the **user is not limited to this abstract API**. The abstraction is created to support different bootstrapping and modular functionality. 

The Modularity aspect addresses the cases where there's need to add functionality during app initialization without having to include it into the initialization root thus preserving the encapsulation. There are various sorts of functionality that can be added using this approach: registering dependencies into the IoC container, initializing third-party engines, etc.

The Extensibility aspect is addressed by defining an abstraction for reusable piece of functionality that is attached to a certain object.This abstraction is represented by the following interface ```IMiddleware<T>``` which defined the ```Apply``` method in a fluent fashion.

The Composition aspect is the only one that has concrete implementation. In essense, it provides a unified way of composing application blocks, including assemblies and composition modules, for further consumption.

Additionally, the Solid library contains various interfaces and implementation for some of the Design Patterns, including ```IAcceptor``` for the **Visitor** pattern, ```IMemento<T>``` for the **Memento** pattern and so on. This saves a lot of duplicate code when such a pattern is used in the app development.

