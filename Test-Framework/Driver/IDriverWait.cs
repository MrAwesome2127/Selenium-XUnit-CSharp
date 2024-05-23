
namespace Test_Framework.Driver
{
    public interface IDriverWait
    {
        IWebElement FindElement(By elementLocator);
        IEnumerable<IWebElement> FindElements(By elementLocator);
    }
}