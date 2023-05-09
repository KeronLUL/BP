using WebScraper.Arguments;
using WebScraper.Database.Entities;
using WebScraper.Database.Facades;
using WebScraper.Database.Facades.Interfaces;
using WebScraper.Database.UnitOfWork;
using WebScraper.EntityHandlers;
using WebScraper.Json.Entities;
using WebScraper.SeleniumDriver;

namespace WebScraper;

public static class Scraper
{
    public static async Task Run(Config? config, List<object> commands, IWebsiteFacade websiteFacade, IElementFacade elementFacade)
    {
        var driver = new SeleniumWebDriver();
        try
        {
            Argument.PrintVerbose($@"Setting up connection to {config!.Url}.");
            driver.SetUp(config!.Url, config!.Driver != null ? config!.Driver : "Firefox");
        }
        catch (Exception)
        {
            await Console.Error.WriteLineAsync($@"Can't connect to URL: '{config!.Url}'");
            driver.Quit();
            throw;
        }
        
        var websiteCtx = new CreateOrRetrieveWebsite(websiteFacade);
        var website = websiteCtx.CreateOrRetrieve(config!.Url);

        do
        {
            foreach (var command in commands)
            {
                var method = command.GetType().GetMethod("Execute");
                var parameters = new object[]
                {
                    driver!.Driver!
                };

                Task<string>? task;
                try
                {
                    Argument.PrintVerbose($@"Executing command {command.GetType().Name}.");
                    task = (Task<string>)method?.Invoke(command, parameters)!;
                    await task.ConfigureAwait(false);
                }
                catch(Exception)
                {
                    await Console.Error.WriteLineAsync($@"Exception has been thrown when executing command: '{command.GetType().Name}'");
                    driver.Quit();
                    throw;
                }

                if (command.GetType().Name.Contains("Save"))
                {
                    var name = command!.GetType()!.GetProperty("Name")!.GetValue(command);
                    //Args.PrintVerbose($@"Saving text {commandProperty.Name}.");
                    var ctx = new CreateOrUpdateElement(elementFacade);
                    ctx.CreateOrUpdate(website.Result, name!.ToString()!,task.Result);
                }
            }
            if (!config.Loop) continue;
            
            Argument.PrintVerbose($@"Loop finished, waiting until next loop starts.");
            Thread.Sleep(config.WaitTime * 1000);
            driver.Close();
            driver.SetUp(config!.Url, config!.Driver != null ? config!.Driver : "Firefox");
        } while (config.Loop);

        driver.Quit();
    }
}