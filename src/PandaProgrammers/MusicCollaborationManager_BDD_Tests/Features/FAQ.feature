@Blake
Feature: FAQ
**As a listener with little knowledge of playlist generators, I'd like a better understanding of them before I use them so I'll know how to use them effectively**

A listener will be able to go to an FAQ page to learn more about how to use the generators and how they work. After observing users during alpha testing, it became 
clear that there may be some confusion on how to use the generators properly. With an FAQ page it would be helpful to let the users know how to user the generators 
correctly and give them information on what to expect from each generator. On top of that, some users may also wish to know how the generators work, which will also 
be detailed here. This description of how they work will be written in such a way that a listener does not need to have CS knowledge to understand how they work on 
a high level.

Background:
	Given the following users exist generator
	  | UserName			 | Email                 | FirstName | LastName | Password     |
	  | chadb@gmail.com	     | chadb@gmail.com       | Chad      | Bass     | Pass321!     |

Scenario: Logged in user can navigate to the FAQ from the generator index page
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I select the FAQ page
	Then I should be redirected to the '<GenPage>' page
	Examples:
	| FirstName | Page           | GenPage |
	| Chad      | GeneratorIndex | FAQ     |

Scenario: Logged in user will see generator information buttons on FAQ page
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I select the FAQ page
	Then I should should see FAQ information buttons
	Examples:
	| FirstName | Page           |
	| Chad      | GeneratorIndex |

Scenario: Logged in user will see generator information appear when clicking on any information button
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I select the FAQ page
		And I click any information button
	Then I should should see FAQ information
	Examples:
	| FirstName | Page           |
	| Chad      | GeneratorIndex |


Scenario: Logged in user will see tutorial videos when clicking on any generator information button
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I select the FAQ page
		And I click any information button
	Then I should should see a tutotial video
	Examples:
	| FirstName | Page           |
	| Chad      | GeneratorIndex |

