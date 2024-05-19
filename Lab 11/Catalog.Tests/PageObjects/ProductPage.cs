using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Catalog.Tests.PageObjects;

public class ProductPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By _addToCart = By.XPath("//a[contains(text(), 'В корзину')]");
    private readonly By _goToCart = By.XPath("//a[contains(text(), 'Перейти в корзину')]");
    private readonly By _addToComparison = By.XPath("//span[@class='catalog-masthead-controls__text helpers_hide_tablet' and contains(text(), 'Добавить к сравнению')]");
    private readonly By _goToComparison = By.XPath("//a[@class='compare-button__sub compare-button__sub_main']");

    public ProductPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public void AddToCart()
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_addToCart)).Click();
    }

    public CartPage GoToCart()
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_goToCart)).Click();

        return new (_driver, _wait);
    }

    public void AddToComparison()
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_addToComparison)).Click();
    }

    public ComparisonPage GoToComparison()
    {
        // Wait for input debounce
        Thread.Sleep(100);

        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_goToComparison)).Click();

        return new (_driver, _wait);
    }

    public void SelectProductOption(string productOption)
    {
        var optionXPath = By.XPath($"//span[@class='offers-description-filter-control__switcher-inner' and contains(text(), '{productOption}')]");

        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(optionXPath)).Click();
    }
}
