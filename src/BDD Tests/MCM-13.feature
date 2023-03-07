Feature: Application Initialization

Scenario: Visitor wants to be able to register an account

    Given I am a visitor
    And I am on the landing page
    Then I should see a register button

    Given I am a visitor
    When I click on the register button
    Then I should be redirected to the register page

    Given I am a visitor on the register page
    When I register without a username or a password
    Then I should not be allowed to register

    Given I am a visitor on the register page
    When I register with the correct information
    Then I should be redirected to a confirm email page

    Given I am a visitor on the confirm email page
    When I click confirm my email
    Then I should see a box saying I confirmed my email

    Given I am a visitor 
    When I click the home button 
    Then I should be taken back to the landing page

    Given I am a visitor on the landing page
    When I click the log in button
    Then I should be taken to the log in page

    Given I am a visitor on the login page
    When I enter incorrect login information 
    Then I should see a message saying that the information was incorrect

    Given I am a visitor on the login page
    When I enter correct login information
    Then I should be redirected to the landing page

    Given I am a User on the landing page
    Then I should see a hello message with my username