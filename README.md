# BankingApp

BankingApp is a .NET Core WPF application that simulates a simple banking system. It provides functionalities to manage bills, customers, and transactions within a virtual banking environment.

## Database Structure

The application uses a SQL Server database with the following tables:



1. **Bills**
    - BillId(PK)
    - CustomerId (FK)
    - DateIssued
    - DueDate
    - AmountDue
    - BillStatus

2. **Customers**
    - CustomerId (PK)
    - FirstName
    - LastName
    - DateOfBirth
    - Email
    - Address
    - PhoneNumber
    - AccountType

3. **Transactions**
    - TransactionId (PK)
    - UserId (FK)
    - TransactionAmount
    - TransactionType
    - TransactionDate
    - CustomerId (FK)

4. **Users** (which represents bankers)
    - UserId (PK)
    - Username
    - Password
	
5. **Parameters**
	- Type (PK)
	- Code (PK)
	- Description 
	- Active

## App.config

Please note that this project uses an `App.config` file for database connection strings, which is not included in the repository for security reasons. To run this project locally, you will need to create an `App.config` file and include your own database connection string.

## Running the application

To run the application, navigate to the project directory and use the `dotnet run` command.
