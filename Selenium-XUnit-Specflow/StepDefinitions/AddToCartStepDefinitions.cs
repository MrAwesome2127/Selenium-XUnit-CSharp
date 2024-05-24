using Selenium_XUnit_Specflow.Pages;

namespace Selenium_XUnit_Specflow.StepDefinitions;

[Binding]
public sealed class AddToCartStepDefinitions 
{
    private readonly ScenarioContext _scenarioContext;
    private readonly IHomePage _homePage;
    private readonly IProductPage _productPage;

    public AddToCartStepDefinitions(ScenarioContext scenarioContext, IHomePage homePage, IProductPage productPage)
    {
        _scenarioContext = scenarioContext;
        _homePage = homePage;
        _productPage = productPage;
    }

    //Background details
    [Given(@"the following Game Details")]
    public void GivenTheFollowingGameDetails(Table table)
    {
        dynamic Game = table.CreateDynamicSet();
        
        foreach(var item in Game)
        {
            Console.WriteLine($"Searched Game: {item.Game}");
        }
    }

    [When(@"I Search for the Game")]
    public void WhenISearchForProduct()
    {
        _homePage.SearchForGame("The Legend of Zelda: Tears of the Kingdom");
    }

    [When(@"I add the Game to cart")]
    public void WhenIAddTheGameToCart()
    {
        throw new PendingStepException();
    }

    [When(@"I click Add Cart and View")]
    public void WhenIClickAddCartAndView()
    {
        throw new PendingStepException();
    }

    public record Game()
    {
        public string Games { get; set; }
    }
}