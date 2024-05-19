using Catalog.Tests.Driver;
using OpenQA.Selenium;

namespace Catalog.Tests.Services;

public static class AuthService
{
    public static void Authorize(IWebDriver driver, string filePath)
    {
        driver.Navigate().GoToUrl("https://baraholka.onliner.by");
        DriverManager.AddCookiesFromJson(filePath);
        driver.Navigate().Refresh();

        driver.Navigate().GoToUrl("https://catalog.onliner.by");
        DriverManager.AddCookiesFromJson(filePath);
        driver.Navigate().Refresh();

        driver.Navigate().GoToUrl("https://chats.onliner.by");
        DriverManager.AddCookiesFromJson(filePath);
        driver.Navigate().Refresh();
    }
}
