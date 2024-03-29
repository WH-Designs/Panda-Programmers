﻿@Blake
Feature: AI Title
**As a listener I want to have an option to have a generated playlist named for me so that I don't have to think of a name for it**

Listeners should have the option to select if they want to have a name automatically generated by and AI for their playlist. This option 
will occur on the playlist generator pages. This option will occur on all pages except the track input generator as that generator uses
an AI generated title automatically without the user specifying so. 

Background:
	Given the following users exist generator
	  | UserName			 | Email                 | FirstName | LastName | Password     |
	  | chadb@gmail.com	     | chadb@gmail.com       | Chad      | Bass     | Pass321!     |

Scenario: Logged in user can see AI title switch on a generator page
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I go to a playlist page
	Then I should see an option to switch to an AI generated title
	Examples:
	| FirstName | Page |
	| Chad      | GeneratorIndex |

Scenario: Logged in user on a generator page can make manual input hide and then reappear by selecting and unselecting AI option
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I go to a playlist page
		And when I select and unselect the AI option switch
	Then I the manual title entry should not be hidden
	Examples:
	| FirstName | Page |
	| Chad      | GeneratorIndex |

Scenario: Logged in user on a generator page can make switch AI option to on and see results on generator output page
	Given I am a logged in user with first name '<FirstName>'
		And I am logged into Spotify
		And I am on the '<Page>' page
	When I go to a playlist page
		And when I select the on AI switch option
		And I select generate playlist 
	Then I should see a generated title on the generator output page
	Examples:
	| FirstName | Page |
	| Chad      | GeneratorIndex |


Scenario: Logged in user on the track input generator should have AI title auto created without any manual activation except for running the generator itself
	Given I am a logged in user with first name '<FirstName>'
		And I am logged into Spotify
		And I am on the '<Page>' page
	When I go to the track input generator page
		And I select generate playlist without selecting an AI title switch
	Then I should see a generated title on the generator output page
	Examples:
	| FirstName | Page |
	| Chad      | GeneratorIndex |