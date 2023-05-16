#File name was: GeneratedPlaylistNameInput

@Michael
Feature: Assign playlist a name

**As a listener I would like to name a generated playlist so that I can tell generated playlists apart**

This feature allows a user the option to provide a generated playlist a name. Pure whitespace and empty
input should result in a default name for the playlist.

Background:
	Given the following users exist generator
	  | UserName			 | Email                 | FirstName | LastName | Password     |
	  | chadb@gmail.com	     | chadb@gmail.com       | Chad      | Bass     | Pass321!     |

Scenario: Playlist name input is visible
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I go to a playlist page
	Then I should see a spot to enter a playlist name
	Examples:
	| Page           | FirstName |
	| GeneratorIndex | Chad      |



Scenario: Playlist name input is valid
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I go to a playlist page
		And I type '<PlaylistName>' as the playlist name
		And I click on the button to generate the playlist
	Then I should see '<PlaylistName>' as the playlist title on the generator output page
	Examples:
	| Page           | FirstName | PlaylistName |
	| GeneratorIndex | Chad      | CatchySongs  |


#
#Scenario: Generated playlist name with invalid name results in default title
#	Given I am a logged in user with first name '<FirstName>'
#		And I am on the '<Page>' page
#	When I go to a playlist page
#		And I type a invalid name for the playlist
#	Then I should see a default title on the generator output page
#	Examples:
#	| Page           | FirstName |
#	| GeneratorIndex | Chad      |
#
#
#
#Scenario: Invalid playlist name input displays naming errors
#	Given I am a logged in user with first name '<FirstName>'
#		And I am on the '<Page>' page
#	When I go to a playlist page
#		And I type a invalid name for the playlist
#	Then I should see errors about what input is invalid
#	Examples:
#	| Page           | FirstName |
#	| GeneratorIndex | Chad      |