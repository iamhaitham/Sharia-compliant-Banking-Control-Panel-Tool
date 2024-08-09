# Banking Control Panel Tool
This is an assignment I have worked on, and to sum up the requirements alongside my implementation:
- I only have to create the API.

- There will be a `User`, and the roles they can have are either a `User` or an `Admin`.

- Users should be able to register and login to the system.

- Admins can add new `Client`s, they can also filter them, sort them, and get suggestions. The suggestions are actually the latest three searches (queries) the admin has done over the clients.
- A client will have the following properties: *Email*, *First name*, *Last name*, *Personal Id*, *Profile photo*, *Mobile number*, *Sex (Male, Female)*, *Address with subfields( Country, City, Street, Zip Code)*, *Account (Account can be added as many as necessary)*.
* These are the validations for a client entity:
    - Email: Should be required and email format.
    - First name: Should be required and less than 60 characters. 
    - Last name: Should be required and less than 60 characters.
    - Mobile number: Should be correct format with country code (you can use some library as well).
    - Personal id: Should be required and it should be exactly 11 characters.
    - Sex: Should be required.
    - Accounts: At least one account is required.


## Architecture
- I had to read a little bit about Clean (Onion) Architecture, and I have followed the rules to adapt to it. 
    - Here are the layers of my solution:
        - Core (An independent one)
        - Infrastructure (References: Core)
        - Business (References: Core, and Infrastructure)
        - API (References: Business)


## Controllers
- There are two controllers:
    - `UserController`, and it can be used by anyone. It has the following functionalities: Login and Registration.
    - `ClientController`, and it can only be used by Admins. It has the following functionalities: Register new clients, Query the clients with some filters parameters, and Get suggestions (last three queries).


## Possible enhancements
- Add unit tests.
- Add integration tests (For example, with a real database).
- Add logging instead of Console.WriteLine the raised errors.


## How to run
1. While on the root level of the solution (The same level of the ".sln" file), run the migrations so you can have the database locally:
```
dotnet ef database update --project Infrastructure --startup-project API
```

2. Being on the same root level again, run the following command to run the application:
```
dotnet run --project API
```

3. Now you can go to the localhost URL that is displayed on the terminal, but make sure to add "/swagger/index.html" at the end. 
Example: `http://localhost:5294/swagger/index.html`.
