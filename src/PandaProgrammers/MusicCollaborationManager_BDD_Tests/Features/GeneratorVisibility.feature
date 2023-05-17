@Michael
Feature: Generator Visibility


**As a listener I want to be able to choose if a generated playlist is public or not**

A listener will be able to select if they want a generated playlist to be public or private. 
This will then determine if user’s newly generated playlist is public or private upon creation.

Background:
	Given the following users exist generator
	  | UserName			 | Email                 | FirstName | LastName | Password     |
	  | chadb@gmail.com	     | chadb@gmail.com       | Chad      | Bass     | Pass321!     |

Scenario: Generator visibility setting is visible
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I go to a playlist page
	Then I should see a playlist visibility setting
	Examples:
	| Page           | FirstName |
	| GeneratorIndex | Chad      |
