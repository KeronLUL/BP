using System.Reflection;
using WebScraper.Arguments;
using WebScraper.EntityHandlers;
using WebScraper.Json.Entities;
using WebScraper.SeleniumDriver;

namespace WebScraper;

public class Scraper
{
    public static void Run(Config? config)
    {
        var driver = new SeleniumWebDriver();
        try
        {
            Args.PrintVerbose($@"Setting up connection to {config!.Url}.");
            driver.SetUp(config!.Url, config!.Driver != null ? config!.Driver : "Firefox");
        }
        catch (Exception e)
        {
            Args.PrintVerbose(e.ToString());
            Console.Error.WriteLine($@"Can't connect to URL: '{config!.Url}'");
            driver.Quit();
            throw;
        }

        var website = CreateOrRetrieveWebsite.CreateOrRetrieve(config);

        do
        {
            foreach (var command in config!.Commands!)
            {
                foreach (var commandProperty in command.GetType().GetProperties())
                {
                    var commandInstance = command.GetType().GetProperty(commandProperty.Name)!.GetValue(command, null);
                    if (commandInstance == null)
                        continue;
                    
                    commandInstance!.GetType().GetProperty("Driver")!.SetValue(commandInstance, driver.Driver);

                    var assembly = Assembly.GetExecutingAssembly().GetTypes();
                    var classObject = assembly.Where(name =>
                            name.FullName == $@"WebScraper.SeleniumCommands.{commandProperty.Name}")
                        .ToArray()[0];
                    var method = classObject.GetMethod("Execute");
                    
                    var value = "";
                    try
                    {
                        Args.PrintVerbose($@"Executing command {commandProperty.Name}.");
                        value = (string)method?.Invoke(commandInstance, null)!;
                    }
                    catch(Exception e)
                    {
                        Args.PrintVerbose(e.ToString());
                        Console.Error.WriteLine($@"Exception has been thrown when executing command: '{commandProperty.Name}'");
                        throw;
                    }
                    CreateOrRetrieveElement.SaveValue(command, commandProperty, website, value);
                }
            }
            
            if (config.Loop)
            {
                Args.PrintVerbose($@"Loop finished, waiting until next loop starts.");
                Thread.Sleep(config.WaitTime * 1000);
                driver.Close();
                driver.SetUp(config!.Url, config!.Driver != null ? config!.Driver : "Firefox");
            }
        } while (config.Loop);

        driver.Quit();
    }
}