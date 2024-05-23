
namespace Test_Framework.Extensions;

public static class WebElementExtension
{
    public static void SelectDropDownByText(this IWebElement element, string text)
    {
        SelectElement select = new SelectElement(element); 
        select.SelectByText(text);
    }
    public static void SelectDropdownByIndex(this IWebElement element, int index)
    {
        SelectElement select = new SelectElement(element); 
        select.SelectByIndex(index);
    }
    public static void SelectDropdownByValue(this IWebElement element, string value)
    {
        SelectElement select = new SelectElement(element);
        select.SelectByValue(value);
    }

    public static void ClearAndSendKeys(this IWebElement element, string value)
    {
        element.Clear();
        element.SendKeys(value);
    }

    public static void ClearAndClick(this IWebElement element)
    {
        element.Clear();
        element.Click();
    }
}