Feature: Middleware Types Wrapper
	As an app developer
	I want to be able to use middlewares by specifying their types only
	In order to support more advanced scenarios during bootstrapping

Scenario: Using creatable middleware by type should raise no issues during middleware creation
	When The middleware types wrapper is created
	And I use a creatable middleware by specifying its type only
	And I ensure the middlewares are created
	Then This middleware is created successfully