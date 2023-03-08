Feature: Change into extra premade themes

Scenario: User changes website theme
    Given I am a listener
    When I am on my profile page
    Then I can see the section with options to change my theme to different pre-made themes

    Given I am a listener on my profile page
    When I click on one of the different pre-made theme buttons
    Then the website responds accordingly and adjusts my theme without a page reload