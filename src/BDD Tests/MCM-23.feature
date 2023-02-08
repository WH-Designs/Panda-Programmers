Feature: Spotify Connection

Scenario: MCM User Connects to Spotify

    Given I am a user that is logged into Spotify
    And I am logged into the MCM site
    When I navigate to the main dashboard page
    Then I should see the featured playlists
