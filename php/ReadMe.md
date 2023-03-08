## Prerequisites

- PHP 8.2 or greater
- cURL

  - Uncomment the following in your `php.ini`:
    ```ini
    ;extension=curl
    ```
  - Download `cacert.pem` from https://curl.se/docs/caextract.html to `C:\php\extras\ssl`. In your `php.ini` make the following changes:

    ```ini
    [curl]
    curl.cainfo = "C:\php\extras\ssl\cacert.pem"

    [openssl]
    openssl.cafile = "C:\php\extras\ssl\cacert.pem"
    ```

### Setting up your project and running code samples

1.  Acquire credentials (client ID and client secret) from Fluentax.
2.  Create a client_secrets.json file associated with your credentials inside the folder where the samples are located:
    ```json
    {
      "client_id": "YOUR_CLIENT_ID",
      "client_secret": "YOUR_CLIENT_SECRET"
    }
    ```
3.  Run the sample from the command line:

    `php sample.php`

4.  Most samples print something to STDOUT.

## Samples in this directory:

### [Retrieve the latest exchange rates for a given bank](/php/retrieve_latest_exchange_rates.php)

Method: DailyRates/Latest<br>
Description: The following code sample performs the authentication to acquire an access token and then calls the Exchange Rate API's <code>latest</code> method to retrieve the latest available exchange rates for the given bank.
