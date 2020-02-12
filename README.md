# Payment Gateway
Payment Gateway is written in **ASP.NET Core 3.1**. It was created as a coding assignment submission.

## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites
At a minimum, you will need **ASP.NET Core 3.1** and **Postgres 9** to run the backend application. If you would like to use the API Client, you will need npm and Vue.js.

### Installing
Download and install the following in order to run this project:

| Project | Location |
| ------ | ------ |
| ASP.NET Core | https://dotnet.microsoft.com/download |
| Postgres | https://www.postgresql.org |
| npm | https://www.npmjs.com/get-npm |

#### Postgres Setup
Once Postgres is installed, you will have to create a database and a role in order for the Payment Gateway to function properly. If Postgres was installed with default settings (localhost, 5432), all you have to do is run the following commands:
```sql
CREATE DATABASE PaymentsGateway;
CREATE ROLE paymentsuser WITH PASSWORD 'paymentspass' SUPERUSER LOGIN;
```
The default database settings can be found in `PaymentsGateway/appsettings.json`.

## Running the tests
The test suite is located in the `PaymentGatewayTests` folder. You can run the test suite by running the following command at the root of this project:
```sh
$ dotnet test
```
The test suite will execute, and the output will follow.

## Running the application
You can run either the backend by itself, and use a HTTP client (Postman, etc) to test the PaymentGateway APIs, or you can run the API Client in this repository to spin up a nice GUI to test the APIs.

### Backend
Navigate to the `PaymentGateway` folder and run:
```sh
$ dotnet build
$ dotnet run
```
The application will start and is accessible via `https://localhost:5001`

#### Note
If you run the backend application, and receive a server certificate error, you will need to generate a certificate for localhost. Run these commands first, and then re-run the `dotnet run` command:
```sh
$ dotnet dev-certs https --clean
$ dotnet dev-certs https -t
```

### Frontend
Navigate to `api-client` and run the following commands:
```sh
$ npm install
$ npm run serve
```
The API Client will be accessible via `http://localhost:8080`

## Use Cases
Payment Gateway supports two features: processing payments and retrieving payment details.

### Processing payments
You can process a payment by submitting a `POST` request:
```http
POST /api/payments
```
The request structure should be:
```javascript
{
	"MerchantId": integer,
	"Currency": string,
	"Amount": double,
	"Card" : {
		"CardNumber": string,
		"CardHolderName": string,
		"ExpiryDate": string,
		"CVV": integer
	}
}
```
All fields are mandatory. This endpoint will return a `HTTP 200` if the request was successful, along with a payment identifier and status, or a `HTTP 400` if the request did not pass validation.

### Retrieving payment details
You can retrieve the details of a payment by submitting a `GET` request:
```http
GET /api/payments/[id]
```
Where `[id]` is the payment identifier (returned by the `POST` endpoint).

## Todos

 - Write more tests
 - Add containerization
 - Add authentication
