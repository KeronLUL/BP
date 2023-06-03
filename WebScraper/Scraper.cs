using System.Globalization;
using Microsoft.Extensions.Logging;
using WebScraper.Database.Entities;
using WebScraper.Database.Facades.Interfaces;
using WebScraper.Json.Entities;
using WebScraper.SeleniumDriver;

namespace WebScraper;

public static class Scraper
{
    private static SeleniumWebDriver DriverSetup(ILogger logger, Config? config)
    {
        var driver = new SeleniumWebDriver();
        try
        {
            logger.LogInformation($@"Setting up connection to {config!.Url}.");
            driver.SetUp(config.Url, config.Driver ?? "Firefox");
        }
        catch (Exception)
        {
            logger.LogError($@"Can't connect to URL: '{config!.Url}'");
            driver.Quit();
            throw;
        }
        return driver;
    }

    private static async ValueTask<WebsiteEntity> GetWebsite(IWebsiteFacade websiteFacade, string url)
    {
        return await websiteFacade.SaveWebsiteAsync(url);
    }

    private static async ValueTask SaveCommand(ILogger logger, object command, IElementFacade elementFacade,
        IEntity website, string? value)
    {
        logger.LogInformation($@"Saving value of command {command.GetType().Name}");
        
        var name = command.GetType().GetProperty("Name")!.GetValue(command);
        
        var entity = new ElementEntity()
        {
            Id = Guid.NewGuid(),
            WebsiteId = website.Id,
            Name = name!.ToString()!,
            Timestamp = DateTime.Now.ToString(CultureInfo.CurrentCulture),
            Value = value
        };
        await elementFacade.SaveAsync(entity);
    }

    private static async ValueTask<string?> ExecuteCommand(ILogger logger, object command, SeleniumWebDriver driver)
    {
        var method = command.GetType().GetMethod("Execute");
        var parameters = new object[]
        {
            driver.Driver!
        };

        string? result;
        try
        {
            logger.LogInformation($@"Executing command {command.GetType().Name}.");
            result =  await (ValueTask<string?>)method?.Invoke(command, parameters)!;
        }
        catch(Exception)
        {
            logger.LogError($@"Exception has been thrown when executing command: '{command.GetType().Name}'");
            driver.Quit();
            throw;
        }

        return result;
    }
    
    public static async ValueTask Run(Config? config, List<object> commands,
        IWebsiteFacade websiteFacade, IElementFacade elementFacade, ILogger logger)
    {
        var driver = DriverSetup(logger, config);
        var website = await GetWebsite(websiteFacade, config!.Url!);

        do
        {
            foreach (var command in commands)
            {
                var result = await ExecuteCommand(logger, command, driver);

                if (command.GetType().Name.Contains("Save"))
                {
                    await SaveCommand(logger, command, elementFacade, website, result);
                }
            }
            if (!config.Loop) continue;
            
            logger.LogInformation($@"Loop finished, waiting until next loop starts.");
            await Task.Delay(config.WaitTime);
            driver.Close();
            driver.SetUp(config.Url, config.Driver ?? "Firefox");
        } while (config.Loop);

        driver.Quit();
    }
}