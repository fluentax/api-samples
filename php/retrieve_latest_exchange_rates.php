<?php
define("CLIENT_SECRETS_FILE", "client_secret.json");
define("TOKEN_ENDPOINT", "https://sso.fluentax.com/auth/realms/fluentax/protocol/openid-connect/token");
define("API_BASE_ADDRESS", "https://fx-api.fluentax.com");

$secrets_string = file_get_contents(CLIENT_SECRETS_FILE);
$secrets = json_decode($secrets_string, false);

$access_token = getAccessToken($secrets);

retrieveLatestExchangeRates($access_token);

function getAccessToken($secrets) {
    $query_data = [
        "grant_type" => "client_credentials",
        "client_id" => $secrets->client_id,
        "client_secret" => $secrets->client_secret,
        "scope" => "fx_api",
    ];
    $data = http_build_query($query_data);
    
    $curl = curl_init();
    curl_setopt_array($curl, array(
        CURLOPT_URL => TOKEN_ENDPOINT,
        CURLOPT_RETURNTRANSFER => true,
        CURLOPT_POST => true,
        CURLOPT_POSTFIELDS => $data
    ));
    
    $response_string = curl_exec($curl);
    $errno = curl_errno($curl);
    if ($errno) {
        return false;
    }
    curl_close($curl);

    $response_json = json_decode($response_string, false);
    
    return $response_json->access_token;
}

function retrieveLatestExchangeRates($access_token) {
    $bank_id = "PLCB";
    $format = "json";
    $request_uri = API_BASE_ADDRESS . "/v1/Banks/$bank_id/DailyRates/Latest?format=$format";

    $curl = curl_init();
    curl_setopt_array($curl, array(
        CURLOPT_URL => $request_uri,
        CURLOPT_RETURNTRANSFER => true,
        CURLOPT_HTTPHEADER => ["Authorization: Bearer $access_token"]
    ));
    
    $response_string = curl_exec($curl);
    $errno = curl_errno($curl);
    if ($errno) {
        return false;
    }
    curl_close($curl);

    $response_json = json_decode($response_string, false);
    
    echo json_encode($response_json, JSON_PRETTY_PRINT) . PHP_EOL;
}
?>