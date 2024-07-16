# Bits.Api.Tests

Home assignment for QA Engineer

Instructions
Below you'll find a task for a developer to implement two new API endpoints. Please
review those requirements. Your assignment consists of three parts:

1. Imagine you are shown this task description before development has commenced,
e.g. during refinement before next sprint begins. Provide feedback regarding
requirements clarity and acceptance criteria completeness. Would you change
anything, or do you believe it's ready for development?
2. Prepare test scenarios covering the acceptance criteria, as well as other edge
cases you've identified.
3. Execute those scenarios using your preferred test automation tool(s) against
the implementation that the developer has marked as "ready for QA". Report any
identified issues.

Endpoints are live in QA environment:
POST https://mzo5slmo45.execute-api.eu-west-2.amazonaws.com/v1/users
GET https://mzo5slmo45.execute-api.eu-west-2.amazonaws.com/v1/users/<userId>
API Key: GombImxOhMCa8AqMmNM9KEFwaSHSFHty

Development task description
Develop two new API endpoints:
1. Creates a new user account
2. Get user account details based on provided user ID

Example request to create a new user:

{
"title": "Mr",
"firstName": "John",
"lastName": "Doe",
"dateOfBirth": "1987-06-04",
"email": "somefake@email.com",
"password": "super secret password",
"rating": 10
}

Example valid response for both endpoints (2xx code):

{
"status": "Success",
"data": {
"userId": "bcc1e22a-9b5f-4d51-a815-91e1f47d6fb6",
"status": "active",
"title": "Mr",
"firstName": "John",
"lastName": "Doe",
"dateOfBirth": "1987-06-04",
"email": "somefake@email.com",
"rating": 10
},
"error": null
}

Example error response for both endpoints (4xx code):
{
"errorType": "SomeErrorType",
"errorMessage": "Human readable description"
}

Acceptance criteria
1. Both endpoints should require authorization using Authorization header
containing the API key, return HTTP 401 on bad token.
2. Follow request/response format JSON outlined above.
3. Endpoint to create user should accept POST requests with data as JSON in the
request body.
4. Validate input fields:
1. title - optional field, but if it's set - only accept values:
1. Mr
2. Mrs
3. Miss
4. Ms
5. Mx
2. firstName - required, string between 2 and 255 characters
3. lastName - required, string between 2 and 255 characters
4. dateOfBirth - required, YYYY-MM-DD format
5. email - required, accept only valid e-mail addresses; e-mails should be
unique - only one user account per e-mail address
6. password - required
7. rating - required, integer number from 0 to 10
5. When user account is created, the initial status should be determined based on
rating . If the rating is 0, set status to rejected . If rating is 5 or higher,
set status to active . Otherwise, set to new .
6. Endpoint to get user details should accept user ID in URL and accept GET
requests
7. Server response should contain an appropriate HTTP code