Feature: MCM users each have their unique spotify accounts connected 

Scenario: Multiple MCM users connect via the same browser

    Given I am on the MCM Visitor Dashboard
    When I am not logged in
    Then I cannot see the Spotify button

    Given I am on the MCM dashboard
    And I am logged into my MCM account
    And my browser is not logged into spotify
    When I click the spotify button
    Then I am redirected to my correctly personalized MCM Dashboard

    Given I am on the MCM dashboard
    And I am logged into my MCM account
    And my browser is logged into the correct spotify account
    When I click the spotify button
    Then I am redirected to my correctly personalized MCM Dashboard
    
    Given I am on the MCM dashboard
    And I am logged into my MCM account
    And my browser is logged into the incorrect spotify account
    When I click the spotify button
    Then I am redirected to my correctly personalized MCM Dashboard