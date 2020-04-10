Feature: Composition Container
	In order to build a modular app
	As an app developer
	I want to be able to dynamically compose loaded app components

Scenario: Composing composition modules should compose all matching modules
	Given I run in .NETStandard environment
	And The assemblies loader used default assembly loading strategy
	When The composition container for composition modules is created in the current folder
	| Prefixes |
	|          |
	And The composition container is composed
	Then There should be 4 composition modules

Scenario: Composing custom modules should compose all matching modules
	Given I run in .NETStandard environment
	And The assemblies loader used default assembly loading strategy
	When The composition container for custom modules is created in the current folder
	| Prefixes |
	|          |
	And The composition container is composed
	Then There should be 2 custom modules

Scenario: Composing custom modules with explicit correct prefix should compose all matching modules
	Given I run in .NETStandard environment
	And The assemblies loader used default assembly loading strategy
	When The composition container for custom modules is created in the current folder
	| Prefixes |
	| Solid    |
	And The composition container is composed
	Then There should be 2 custom modules

Scenario: Composing custom modules with explicit incorrect prefix should compose no modules
	Given I run in .NETStandard environment
	And The assemblies loader used default assembly loading strategy
	When The composition container for custom modules is created in the current folder
	| Prefixes  |
	| Incorrect |
	And The composition container is composed
	Then There should be 0 custom modules

Scenario: Composing other modules should compose all matching modules
	Given I run in .NETStandard environment
	And The assemblies loader used default assembly loading strategy
	When The composition container for other modules is created in the current folder
	| Prefixes |
	|          |
	And The composition container is composed
	Then There should be 1 other modules

