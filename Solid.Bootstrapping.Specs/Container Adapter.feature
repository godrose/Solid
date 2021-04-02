Feature: Container Adapter
	In order to write modular apps
	As an app developer
	I want to be able to register dependencies into the dedicated container adapter

Scenario: Register transient dependency using composition module
	When The new container adapter is created
	And The composition modules are registered for the container adapter
	Then The registered dependency should be of correct type
	And The registered dependency should be transient
