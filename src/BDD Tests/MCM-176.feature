Feature: Light and dark modes

Scenario: 
Given I am a listener

When I go to the profile page

Then I should see a button that allows me to change the light mode



Given I am a listener and on the profile page

When I click on the light change mode button

Then the light mode should change without a page reload



Given I am a listener and have changed the light mode

When I reload the page 

Then the light mode should remain the same



Given I am a listener and have changed the light mode

When I transition to another page

Then the light mode should remain the same