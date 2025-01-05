## Prerequisites

- Node.js 21 or greater
- npm 10 or greater

### Setting up your project and running code samples

1.  Acquire credentials (client ID and client secret) from Fluentax.
2.  Create a `.env` file associated with your credentials inside the folder where the samples are located:
    ```sh
    CLIENT_ID=your-client-id
    CLIENT_SECRET=your-client-secret
    ```
3.  Install dependencies from the command line:

    `npm install`

4.  Run the sample from the command line and set command-line arguments as necessary:

    `node retrieveLatestExchangeRates`

5.  Most samples print something to console.

## Samples in this directory:

### [Retrieve the latest exchange rates for a given bank](/node/retrieveLatestExchangeRates.js)

Method: DailyRates/Latest<br>
Description: The following code sample performs the authentication to acquire an access token and then calls the Exchange Rate API's <code>latest</code> method to retrieve the latest available exchange rates for the given bank.