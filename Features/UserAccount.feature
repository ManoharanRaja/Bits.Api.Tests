Feature: UserAccount

As a user, I want to test the following API endpoints:

1. Creates a new user account - https://mzo5slmo45.execute-api.eu-west-2.amazonaws.com/v1/users
2. Get user account details based on provided user ID - https://mzo5slmo45.execute-api.eu-west-2.amazonaws.com/v1/users/%3CuserId

Scenario: Create User with Valid Data
    Given I have a valid API key
    And I have a valid user payload
    When I send a POST request to "/users"
    Then the response status code should be 200
    And the response status should be "Success"

Scenario: Get User Details with Valid User ID
    Given I have a valid API key
    And I have a valid user ID
    When I send a GET request to "/users/<userId>"
    Then the response status code should be 200
    And the response status should be "Success"

Scenario: Unauthorized Access with Invalid API Key for POST
    Given I have a invalid API key
    And I have a valid user payload
    When I send a POST request to "/users"
    Then the response status code should be 401
    And the response status should be "Success"

Scenario: Unauthorized Access with Invalid API Key for GET
    Given I have a invalid API key
    And I have a valid user ID
    When I send a GET request to "/users/<userId>"
    Then the response status code should be 401
    And the response status should be "Success"

Scenario Outline: Verifying created user's initial status by rating
    Given I have a valid API key
    And I have a user payload with "<title>", "<firstName>", "<lastName>", "<dateOfBirth>", "<email>", "<password>", and <rating> 
    When I send a POST request to "/users"
    Then the response status code should be 200
    And the response status should be "Success"
    And the user initial status should be "<status>"
Examples: 
 | title | firstName | lastName | dateOfBirth | email                         | password        | rating | status   |
 | Mr    | AJ        | JP       | 1990-11-11  | mrajjp@email.com              | secret password | 0      | rejected |
 | Mrs   | May       | Day      | 1960-12-11  | mrsmayday@email.co.uk         | secret password | 1      | new      |
 | Miss  | April     | Amber    | 2000-02-29  | missaprilamber@email.com      | secret password | 4      | new      |
 | Ms    | FirstName | LastName | 1990-12-11  | msfirstnamelastname@email.com | secret password | 5      | active   |
 |       | James     | Bond     | 2008-02-29  | mxjamesbond@email.com         | secret password | 9      | active   |
 | Mx    | James     | Bond     | 2008-01-29  | mxjamesbond@email.com         | secret password | 10     | active   |

Scenario Outline: Create user with Invalid Rating
    Given I have a valid API key
    And I have a user payload with "<title>", "<firstName>", "<lastName>", "<dateOfBirth>", "<email>", "<password>", and <rating> 
    When I send a POST request to "/users"
    Then the response status code should be 400
Examples: 
 | title | firstName | lastName | dateOfBirth | email                 | password        | rating | 
 | Mx    | James     | Bond1    | 2008-02-28  | mxjamesbond@email.com | secret password | 11     |
 | Mx    | James     | Bond2    | 2008-02-28  | mxjamesbond@email.com | secret password | -1     |

Scenario Outline: Create user with Invalid Date Of Birth
    Given I have a valid API key
    And I have a user payload with "<title>", "<firstName>", "<lastName>", "<dateOfBirth>", "<email>", "<password>", and <rating> 
    When I send a POST request to "/users"
    Then the response status code should be 400
    And the error response errorType should be "BadRequest" 
    And the error response errorMessage should be "Validation error - date of birth must be in YYYY-MM-DD format"
Examples: 
 | title | firstName | lastName    | dateOfBirth | email                       | password        | rating |
 | Mr    | DOB1      | DateOfBirth | 2008/01/30  | mrdob1dateofbirth@email.com | secret password | 7      |
 | Ms    | DOB4      | DateOfBirth | 20121212    | msdob4dateofbirth@email.com | secret password | 8      |
 | Mr    | DOB2      | DateOfBirth | 2008-02-30  | mrdob2dateofbirth@email.com | secret password | 5      |
 | Ms    | DOB3      | DateOfBirth | 2008-24-28  | msdob3dateofbirth@email.com | secret password | 6      |
 
