Feature: Set up music player

Scenario: 
Give I am a Listener connected to Spotify

When I am on my dashboard

Then I can see the music player at the bottom of the page



Give I am a Listener disconnected to Spotify

When I am on my dashboard

Then I can see the embedded music player at the bottom of the page



Give I am a visitor

When I am on the dashboard

Then I can see the music player at the bottom of the page but I cannot interact with it