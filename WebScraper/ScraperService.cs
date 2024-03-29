﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebScraper.Arguments;
using WebScraper.Database.Facades.Interfaces;
using WebScraper.Json;
using WebScraper.Json.Entities;

namespace WebScraper;

internal sealed class ScraperService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IWebsiteFacade _websiteFacade;
    private readonly IElementFacade _elementFacade;
    
    private int? _exitCode;

    public ScraperService(
        ILogger<ScraperService> logger,
        IHostApplicationLifetime appLifetime,
        IWebsiteFacade websiteFacade,
        IElementFacade elementFacade)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _websiteFacade = websiteFacade;
        _elementFacade = elementFacade;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("WebScraper is starting...");

        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                if (Argument.GetFilename() == "")
                {
                    _logger.LogError("Argument parsing failed: path to file is invalid or missing");
                    _exitCode = ReturnCodes.ArgumentError;
                    _appLifetime.StopApplication();
                    return;
                }

                if (!File.Exists(Paths.ProjectPath))
                {
                    _logger.LogError("Program started from wrong directory. Please start this program from WebScraper directory");
                    _exitCode = ReturnCodes.ProjectPathError;
                    _appLifetime.StopApplication();
                    return;
                }
                
                JsonGenerator.GenerateJsonSchema(Paths.ConfigPath, _logger);
                
                if (!JsonValidator.Validate(Paths.ConfigPath, Argument.GetFilename(), _logger))
                {
                    _logger.LogError("Config file is not valid");
                    _exitCode = ReturnCodes.ConfigError;
                    _appLifetime.StopApplication();
                    return;
                }
                
                Config? config = null;
                var commandList = new List<object>();
                try
                {
                    config = JsonDeserializer.Deserialize(ref commandList, _logger);
                    _logger.LogDebug("Config file deserialized");
                }
                catch (Exception)
                {
                    _logger.LogError($@"Failed to deserialize JSON from file: '{Argument.GetFilename()}");
                    _exitCode = ReturnCodes.JsonError;
                    _appLifetime.StopApplication();
                    return;
                }
                
                try
                {
                    _logger.LogInformation("Running WebScraper...");
                    await Scraper.Run(config, commandList, _websiteFacade, _elementFacade, _logger);
                }
                catch (Exception e)
                {
                    _logger.LogError("WebScraper finished with error." + Environment.NewLine + e);
                    _exitCode = ReturnCodes.ScraperError;
                    _appLifetime.StopApplication();
                    return;
                }
                _logger.LogInformation("WebScraper finished.");
                
                _exitCode = 0;
                _appLifetime.StopApplication();
            }, cancellationToken);
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Exiting with return code: {_exitCode}");
        Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
        return Task.CompletedTask;
    }
}