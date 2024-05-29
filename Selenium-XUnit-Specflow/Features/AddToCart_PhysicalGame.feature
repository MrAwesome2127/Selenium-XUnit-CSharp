Feature: AddToCart_PhysicalGame
A short summary of the feature


Background:
	Given the following Game Details
		| Game                                  | Price |
		| Legend of Zelda: Breathe of the Wild  | 59.99 |
		| Legend of Zelda: Tears of the Kingdon | 69.99 |

@AddToCart @Smoke @Regression
Scenario: Add Physical copy to cart
	#Home Page
	When I Search for the Game
		And I add the Game to cart

	#Product Page
	When I click Add Cart and View 

	#Add to Cart Page

@AddToCart @Smoke @Regression
Scenario: Add Digital copy to cart
	#Home Page
	When I Search for the Game
		And I add the Game to cart

	#Product Page
	When I click Add Cart and View 

	#Add to Cart Page
	