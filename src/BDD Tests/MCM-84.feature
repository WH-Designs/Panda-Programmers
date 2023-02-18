Feature: Save generated playlists

Scenario: 
Given I am a listener on the generator output page

When I generate a playlist

Then I should see an option to save it to my account



Given I am a listener on the generator output page

When I generate a playlist choose to save the playlist

Then I should see a message telling me that the playlist was successfully save
