Feature: Language

Verify Languages functionality

Background:
	Given User at Profile Page

@regression @language
Scenario: 1 Verify Choose Language Level has 4 options
	And User click Language Tab
	When Add New button is pressed
	Then Choose Language dropdown has four level options

@regression @language @removeLanguageTearDown
Scenario Outline: 2 Verify user is able to add languages
	And User click Language Tab
	When User adds language: <language> <level>
	Then User is able to see Language details: <language> <level>
	
	Examples:
	| language												| level            |
	| a														| Basic			   |
	| Specialchar !@#$%^&*()_+{}:">?						| Conversational   |
	| LongName 50chars 890123456789012345678901234567890	| Fluent           |
	| 中国人													| Native/Bilingual |


#Scenario: Verify user is able to add languages
#	And User click Language Tab
#	And There are 3 or less languages added
#	Then User is able to Add Language

@regression @language @removeLanguageTearDown
Scenario: 3 Verify user is able to update language
	And User click Language Tab
	And One language is present
	Then User is able to Update Language

@regression @language 
Scenario: 4 Verify user is able to delete language
	And User click Language Tab
	And One language is present
	Then User is able to Delete Language

@regression @language 
Scenario: 5 Verify user is able to add only 4 languages
	And User click Language Tab
	When There are four languages
	Then User is unable to add more language