
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
    private IWebElement btnAddToCart => _driver.FindElement(By.ClassName("sc-1rgmf3w-0 hVjtEL"));
    private IWebElement btnViewCartandCheckout => _driver.FindElement(By.XPath("//span[contains(text(),'View cart and check out')]"));
    #endregion

    public void AddGame()
    {
        rdoPhysical.Click();
        btnAddToCart.Click();
        btnViewCartandCheckout.Click();
    }
}

