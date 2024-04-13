Feature: Skills

Verify Skills functionality

Background:
	Given User at Profile Page

@regression
Scenario: Verify Choose Skill Level has 4 options
	And User click Skills Tab
	Then Choose Skill dropdown has three level options

@regression
Scenario: Verify user is able to add skill
	And User click Skills Tab
	Then User is able to Add Skill

@regression
Scenario: Verify user is able to update skill
	And User click Skills Tab
	Then User is able to Update Skill

@regression
Scenario: Verify user is able to delete skill
	And User click Skills Tab
	And Atleast one skill present
	Then User is able to Delete Skill
