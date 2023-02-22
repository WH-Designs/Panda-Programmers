Feature: Spotify account information on profile page

Scenario: 
Give I am a user on the landing page

When I login into my MCM account

And I login into my Spotify account

And I go to my profile page

Then I should see my Spotify and personal information



Given I am a user on the landing page

When I click the dashboard button on the navbar to take me to my profile page

And I am not logged into Spotify 

Then I should NOT see my Spotify information and just see my personal information
