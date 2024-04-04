Feature: Language

Verify Languages functionality

Background:
	Given User at Profile Page

@regression
Scenario: Verify Choose Language Level has 4 options
	And User click Language Tab
	When Add New button is pressed
	Then Choose Language dropdown has four level options

Scenario: Verify user is able to add languages
	And User click Language Tab
	And There are 3 or less languages added
	Then User is able to Add Language

Scenario: Verify user is able to update language
	And User click Language Tab
	Then User is able to Update Language

Scenario: Verify user is able to delete language
	And User click Language Tab
	And Atleast one language present
	Then User is able to Delete Language

Scenario: Verify user is able to add only 4 languages
	And User click Language Tab
	When There are four languages
	Then User is unable to add more language