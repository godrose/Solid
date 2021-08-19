Feature: Automagical Registration
	In order to simplify IoC implementation in my apps
	As an app developer
	I want to be able to register dependencies automagically

Scenario: Registering dependencies automagically by ending should allow successful resolution
	Given There are valid implementations for all declared dependencies
	When I use object container
	When I use registration by ending
	Then All dependencies can be resolved successfully

Scenario: Registering dependencies automagically by contract which is an interface should allow successful resolution
	Given There are valid implementations for all declared dependencies
	When I use object container
	When I use registration by contract which is an interface
	Then All dependencies can be resolved successfully

Scenario: Registering dependencies automagically by contract which is a class should allow successful resolution
	Given There are valid implementations for all declared dependencies
	When I use object container
	When I use registration by contract which is a class
	Then All dependencies can be resolved successfully