using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Catalog.Tests.PageObjects;

public class HomePage
{
    private IWebDriver _driver;
    private WebDriverWait _wait;

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
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'Электроника')]"))).Click();
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(), 'Мобильные телефоны и аксессуары')]"))).Click();
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'Смартфоны')]"))).Click();

        return new (_driver, _wait);
    }
}
