# accenture-technical-assessment

## Functional Requirements

1. When the endpoint `GET /brew-coffee` is called, the API returns:
   - Status: `200 OK`
   - Response body: JSON object with current date/time formatted as ISO-8601

   ```json
   {
     "message": "Your piping hot coffee is ready",
     "prepared": "2021-02-03T11:56:24+0900"
   }

2. On every fifth call to the endpoint defined in #1, the endpoint should return:
   - Status: 503 Service Unavailable
   - Response body: empty
   - This signifies that the coffee machine is out of coffee.
  
3. If the date is April 1st, then all calls to the endpoint defined in #1 should return:
   - Status: 418 I'm a teapot
   - Response body: empty
   - This signifies that the endpoint is not brewing coffee today.
   - Reference: <https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/418>
