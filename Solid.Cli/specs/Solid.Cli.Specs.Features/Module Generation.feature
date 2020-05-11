Feature: Module Generation
	In order to provide tools to developers who want to develop modular Solid-based apps
	As a framework developer
	I want to be able to install and use the correspondent template via existing dotnet means

Scenario: Installing template pack should install all correspondent templates
	When I install the template pack 'Solid' from local package
	Then The template for 'solid-module' is installed with the following parameters
	| Description              | Short Name   | Languages | Tags             |
	| Solid Composition Module | solid-module | [C#]      | Solid/Modularity |
