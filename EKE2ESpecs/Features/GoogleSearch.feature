Feature: GoogleSearch
	In order finish
	As student of universe
	I want use google search to completing my assignment 

@mytag
Scenario: Search for nothing on google
	Given I am on google search
	And I search for "universal solution"
	When I search
	Then I should see the solution of universal problem
