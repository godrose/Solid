Feature: Object Container Adapter
	In order to write modular apps
	As an app developer
	I want to be able to register dependencies into the dedicated container

Scenario: Registering collection of services should register implementations
	When The new container is created
	And The services collection is registered
	Then The services collection should be resolved by implementations

