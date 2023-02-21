Feature: Spotify account information on profile page

Scenario: 
Give I am a user on the landing page

When I login into my MCM account

And I login into my Spotify account

Then I should see the landing page



Given I am a user on the landing page

When I click the dashboard button on the navbar

Then I should be taken to a personal dashboard page



Given I am a user on the dashboard page

Then I should see all of my Spotify Information displayed in a clean and organized fashion
