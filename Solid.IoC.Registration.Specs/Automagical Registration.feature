Feature: Automagical Registration
	In order to simplify IoC implementation in my apps
	As an app developer
	I want to be able to register dependencies automagically

Scenario: Registering dependencies automagically by ending should allow successful resolution
	Given There are valid implementations for all declared dependencies
	When I use registration by ending
	Then All dependencies can be resolved successfully