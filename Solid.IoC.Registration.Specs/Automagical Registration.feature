Feature: Automagical Registration
	In order to simplify IoC implementation in my apps
	As an app developer
	I want to be able to register dependencies automagically

Scenario Outline: Registering dependencies automagically by ending should allow successful resolution
	Given There are valid implementations for all declared dependencies
	When I use '<container name>'
	When I use registration by ending
	Then All dependencies by ending can be resolved successfully
Examples:
	| container name            |
	| specflow object container |
	| microsoft di container    |

Scenario Outline: Registering dependencies automagically by contract which is an interface should allow successful resolution
	Given There are valid implementations for all declared dependencies
	When I use '<container name>'
	When I use registration by contract which is an interface
	Then All dependencies by contract which is an interface can be resolved successfully
Examples:
	| container name            |
	| specflow object container |
	| microsoft di container    |

Scenario Outline: Registering dependencies automagically by contract which is a class should allow successful resolution
	Given There are valid implementations for all declared dependencies
	When I use '<container name>'
	When I use registration by contract which is a class
	Then All dependencies by contract which is a class can be resolved successfully
Examples:
	| container name            |
	| specflow object container |
	| microsoft di container    |

Scenario Outline: Registering all dependencies automagically with implicit default registration method should allow successful resolution for all dependencies
	Given There are valid implementations for all declared dependencies	
	When I use '<container name>' with default registration method
	When I use automagical registration
	Then All dependencies can be resolved successfully
Examples:
	| container name            |
	| specflow object container |
	| microsoft di container    |