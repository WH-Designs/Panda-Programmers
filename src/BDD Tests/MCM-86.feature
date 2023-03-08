Feature: Playlist Generator

Scenario: Listeners will be able to generate a unique description for their playlist using Open-AI ChatGPT

    Given I am a listener
    When I am on any playlist generator page
    Then I should see an option to enter a few words for a description

    Given I am a listener and have entered a few words for a description
    When I submit the page to get my playlist
    Then I should be shown a description based off of my input for my playlist

    Given I am a listener and have entered nothing into the input
    When I submit the page to get my playlist
    Then I should be shown a default description based off of the genre of the playlist

    Given I am a listener and have gotten a description based off of my input for my playlist
    When I submit save my playlist
    Then this description should be used for my new playlist