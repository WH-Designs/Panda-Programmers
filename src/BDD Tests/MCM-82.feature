Feature: Playlist generator with time

Scenario: 
Given I am a listener

When I go to the time playlist generator

Then I should see a button to generate a playlist



Given I am a listener and on the time playlist generator page

When I click on the button to generate a playlist

Then I should get back a playlist appropriate to the common work week