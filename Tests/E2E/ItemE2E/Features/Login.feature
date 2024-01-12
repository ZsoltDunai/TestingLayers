Feature: Login

@SuccessfulLogin
Scenario: SuccessfulLogin
	When I navigate to item page
	And the following user tries to login
		| Key      | Value |
		| Username | a     |
		| Password | a     |
	Then the main page is visible