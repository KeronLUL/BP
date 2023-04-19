using System.Reflection;
using SeleniumGenerated;
using WebScraper.Arguments;
using WebScraper.EntityHandlers;
using WebScraper.Json.Entities;

namespace WebScraper;

public class Scraper
{
    public void Run(Config? config)
    {
        try
        {
            Args.PrintVerbose($@"Setting up connection to {config!.Url}.");
            SeleniumWebDriver.SetUp(config!.Url, config!.Driver != null ? config!.Driver : "Firefox");
        }
        catch (Exception e)
        {
            Args.PrintVerbose(e.ToString());
            Console.Error.WriteLine($@"Can't connect to URL: '{config!.Url}'");
            SeleniumWebDriver.Quit(); //wtf is this
            throw;
        }

        var website = CreateOrRetrieveWebsite.CreateOrRetrieve(config);

        do
        {
            foreach (var command in config!.Commands!)
            {
                foreach (var commandProperty in command.GetType().GetProperties())
                {
                    if (!Object.CheckObject(command, commandProperty))
                        continue;
                    
                    var assembly = Assembly.GetExecutingAssembly().GetTypes();
                    var method = assembly.Where(name => name.FullName == $@"SeleniumCommands.{commandProperty.Name}").ToArray()[0].GetMethods()[0];
                    var parameters = method.GetParameters();
        
                    var parameterObject = new object?[parameters.Length];
                
                    for (int index = 0; index < parameterObject.Length; index++)
                    {
                        parameterObject[index] = Object.GetParameter(command, commandProperty, parameters[index]);
                        switch (commandProperty.Name)
                        {
                            case "ImplicitWait":
                            case "WaitUntilClickable" or "WaitUntilExists" when parameters[index].Name == "time":
                                parameterObject[index] = int.Parse(parameterObject[index]!.ToString()!);
                                break;
                        }
                    }
                    
                    var value = "";
                    try
                    {
                        Args.PrintVerbose($@"Executing command {commandProperty.Name}.");
                        value = (string)method?.Invoke(Activator.CreateInstance(assembly[0]), parameterObject)!;
                    }
                    catch(Exception e)
                    {
                        Args.PrintVerbose(e.ToString());
                        Console.Error.WriteLine($@"Exception has been thrown when executing command: '{commandProperty.Name}' with XPath: '{parameterObject[0]}'");
                        throw;
                    }
                    CreateOrRetrieveElement.SaveValue(command, commandProperty, website, value);
                }
            }
            
            if (config.Loop)
            {
                Args.PrintVerbose($@"Loop finished, waiting until next loop starts.");
                Thread.Sleep(config.WaitTime * 1000);
                SeleniumWebDriver.Close();
                SeleniumWebDriver.SetUp(config!.Url, config!.Driver != null ? config!.Driver : "Firefox");
            }
        } while (config.Loop);

        SeleniumWebDriver.Quit();
    }
}