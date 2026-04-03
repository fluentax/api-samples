## Prerequisites

- .NET 10.0 SDK

## Samples in this directory:

### [Retrieve the latest exchange rates for a given bank](/dotnet/retrieve-latest-exchange-rates.cs)

#### Setup
Run the following commands in the sample directory to set up the credentials:
```ps
dotnet user-secrets set "Auth:ClientId" "[YourClientId]" --file retrieve-latest-exchange-rates.cs
dotnet user-secrets set "Auth:ClientSecret" "[YourClientSecret]" --file retrieve-latest-exchange-rates.cs
```

#### Run sample application
```ps
dotnet run retrieve-latest-exchange-rates.cs
```

Method: `DailyRates/Latest`<br>
Description: The following code sample performs the authentication to acquire an access token and then calls the Exchange Rate API's `latest` method to retrieve the latest available exchange rates for the given bank.
