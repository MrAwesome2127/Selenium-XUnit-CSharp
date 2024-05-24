using OpenQA.Selenium.Support.Extensions;
using Test_Framework.Config;

namespace Test_Framework.Driver;

public class DriverFixture : IDriverFixture, IDisposable
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

    public string TakeScreenshotAsPath(string filename)
    {
        var screenshot = Driver.TakeScreenshot();
        var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}//{filename}.png";
        screenshot.SaveAsFile(path);
        return path;
    }

    public void Dispose()
    {
        Driver.Dispose();
    }

    public enum BrowserType
    {
        Chrome,
        Edge,
        Firefox
    }
}
