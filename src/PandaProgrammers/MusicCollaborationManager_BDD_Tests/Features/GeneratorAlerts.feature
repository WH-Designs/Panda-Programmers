@Jasper
Feature: Generator Error Alert Handling
**As a listener I would like to be informed when there is an error in using the playlist generators**

This feature is to keep the user informed of what is happening. Before this was added, if there was an exception, the user would
have been sent to the dashboard without being notified. An alert allows the user to be knowledgeable of what is happening and how to fix it.


Background:
	Given the following users exist generator
		| UserName        | Email           | FirstName | LastName | Password |
		| chadb@gmail.com | chadb@gmail.com | Chad      | Bass     | Pass321! |

Scenario: Logged in user generating a playlist that fails returns to the dashboard 
	Given I am a logged in user with first name '<FirstName>'
	    And I am on the '<Page>' page
    When I click on the generate playlist button
	Then I am redirected to the '<RedirectPage>' page
    Examples:
    | FirstName | Page           | RedirectPage |
    | Chad      | GeneratorIndex | Dashboard |