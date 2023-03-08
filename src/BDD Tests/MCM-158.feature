Feature: Search for MCM User

    MCM User should be able to find info other users by inputting their name in a search bar

Scenario: Logged in user enters existing username
    Given that I am a user on MCM
    When I fill out the search bar with the username of an MCM user
     And I click “search” on the search bar
    Then I will see info about that MCM user.

Scenario: Logged in user enter non-existing username
    Given that I am a user on MCM
    When I fill out the search bar with a username
     And no MCM user with that username exists
    Then I will see a message that the user was not found.