Feature: Items

@AddNewItem
Scenario: AddNewItem
	When I navigate to item page
	And the following user tries to login
		| Key      | Value |
		| Username | a     |
		| Password | a     |
	And the following item is added
		| Key      | Value     |
		| ItemName | Item Test |
	Then the following item is visible in the items list
		| Key      | Value     |
		| ItemName | Item Test |

@UpdateItem
Scenario: UpdateItem
	When I navigate to item page
	And the following user tries to login
		| Key      | Value |
		| Username | a     |
		| Password | a     |
	And the following item is added
		| Key      | Value        |
		| ItemName | ItemToUpdate |
	And the following item is updated
		| Key              | Value        |
		| OriginalItemName | ItemToUpdate |
		| NewItemName      | Item Updated |
	Then the following item is visible in the items list
		| Key      | Value        |
		| ItemName | Item Updated |
