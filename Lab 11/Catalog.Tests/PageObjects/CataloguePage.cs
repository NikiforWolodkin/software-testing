using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Catalog.Tests.PageObjects;

public class CataloguePage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By _minPriceInput = By.XPath("//input[@class='input-style input-style_primary input-style_small catalog-form__input catalog-form__input_width_full'][@placeholder='от']");
    private readonly By _maxPriceInput = By.XPath("//input[@class='input-style input-style_primary input-style_small catalog-form__input catalog-form__input_width_full'][@placeholder='до']");
    private readonly By _link = By.XPath("//a[contains(@class, 'catalog-form__link catalog-form__link_nodecor catalog-form__link_primary-additional catalog-form__link_huge-additional catalog-form__link_font-weight_bold')]");
    private readonly By _loadingIndicator = By.CssSelector("div.catalog-interaction__state.catalog-interaction__state_initial.catalog-interaction__state_disabled.catalog-interaction__state_animated.catalog-interaction__state_control");

    public CataloguePage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public void InputMinimumPrice(int minimumPrice)
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_minPriceInput));
        _driver.FindElement(_minPriceInput).SendKeys(minimumPrice.ToString());
    }

    public void InputMaximumPrice(int maximumPrice)
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_maxPriceInput));
        _driver.FindElement(_maxPriceInput).SendKeys(maximumPrice.ToString());
    }

    public IWebElement? GetFilterButton(string filterText)
    {
        var buttonXPath = By.XPath($"//div[contains(@class, 'button-style button-style_either button-style_small catalog-form__button catalog-form__button_tag') and contains(text(), '{filterText}')]");

        try
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(buttonXPath));
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    public void WaitForFilterToApply()
    {
        // Wait for input debounce 
        Thread.Sleep(500);

        WaitForProductsToUpdate();
    }

    public ICollection<double> GetPrices()
    {
        var links = _driver.FindElements(_link);

        return links
            .Select(link => 
            {
                var priceText = link.Text;

                var priceNumberText = priceText
                    .Replace("от ", "")
                    .Replace(" р.", "")
                    .Replace(",", ".");

                return double.Parse(priceNumberText);
            })
            .ToList();
    }

    public void WaitForProductsToUpdate()
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(_loadingIndicator));
    }
}
