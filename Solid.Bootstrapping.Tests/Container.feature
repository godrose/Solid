Feature: Container
	In order to write modular apps
	As an app developer
	I want to be able to register dependencies into the dedicated container

Scenario: Register transient dependency using composition module
	When The new container is created
	And The composition modules are registered for the container
	Then The registered dependency should be of correct type
	And The registered dependency should be transient
