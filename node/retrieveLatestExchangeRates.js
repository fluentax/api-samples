import dotenv from "dotenv";
dotenv.config();
import { URLSearchParams } from "url";

const TOKEN_ENDPOINT =
  "https://sso.fluentax.com/auth/realms/fluentax/protocol/openid-connect/token";
const API_BASE_ADDRESS = "https://fx-api.fluentax.com";

async function getAccessToken() {
  const clientId = process.env.CLIENT_ID;
  const clientSecret = process.env.CLIENT_SECRET;

  if (!clientId || !clientSecret) {
    throw new Error(
      "Missing CLIENT_ID or CLIENT_SECRET in environment variables."
    );
  }

  const tokenRequestBody = new URLSearchParams({
    grant_type: "client_credentials",
    client_id: clientId,
    client_secret: clientSecret,
    scope: "fx_api",
  });

  const tokenResponse = await fetch(TOKEN_ENDPOINT, {
    method: "POST",
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
    },
    body: tokenRequestBody,
  });

  if (!tokenResponse.ok) {
    const errorText = await tokenResponse.text();
    throw new Error(
      `Token request failed: ${tokenResponse.statusText}, Details: ${errorText}`
    );
  }

  const tokenData = await tokenResponse.json();
  const token = tokenData.access_token;
  return token;
}

async function getLatestRates(token) {
  const bankId = "PLCB";
  const format = "csv";
  const apiUrl = `${API_BASE_ADDRESS}/v1/Banks/${bankId}/DailyRates/Latest?format=${format}`;

  const apiResponse = await fetch(apiUrl, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!apiResponse.ok) {
    const errorText = await apiResponse.text();
    throw new Error(
      `API request failed: ${apiResponse.statusText}, URL: ${apiUrl}, Details: ${errorText}`
    );
  }

  const latestRates = await apiResponse.text();
  console.log(latestRates);
}

(async () => {
  try {
    const token = await getAccessToken();
    await getLatestRates(token);
  } catch (error) {
    console.error("An error occurred:", error.message);
  }
})();
