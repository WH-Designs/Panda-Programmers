﻿@Michael
Feature: Preset Themes

**As a listener I want to be able to see pre-made themes so I can customize the website to my preferences**

This story is about being able to use pre-made themes. We want this to be editable and displayable 
without page loads.


Background:
	Given the following users exist settings
	  | UserName           | Email              | FirstName | LastName | Password |
	  | chadb@gmail.com    | chadb@gmail.com    | Chad      | Bass     | Pass321! |
	  | tiffanyf@gmail.com | tiffanyf@gmail.com | Tiffany   | Fox      | Pass321! |
	  | dwightm@gmail.com  | dwightm@gmail.com  | Dwight    | Morse    | Pass321! |

Scenario: Logged in user can reach settings page
	Given I am a logged in user
	When I click on the settings button in the navbar
	Then I should be taken to the settings page

Scenario Outline: Logged in user can see first and last name
	Given I am a logged in user with first name '<FirstName>' and last name '<LastName>'
	When I click on the settings button in the navbar
	Then I should be taken to the settings page
	And I should see my first name '<FirstName>' and last name `'<LastName>'
	Examples:
	| FirstName | LastName |
	| Chad      | Bass     |
	| Tiffany   | Fox      |
	| Dwight    | Morse    |


Scenario: Logged in user can see section of page to change site theme
	Given I am a listener
	When I am on my profile page
	Then I can see the section with options to change my theme to different pre-made themes

Scenario: Listener can toggle different themes
	Given I am a listener on my settings page
	When I click on one of the different pre-made theme buttons
	Then the website responds accordingly and adjusts my theme without a page reload