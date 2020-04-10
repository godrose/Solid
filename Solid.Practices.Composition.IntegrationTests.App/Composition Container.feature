Feature: Composition Container
	In order to build a modular app
	As an app developer
	I want to be able to dynamically compose loaded app components

Scenario: Compose modules using custom assebly loading strategy should create matching types
	Given I run in .NETStandard environment
	And The assemblies loader used custom assembly loading strategy
	When The composition container is created in the current folder
	And The composition container is composed
	And The single composition module is registered
	Then The loaded placeholder type is OK
