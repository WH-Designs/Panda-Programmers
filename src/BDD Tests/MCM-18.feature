Feature: Application Initialization

Scenario: MCM user wants to have an organized dashboard when logged in

    Given I am a user on the landing page
    When I login
    Then I am redirected to my dash with my information present

    Given I am a user 
    When I am on my dashboard
    Then I can see my username 

    Given I am a visitor
    When I am on the visitor dashboard
    Then I can see the login button instead of a username