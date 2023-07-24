//using System;
//using BankingApp.BLL;
//using BankingApp.DAL;
//using System.Collections.Generic;
//using BankingApp.Domain;

//namespace BankingApp.PasswordUpdater
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            UserService userService = new UserService();


//            //// Test the UpdateUserPassword method
//            //userService.UpdateUserPassword(2, "ahmet123sifre");
//            //Console.WriteLine("Password updated successfully.");

//            //// Test the FetchUser method
//            //var user = userService.FetchUser("kahvecikaan123", "thisismypassword");
//            //if (user != null)
//            //{
//            //    Console.WriteLine("User fetched successfully.");
//            //    Console.WriteLine("Username: " + user.Username);
//            //    Console.WriteLine("Password: " + user.Password); // Should be hashed
//            //}
//            //else
//            //{
//            //    Console.WriteLine("User not found.");
//            //}

//            userService.ConvertAllPasswordsToHashed();
//            Console.WriteLine("All passwords updated successfully.");

//            //BillData billData = new BillData();
//            //BillService billService = new BillService(billData);

//            //// Create a new bill
//            //Bill newBill = new Bill
//            //{
//            //    CustomerId = 11,
//            //    DateIssued = DateTime.Now,
//            //    DueDate = DateTime.Now.AddDays(15),
//            //    AmountDue = 1000.00m,
//            //    BillStatus = "Pending"
//            //};

//            //try
//            //{
//            //    billService.InsertBill(newBill);
//            //    Console.WriteLine($"New bill inserted with ID: {newBill.BillId}");
//            //}
//            //catch (Exception ex)
//            //{
//            //    Console.WriteLine($"Error while inserting a new bill: {ex.Message}");
//            //}

//            //// Update the bill
//            //newBill.AmountDue = 900.00m;
//            //newBill.BillStatus = "Paid";

//            //try
//            //{
//            //    if (billService.UpdateBill(newBill))
//            //    {
//            //        Console.WriteLine("Bill updated successfully");
//            //    }
//            //}
//            //catch (Exception ex)
//            //{
//            //    Console.WriteLine($"Error while updating the bill: {ex.Message}");
//            //}

//            //// Fetch all bills of a customer
//            //try
//            //{
//            //    List<Bill> bills = billService.FetchBillsByCustomer(newBill.CustomerId);

//            //    Console.WriteLine($"Found {bills.Count} bills for customer ID: {newBill.CustomerId}");
//            //    foreach (var bill in bills)
//            //    {
//            //        Console.WriteLine($"Bill ID: {bill.BillId}, Amount Due: {bill.AmountDue}, Status: {bill.BillStatus}");
//            //    }
//            //}
//            //catch (Exception ex)
//            //{
//            //    Console.WriteLine($"Error while fetching bills: {ex.Message}");
//            //}

//            // Delete the bill
//            //try
//            //{
//            //    billService.DeleteBill(newBill.BillId);
//            //    Console.WriteLine("Bill deleted successfully");
//            //}
//            //catch (Exception ex)
//            //{
//            //    Console.WriteLine($"Error while deleting the bill: {ex.Message}");
//            //}


//        }
//    }
//}


using System;
using System.Linq;
using BankingApp.BLL;
using BankingApp.DAL;

namespace BankingApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var userData = new UserData();
            var userService = new UserService(userData);
            userService.ConvertAllPasswordsToHashed();
            Console.WriteLine("Success!");


            // Instantiate data and service classes
        //    var customerData = new CustomerData();
        //    var customerService = new CustomerService(customerData);

        //    Console.WriteLine("Enter Customer ID, First Name, or Last Name (leave empty to skip):");
        //    Console.Write("Customer ID: ");
        //    string customerIdInput = Console.ReadLine();
        //    int? customerId = null;
        //    if (int.TryParse(customerIdInput, out int customerIdParsed))
        //    {
        //        customerId = customerIdParsed;
        //    }

        //    Console.Write("First Name: ");
        //    string firstName = Console.ReadLine();

        //    Console.Write("Last Name: ");
        //    string lastName = Console.ReadLine();

        //    var customers = customerService.SearchCustomers(customerId, firstName, lastName);

        //    if (customers.Any())
        //    {
        //        foreach (var customer in customers)
        //        {
        //            Console.WriteLine($"ID: {customer.CustomerId}, First Name: {customer.FirstName}, Last Name: {customer.LastName}, DOB: {customer.DateOfBirth:d}, Email: {customer.Email}, Address: {customer.Address}, Phone: {customer.PhoneNumber}, Account Type: {customer.AccountType}");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("No customers found.");
        //    }
        }
    }
}
