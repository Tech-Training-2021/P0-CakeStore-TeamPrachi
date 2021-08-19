using System;
using System.IO;
using System.Collections.Generic;
using CakeShopLibrary;


namespace CakeShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            Functions f = new Functions();

            var option = "";

            do
            {
                Console.WriteLine("Choose Your Options :\n1 - Order Now\n2 - Update Customer Details\n3 - Delete Customer Details\n4 - Search\n5 - Exit\n");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        customer.AddCustomer();
                        break;
                    case "2":
                        customer.UpdateCustomer();
                        break;
                    case "3":
                        customer.DeleteCustomer();
                        break;
                    case "4":
                        Console.WriteLine("\n1. Search By Customer Name \n2. Display all history of a customer");
                        int ch = int.Parse(Console.ReadLine());
                        if (ch == 1)
                            f.SearchByName();
                        else if (ch == 2)
                            f.allCustomerHistory();
                        break;
                        
                    case "5":
                        Console.WriteLine("Thank You!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Wrong Option!");
                        break;
                }
            } while (option != "4");


            Console.ReadLine();

        }
    }
}
