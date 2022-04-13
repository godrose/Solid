Feature: Using extensibility by type
	As an app developer
	I would like to include extensibility by type functinality into the app
	In order to support complex bootstrapping scenarios

Scenario: Using the extensibility by type aspect should allow using middlewares by type
	Given The creatable middleware can be created
	When The new bootstrapper with support for extensibility by type is created
	And The extensibility by type aspect is used
	And The creatable middleware is used by type
	And The bootstrapper is initialized
	Then The creatable middleware is created