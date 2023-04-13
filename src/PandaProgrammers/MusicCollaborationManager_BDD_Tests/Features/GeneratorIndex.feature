@Blake
Feature: Generator Index
**As a registered user I would like to be able to go to a generator landing page**

This feature allows the user to have access to a button that will take the user to each of the generators. On top
of this there is a short description for each of the generators. This page can be accessed via the navbar. 

Background:
	Given the following users exist generator
	  | UserName			 | Email                 | FirstName | LastName | Password     |
	  | chadb@gmail.com	     | chadb@gmail.com       | Chad      | Bass     | Pass321!     |
	And the following generator titles exist
	  | Title                   | titleKey |
	  | Questionnaire Generator | Q   |
	And the following generartor descriptions exist
	  | descriptionKey | Description                                                                                                                                                                                                                                                            |
	  | Q   | With this generator you will be able to fill out a short questionnaire about songs and then get back a playlist of songs that fit your desired input. If you like the playlist that is returned, then you will have the option to save it to your own Spotify account! |


Scenario Outline: Logged in user can navigate to generator landing page
	Given I am a logged in user with first name '<FirstName>'
	When I click the generator button in the navbar
	Then I should be redirected to the '<Page>' page
	Examples:
	| FirstName | Page |
	| Chad      | GeneratorIndex |

Scenario Outline: Logged in user can see a description of a generator
	Given I am a logged in user with first name '<FirstName>'
	When I am on the '<Page>' page
	Then I should see a '<descriptionKey>' description of a generator
	Examples:
	| FirstName | Page           | descriptionKey |
	| Chad      | GeneratorIndex | Q   |

Scenario Outline: Logged in user can navigate to a generator using one of buttons
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I click on one of the generator buttons
	Then I should be navigated to that generators '<GeneratorPage>' page
	Examples:
	| FirstName | Page           | GeneratorPage |
	| Chad      | GeneratorIndex | QGenerator    |

Scenario Outline: Logged in user can see a header that contains the title of a playlist generator
	Given I am a logged in user with first name '<FirstName>'
	When I am on the '<Page>' page
	Then I should see a '<titleKey>' title of a generator
	Examples:
	| FirstName | Page           | titleKey |
	| Chad      | GeneratorIndex | Q   |




