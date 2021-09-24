Feature: Middleware Types
	As an app developer
	I want to be able to use middlewares by specifying their types only
	In order to support more advanced scenarios during bootstrapping

Scenario: Using creatable middleware by type should succeed
	Given The creatable middleware can be created
	When The middleware types wrapper is created
	And I use a creatable middleware by specifying its type only
	And I ensure the middlewares are created
	Then This middleware is created successfully

Scenario: Using non-creatable middleware by type should fail
	Given The creatable middleware can not be created
	When The middleware types wrapper is created
	And I use a creatable middleware by specifying its type only
	And I ensure the middlewares are created
	Then This middleware fails to create