namespace Test_Framework.Driver;

public interface IDriverFixture
{
    IWebDriver Driver { get; }

    string TakeScreenshotAsPath(string fileName);
}