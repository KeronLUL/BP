using System.Reflection;
using Newtonsoft.Json;
using SeleniumGenerated;
using WebScraper.Arguments;
using WebScraper.Json.Entities;

namespace WebScraper;

public static class Scraper
{
    public static int Run(Config? config)
    {
        SeleniumWebDriver.SetUp(config!.url, config!.driver != null ? config!.driver : "Firefox");

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
                
                    for (int k = 0; k < parameterObject.Length; k++)
                    {
                        parameterObject[k] = Object.GetParameter(command, commandProperty, parameters[k]);
                    }
                    
                    var str = "";
                    try //idk man
                    {
                        str = (string)method?.Invoke(Activator.CreateInstance(assembly[0]), parameterObject)!;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        method?.Invoke(Activator.CreateInstance(assembly[0]), parameterObject);
                    }
                    Console.WriteLine(str);
                }
            }
            
            //back to begining url and wait
        } while (config.loop);
        
        SeleniumWebDriver.Quit();
        return 0;
    }
}