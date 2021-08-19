using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace CakeShopLibrary
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string Address { get; set; }

        Store s = new Store();
        public const string mobileRegEx = @"^[0-9]{10}$";
        public static bool IsPhoneNbr(string number)
        {
            if (number != null) return Regex.IsMatch(number, mobileRegEx);
            else return false;
        }

        public void AddCustomer()
        {
            string _path = @"C:\PROJECT\CakeShop\CakeShop\Customer.json";
            var json = File.ReadAllText(_path);
            var jsonObj = JObject.Parse(json);
            JArray customerArray = new JArray();
            customerArray = jsonObj.GetValue("customers") as JArray;

            Console.WriteLine("----------Welcome here----------");

            Console.Write("Are you a New Customer(Y/N): ");
            string regtype = Console.ReadLine();

            if (regtype == "Y" || regtype == "y")
            {
                Console.WriteLine("Enter Full Name: ");
                CustomerName = Console.ReadLine();
                mobile: Console.WriteLine("Enter Contact NO.: ");
                CustomerContact = Console.ReadLine();

                var regop = IsPhoneNbr(CustomerContact);
                if (!regop)
                {
                    Console.WriteLine("Enter valid number!");
                    goto mobile;
                }
                else
                {
                    Console.WriteLine("Validated");
                }

                Console.WriteLine("Enter your address: ");
                Address = Console.ReadLine();
                Console.WriteLine("\n");

                CustomerID = customerArray.Count + 1;

                string newCustomer = "{ 'id': " + CustomerID + ",'customerName': '" + CustomerName + "', 'customerContact': " + CustomerContact + ", 'customerAddress': '" + Address + "' }";

                var newCompany = JObject.Parse(newCustomer);
                customerArray.Add(newCompany);

                jsonObj["customers"] = customerArray;
                string newJsonResult = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(_path, newJsonResult);

                s.menu(CustomerID,CustomerName, CustomerContact,Address);

            }
            else if (regtype == "N" || regtype == "n")
            {
                
                Console.WriteLine("Enter your Contact No.: ");
                string alreadyCustomer = Console.ReadLine();

                var CustomerName = "";
                var CustomerContact = "";
                var Address = "";
                var CustomerID = 0;


                foreach (var c in customerArray)
                {
                    if (c["customerContact"].ToString() == alreadyCustomer)
                    {
                        CustomerID = int.Parse(c["id"].ToString());
                        CustomerName = c["customerName"].ToString();
                        CustomerContact = c["customerContact"].ToString();
                        Address = c["customerAddress"].ToString();

                        Console.WriteLine("Your Name is: " + c["customerName"]);
                        Console.WriteLine("Your address is: " + c["customerAddress"]);

                        s.menu(CustomerID, CustomerName, CustomerContact, Address);
                    }
                    else
                    {
                        Console.WriteLine("The Number is invalid");
                        break;
                    }
                }
                
            }

            else
            {
                Console.WriteLine("Please Type Y/N");
                AddCustomer();
            }
        }

        public void UpdateCustomer()
        {
            string path = @"C:\PROJECT\CakeShop\CakeShop\Customer.json";
            var json_ = File.ReadAllText(path);
            var jsonObj_ = JObject.Parse(json_);
            JArray customerArray = new JArray();
            customerArray = jsonObj_.GetValue("customers") as JArray;

            Console.Write("Enter Customer ID to Update Customer : ");
            var companyId = Convert.ToInt32(Console.ReadLine());

            if (companyId > 0)
            {
                Console.Write("Enter new Customer name : ");
                var companyName = Convert.ToString(Console.ReadLine());

                foreach (var customer in customerArray.Where(obj => obj["id"].Value<int>() == companyId))
                {
                    customer["customerName"] = !string.IsNullOrEmpty(companyName) ? companyName : "";
                }

                jsonObj_["customers"] = customerArray;
                string output = JsonConvert.SerializeObject(jsonObj_, Formatting.Indented);
                File.WriteAllText(path, output);
            }
        }

        public void DeleteCustomer()
        {

            string path = @"C:\PROJECT\CakeShop\CakeShop\Customer.json";
            var json_ = File.ReadAllText(path);
            var jsonObj_ = JObject.Parse(json_);
            JArray customerArray = (JArray)jsonObj_["customers"];
            customerArray = jsonObj_.GetValue("customers") as JArray;


            Console.Write("Enter Company ID to Delete Company : ");
            var companyId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Data Deleted Succesffully! \n\n");

            if (companyId > 0)
            {
                var companyName = string.Empty;
                var companyToDeleted = customerArray.FirstOrDefault(obj => obj["id"].Value<int>() == companyId);

                customerArray.Remove(companyToDeleted);

                string output = JsonConvert.SerializeObject(jsonObj_, Formatting.Indented);
                File.WriteAllText(path, output);
            }

        }


    }
}
