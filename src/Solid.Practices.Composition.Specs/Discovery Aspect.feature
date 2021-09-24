Feature: Discovery Aspect
	In order to build a modular app
	As an app developer
	I want to have ability to discover app components

Scenario: Using discovery aspect should discover required assemblies
	Given I run in .NETStandard environment
	And The assemblies loader used default assembly loading strategy
	When The discovery aspect is created
	And The discovery aspect is initialized
	Then There should be 14 assemblies
