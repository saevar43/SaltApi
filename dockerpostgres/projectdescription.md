# Project description

Our merchants have requested a web interface that makes it easier for them to see and refund their transactions.
Please provide us with a RESTful API that can provide the following functionality to the web interface.

* Merchant wants to be able to see a list of their transactions given a specific date.
* Merchant wants to be able to see how much they will be paid given a specific date.
* Merchant wants to be able to refund(void) a transaction.

You will be provided with a Postgres database with simplified demo data.

The database can be started using docker-compose, docker-compose will also start a database admin interface on port 8080 which can be useful to understand the data.

## Database parameters ##

* Database name: test
* User: borgun
* Password: salt
* Table: transactions

Definition of the transactions table:
``` sql
    Id uuid DEFAULT uuid_generate_v4(), -- Unique ID of transaction.
    TransDate TIMESTAMP, -- Transaction date.
    Merchant VARCHAR(7), -- Merchant Identifier.
    Amount DECIMAL(12,2), -- Transaction amount.
    Currency CHAR(3), -- Currency in ISO 4217 format.
    PAN VARCHAR(24), -- Masked card number.
    Voided BOOLEAN DEFAULT '0' -- Has transaction been refunded ?
```

### Notes on the data ###
 * All transactions of a specific merchant are in the same currency (ISK or EUR)
 * Merchant will not get paid for refunded transactions.

### Starting the database ### 
With docker and docker compose installed. In the project folder containing docker-compose.yml run

`>docker-compose up`

This will download the Postgres image and initialize the development database for you.

## Additional requirements ##
 * Use docker, a Salt/Borgun employee should be able to start the web service without any specific developer tools or libraries other than Docker installed.
 * Please write tests and give a description on how to run them.
 * While you are free to write the REST service in a tech stack of your choice we recommend:
   * .NET Core (or .NET 5) (https://dotnet.microsoft.com/learn/aspnet/microservices-architecture)
   * Python and Flask (https://flask.palletsprojects.com/en/1.1.x/quickstart/)

# Turn in #
Please provide us with
 * The code
 * A short description on how you went by solving the project.
 * A description on how to run the code.
 * A description on how to use the service f.ex. in text or in Swagger (OpenAPI).

**Feel free to contact us if you have any questions.**
