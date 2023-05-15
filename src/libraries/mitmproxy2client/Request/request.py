import requests
import json
import time
import os


# Load environment variables for request headers
cookie = os.getenv("REQUEST_HEADER_COOKIE")
csrf = os.getenv("REQUEST_HEADER_CSRF")
agent = os.getenv("REQUEST_HEADER_USER_AGENT")
content = os.getenv("REQUEST_HEADER_CONTENT_TYPE")

# Create the request headers dictionary
requestHeader = {'Content-Type': content, 'User-Agent': agent,
                 'X-CSRF-Token': csrf, 'Cookie': cookie}

# Define the proxy settings
proxies = {
    'http': 'http://MitmProxy:8080',
    'https': 'http://MitmProxy:8080',
}

# Prepare the data for the request if exists
dataForRequest = json.loads(os.getenv("DATA_FOR_REQUEST"))
if dataForRequest:
    data = {}
    for key, value in dataForRequest.items():
        data[key] = value

# Construct the URL for getting user IDs
urlGetId = os.getenv("BASE_URL")+os.getenv("END_POINT")

# Get the value of the environment variable 'ITERATE_FIRST_REQUEST'
ITERATE_FIRST_REQUEST = os.getenv('ITERATE_FIRST_REQUEST')

# If first request is a list of objects and need to iterate said list
if ITERATE_FIRST_REQUEST and ITERATE_FIRST_REQUEST.lower() in ("true", "1"):

    # Send the request to fetch user IDs
    r = requests.get(url=urlGetId, timeout=int(os.getenv("REQUESTS_TIMEOUT")),
                    headers=requestHeader, proxies=proxies, json=data, verify=os.getenv("SSL_CERT_PATH"))
    print(os.getenv("ITERATE_FIRST_REQUEST"))

    # Parse the response JSON
    users = json.loads(r.text)

    # Iterate through the users, fetching their CV data
    for user in users:
        urlGetId = os.getenv("BASE_URL") + \
            f'v3/cvs/{user["user_id"]}/{user["default_cv_id"]}'

        # Send the request to fetch the user's CV data
        r = requests.get(url=urlGetId, timeout=int(os.getenv("REQUESTS_TIMEOUT")), headers=requestHeader,
                     proxies=proxies, verify=os.getenv("SSL_CERT_PATH"))

        # Add a delay between requests to avoid rate-limiting
        time.sleep(0.5)
else:
    # Send a request if not fetching CV partner data
    r = requests.get(url=urlGetId, timeout=int(os.getenv("REQUESTS_TIMEOUT")), headers=requestHeader,
    proxies=proxies, verify=os.getenv("SSL_CERT_PATH"))

# Print the completion message
print("Requests completed, stopping mitmproxy")

# Send a request to stop the mitmproxy
url = "http://mitmproxy:3000/stopmitmdump"

response = requests.get(url)
