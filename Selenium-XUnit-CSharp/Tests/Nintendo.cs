using Selenium_XUnit_CSharp.Pages;
using Test_Framework.Config;
using Test_Framework.Driver;

namespace Selenium_XUnit_CSharp.Tests;

public class Nintendo : IDisposable
{
    private IDriverFixture _driverFixture;
    private IDriverWait _driverWait;
    private readonly IHomePage _homePage;
    private readonly IProductPage _productPage;

    public Nintendo()//IHomePage homePage, IProductPage productPage)
    {
        var testSettings = ConfigReader.ReadConfig();
        _driverFixture = new DriverFixture(testSettings);
        _driverWait = new DriverWait(_driverFixture, testSettings);
        //_homePage = homePage;
        //_productPage = productPage;
    }

    [Fact]
    public async void AddPhysicalGameToCart()
    {
        HomePage _homePage = new HomePage(_driverWait);
        _homePage.SearchForGame("The Legend of Zelda: Tears of the Kingdom");

        ProductPage _productPage = new ProductPage(_driverWait);
        _productPage.AddGame();
    }

    [Theory]
    [InlineData("The Legend of Zelda: Breathe of the Wild")]
    [InlineData("The Legend of Zelda: Tears of the Kingdom")]
    public async void AddGamesToCart(string Game)
    {
        HomePage _homePage = new HomePage(_driverWait);
        _homePage.SearchForGame(Game);

        ProductPage _productPage = new ProductPage(_driverWait);
        _productPage.AddGame();
    }

    public void Dispose()
    {
        _driverFixture.Driver?.Quit(); //?Conditional if it is not null to prevent null expression.
    }
}