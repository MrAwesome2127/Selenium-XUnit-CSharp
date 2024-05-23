using Test_Framework.Driver;

namespace Selenium_XUnit_CSharp.Pages;

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
    private IWebElement imgGame => _driver.FindElement(By.CssSelector("a[aria-label='The Legend of Zelda™: Tears of the Kingdom'] div[class='sc-q6g3tu-2 vwkFq']"));
    #endregion

    public void SearchForGame(string ProductName)
    {
        btnSearch.Click();
        fldSearch.SendKeys("The Legend of Zelda: Tears of the Kingdom");
        imgGame.Click();
    }
}
