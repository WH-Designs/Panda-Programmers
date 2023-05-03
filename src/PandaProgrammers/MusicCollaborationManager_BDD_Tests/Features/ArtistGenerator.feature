@Blake
Feature: Artist Generator
**As a registered user I would like to be able to go to have a playlist generated based on my top artists**

This feature allows the user to have access to generate a playlist based on their top listened to artists. This 
generator should be accessible from the generator index page and should require no input from the user as it gets
their top artists automatically.

Background:
	Given the following users exist generator
	  | UserName			 | Email                 | FirstName | LastName | Password     |
	  | chadb@gmail.com	     | chadb@gmail.com       | Chad      | Bass     | Pass321!     |

Scenario: Logged in user can navigate to the arist generator from the generator index page
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I select the top track artist page
	Then I should be redirected to the '<GenPage>' page
	Examples:
	| FirstName | Page           | GenPage   |
	| Chad      | GeneratorIndex | TopArtist |

Scenario: Logged in user that is one the artist generator page should see a button
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I go to the top artist playlist page
	Then I should be able to select a button to generate tracks automatically with no input
	Examples:
	| FirstName | Page |
	| Chad      | GeneratorIndex |

Scenario: Logged in user that clicks on the generate button should have a playlist generated for them
	Given I am a logged in user with first name '<FirstName>'
		And I am a listener on the top artist playlist page
	When I select generate playlist 
	Then I should get a playlist that has similar tracks to my top artists
	Examples:
	| FirstName |
	| Chad      |