Feature: View Spotify recommended tracks

User signed into Spotify through MCM viewing recommended tracks.

Scenario: Logged into Spotify
    Given I am a user that is logged into Spotify
     And I am logged into the MCM site
    When I navigate to the main dashboard page
    Then I should see their recommended tracks