using Test_Framework.Config;

namespace Test_Framework.Driver;

public class DriverFixture : IDriverFixture, IDisposable
{
    private readonly TestSettings _testSettings;

    public IWebDriver Driver { get; }

    public DriverFixture(TestSettings testSettings)
    {
        _testSettings = testSettings;
        Driver = _testSettings.TestRunType == TestRunType.Local ? GetWebDriver(): GetRemoteWebDriver();
        Driver.Navigate().GoToUrl(_testSettings.ApplicationUrl);
    }

    private IWebDriver GetWebDriver() // Local
    {
        return _testSettings.BrowserType switch
        {
            BrowserType.Chrome => new ChromeDriver(),
            BrowserType.Edge => new EdgeDriver(),
            BrowserType.Firefox => new FirefoxDriver(),
            _ => new ChromeDriver(),
        };
    }

    private IWebDriver GetRemoteWebDriver() // Grid
    {
        return _testSettings.BrowserType switch
        {
            BrowserType.Chrome => new RemoteWebDriver(_testSettings.SeleniumGridUri, new ChromeOptions()),
            BrowserType.Edge => new RemoteWebDriver(_testSettings.SeleniumGridUri, new EdgeOptions()),
            BrowserType.Firefox => new RemoteWebDriver(_testSettings.SeleniumGridUri, new FirefoxOptions()),
            _ => new RemoteWebDriver(_testSettings.SeleniumGridUri, new ChromeOptions()),
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
