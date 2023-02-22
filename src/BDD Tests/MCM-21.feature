Feature: View top Genres on Authenticated Dashboard

Scenario: MCM User connects to spotify and is on their dashboard

    Given I am on the authenticated MCM dashboard
    And I am logged in as a user on MCM
    And I am logged into Spotify
    Then I should see my top genres from Spotify

    Given I am on the authenticated MCM Dashboard
    and I am not logged in as a user on MCM
    and I am not logged into spotify
    then I should not see top genres from Spotify

    Given I am on the authenticated MCM Dashboard
    and I am logged in as a user on MCM
    and I am not logged into spotify
    then I should not see top genres from Spotify

