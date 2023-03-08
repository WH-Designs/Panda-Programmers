Feature: As a listener I want to be able to search for things from spotify on MCM

Scenario: MCM User is searching for something from spotify on MCM

    Given I am a user on the dashboard
    And I can see the browse/search button on the navbar.
    When I click on that button
    Then I should be redirected to a page that has a search bar on it 

    Given I am a user on the Browse/Search page
    And I type a search query into the search bar
    When I click submit
    Then I should see the search results appear   

    Given I am a user on the browse/search page
    When I have submitted a search query
    Then I should be able to see results with detailed information like photos, titles, etc.

    Given I am an MCM user on the dashboard page
    And I am not signed into my spotify account
    When I click on the browse/search button on the navbar
    Then I should be sent to spotify to login to my account

    Given I am a user on the browse/search page
    When I try to submit an empty query
    Then I should see a message that indicates I need to input a query

    Given I am a user on the browse/search page
    When I submit a query that has no results
    Then I should see a message indicating that spotify did not yield any results