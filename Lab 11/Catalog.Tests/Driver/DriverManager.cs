using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Catalog.Tests.Utilities;

namespace Catalog.Tests.Driver;

public static class DriverManager
{
    private static IWebDriver? _driver;
    private static WebDriverWait? _wait;

    public static IWebDriver GetInstance(bool useDockerConfig)
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--start-maximized");

        if (useDockerConfig)
        {
            chromeOptions.AddArgument("--window-size=1920,1080");
            chromeOptions.AddArguments("--no-sandbox");
            chromeOptions.AddArguments("--disable-dev-shm-usage");
            chromeOptions.AddArguments("--headless");
        }

        _driver = new ChromeDriver(chromeOptions);

        return _driver;
    }

    public static WebDriverWait GetWaitInstance()
    {
        if (_wait is null)
        {
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        return _wait;
    }

    public static void AddCookiesFromJson(string filePath)
    {
        var cookies = ConfigReader.ReadCookiesFromJson(filePath);

        if (cookies is null) 
            return;

        foreach (var cookie in cookies)
        {
            _driver?.Manage().Cookies.AddCookie(new Cookie(cookie.Key, cookie.Value));
        }
    }

    public static void Dispose()
    {
        _driver?.Quit();
        _driver = null;
        _wait = null;
    }
}
