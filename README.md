# WebScraper

### A tool for scraping dynamic websites.

This tool uses a configuration file specifying a website's flow, which simulates user activity.
Please read the technical report section 4.1.3 Configuration file for configuration specifications.

## Setup

**Requirements:** .NET 7.0

- ### Database

The database is created in two steps. The first step is to create initial migration.
The second step is to create the database.

Commands below need to be run in the **WebScraper.Database** directory or the argument --project must contain a path to the database project.

1. `dotnet ef migrations add InitialCreate --context WebScraperDbContext` - creates an initial migration
2. `dotnet ef database update --context WebScraperDbContext` - creates the database according to the migration

To delete an already existing database, use the following command:

- `dotnet ef database drop --context WebScraperDbContext`

## Usage

**1. Running WebScraper as a standalone application in the command line**

Available arguments: 
- -f path/to/config or --filename /path/to/config - Required. Specifies configuration file to be used.
- -h or --headless - Turn on the headless option for browsers (Firefox and Chrome only).
- -m or --maximize - Maximizes the browser (Chrome only).
- --help - Prints help message.

Examples:
- `dotnet run -- -f path/to/configuration`
- `dotnet run -- -f path/to/configuration -h -m`

**2. Running a WebScraper WebApp**

Run following command in the **WebScraper.WebApp** directory: `dotnet run`



