Feature: View reorganized personal dashboard

Scenario: Only 5 items in a category

    Given that I am a logged in user on my dashboard
    When I scroll through my dashboard
    And I have more than 5 items in a category
    Then I should only see the first 5 items in per category

Scenario: More than 5 items in a category

    Given that I am a logged in user on my dashboard
    And I have more than 5 items in a category
    Then I should see a clickable icon that allows me to see up to 15 more items for that category.

    Given that I am a user on my dashboard
    When I have more than 20 items for a category
    And I have reached the 20th item in the category
    Then I will see a button that takes me to Spotify

Scenario: No items in a category

    Given that I am a user on my dashboard
    When I have no items for a category
    Then I will see a message telling me there is no content to display for that category.
