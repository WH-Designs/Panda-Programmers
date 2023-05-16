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
	#Tihs line is UNIQUE.
	Given I am a logged in user with first name '<FirstName>'
		And I am on the '<Page>' page
	When I go to a playlist page
	Then I should see a spot to enter a playlist name
	#Necessary because 'TrackInput' Generator is NOT supposed to allow for the user to give the playlist a name.
	Examples:
	| Page           | FirstName |
	| GeneratorIndex | Chad      |