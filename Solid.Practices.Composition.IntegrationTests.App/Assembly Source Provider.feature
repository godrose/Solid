Feature: Assembly Source Provider
	In order to build a modular app
	As an app developer
	I want to be able to dynamically load app components

Scenario: Load assemblies using default assembly loading strategy should create matching types
	Given I run in .NETStandard environment
	And The assemblies loader used default assembly loading strategy
	When The assemblies provider loads the assemblies in the current folder
	Then The loaded implementation type implements the loaded contract type

@Ignore
#This test fails on purpose to demonstrate dynamic loading issue thus it's ignored for now
Scenario: Load assemblies using custom assembly loading strategy should create matching types
	Given I run in .NETStandard environment
	And The assemblies loader used custom assembly loading strategy
	When The assemblies provider loads the assemblies in the current folder
	Then The loaded implementation type implements the loaded contract type
