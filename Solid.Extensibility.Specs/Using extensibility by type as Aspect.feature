Feature: Using extensibility by type as Aspect
	As an app developer
	I would like to include extensibility by type functinality into the app
	In order to support complex bootstrapping scenarios

Scenario: Initializing the aspect should execute all used middlewares
	Given The creatable middleware can be created
	When The extensibility by type aspect is created
	And The creatable middleware is used by the aspect
	And The extensibility by type aspect is initialized
	Then The creatable middleware is executed

Scenario: Creating the aspect without initialization should not execute any of used middlewares
	Given The creatable middleware can be created
	When The extensibility by type aspect is created
	And The creatable middleware is used by the aspect	
	Then The creatable middleware is not executed
