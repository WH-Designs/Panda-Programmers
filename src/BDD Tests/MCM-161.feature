Feature: As a listener I want to be able to filter my search on MCM

Scenario: MCM User is filtering their search on MCM

    Given I am a User on the browse/search page
    And I type in a search query
    When I use a filter
    Then i should see results affected by the filter

    Given I am a user on the browse/search page
    And I type in a search query
    When I am not using a filter
    Then I should see results that are unfiltered
    
    Given I am a user on the browse/search page
    And I do not type in a search query
    When I use a filter and press submit
    Then I should see no results with a message indicating I need to input something into the search bar