# BankingApp

BankingApp is a .NET Core WPF application that simulates a simple banking system.

## Database Structure

The application uses a SQL Server database with the following tables:

1. **Actions**
    - ActionId (PK)
    - UserId (FK)
    - CustomerId (FK)
    - ActionType
    - ActionDate
    - Details

2. **Bills**
    - BillId(PK)
    - CustomerId (FK)
    - DateIssued
    - DueDate
    - AmountDue
    - BillStatus

3. **Customers**
    - CustomerId (PK)
    - FirstName
    - LastName
    - DateOfBirth
    - Email
    - Address
    - PhoneNumber
    - AccountType

4. **Transactions**
    - TransactionId (PK)
    - UserId (FK)
    - TransactionAmount
    - TransactionType
    - TransactionDate
    - CustomerId (FK)

5. **Users** (which represents bankers)
    - UserId (PK)
    - Username
    - Password

## App.config

Please note that this project uses an `App.config` file for database connection strings, which is not included in the repository for security reasons. To run this project locally, you will need to create an `App.config` file and include your own database connection string.

## Running the application

To run the application, navigate to the project directory and use the `dotnet run` command.
