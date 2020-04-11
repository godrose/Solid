Feature: Middleware Dependencies
	In order to write robust apps
	As an app developer
	I want to make sure the middlewwares are applied in correct order

Scenario: Applying middlewares with dependencies should respect the correspondent dependencies
	Given There are middlewares with internal dependencies only
	When The middlewares are applied
	Then The result should be 'CBA'

Scenario: Applying middlewares with mixed dependencies including explicit should respect the correspondent dependencies
	Given There are middlewares with dependencies and without dependencies including explicit
	When The middlewares are applied
	Then The result should be 'CBAIndExpl'

Scenario: Applying middlewares with mixed dependencies including implicit should respect the correspondent dependencies
	Given There are middlewares with dependencies and without dependencies including implicit
	When The middlewares are applied
	Then The result should be 'CBAIndImpl'

Scenario: Applying middlewares with dependencies specified by attibrutes should respect the correspondent dependencies
	Given There are middlewares with internal dependencies only specified by attributes
	When The middlewares are applied
	Then The result should be 'CBA'