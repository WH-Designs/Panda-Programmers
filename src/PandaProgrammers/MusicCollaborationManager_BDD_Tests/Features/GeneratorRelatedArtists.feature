@Wyatt
Feature: Generator Related Artists

Background:
	Given the following users exist generator
	  | UserName			 | Email                 | FirstName | LastName | Password     |
	  | chadb@gmail.com	     | chadb@gmail.com       | Chad      | Bass     | Pass321!     |

Scenario: Logged in user can navigate to the related arist generator from the generator index page
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I select the related artist page
	Then I should be redirected to the related artists page
	Examples:
	| FirstName | Page           |
	| Chad      | GeneratorIndex |

Scenario: Logged in user that is on the related artist generator page should see a button
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I go to the related artist playlist page
	Then I should see a select artist option
	Examples:
	| FirstName | Page |
	| Chad      | GeneratorIndex |

Scenario: Logged in user that clicks on the generate button should have a playlist generated for them
	Given I am a logged in user with first name '<FirstName>'
		And I am a listener on the related artist playlist page
	When I select generate the playlist 
	Then I should get a playlist that has similar tracks to my selected artists
	Examples:
	| FirstName |
	| Chad      |