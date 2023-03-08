Feature: Playlist generator with mood

Scenario: 
Given I am a listener

When I go to the mood playlist page

Then I should be able to select a certain mood



Given I am a listener on the mood playlist page

When I select a certain mood and submit my choice

Then I should get a playlist that matches the mood I entered
