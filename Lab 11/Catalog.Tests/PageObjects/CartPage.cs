using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using AngleSharp.Dom;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.DevTools.V121.CacheStorage;

namespace Catalog.Tests.PageObjects;

public class CartPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By _trashContainer = By.XPath("//div[@class='cart-form__control']//a[@class='button-style button-style_auxiliary button-style_small cart-form__button cart-form__button_remove']");
    private readonly By _trash = By.XPath("//a[@class='button-style button-style_auxiliary button-style_small cart-form__button cart-form__button_remove']");
    private readonly By _close = By.XPath("//a[@class='cart-form__link cart-form__link_other cart-form__link_small' and contains(text(), 'Закрыть')]");
    private readonly By _emptyCart = By.XPath("//div[@class='cart-message__title cart-message__title_big' and contains(text(), 'Ваша корзина пуста')]");

    public CartPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public IWebElement? GetProductLink(string productName)
    {
        var linkXPath = By.XPath($"//a[contains(text(), '{productName}')]");

        try
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(linkXPath));
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    public void RemoveProduct()
    {
        MoveCursorToTrashIcon();

        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_trash)).Click();
    }

    public IWebElement? GetDeletedMessage(string productName)
    {
        var messageXPath = By.XPath($"//div[@class='cart-form__description cart-form__description_primary cart-form__description_base-alter cart-form__description_condensed-extra' and contains(text(), 'Вы удалили {productName}')]");

        try
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(messageXPath));
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    public void CloseDeletedProduct()
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_close)).Click();
    }

    public IWebElement? GetEmptyCartMessage()
    {
        try
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_emptyCart));
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    private void MoveCursorToTrashIcon()
    {
        var container = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(_trashContainer));

        var action = new Actions(_driver);

        action.MoveToElement(container).Perform();
    }
}
