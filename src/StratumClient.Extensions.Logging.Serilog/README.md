# Stratum Client Serilog Extensions

### This package allows the StratumClient to log to a Serilog ILogger instance.

#### Development Branch
![Stratum-Client-DEV-NugetVersion](https://img.shields.io/nuget/vpre/StratumClient.Extensions.Logging.Serilog)
#### Release Branch
![Stratum-Client-RELEASE-NugetVersion](https://img.shields.io/nuget/v/StratumClient.Extensions.Logging.Serilog)

Example usage:
```CS
var client = new StratumClient()
    .UseSerilog(
        new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger());
```