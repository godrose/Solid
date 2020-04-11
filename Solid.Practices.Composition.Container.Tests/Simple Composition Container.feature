Feature: Simple Composition Container
	In order to build a modular app
	As an app developer
	I want to be able to dynamically compose loaded app components

Scenario: Composing a module which throws exception should bubble it until it's handled
	Given That any type loading from an assembly would throw an exception
	When The composition container is initialized with the current folder
	And The composition container is composed
	Then The exception should be of correct type and have the following message 'Unable to load defined types'

Scenario: Composing two modules when one of them throws an exception should register the error info
	Given There are two types in two modules and loading the first one is Ok and loading of the second throws an exception
	When The composition container is initialized with the current folder
	And The composition container is composed
	Then The exception should be of correct type and have the following message 'Unable to create composition modules'
	And The exception should contain info for the second type with message 'Unable to create module for the specified type'