Scenario Outline: Create user with Invalid Title
    Given I have a valid API key
    And I have a user payload with "<title>", "<firstName>", "<lastName>", "<dateOfBirth>", "<email>", "<password>", and <rating> 
    When I send a POST request to "/users"
    Then the response status code should be 400
    And the error response errorType should be "BadRequest" 
    And the error response errorMessage should be "Validation error - unknown title"
Examples: 
 | title  | firstName | lastName     | dateOfBirth | email                       | password        | rating |
 | Doctor | Title     | InvalidTitle | 2005-10-18  | titleinvalidtitle@email.com | secret password | 1      |

Scenario Outline: Create user with Invalid First Name
   Given I have a valid API key
   And I have a user payload with First Name containing <characters> characters
   When I send a POST request to "/users"
   Then the response status code should be 400
   And the error response errorType should be "BadRequest" 
   And the error response errorMessage should be "__ERR_FNAME_INVALID__"
   #And the error response errorMessage should be "Validation error - first name must be between 2 and 255 characters"
Examples: 
| characters |
| 1          |
| 256        |

Scenario Outline: Create user with Invalid Last Name
   Given I have a valid API key
   And I have a user payload with Last Name containing <characters> characters
   When I send a POST request to "/users"
   Then the response status code should be 400
   And the error response errorType should be "BadRequest" 
   And the error response errorMessage should be "Validation error - last name must be between 2 and 255 characters"
Examples: 
| characters |
| 1          |
| 256        |

Scenario Outline: Create user with Invalid E-mail addresses
    Given I have a valid API key
    And I have a user payload with "<title>", "<firstName>", "<lastName>", "<dateOfBirth>", "<email>", "<password>", and <rating> 
    When I send a POST request to "/users"
    Then the response status code should be 400
    And the error response errorType should be "BadRequest" 
    And the error response errorMessage should be "Validation error - must provide valid e-mail add"

Examples: 
 | title | firstName     | lastName | dateOfBirth | email                | password        | rating |
 | Mr    | InvalidEmail1 | Email    | 2005-10-18  | invalidemail.com     | secret password | 4      |
 | Miss  | InvalidEmail2 | Email    | 2005-10-18  | @example.com         | secret password | 5      |
 | Mx    | InvalidEmail3 | Email    | 2005-10-18  | InvalidEmail3        | secret password | 2      |
 | Ms    | InvalidEmail4 | Email    | 2005-10-18  | Abc..123@example.com | secret password | 6      |
 
Scenario Outline: Create user without required fileld
    Given I have a valid API key
    And I have a user payload without "<field>" field
    When I send a POST request to "/users"
    Then the response status code should be 400
    And the error response errorType should be "BadRequest" 
    And the error response errorMessage should be "<errorMessage>"

Examples: 
 | field       | errorMessage                               |
 | firstName   | Validation error - firstName is required   |
 | lastName    | Validation error - lastName is required    |
 | dateOfBirth | Validation error - dateOfBirth is required |
 | email       | Validation error - email is required       |
 | password    | Validation error - password is required    |
 | rating      | Validation error - rating is required      |

Scenario: Create User with Empty Json body
   Given I have a valid API key
   And I have a empty user payload 
   When I send a POST request to "/users"
   Then the response status code should be 400
   And the error response errorType should be "BadRequest" 

Scenario: Create User with Additional fields not defined in the requirements
   Given I have a valid API key
   And I have a user payload with Additional fields
   When I send a POST request to "/users"
   Then the response status code should be 200

Scenario: Get User Details with Invalid User ID
    Given I have a valid API key
    And I have a invalid user ID
    When I send a GET request to "/users/<userId>"
    Then the response status code should be 400
    And the error response errorType should be "Not Found" 
    And the error response errorMessage should be "User not found"