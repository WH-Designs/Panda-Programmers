Feature: See Featured Playlists on Dashboard

Scenario: MCM User Connected to Spotify sees Featured Playlists on Dashboard
    
    Given I am a user that is logged into Spotify
    And I am logged into the MCM site
    When I navigate to the main dashboard page
    Then I should see the featured playlists