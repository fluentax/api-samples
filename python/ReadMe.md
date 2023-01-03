## Prerequisites

- Python 3.10 or greater
- urllib3
  ```
  python -m pip install urllib3
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
3.  Run the sample from the command line and set command-line arguments as necessary:

    `python sample.py --arg1=value1 --arg2=value2 ...`

4.  Most samples print something to STDOUT.

## Samples in this directory:

### [Retrieve the latest exchange rates for a given bank](/python/retrieve_latest_exchange_rates.py)

Method: DailyRates/Latest<br>
Description: The following code sample performs the authentication to acquire an access token and then calls the Exchange Rate API's <code>latest</code> method to retrieve the latest available exchange rates for the given bank.
