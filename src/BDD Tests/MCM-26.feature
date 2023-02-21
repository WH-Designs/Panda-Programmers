Feature: Play any playlist 

Scenario: 
Give I am a user who is logged into the site

And have a list of my playlists

When I press on one of the playlists

Then I should be taken to the playlist page where it shows all of the details of the playlist



Given I am a user who is on a single playlist page

When I press on the play button near the title of the playlist

Then the site should start playing the songs in order from the music player without a page reload
