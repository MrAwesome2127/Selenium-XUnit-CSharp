using Test_Framework.Config;

namespace Test_Framework.Driver;

public class DriverFixture : IDriverFixture
{
    private readonly TestSettings _testSettings;

    public IWebDriver Driver { get; }

    public DriverFixture(TestSettings testSettings)
    {
        _testSettings = testSettings;
        Driver = GetDriverType(testSettings.BrowserType);
        Driver.Navigate().GoToUrl(testSettings.ApplicationUrl);
    }

    private IWebDriver GetDriverType(BrowserType browserType)
    {
        return browserType switch
        {
            BrowserType.Chrome => new ChromeDriver(),
            BrowserType.Edge => new EdgeDriver(),
            BrowserType.Firefox => new FirefoxDriver(),
            _ => new ChromeDriver(),
        };
    }

    public enum BrowserType
    {
        Chrome,
        Edge,
        Firefox
    }
}
