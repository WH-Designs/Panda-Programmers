@Jasper
Feature: Search Function
**As a registered user I would like to be able to go to a search landing page**

This feature allows the user to have access to a search bar that will take a user to input a search that will search Spotify. This
feature will be accessable from the nav bar.

Scenario: Logged in user can reach search page
	Given I am a logged in user
	When I click the search button in the navbar
	Then I should be redirected to the '<Search>' page

Scenario: Logged in user can search for something successfully on the search page
    Given I am a listener on the search page
    And I type a query into the search bar
    When I click the search button
    Then I can see the search results

Scenario: Logged in user doesn't input anything into the search bar and recieves an error
    Given I am a listener on the search page
    When I type an empty query into the search bar
    Then I should see a message that indicates I need to input a query

Scenario: Logged in user searches for something and recieves no results
    Given I am a listener on the search page
    When I submit a query that has no results
    Then I should see a message indicating that there are no results