using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Catalog.Tests.PageObjects;

public class CataloguePage
{
    private IWebDriver _driver;
    private WebDriverWait _wait;

    public CataloguePage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public void InputMinimumPrice(int minimumPrice)
    {
        var priceInputXPath = "//input[@class='input-style input-style_primary input-style_small catalog-form__input catalog-form__input_width_full'][@placeholder='от']";
        var priceInput = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(priceInputXPath)));
        priceInput.SendKeys(minimumPrice.ToString());
    }

    public void InputMaximumPrice(int maximumPrice)
    {
        var priceInputXPath = "//input[@class='input-style input-style_primary input-style_small catalog-form__input catalog-form__input_width_full'][@placeholder='до']";
        var priceInput = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(priceInputXPath)));
        priceInput.SendKeys(maximumPrice.ToString());
    }

    public IWebElement? GetFilterButton(string filterText)
    {
        var buttonXPath = $"//div[contains(@class, 'button-style button-style_either button-style_small catalog-form__button catalog-form__button_tag') and contains(text(), '{filterText}')]";

        try
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(buttonXPath)));
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
        var linkXPath = "//a[contains(@class, 'catalog-form__link catalog-form__link_nodecor catalog-form__link_primary-additional catalog-form__link_huge-additional catalog-form__link_font-weight_bold')]";
        var links = _driver.FindElements(By.XPath(linkXPath));

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
        var cssSelector = "div.catalog-interaction__state.catalog-interaction__state_initial.catalog-interaction__state_disabled.catalog-interaction__state_animated.catalog-interaction__state_control";

        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(cssSelector)));
    }
}
