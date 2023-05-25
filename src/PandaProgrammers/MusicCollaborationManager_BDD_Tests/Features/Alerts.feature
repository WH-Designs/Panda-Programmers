@Jasper
Feature: Full Error Alert Handling
**As a listener I would like to see alerts on every page when something goes wrong so that I can be informed**

This feature is about keeping the user informed when an error occurs and give the user a possible solution.

Scenario: Logged in user not signed into spotify tries to access the dashboard
	Given I am on the '<Page>' page
    When I click on the dashboard button
	Then I will see an alert message
   Examples:
   | Page |
   | Home |


Scenario: Logged in user not signed into spotify tries to access the search page
	Given I am on the '<Page>' page
    When I click on the search button
	Then I will see an alert message
   Examples:
   | Page |
   | Home |
