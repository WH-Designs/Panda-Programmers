Feature: Playlsit generator with questionaire

Scenario: 
Given I am a listener

When I go to the playlist generator page

Then I should see a questionnaire with questions related to music



Given I am a listener

When I fill out the questionnaire and submit it

Then I should get a playlist with tracks based on my answers



Given I am a listener

When I fill out part of the questionnaire 

Then I should get a playlist with tracks based on my answers and randomly generated answers for the inputs I left blank