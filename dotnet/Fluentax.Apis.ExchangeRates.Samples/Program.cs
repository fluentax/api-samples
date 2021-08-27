﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Fluentax.Apis.ExchangeRates.Samples
{
    class Program
    {
        static async Task Main()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .AddUserSecrets<Program>();

            var config = builder.Build();

            string token = null;
            using (var tokenClient = new HttpClient())
            {
                using var tokenRequestContent = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" },
                    { "client_id", config["Auth:ClientId"] },
                    { "client_secret", config["Auth:ClientSecret"] },
                    { "scope", "fx_api" },
                });

                using var response = await tokenClient.PostAsync(config["Auth:TokenEndpoint"], tokenRequestContent);

                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                token = tokenResponse.access_token;
            }

            using var apiClient = new HttpClient
            {
                BaseAddress = new Uri(config["ApiBaseAddress"])
            };

            apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var bankId = "PLCB";
            var format = "csv";
            var latestRates = await apiClient.GetStringAsync($"v1/Banks/{bankId}/DailyRates/Latest?format={format}");

            Console.WriteLine(latestRates);

            Console.ReadKey();
        }
    }

    record TokenResponse
    {
        public string access_token { get; set; }
    }
}
