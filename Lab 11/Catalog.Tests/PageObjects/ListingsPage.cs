using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Catalog.Tests.PageObjects;

public class ListingsPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By _addListing = By.XPath("//strong[normalize-space()='Разместить объявление']");
    private readonly By _imageContainer = By.XPath("//span[@class='drop-style__link-faux' and normalize-space()='выберите файлы']");
    private readonly By _uploadFailedMessage = By.XPath("//div[@class='drop-style__upload-title drop-style__upload-title_error' and normalize-space()='Не загрузилось']");

    public ListingsPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public void AddListing()
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_addListing)).Click();
    }

    public void UploadFile(string filePath)
    {
        var input = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_imageContainer));

        DropFile(filePath, input, 0, 0);
    }

    public IWebElement? GetUploadFailedMessage()
    {
        try
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_uploadFailedMessage));
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    public void DropFile(string filePath, IWebElement target, int offsetX, int offsetY)
    {
        if (!File.Exists(filePath))
            throw new WebDriverException("File not found: " + filePath);

        IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;

        string JS_DROP_FILE =
                "var target = arguments[0]," +
                        "    offsetX = arguments[1]," +
                        "    offsetY = arguments[2]," +
                        "    document = target.ownerDocument || document," +
                        "    window = document.defaultView || window;" +
                        "" +
                        "var input = document.createElement('INPUT');" +
                        "input.type = 'file';" +
                        "input.style.display = 'none';" +
                        "input.onchange = function () {" +
                        "  var rect = target.getBoundingClientRect()," +
                        "      x = rect.left + (offsetX || (rect.width >> 1))," +
                        "      y = rect.top + (offsetY || (rect.height >> 1))," +
                        "      dataTransfer = { files: this.files };" +
                        "" +
                        "  ['dragenter', 'dragover', 'drop'].forEach(function (name) {" +
                        "    var evt = document.createEvent('MouseEvent');" +
                        "    evt.initMouseEvent(name, true, true, window, 0, 0, 0, x, y, false, false, false, false, 0, null);" +
                        "    evt.dataTransfer = dataTransfer;" +
                        "    target.dispatchEvent(evt);" +
                        "  });" +
                        "" +
                        "  setTimeout(function () { document.body.removeChild(input); }, 25);" +
                        "};" +
                        "document.body.appendChild(input);" +
                        "return input;";

        IWebElement input = (IWebElement)jse.ExecuteScript(JS_DROP_FILE, target, offsetX, offsetY);
        input.SendKeys(Path.GetFullPath(filePath));
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.StalenessOf(input));
    }
}
