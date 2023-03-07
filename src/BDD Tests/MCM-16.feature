Feature: View playlists on dashboard

Scenario: 
Given I am a user that is logged into Spotify

And I am logged into the MCM site

When I navigate to the main dashboard page

Then I should see my saved playlists inside of a container away from all other content
