#!/usr/bin/env python3

###
#
# This script demonstrates how the latest exchange rates can be retrieved from the Fluentax Exchange Rates API.
# Usage:
# python retrieve_latest_exchange_rates.py
#
###

import json
import urllib.parse
import urllib3
from urllib3.poolmanager import PoolManager

# The CLIENT_SECRETS_FILE variable specifies the name of a file that contains
# your credentails for the API (client_id and client_secret).
CLIENT_SECRETS_FILE = 'client_secret.json'

TOKEN_ENDPOINT = 'https://sso.fluentax.com/auth/realms/fluentax/protocol/openid-connect/token'
API_BASE_ADDRESS = 'https://fx-api.fluentax.com'


def read_secrets() -> dict[str, str]:
    try:
        with open(CLIENT_SECRETS_FILE, mode='r') as f:
            secrets: dict[str, str] = json.loads(f.read())
            return secrets
    except FileNotFoundError:
        return {}


def get_access_token(http: PoolManager) -> str:
    secrets = read_secrets()
    client_id = secrets['client_id']
    client_secret = secrets['client_secret']
    fields: dict[str, str] = {'grant_type': 'client_credentials', 'client_id': client_id,
                              'client_secret': client_secret, 'scope': 'fx_api'}

    token_response = http.request(
        'POST',
        TOKEN_ENDPOINT,
        fields=fields,
        encode_multipart=False
    )
    token_response_data = json.loads(token_response.data.decode('utf-8'))
    access_token: str = token_response_data['access_token']

    return access_token


def retrieve_latest_exchange_rates():
    http = urllib3.PoolManager()

    access_token = get_access_token(http)

    bank_id = 'PLCB'
    format = 'json'
    latest_rates_request_url = urllib.parse.urljoin(
        API_BASE_ADDRESS, f'v1/Banks/{bank_id}/DailyRates/Latest?format={format}')

    latest_rates_response = http.request(
        'GET',
        latest_rates_request_url,
        headers={'Authorization': f'bearer {access_token}'}
    )

    latest_rates_response_data = json.loads(
        latest_rates_response.data.decode('utf-8'))
    print(json.dumps(latest_rates_response_data, indent=2))


if __name__ == '__main__':
    retrieve_latest_exchange_rates()
