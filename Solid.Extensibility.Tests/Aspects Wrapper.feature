Feature: Aspects Wrapper
	In order to write predictable apps
	As an app developer
	I want to be sure the aspects are invoked according to their dependencies

Scenario: Initializing aspects with basic dependency should respect the dependencies
	Given The aspect is created with Id 'Basic' and Dependencies ''
	And The aspect is created with Id 'Dependent' and Dependencies 'Basic'
	When The aspects wrapper is created with the aspects and initialized
	Then the aspects should be invoked in the following order 'Basic;Dependent'

Scenario: Initializing aspects with dependencies at the same level should respect the dependencies
	Given The aspect is created with Id 'Basic' and Dependencies ''
	And The aspect is created with Id 'DependentB' and Dependencies 'Basic'
	And The aspect is created with Id 'DependentA' and Dependencies 'Basic'
	When The aspects wrapper is created with the aspects and initialized
	Then the aspects should be invoked in the following order 'Basic;DependentB;DependentA'

Scenario: Initializing aspects with chained dependencies should respect the dependencies
	Given The aspect is created with Id 'Platform' and Dependencies ''
	And The aspect is created with Id 'Discovery' and Dependencies 'Platform'
	And The aspect is created with Id 'Modularity' and Dependencies 'Platform;Discovery'
	When The aspects wrapper is created with the aspects and initialized
	Then the aspects should be invoked in the following order 'Platform;Discovery;Modularity'

Scenario: Initializing aspects with missing dependency should result in error with correspondent message
	Given The aspect is created with Id 'Basic' and Dependencies 'Missing'
	When The aspects wrapper is created with the aspects and initialized
	Then There should be an error with the following message 'Missing dependency Missing'

Scenario: Initializing aspects with cyclic dependency should result in error with correspondent message
	Given The aspect is created with Id 'Basic' and Dependencies 'Missing'
	Given The aspect is created with Id 'Missing' and Dependencies 'Basic'
	When The aspects wrapper is created with the aspects and initialized
	Then There should be an error with the following message 'Cyclic dependency found.'

Scenario: Initializing aspects with same id should result in error with correspondent message
	Given The aspect is created with Id 'Basic' and Dependencies ''
	Given The aspect is created with Id 'Basic' and Dependencies ''
	When The aspects wrapper is created with the aspects and initialized
	Then There should be an error with the following message 'Id must be unique - Basic'