Feature: Application Initialization

Scenario: Visitor wants to be able to register an account

    Given I am a visitor
    And I am on the landing page
    Then I should see a register button

    Given I am a visitor
    When I click on the register button
    Then I should be redirected to the welcome page

    Given I am a visitor on the register page
    When I register without a username or a password
    Then I should not be allowed to register

    Given I am a visitor on the register page
    When I register with the correct information
    Then I should be redirected to a landing page welcoming me to the site 