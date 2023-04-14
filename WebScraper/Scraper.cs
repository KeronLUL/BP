using System.Reflection;
using Newtonsoft.Json;
using OpenQA.Selenium;
using SeleniumGenerated;
using WebScraper.Arguments;
using WebScraper.Database.Entities;
using WebScraper.Database.EntityCreator;
using WebScraper.Database.Facades;
using WebScraper.EntityHandlers;
using WebScraper.Json.Entities;

namespace WebScraper;

public class Scraper
{
    public async void Run(Config? config)
    {
        try
        {
            SeleniumWebDriver.SetUp(config!.url, config!.driver != null ? config!.driver : "Firefox");
        }
        catch (WebDriverArgumentException)
        {
            await Console.Error.WriteLineAsync($@"Can't connect to URL: '{config!.url}'");
            throw;
        }

        var website = CreateOrRetrieveWebsite.CreateOrRetrieve(config);

        do
        {
            foreach (var command in config!.commands!)
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
                    }
                    
                    var value = "";
                    try
                    {
                        value = (string)method?.Invoke(Activator.CreateInstance(assembly[0]), parameterObject)!;
                    }
                    catch(Exception)
                    {
                        await Console.Error.WriteLineAsync($@"Exception has been thrown when executing command: '{commandProperty.Name}' with XPath: '{parameterObject[0]}'");
                        throw;
                    }

                    if (commandProperty.Name == "SaveText")
                    {
                        CreateOrRetrieveElement.CreateOrRetrieve(website, command!.SaveText!.args![0].name, value);
                    }// attribute save
                }
            }
            
            //back to begining url and wait
        } while (config.loop);
        
        SeleniumWebDriver.Quit();
    }
}