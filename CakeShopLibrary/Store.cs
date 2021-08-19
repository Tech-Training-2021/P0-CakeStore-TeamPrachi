using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.Json;


namespace CakeShopLibrary
{
    public class Store
    {
        int price;
        string description, flv1;

        public void getCake(string flavour)
        {

            string _path = @"C:\PROJECT\CakeShop\CakeShop\Menu.json";
            var json = File.ReadAllText(_path);
            var jsonObj = JObject.Parse(json);
            string _path1 = @"C:\PROJECT\CakeShop\CakeShop\Store.json";
            var json_1 = File.ReadAllText(_path1);
            var jsonObj_1 = JObject.Parse(json_1);
            Console.WriteLine($"{flavour} Cake Flavours: ");
            int index = 0;
            foreach (var flav in jsonObj)
            {
                if (flav.Key == flavour)
                {
                    foreach (var item in flav.Value)
                    {
                        index++;
                        flv1 = item.ToString();
                        Console.Write($"{index}.{item}\n");
                    }
                }
            }
            Console.WriteLine("\nWhich flavour you want? ");
            int ch = int.Parse(Console.ReadLine());
            JArray chocolateArray = new JArray();
            chocolateArray = jsonObj.GetValue(flavour) as JArray;
            var flv = chocolateArray[ch - 1];
            var x = jsonObj_1[flavour];
            foreach (var item in x)
            {
                var y = item["Flavour"].ToString();
                if (y == flv.ToString())
                {
                    price = Convert.ToInt32(item["Price"]);
                    description = item["description"].ToString();
                }
            }
        }
        public void menu(int CustomerID, string CustomerName, string CustomerContact, string Address)
        {
            string path = @"C:\PROJECT\CakeShop\CakeShop\Order.json";
            var json_ = File.ReadAllText(path);
            var jsonObj_ = JObject.Parse(json_);
            JArray FlavourArray = new JArray();
            FlavourArray = jsonObj_.GetValue("Details") as JArray;

            string _path = @"C:\PROJECT\CakeShop\CakeShop\Menu.json";
            var json = File.ReadAllText(_path);
            var jsonObj = JObject.Parse(json);
            string _path1 = @"C:\PROJECT\CakeShop\CakeShop\Store.json";
            var json_1 = File.ReadAllText(_path1);
            var jsonObj_1 = JObject.Parse(json_1);
            Console.WriteLine("-----------Menu List------------");
            Console.WriteLine("1. Chocolate \n2. Vanilla\n3.Strawberry\n4.Pinapple\n5.Rasmalai\n"); ;
            Console.Write("Enter Your Choice: ");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            string flavour = "";
            switch (choice)
            {
                case 1:
                    flavour = "Chocolate";
                    getCake("Chocolate");
                    break;
                case 2:
                    flavour = "Vanilla";
                    getCake("Vanilla");
                    break;
                case 3:
                    flavour = "Strawberry";
                    getCake("Strawberry");
                    break;
                case 4:
                    flavour = "Pineapple";
                    getCake("Pineapple");
                    break;
                case 5:
                    flavour = "Rasmalai";
                    getCake("Rasmalai");
                    break;
                default: break;
            }

            List<Dictionary<string, string>> order_details = new List<Dictionary<string, string>>();

            var arlist2 = new ArrayList()
                {
                     "flavour", $"{flavour}"+"cake flavour", "price", "description"
                };
            var arlist1 = new ArrayList()
                {
                     flavour.ToString(), flv1, price.ToString(), description.ToString()
                };
            IDictionary<string, string> c_order = new Dictionary<string, string>();

            for (int i = 0; i < arlist1.Count; i++)
                c_order.Add(arlist2[i].ToString(), arlist1[i].ToString());

            order_details.Add(new Dictionary<string, string>(c_order));
            var orderResult = JsonConvert.SerializeObject(order_details);
            //File.WriteAllText(path, orderResult);



            string flavourChoice = "{ 'id': " + CustomerID + ",'customerName': '" + CustomerName + "', 'customerContact': " + CustomerContact + ", 'customerAddress': '" + Address + "' ,'order': " + orderResult +"}";
            var Flavour = JObject.Parse(flavourChoice);
            FlavourArray.Add(Flavour);
            jsonObj_["Details"] = FlavourArray;
            string newJsonResult = JsonConvert.SerializeObject(jsonObj_, Formatting.Indented);
            File.WriteAllText(path, newJsonResult);

            Console.WriteLine("Your Name: " + CustomerName);
            Console.WriteLine("Your Address: " + Address);
            Console.WriteLine("Total Bill: " + price);

            menu(CustomerID, CustomerName, CustomerContact, Address);
        }

    }
}
