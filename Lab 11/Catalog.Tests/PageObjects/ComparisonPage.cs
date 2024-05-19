using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Catalog.Tests.PageObjects;

public class ComparisonPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public ComparisonPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public IWebElement? GetProduct(string productName)
    {
        var productXPath = By.XPath($"//span[@class='product-summary__caption' and contains(text(), '{productName}')]");

        try
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(productXPath));
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    public IWebElement? GetHighlightedParameter(string parameter) 
    {
        var parameterXPath = By.XPath($"//td[contains(@class, 'product-table__cell') and contains(@class, 'product-table__cell_accent')]//span[@class='value__text' and contains(text(), '{parameter}')]");

        try
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(parameterXPath));
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }
}
