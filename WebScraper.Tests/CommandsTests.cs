using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using WebScraper.SeleniumCommands;
using WebScraper.SeleniumDriver;
using Xunit;

namespace WebScraper.Tests;

public sealed class CommandsTests
{
    private readonly SeleniumWebDriver _driver;

    private IWebDriver? GetDriver()
    {
        return _driver.Driver;
        
    }

    public CommandsTests()
    {
        _driver = new SeleniumWebDriver();
        _driver.SetUp("https://refactoring.guru/design-patterns/command", "Chrome");
    }

    [Fact]
    public async ValueTask ClickTest()
    {
        var click = new Click()
        {
            Path = "//a[@href='/design-patterns']"
        };
        
        await click.Execute(GetDriver());
        
        Assert.True(GetDriver()!.Url == "https://refactoring.guru/design-patterns");
        GetDriver()!.Quit();
    }

    [Fact]
    public async ValueTask BackTest()
    {
        var back = new Back();
        _driver.Driver!.FindElement(By.XPath("//a[@href='/design-patterns']")).Click();
        
        await back.Execute(_driver.Driver);

        Assert.True(GetDriver()!.Url == "https://refactoring.guru/design-patterns/command");
        GetDriver()!.Quit();
    }

    [Fact]
    public async ValueTask ForwardTest()
    {
        var forward = new Forward();
        GetDriver()!.FindElement(By.XPath("//a[@href='/design-patterns']")).Click();
        GetDriver()!.Navigate().Back();
        
        await forward.Execute(GetDriver());
        
        Assert.True(GetDriver()!.Url == "https://refactoring.guru/design-patterns");
        GetDriver()!.Quit();
    }

    [Fact]
    public async ValueTask SaveTextTest()
    {
        var saveText = new SaveText()
        {
            Path = "//h1"
        };

        var result = await saveText.Execute(GetDriver());
        
        Assert.True(result == "Command");
        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask SaveTitleTest()
    {
        var saveTitle = new SaveTitle();

        var result = await saveTitle.Execute(GetDriver());
        
        Assert.True(result == GetDriver()!.Title);
        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask SaveHtmlTest()
    {
        var saveHtml = new SaveHtml();

        var result = await saveHtml.Execute(GetDriver());
        
        Assert.True(result == GetDriver()!.PageSource);
        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask NavigateTest()
    {
        var navigate = new Navigate()
        {
            Path = "https://refactoring.guru/design-patterns"
        };

         await navigate.Execute(GetDriver());
        
        Assert.True(GetDriver()!.Url == "https://refactoring.guru/design-patterns");
        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask SendKeysTest()
    {
        var sendKeys = new SendKeys()
        {
            Path = "//input[@id='email']",
            Text = "test"
        };
        
        GetDriver()!.Navigate().GoToUrl("https://refactoring.guru/login");
        await sendKeys.Execute(GetDriver());
        
        Assert.True(GetDriver()!.FindElement(By.XPath("//input[@id='email']")).Text == "test");
        
        GetDriver()!.Quit();
    }

    [Fact]
    public async ValueTask ClearTest()
    {
        var clear = new Clear()
        {
            Path = "//input[@id='email']"
        };
        
        GetDriver()!.Navigate().GoToUrl("https://refactoring.guru/login");
        GetDriver()!.FindElement(By.XPath("//input[@id='email']")).SendKeys("test");
        await clear.Execute(GetDriver());
        
        Assert.True(GetDriver()!.FindElement(By.XPath("//input[@id='email']")).Text == "");
        
        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask SubmitTest()
    {
        var submit = new Submit()
        {
            Path = "//input[@id='email']"
        };
        
        GetDriver()!.Navigate().GoToUrl("https://refactoring.guru/login");
        await submit.Execute(GetDriver());
        
        Assert.True(GetDriver()!.FindElement(By.XPath("//div[@class='alert alert-danger']")).Displayed);
        
        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask SaveAttributeTest()
    {
        var saveAttribute = new SaveAttribute()
        {
            Path = "//h1",
            Attribute = "class"
        };
        
        var result = await saveAttribute.Execute(GetDriver());
        
        Assert.True(GetDriver()!.FindElement(By.XPath("//h1")).GetAttribute("class") == result);
        
        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask SaveTagName()
    {
        var saveTagName = new SaveTagName()
        {
            Path = "//h1",
        };
        
        var result = await saveTagName.Execute(GetDriver());
        
        Assert.True(GetDriver()!.FindElement(By.XPath("//h1")).TagName == result);
        
        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask SaveCssValue()
    {
        var saveCssValue = new SaveCssValue()
        {
            Path = "//h1",
            Property = "color"
        };
        
        var result = await saveCssValue.Execute(GetDriver());
        
        Assert.True(GetDriver()!.FindElement(By.XPath("//h1")).GetCssValue("color") == result);
        
        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask AcceptAlertTest()
    {
        var acceptAlert = new AcceptAlert();
        GetDriver()!.Navigate().GoToUrl("https://testpages.herokuapp.com/styled/alerts/alert-test.html");
        GetDriver()!.FindElement(By.XPath("//input[@id='confirmexample']")).Click();
        
        await acceptAlert.Execute(GetDriver());
        Assert.True(GetDriver()!.FindElement(By.XPath("//p[@id='confirmreturn']")).Text == "true");

        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask DismissAlertTest()
    {
        var dismissAlert = new DismissAlert();
        
        GetDriver()!.Navigate().GoToUrl("https://testpages.herokuapp.com/styled/alerts/alert-test.html");
        GetDriver()!.FindElement(By.XPath("//input[@id='confirmexample']")).Click();
        
        await dismissAlert.Execute(GetDriver());
        
        Assert.True(GetDriver()!.FindElement(By.XPath("//p[@id='confirmreturn']")).Text == "false");
        
        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask SendKeysAlert()
    {
        var sendKeysAlert = new SendKeysAlert()
        {
            Text = "test"
        };

        var acceptAlert = new AcceptAlert();
        
        GetDriver()!.Navigate().GoToUrl("https://testpages.herokuapp.com/styled/alerts/alert-test.html");
        GetDriver()!.FindElement(By.XPath("//input[@id='promptexample']")).Click();
        
        await sendKeysAlert.Execute(GetDriver());
        await acceptAlert.Execute(GetDriver());
        
        Assert.True(GetDriver()!.FindElement(By.XPath("//p[@id='promptreturn']")).Text == "test");
        
        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask ExecuteJavaScriptTest()
    {
        var executeJs = new ExecuteJavaScript()
        {
            Script = "return title"
        };

        var result = await executeJs.Execute(GetDriver());
        Assert.True(GetDriver()!.Title == result);

        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask AddCookieTest()
    {
        var addCookie = new AddCookie()
        {
            Key = "key",
            Value = "value"
        };
        
        await addCookie.Execute(GetDriver());
        
        Assert.True(GetDriver()!.Manage().Cookies.GetCookieNamed("key").Value == "value");

        GetDriver()!.Quit();
    }
    
    [Fact]
    public async ValueTask DeleteAllCookiesTest()
    {
        var deleteAllCookies = new DeleteAllCookies();
        
        GetDriver()!.Manage().Cookies.AddCookie(new Cookie("key", "value"));
        await deleteAllCookies.Execute(GetDriver());
        
        Assert.True(GetDriver()!.Manage().Cookies.AllCookies.Count == 0);

        GetDriver()!.Quit();
    }
}