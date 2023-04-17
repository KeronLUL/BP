using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SeleniumGenerator
{
    [Generator]
    public class SeleniumCommandsGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            StringBuilder sourceBuilder = new StringBuilder($@"
using System;
using OpenQA.Selenium;
using SeleniumGenerated;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumCommands
{{
    interface ICommand
    {{
        public static void Execute(String path) {{ }}
        public static void Execute(int time) {{ }}
        public static void Execute(String path, int time) {{ }}
        public static void Execute(String path, String text) {{ }}
        public static string Get(String path) {{ return """"; }}
        public static string Get(String path, String attribute) {{ return """"; }}
    }}

    public class Click : ICommand
    {{
        public static void Execute(String path)
        {{
            SeleniumWebDriver.Driver.FindElement(By.XPath(path)).Click();
        }}
    }}

    public class SaveText : ICommand
    {{
        public static string Get(String path, String name)
        {{
            return SeleniumWebDriver.Driver.FindElement(By.XPath(path)).Text;
        }}
    }}

    public class SaveAttribute : ICommand
    {{
        public static string Get(String path, String attribute, String name)
        {{
            return SeleniumWebDriver.Driver.FindElement(By.XPath(path)).GetAttribute(attribute);
        }}
    }}

    public class Clear : ICommand
    {{
        public static void Execute(String path)
        {{
            SeleniumWebDriver.Driver.FindElement(By.XPath(path)).Clear();
        }}
    }}

    public class Submit : ICommand
    {{
        public static void Execute(String path)
        {{
            SeleniumWebDriver.Driver.FindElement(By.XPath(path)).Submit();
        }}
    }}

    public class SendKeys : ICommand
    {{
        public static void Execute(String path, String text)
        {{
            SeleniumWebDriver.Driver.FindElement(By.XPath(path)).SendKeys(text);
        }}
    }}

    public class Navigate : ICommand
    {{
        public static void Execute(String path)
        {{
            SeleniumWebDriver.Navigate(path);
        }}
    }}

    public class ImplicitWait : ICommand
    {{
        public static void Execute(int time)
        {{
            SeleniumWebDriver.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
        }}
    }}

    public class WaitUntilExists : ICommand
    {{
        public static void Execute(String path, int time)
        {{
            WebDriverWait wait = new WebDriverWait(SeleniumWebDriver.Driver, TimeSpan.FromSeconds(time));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(path)));
        }}
    }}

    public class WaitUntilClickable : ICommand
    {{
        public static void Execute(String path, int time)
        {{
            WebDriverWait wait = new WebDriverWait(SeleniumWebDriver.Driver, TimeSpan.FromSeconds(time));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(path)));
        }}
    }}
}}");
            
            context.AddSource("commands.g.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required
        }
    }
}