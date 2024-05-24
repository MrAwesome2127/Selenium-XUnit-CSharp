using Test_Framework.Driver;

namespace Selenium_XUnit_Specflow.Pages;

public interface IHomePage
{
    void SearchForGame(string ProductName);
}

public class HomePage : IHomePage
{
    private readonly IDriverWait _driver;

    public HomePage(IDriverWait driver)
    {
        _driver = driver;
    }

    #region Locators
    private IWebElement btnSearch => _driver.FindElement(By.CssSelector(".bLIHTE.sc-1r59ztq-3"));
    private IWebElement fldSearch => _driver.FindElement(By.XPath("//input[@placeholder=\'Search games, hardware, news, etc\']"));
    private IWebElement imgGame => _driver.FindElement(By.XPath("//div[@data-testid='card']"));
    private IWebElement lnkAllResults => _driver.FindElement(By.XPath("//div//button[@class='sc-1o4eb18-1 juWvlA sc-ngspk1-6 fhiCKu']"));
    #endregion

    public void SearchForGame(string ProductName)
    {
        btnSearch.Click();
        fldSearch.SendKeys(ProductName);
        lnkAllResults.Click();
        imgGame.Click();
    }
}
