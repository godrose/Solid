Feature: Registration Method Context
	In order to simplify IoC implementation in my apps
	As an app developer
	I want to be able to manage default registration method per specific IoC container

#TODO: Think about running in parallel
@cleanRegistrationMethodContext
Scenario: Setting default registration method for the specific IoC container should be successful
	When I use '<container name>' with default registration method
	Then The default registration method for '<container name>' is set
	And There are no errors
Examples:
	| container name            |
	| specflow object container |
	| microsoft di container    |

@cleanRegistrationMethodContext
Scenario: Getting default registration method for the specific IoC container after setting it should be successful
	When I use '<container name>' with default registration method
	Then The default registration method for '<container name>' is set
	And There are no errors
Examples:
	| container name            |
	| specflow object container |
	| microsoft di container    |

@cleanRegistrationMethodContext
Scenario: Setting default registration method for the specific IoC container more than once should be successful
	When I use '<container name>' with default registration method
	And I set default registration method for '<container name>'
	Then The default registration method for '<container name>' is overridden 
	And There are no errors
Examples:
	| container name            |
	| specflow object container |
	| microsoft di container    |

@cleanRegistrationMethodContext
Scenario Outline: Getting default registration method for the specific IoC container without setting it should result in error
	When I use '<container name>'
	And I get default registration method for '<container name>'
	Then The correspondent error with details for get default registration method for '<container name>' is thrown
Examples:
	| container name            |
	| specflow object container |
	| microsoft di container    |