Feature: Middleware
	In order to write robust apps
	As an app developer
	I want to be able to extend app functionality by applying middlewares

Scenario: Applying modules registration middleware should register the correspondent dependencies
	When The new container adapter is created
	And The new bootstrapper with composition modules is created
	And The composition modules middleware is applied onto the bootstrapper
	Then The registered dependency should be of correct type
	And The registered dependency should be transient

Scenario: Applying collection registration middleware should register the correspondent dependencies as collection
	When The new container adapter is created
	And The new bootstrapper with current assembly is created
	And The collection registration middleware is applied onto the bootstrapper
	Then The dependencies are registered as a collection
