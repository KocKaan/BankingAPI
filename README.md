# BankingAPI
Created a simple banking api that has Account/user creation with details and transactions such as withdrawal, deposit and transfer. 
The transactions are logged and every transaction is verified.

-> Has Open API Specification: Swagger.
-> You can create and update accounts
-> Get all acounts as a list

## Design
Adheres to SOLID principles where there are Interfaces that are used for accessing the 
service layer at Controllers. There are as much seperation of concerns as possible thus easy
to scale. Uses auto mappers for dto's therefore no need for manual data transfer between data objects. 
As DTO's are used for every possible case, sensitive Account data's do not reach to the users.


## How to use it
git clone https://github.com/KocKaan/BankingAPI.git

set up directory on your local machine

replace connectionStrings value in appsettings.json with ConnectionString to your own Local/Remote SQL Server DB

You may run "Add-Migration initial" on the Package Manager Console

Run "Update-Database" on the Package Manager Console

To be able to run transactions you have to register a new account. It has to be you bank's settlement
and after creating the settlement account put the account number to "NetCoreBankSettlementAccount": "2842017209" in 
appsettings.json file.

