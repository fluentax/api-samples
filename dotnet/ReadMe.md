## Prerequisites

- .NET 10.0 SDK

### Setting up your project and running code samples

1.  Acquire credentials (client ID and client secret) from Fluentax.
2.  Run the following commands in the sample directory to set up the credentials:
    ```ps
    dotnet user-secrets set "Auth:ClientId" "[YourClientId]" --id FluentaxSamples
    dotnet user-secrets set "Auth:ClientSecret" "[YourClientSecret]" --id FluentaxSamples
    ```
3.  Run the sample from the command line:

    ```ps
    dotnet run retrieve-latest-exchange-rates.cs
    ```

4.  Most samples print something to STDOUT.

## Samples in this directory:

### [Retrieve the latest exchange rates for a given bank](/dotnet/retrieve-latest-exchange-rates.cs)

Method: `DailyRates/Latest`<br>
Description: This sample uses the client credentials flow to acquire an access token, then calls the Exchange Rate API's `latest` method to retrieve the most recent exchange rates for the specified bank.