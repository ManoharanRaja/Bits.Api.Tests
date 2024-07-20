using Bits.Api.Tests.DataModel;
using Bits.Api.Tests.Support;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

namespace Bits.Api.Tests.StepDefinitions
{
    [Binding]
    public sealed class UserAccountStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private string _apiKey;
        
        public UserAccountStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _client = new RestClient("https://mzo5slmo45.execute-api.eu-west-2.amazonaws.com/v1");

        }

        [Given(@"I have a valid API key")]
        public void GivenIHaveAValidAPIKey()
        {
            _apiKey = "GombImxOhMCa8AqMmNM9KEFwaSHSFHty";
        }  

        [Given(@"I have a invalid API key")]
        public void GivenIHaveAInvalidAPIKey()
        {
            _apiKey = "GombImx";
        }

        [Given(@"I have a invalid user ID")]
        public void GivenIHaveAInvalidUserID()
        {
            _scenarioContext["UserId"] = "f40d8b297a4a471aa91aa9e548f5411a";
        }

        [Given(@"I have a valid user ID")]
        public void GivenIHaveAValidUserID()
        {
            _scenarioContext["UserId"] = "f40d8b29-7a4a-471a-a91a-a9e548f5411a";
        }

        [Given(@"I have a valid user payload")]
        public void GivenIHaveAValidUserPayload()
        {
            var user = new User()
            {
                title = "Mr",
                firstName = "John",
                lastName = "Doe",
                dateOfBirth = "1987-06-04",
                email = "test@email.com",
                password = "password123",
                rating = 7
            };
            _scenarioContext["Payload"] = user;
        }

        [Given(@"I have a user payload with ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", and (.*)")]
        public void GivenIHaveAUserPayloadWithAnd(string title, string firstName, string lastName, string dateOfBirth, string email, string password, int rating)
        {
            var user = new User()
            {
                title = title,
                firstName = firstName,
                lastName = lastName,
                dateOfBirth = dateOfBirth,
                email = email,
                password = password,
                rating = rating
            };
            _scenarioContext["Payload"] = user;
        }

        [Given(@"I have a user created as ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", and (.*)")]
        public void GivenIHaveAUserCreatedAsAnd(string title, string firstName, string lastName, string dateOfBirth, string email, string password, int rating)
        {
            GivenIHaveAValidAPIKey();
            GivenIHaveAUserPayloadWithAnd(title, firstName, lastName, dateOfBirth, email, password, rating);
            WhenISendAPOSTRequestTo("/users");
            ThenTheResponseStatusCodeShouldBe(200);
        }


        [Given(@"I have a user payload without ""([^""]*)"" field")]
        public void GivenIHaveAUserPayloadWithoutField(string filedName)
        {
            var payload = DataHelper.GetUserDetailsWithout(filedName);
            
            _scenarioContext["Payload"] = payload;
        }

        [Given(@"I have a empty user payload")]
        public void GivenIHaveAEmptyUserPayload()
        {
            var payload = new { };
            _scenarioContext["Payload"] = payload;  
        }

        [Given(@"I have a invalid user payload")]
        public void GivenIHaveAInvalidUserPayload()
        {

            var payload = @"
                        {
                          ""title"": ""Mr"",
                          ""firstName"": ""John"",
                          ""lastName"": ""Doe"",
                          ""dateOfBirth"": ""1987-06-04"",
                          ""email"": ""test@email.com"",
                          ""password"": ""password123"",
                          ""rating"": 7,
                          ""address"": ""London""
                         "; // Invalid JSON

            _scenarioContext["Payload"] = payload;
        }

        [Given(@"I have a user payload with Additional fields")]
        public void GivenIHaveAUserPayloadWithAdditionalFields()
        {
            var user = new
            {
                title = "Mr",
                firstName = "John",
                lastName = "Doe",
                dateOfBirth = "1987-06-04",
                email = "test@email.com",
                password = "password123",
                rating = 7,
                address = "London" //additional field not defined in the requirements

            };
            _scenarioContext["Payload"] = user;
        }

        [Given(@"I have a user payload with First Name containing (.*) characters")]
        public void GivenIHaveAUserPayloadWithFirstNameContainingCharacters(int numberOfChar)
        {
            var user = new User()
            {
                title = "Mr",
                firstName = DataHelper.GetRandomString(numberOfChar),
                lastName = "Doe",
                dateOfBirth = "1987-06-04",
                email = "test@email.com",
                password = "password123",
                rating = 7
            };
            _scenarioContext["Payload"] = user;
        }

        [Given(@"I have a user payload with Last Name containing (.*) characters")]
        public void GivenIHaveAUserPayloadWithLastNameContainingCharacters(int numberOfChar)
        {
            var user = new User()
            {
                title = "Mr",
                firstName = "John",
                lastName = DataHelper.GetRandomString(numberOfChar),
                dateOfBirth = "1987-06-04",
                email = "test@email.com",
                password = "password123",
                rating = 7
            };
            _scenarioContext["Payload"] = user;
        }



        [When(@"I send a POST request to ""([^""]*)""")]
        public void WhenISendAPOSTRequestTo(string endpoint)
        {
            _request = new RestRequest(endpoint, Method.Post);
            _request.AddHeader("Authorization", $"Bearer {_apiKey}");
            _request.AddJsonBody(_scenarioContext["Payload"]);
            _response = _client.Execute(_request);
            _scenarioContext["Response"] = _response;

        }


        [When(@"I send a GET request to ""([^""]*)""")]
        public void WhenISendAGETRequestTo(string endpoint)
        {
            _request = new RestRequest(endpoint.Replace("<userId>", (string)_scenarioContext["UserId"]), Method.Get);
            _request.AddHeader("Authorization", $"Bearer {_apiKey}");
            _response = _client.Execute(_request);
            _scenarioContext["Response"] = _response;
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            _response = (RestResponse)_scenarioContext["Response"];
            Assert.That(statusCode == (int)_response.StatusCode,$"Expected Response Code is {statusCode}.\nActual Response Code is {(int)_response.StatusCode}.\n {_response.Content}");
        }

        [Then(@"the response status should be ""([^""]*)""")]
        public void ThenTheResponseShouldBe(string status)
        {
            var response = JsonConvert.DeserializeObject<ApiResponse>(_response.Content);
            Assert.That(response.status.Equals(status), $"Expected Response status is {status}.\nActual Response Code is {response.status}.\n {_response.Content}");

        }

        [Then(@"the user initial status should be ""([^""]*)""")]
        public void ThenTheUserInitialStatusShouldBe(string status)
        {
            var response = JsonConvert.DeserializeObject<ApiResponse>(_response.Content);
            Assert.That(response.data.status.Equals(status), $"Expected user initial status is {status}.\nActual user initial status is {response.data.status}.\n {_response.Content}");
        }

        [Then(@"the error response errorType should be ""([^""]*)""")]
        public void ThenTheErrorResponseErrorTypeShouldBe(string type)
        {
            var response = JsonConvert.DeserializeObject<ErrorResponse>(_response.Content);
            Assert.That(response != null, "Response does not contain error");
            Assert.That(response.errorType.Equals(type), $"Expected errorType is {type}.\nActual errorType status is {response.errorType}.\n {_response.Content}");

        }

        [Then(@"the error response errorMessage should be ""([^""]*)""")]
        public void ThenTheErrorResponseErrorMessageShouldBe(string message)
        {
            var response = JsonConvert.DeserializeObject<ErrorResponse>(_response.Content);
            Assert.That(response != null, "Response does not contain error");
            Assert.That(response.errorMessage.Equals(message), $"Expected errorMessage is {message}.\nActual errorMessage is {response.errorMessage}.\n {_response.Content}");

        }
        
    }
}
