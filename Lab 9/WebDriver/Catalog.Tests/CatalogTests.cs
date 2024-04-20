using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebDriver;

public class CatalogTests
{
    private IWebDriver _driver;
    private WebDriverWait _wait;

    [SetUp]
    public void Setup()
    {
        _driver = new ChromeDriver();
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    [Test]
    public void Test()
    {
        _driver.Manage().Window.Maximize();

        _driver.Navigate().GoToUrl("https://catalog.onliner.by/");

        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'Электроника')]"))).Click();
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(), 'Мобильные телефоны и аксессуары')]"))).Click();
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'Смартфоны')]"))).Click();

        var priceInputXPath = "//input[@class='input-style input-style_primary input-style_small catalog-form__input catalog-form__input_width_full'][@placeholder='от']";
        var priceInput = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(priceInputXPath)));
        priceInput.Clear();
        priceInput.SendKeys("500");
        priceInput.SendKeys(Keys.Enter);

        var buttonXPath = "//div[contains(@class, 'button-style button-style_either button-style_small catalog-form__button catalog-form__button_tag') and contains(text(), 'от 500')]";
        var button = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(buttonXPath)));

        Assert.That(button.Text, Is.EqualTo("от 500"));
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}
