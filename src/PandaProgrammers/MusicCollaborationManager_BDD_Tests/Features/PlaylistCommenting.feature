@Wyatt
Feature: Playlist Commenting

Background:
	Given the following users exist
	  | UserName			 | Email                 | FirstName | LastName | Password     |
	  | chadb@gmail.com	     | chadb@gmail.com       | Chad      | Bass     | Pass321!     |
	  | admin@example.com    | admin@example.com     | The       | Admin    | EbAE6bbr6p!3 |
	  | tiffanyf@gmail.com   | tiffanyf@gmail.com    | Tiffany   | Fox	    | Pass321!     |

Scenario: A User can see the commenting form on the playlist page
	Given I am a user with first name 'Chad'
	When I load previously saved cookies
		And I am on the playlist page
	Then I can see the comment form on the page

Scenario Outline: A User can input text into the form
	Given I am a user with first name 'Chad'
	When I load previously saved cookies
		And I input '<text>' into the form
		And I click the submit button
	Then I should see '<text>' on the page
	Examples:
	| text            |
	| Hello World     |
	| My name is Chad |

Scenario: A User can see an error message when clicking the submit button without inputing any text
	Given I am a user with first name 'Chad'
	When I load previously saved cookies
		And I click the submit button
	Then I should see submit error message

