# Stratum Client

### A stratum v1 client for mining pool participation.

#### Development Branch
[![Build Status](https://dev.azure.com/nellisgowland/StratumClient/_apis/build/status/StratumClient/Publish%20PreRelease) ![Stratum-Client-DEV-NugetVersion](https://img.shields.io/nuget/vpre/StratumClient)
#### Release
[![Build Status](https://dev.azure.com/nellisgowland/StratumClient/_apis/build/status/StratumClient/Publish%20Release) ![Stratum-Client-RELEASE-NugetVersion](https://img.shields.io/nuget/v/StratumClient)

Example usage:
```CS
var client = new StratumClient.V1.StratumClient();

client.OnSubscribeResponse += (subscribeResponse, subscribeRequest) =>
{
    Console.WriteLine($"Subscribed | Extranonce1 : {subscribeResponse.Extranonce1}");

    // On subscribe response, send an authorise message.
    client.Authorize(
        new AuthorizeRequest(
            "94be816cc10c7705dc706dfc19c2f3b8.StratumClient",
            "myPassword"));
};

client.OnAuthorizeResponse += (authorizeResponse, authorizeRequest) =>
{
    Console.WriteLine($"Authorized | Success : {authorizeResponse.Success}");
};

client.OnSetDifficultyRequest += (setDifficultyRequest) =>
{
    Console.WriteLine($"SetDifficulty | Difficulty : {setDifficultyRequest.Difficulty}");
};

client.OnNotifyRequest += (notifyRequest) =>
{
    Console.WriteLine($"Notify | JobId : {notifyRequest.JobId}");
};

// Connect
await client.ConnectAsync(new Uri("tcp://eu.stratum.slushpool.com:3333"));

// Send the initial Subscribe message
client.Subscribe(new SubscribeRequest());
```