@Michael
Feature: Preset Themes

**As a listener I want to be able to see pre-made themes so I can customize the website to my preferences**

This story is about being able to use pre-made themes. We want this to be editable and displayable 
without page loads.


Background:
	Given the following users exist settings
	  | UserName			 | Email                 | FirstName | LastName | Password     |
	  | chadb@gmail.com	     | chadb@gmail.com       | Chad      | Bass     | Pass321!     |

Scenario Outline: Logged in user can reach settings page
	Given I am a user logged in with the username '<FirstName>'
	When I click on the settings button in the navbar
	Then I should be taken to the '<Page>' page
	Examples:
	| FirstName			| Page	|
	| Chad				| Settings |