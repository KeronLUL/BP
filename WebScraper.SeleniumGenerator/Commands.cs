using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace WebScraper.SeleniumGenerator
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

namespace SeleniumCommands
{{
    interface ICommand
    {{
        public static void Execute(String path) {{ }}
        public static void Execute(int time) {{ }}
        public static void Execute(String path, String text) {{ }}
        public static string Get(String path) {{ return """"; }}
        public static string Get(String path, String attribute) {{ return """"; }}
    }}

    public class Click : ICommand
    {{
        public static void Execute(String path)
        {{
            SeleniumGenerated.SeleniumWebDriver.Driver.FindElement(By.XPath(path)).Click();
        }}
    }}

    public class Text : ICommand
    {{
        public static string Get(String path)
        {{
            return SeleniumGenerated.SeleniumWebDriver.Driver.FindElement(By.XPath(path)).Text;
        }}
    }}

    public class Attribute : ICommand
    {{
        public static string Get(String path, String attribute)
        {{
            return SeleniumGenerated.SeleniumWebDriver.Driver.FindElement(By.XPath(path)).GetAttribute(attribute);
        }}
    }}

    public class Clear : ICommand
    {{
        public static void Execute(String path)
        {{
            SeleniumGenerated.SeleniumWebDriver.Driver.FindElement(By.XPath(path)).Clear();
        }}
    }}

    public class Submit : ICommand
    {{
        public static void Execute(String path)
        {{
            SeleniumGenerated.SeleniumWebDriver.Driver.FindElement(By.XPath(path)).Submit();
        }}
    }}

    public class SendKeys : ICommand
    {{
        public static void Execute(String path, String text)
        {{
            SeleniumGenerated.SeleniumWebDriver.Driver.FindElement(By.XPath(path)).SendKeys(text);
        }}
    }}

    public class ImplicitWait : ICommand
    {{
        public static void Execute(int time)
        {{
            SeleniumGenerated.SeleniumWebDriver.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
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