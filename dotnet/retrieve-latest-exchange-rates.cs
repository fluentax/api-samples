#:property TargetFramework=net10.0
#:property PublishAot=false

#:package Microsoft.Extensions.Configuration.EnvironmentVariables@10.0.5
#:package Microsoft.Extensions.Configuration.UserSecrets@10.0.5

using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

var builder = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddUserSecrets("FluentaxSamples");

var config = builder.Build();

using var httpClient = new HttpClient { BaseAddress = new Uri(config["ApiBaseAddress"] ?? throw new InvalidOperationException("Missing ApiBaseAddress configuration")) };

using var authResponse = await httpClient.PostAsync(
    config["Auth:TokenEndpoint"] ?? throw new InvalidOperationException("Missing Auth:TokenEndpoint configuration"),
    new FormUrlEncodedContent(
        [
            new("grant_type", "client_credentials"),
            new("client_id", config["Auth:ClientId"] ?? throw new InvalidOperationException("Missing Auth:ClientId configuration")),
            new("client_secret", config["Auth:ClientSecret"] ?? throw new InvalidOperationException("Missing Auth:ClientSecret configuration")),
            new("scope", "fx_api")
        ]));

authResponse.EnsureSuccessStatusCode();

var authJson = await authResponse.Content.ReadFromJsonAsync<TokenResponse>();

httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authJson!.AccessToken);

using var latestRatesResponse = await httpClient.GetAsync("v1/Banks/PLCB/DailyRates/Latest?format=csv");

latestRatesResponse.EnsureSuccessStatusCode();

var latestRates = await latestRatesResponse.Content.ReadAsStringAsync();

Console.WriteLine(latestRates);

Console.ReadKey();

record TokenResponse([property: JsonPropertyName("access_token")] string AccessToken);