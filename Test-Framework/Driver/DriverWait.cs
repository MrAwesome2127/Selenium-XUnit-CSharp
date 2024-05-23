using Test_Framework.Config;

namespace Test_Framework.Driver;

public class DriverWait : IDriverWait
{
    private readonly IDriverFixture _driverFixture;
    private readonly TestSettings _testSettings;
    private readonly Lazy<WebDriverWait> _webDriverWait;

    public DriverWait(IDriverFixture driverFixture, TestSettings testSettings)
    {
        _driverFixture = driverFixture;
        _testSettings = testSettings;
        _webDriverWait = new Lazy<WebDriverWait>(GetWaitDriver);
    }

    public IWebElement FindElement(By elementLocator)
    {
        return _webDriverWait.Value.Until(_ => _driverFixture.Driver.FindElement(elementLocator));
    }

    public IEnumerable<IWebElement> FindElements(By elementLocator)
    {
        return _webDriverWait.Value.Until(_ => _driverFixture.Driver.FindElements(elementLocator));
    }

    private WebDriverWait GetWaitDriver()
    {
        return new WebDriverWait(_driverFixture.Driver, timeout: TimeSpan.FromSeconds(_testSettings.TimeoutInternal ?? 30))
        {
            PollingInterval = TimeSpan.FromSeconds(_testSettings.TimeoutInternal ?? 1)
        };
    }
}
