Feature: Application Initialization

Scenario: Visitors can confirm their email address after registering    

    Given I am a Visitor 
    When I go to register a new account
    Then I should get an email sent to my inbox

    Given I am a Visitor
    When I go to login before I confirm my email
    Then I should see a message saying that I need to confirm my email first

    Given I am a Visitor
    When I go to my email inbox
    Then I should see a email from the application 
    And I should see a clickable link that says click here to confirm your email

    Given I am a Visitor
    When I click on the link
    Then I should be sent back to the site 
    And should see a message saying my email has been confirmed

    Given I am a Visitor
    When I go to login with the correct email
    And I have confirmed my email 
    Then I should be logged into the site