﻿Feature: Language

Verify Languages functionality

Background:
	Given User at Profile Page

@tag1
Scenario: Verify Choose Language Level has 4 options
	And User click Language Tab
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