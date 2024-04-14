Feature: Skills

Verify Skills functionality

Background:
	Given User at Profile Page

@regression @skill
Scenario: 1 Verify Choose Skill Level has 3 options
	And User click Skills Tab
	Then Choose Skill dropdown has three level options

@regression @skill @removeSkillTearDown
Scenario Outline: 2 Verify user is able to add skill
	And User click Skills Tab
	When User adds skill: <skill> <level>
	Then User is able to see skill details: <skill> <level>
	
	Examples:
	| skill													| level          |
	| a														| Beginner		 |
	| Specialchar !@#$%^&*()_+{}:">?						| Intermediate   |
	| LongName 50chars 890123456789012345678901234567890	| Expert         |
	| スキル													| Beginner		 |

#Scenario: Verify user is able to add skill
#	And User click Skills Tab
#	Then User is able to Add Skill

@regression @skill @removeSkillTearDown
Scenario: 3 Verify user is able to update skill
	And User click Skills Tab
	And One skill is present
	Then User is able to Update Skill

@regression @skill
Scenario: 4 Verify user is able to delete skill
	And User click Skills Tab
	And One skill is present
	Then User is able to Delete Skill

Scenario: 5 Verify user is able to add 10 skills
	And User click Skills Tab
	Then User is able to add 10 skills