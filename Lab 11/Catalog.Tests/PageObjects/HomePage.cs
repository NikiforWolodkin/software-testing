using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Catalog.Tests.PageObjects;

public class HomePage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By _electronics = By.XPath("//span[contains(text(), 'Электроника')]");
    private readonly By _phones = By.XPath("//div[contains(text(), 'Мобильные телефоны и аксессуары')]");
    private readonly By _smartphones = By.XPath("//span[contains(text(), 'Смартфоны')]");
    private readonly By _searchInput = By.XPath("//form[contains(@class, 'fast-search__form')]//input[@type='text']");
    private readonly By _notFoundMessage = By.XPath("//div[contains(@class, 'search__suggest-addon') and contains(text(), 'Ничего не найдено')]");

    public HomePage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public void GoToPage()
    {
        _driver.Navigate().GoToUrl("https://catalog.onliner.by/");
    }

    public CataloguePage GoToSmartphones()
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_electronics)).Click();
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_phones)).Click();
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_smartphones)).Click();

        return new (_driver, _wait);
    }

    public void Search(string searchText)
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_searchInput));
        _driver.FindElement(_searchInput).SendKeys(searchText);
    }

    public IWebElement? GetProductLink(string productName)
    {
        var linkXPath = By.XPath($"//a[contains(@class, 'product__title-link') and contains(text(), '{productName}')]");

        try
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(linkXPath));
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    public IWebElement? GetNotFoundMessage()
    {
        try
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_notFoundMessage));
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    public ProductPage GoToProductPage(string productName)
    {
        GetProductLink(productName)!.Click();

        return new (_driver, _wait);
    }
}