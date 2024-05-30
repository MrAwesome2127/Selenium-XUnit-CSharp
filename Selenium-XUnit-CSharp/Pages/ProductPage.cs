
using Test_Framework.Driver;

namespace Selenium_XUnit_CSharp.Pages;

public interface IProductPage
{
    void AddGame();
}

public class ProductPage : IProductPage
{
    private readonly IDriverWait _driver;

    public ProductPage(IDriverWait driver)
    {
        _driver = driver;
    }

    #region Locators
    private IWebElement rdoPhysical => _driver.FindElement(By.Id("Physical-radio-2"));
    private IWebElement btnAddToCart => _driver.FindElement(By.XPath("//button[@class='sc-1rgmf3w-2 cFSrMj sc-10vf2cu-9 iDEsvZ']//*[@data-testid='ShoppingCartIcon']"));
    private IWebElement btnViewCartandCheckout => _driver.FindElement(By.XPath("//div[@class='sc-jqi7uv-0 hJPkdr']//span[@class='sc-1rgmf3w-0 hVjtEL']"));
    #endregion

    public void AddGame()
    {
        rdoPhysical.Click();
        btnAddToCart.Click();
        btnViewCartandCheckout.Click();
    }
}

