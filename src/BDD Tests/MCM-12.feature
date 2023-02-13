Feature: Landing page

Landing page for MCM

Scenario: User visits MCM site
	Given I am a visitor
    When I go to MCM landing page
    Then I should see a title, logo, and description

    Given I am a visitor
    When I am on the visitor dashboard
    Then I can see spots that would be for top songs, top categories, playlist generator, and top playlists

    Given I am a visitor
    When I go to MCM landing page
    Then I should see sample data from the Spotify API