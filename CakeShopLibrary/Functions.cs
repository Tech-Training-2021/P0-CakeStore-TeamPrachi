using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CakeShopLibrary
{
    public class Functions
    {
        public void SearchByName()
        {
            string _path = @"C:\PROJECT\CakeShop\CakeShop\Customer.json";
            var json = File.ReadAllText(_path);
            var jsonObj = JObject.Parse(json);
            JArray customerArray = new JArray();
            customerArray = jsonObj.GetValue("customers") as JArray;

            Console.WriteLine("Enter Name to Search: ");

            string customer_name = Console.ReadLine();

            foreach (var c in customerArray)
            {
                if (c["customerName"].ToString() == customer_name)
                {
                    Console.WriteLine("Customer Name is: " + c["customerName"]);
                    Console.WriteLine("Customer address is: " + c["customerAddress"]);
                    Console.WriteLine("Customer Contact No. is: " + c["customerContact"]);
                    Console.WriteLine("\n");
                }
            }
        }

        public void allCustomerHistory()
        {
            string _path = @"C:\PROJECT\CakeShop\CakeShop\Order.json";
            var json = File.ReadAllText(_path);
            var jsonObj = JObject.Parse(json);
            JArray orderArray = new JArray();
            orderArray = jsonObj.GetValue("Details") as JArray;

            Console.WriteLine("Enter Customer ID to Search his Order History: ");

            var customerId = int.Parse(Console.ReadLine());
            bool c = true;
            
            foreach (var customer in orderArray.Where(obj => obj["id"].Value<int>() == customerId))
            {
                if (c)
                {
                    Console.WriteLine("Customer Name: " + customer["customerName"].ToString());
                    Console.WriteLine("Customer Contact: " + customer["customerContact"].ToString());
                    c = false;
                }
                int i = 0;
                foreach (var order in customer)
                {
                    if (i == 4)
                    {
                        Console.Write(order+"\n");
                        break;
                    }
                    i++;
                }
            }
        }
    }
}
