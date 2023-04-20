@Michael
Feature: Visitor can see YouTube music videos on landing page

This feature allows a visitor to see the top music videos from YouTube
on the MCM landing page.

Scenario: Visitor can see music videos
	Given I am a visitor
	When I am on the "Home" page
	Then I should the top music videos from YouTube