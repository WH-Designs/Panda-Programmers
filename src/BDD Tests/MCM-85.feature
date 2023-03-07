Feature: Playlist generator landing page

Scenario:
Given I am a listener

When I go the playlist landing page

I should see a description of each generator



Given I am a listener on the playlist generator landing page

When I click on one of the links to a specific generator

Then I should be taken to that generators page



Given I am a listener on any page

When I click on the link for the playlist landing page in the navbar 

Then I will be taken to the playlist landing page



Given I am a listener on the playlist generator landing page

When I click on one of the links to a specific playlist generator and the generator is not set up yet

Then I will be taken to a blank page where the generator will go in the future