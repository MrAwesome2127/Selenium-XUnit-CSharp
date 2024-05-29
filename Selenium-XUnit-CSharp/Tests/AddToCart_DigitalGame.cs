using Selenium_XUnit_CSharp.Pages;

namespace Selenium_XUnit_CSharp.Tests;

public class AddToCart_DigitalGame
{
    private readonly IHomePage _homePage;
    private readonly IProductPage _productPage;

    public AddToCart_DigitalGame(IHomePage homePage, IProductPage productPage)
    {
        _homePage = homePage;
        _productPage = productPage;
    }

    [Fact]
    public async void AddPhysicalGameToCart()
    {
        _homePage.SearchForGame("The Legend of Zelda: Tears of the Kingdom");
        _productPage.AddGame();
    }

    [Theory]
    [InlineData("The Legend of Zelda: Breathe of the Wild")]
    [InlineData("The Legend of Zelda: Tears of the Kingdom")]
    public async void AddGamesToCart(string Game)
    {
        _homePage.SearchForGame(Game);
        _productPage.AddGame();
    }
}