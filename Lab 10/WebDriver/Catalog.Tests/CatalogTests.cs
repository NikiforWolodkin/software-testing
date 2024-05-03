using Catalog.Tests.PageObjects;
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
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArguments("--no-sandbox");
        chromeOptions.AddArguments("--disable-dev-shm-usage");

        _driver = new ChromeDriver(chromeOptions);
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    [Test]
    public void WhenUsingPriceFilter_PriceFilterTagShouldAppear_WithCorrectPrice()
    {
        const int price = 500;

        var homePage = new HomePage(_driver, _wait);

        _driver.Manage().Window.Maximize();

        homePage.GoToPage();

        var cataloguePage = homePage.GoToSmartphones();

        cataloguePage.InputMaximumPrice(price);

        var button = cataloguePage.GetFilterButton($"до {price}");

        Assert.That(button, !Is.Null);
    }

    [Test]
    public void WhenUsingPriceFilter_ProductsShouldFilterByPrice()
    {
        const int price = 500;

        var homePage = new HomePage(_driver, _wait);

        _driver.Manage().Window.Maximize();

        homePage.GoToPage();

        var cataloguePage = homePage.GoToSmartphones();

        cataloguePage.InputMaximumPrice(price);
        cataloguePage.WaitForFilterToApply();

        var prices = cataloguePage.GetPrices();

        Assert.That(prices, Is.All.LessThanOrEqualTo(price));
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}
