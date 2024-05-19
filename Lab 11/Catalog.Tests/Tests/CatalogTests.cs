using Catalog.Tests.Driver;
using Catalog.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Catalog.Tests.Tests;

public class CatalogTests
{
    private IWebDriver _driver;
    private WebDriverWait _wait;

    [SetUp]
    public void Setup()
    {
        _driver = DriverManager.GetInstance(false);
        _wait = DriverManager.GetWaitInstance();
    }

    [Test]
    public void WhenUsingPriceFilter_PriceFilterTagShouldAppear_WithCorrectPrice()
    {
        const int price = 500;

        var homePage = new HomePage(_driver, _wait);

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

        homePage.GoToPage();

        var cataloguePage = homePage.GoToSmartphones();

        cataloguePage.InputMaximumPrice(price);
        cataloguePage.WaitForFilterToApply();

        var prices = cataloguePage.GetPrices();

        Assert.That(prices, Is.All.LessThanOrEqualTo(price));
    }

    [Test]
    public void WhenUsingSearch_ProductsShouldFilterByName()
    {
        const string search = "iphone 15 pro dual sim 128gb (черный)";
        const string iphone = "Смартфон Apple iPhone 15 Pro Dual SIM 128GB (черный титан)";

        var homePage = new HomePage(_driver, _wait);

        homePage.GoToPage();

        homePage.Search(search);

        var link = homePage.GetProductLink(iphone);

        Assert.That(link, !Is.Null);
    }

    [Test]
    public void WhenUsingSearch_AndProductIsNotFound_NotFoundMessageShouldBeShown()
    {
        const string search = "iphone 18 pro";

        var homePage = new HomePage(_driver, _wait);

        homePage.GoToPage();

        homePage.Search(search);

        var message = homePage.GetNotFoundMessage();

        Assert.That(message, !Is.Null);
    }

    [Test]
    public void WhenAddingToCart_ProductShouldBeAppearInCart()
    {
        const string search = "iphone 15 pro dual sim 128gb (черный)";
        const string iphone = "Смартфон Apple iPhone 15 Pro Dual SIM 128GB (черный титан)";

        var homePage = new HomePage(_driver, _wait);

        homePage.GoToPage();

        homePage.Search(search);

        var productPage = homePage.GoToProductPage(iphone);

        productPage.AddToCart();
        var cartPage = productPage.GoToCart();

        var link = cartPage.GetProductLink(iphone);

        Assert.That(link, !Is.Null);
    }

    [Test]
    public void WhenProductIsDeletedFromCart_DeletedMessageShouldAppear()
    {
        const string search = "iphone 15 pro dual sim 128gb (черный)";
        const string iphone = "Смартфон Apple iPhone 15 Pro Dual SIM 128GB (черный титан)";

        var homePage = new HomePage(_driver, _wait);

        homePage.GoToPage();

        homePage.Search(search);

        var productPage = homePage.GoToProductPage(iphone);

        productPage.AddToCart();
        var cartPage = productPage.GoToCart();

        cartPage.RemoveProduct();

        var message = cartPage.GetDeletedMessage(iphone);

        Assert.That(message, !Is.Null);
    }

    [Test]
    public void WhenProductIsDeletedFromCart_AndClosed_EmptyCartMessageShouldAppear()
    {
        const string search = "iphone 15 pro dual sim 128gb (черный)";
        const string iphone = "Смартфон Apple iPhone 15 Pro Dual SIM 128GB (черный титан)";

        var homePage = new HomePage(_driver, _wait);

        homePage.GoToPage();

        homePage.Search(search);

        var productPage = homePage.GoToProductPage(iphone);

        productPage.AddToCart();
        var cartPage = productPage.GoToCart();

        cartPage.RemoveProduct();
        cartPage.CloseDeletedProduct();

        var message = cartPage.GetEmptyCartMessage();

        Assert.That(message, !Is.Null);
    }

    [Test]
    public void WhenProductIsAddedToComparison_ProductShouldAppearInComparison()
    {
        const string search = "iphone 15 pro dual sim 128gb (черный)";
        const string iphone = "Смартфон Apple iPhone 15 Pro Dual SIM 128GB (черный титан)";

        var homePage = new HomePage(_driver, _wait);

        homePage.GoToPage();

        homePage.Search(search);

        var productPage = homePage.GoToProductPage(iphone);

        productPage.AddToComparison();
        var comparisonPage = productPage.GoToComparison();

        var message = comparisonPage.GetProduct(iphone);

        Assert.That(message, !Is.Null);
    }

    [Test]
    public void WhenProductsAreAddedToComparison_DifferencesAreHighlighted()
    {
        const string search = "iphone 15 pro dual sim 128gb (черный)";
        const string iphone = "Смартфон Apple iPhone 15 Pro Dual SIM 128GB (черный титан)";
        const string option = "256 ГБ";

        var homePage = new HomePage(_driver, _wait);

        homePage.GoToPage();

        homePage.Search(search);

        var productPage = homePage.GoToProductPage(iphone);

        productPage.AddToComparison();
        productPage.SelectProductOption(option);
        productPage.AddToComparison();

        var comparisonPage = productPage.GoToComparison();

        var parameter = comparisonPage.GetHighlightedParameter(option);

        Assert.That(parameter, !Is.Null);
    }

    [TearDown]
    public void TearDown()
    {
        DriverManager.Dispose();
    }
}
