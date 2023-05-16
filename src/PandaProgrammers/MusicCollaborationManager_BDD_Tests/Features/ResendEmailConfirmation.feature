@Wyatt
Feature: User resending email confirmation


Scenario Outline: Login page contains a link
	Given I am a visitor
	When I am on '<Page>' page
	Then I can see a link that contains '<Text>'
	Examples: 
	| Text                      |
	| Resend email confirmation |

Scenario: User can navigate to the Resend Email Confirmation page
	Given I am a visitor
	When I am on '<Page>' page
	And I click on the resend email confirmation link
	Then I am redirected to the resend email confirmation page

Scenario: Resend Email Confirmation page contains a input
	Given I am a visitor
	When I am on the Resend Email Confirmation page
	Then I can see an email input