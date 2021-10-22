using System;
using System.Collections.Generic;
using Persistence;
using BL;
namespace ConsoleAppPL
{
    class Program
    {
        static void Main(string[] args)
        {
            ShoesBL ibl = new ShoesBL();
            InvoiceBL obl = new InvoiceBL();
            CustomerBL cbl = new CustomerBL();
            List<Shoes> lst;
            SizesColors sc;
            while (true)
            {
                Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                Console.WriteLine("|                                                                                                 |");
                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                Console.WriteLine("                            ......Press Enter to login to System.........                          ");
                Console.ReadKey();
                string userName;
                string pass;
                do
                {
                    Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                    Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                    Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                    Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                    Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                    Console.WriteLine("|                                                                                                 |");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("|                                  ...LOGIN TO SHOE STORE SYSTEM...                               |");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.Write("| User Name: ");
                    userName = Console.ReadLine();
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.Write("| Password: ");
                    pass = GetPassword();
                    Console.WriteLine("\n└─────────────────────────────────────────────────────────────────────────────────────────────────┘");
                    if (pass.Length < 8)
                    {
                        Console.WriteLine("Password must be 8 characters or more");
                    }
                } while (pass.Length < 8);
                Staff staff = new Staff() { UserName = userName, Password = pass };
                StaffBl bl = new StaffBl();

                staff = bl.Login(staff);
                if (staff.Role <= 0)
                {
                    Console.WriteLine("Can't Login");
                    Console.ReadLine();
                    
                }
                else
                {
                    Console.WriteLine("\t\t\t\t WELCOME TO SYSTEM " + staff.StaffName + "");
                    // break;
                }
                while (true)
                {
                    Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                    Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                    Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                    Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                    Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                    Console.WriteLine("|                                                                                                 |");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("|                                    Shoe Store System-Menu                                       |");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("| 1.Search                                                                                        |");
                    Console.WriteLine("| 2.Invoices                                                                                      |");
                    Console.WriteLine("| 3.Exit System                                                                                   |");
                    Console.WriteLine("└─────────────────────────────────────────────────────────────────────────────────────────────────┘");
                    Console.Write(" # Your choice: ");
                    int choice;
                    if (Int32.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                int choi;
                                do
                                {
                                    Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                                    Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                                    Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                                    Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                                    Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                                    Console.WriteLine("|                                                                                                 |");
                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                    Console.WriteLine("|                                  SHOE STORE SYSTEM - Search                                     |");
                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                    Console.WriteLine("| 1.Search By Name                                                                                |");
                                    Console.WriteLine("| 2.Search BY Id                                                                                  |");
                                    Console.WriteLine("| 3.Search By Brand                                                                               |");
                                    Console.WriteLine("| 4.Show All Shoes                                                                                |");
                                    Console.WriteLine("| 5.Exits Search                                                                                  |");
                                    Console.WriteLine("└─────────────────────────────────────────────────────────────────────────────────────────────────┘");
                                    Console.Write(" # Your choice: ");
                                    if (Int32.TryParse(Console.ReadLine(), out choi))
                                    {
                                        // choi = Convert.ToInt32(Console.ReadLine());
                                        switch (choi)
                                        {
                                            case 1: //search name
                                                string shoeName;
                                                Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                                                Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                                                Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                                                Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                                                Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                                                Console.WriteLine("|                                                                                                 |");
                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                Console.WriteLine("|                                  SHOE STORE SYSTEM - Search                                     |");
                                                Console.WriteLine("|                                       Search By Name                                            |");
                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                Console.Write("| Input Search Name: ");
                                                shoeName = Convert.ToString(Console.ReadLine());
                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                lst = ibl.SearchByName(shoeName);
                                                if (lst.Count <= 0)
                                                {
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    Console.WriteLine("\tNo results found for " + shoeName);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("\nFound " + lst.Count + " results with the Name: " + shoeName);
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                }
                                                Console.Write("\n\t\t    Press Enter key to back search menu...");
                                                Console.ReadKey();
                                                break;
                                            case 2: //search id
                                                int shoeId;
                                                int colorId;
                                                string cn;
                                                do
                                                {
                                                    Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                                                    Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                                                    Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                                                    Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                                                    Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                                                    Console.WriteLine("|                                                                                                 |");
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    Console.WriteLine("|                                  SHOE STORE SYSTEM - Search                                     |");
                                                    Console.WriteLine("|                                       Search By ID                                              |");
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    Console.Write("| Input Search Id:");
                                                    if (Int32.TryParse(Console.ReadLine(), out shoeId))
                                                    {
                                                        Shoes i = ibl.SearchById(shoeId);
                                                        if (i != null)
                                                        {
                                                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                            Console.WriteLine("| Shoe Name:  {0}", i.ShoeName);
                                                            Console.WriteLine("| Shoe Price: {0} VND", i.ShoePrice);
                                                            Console.WriteLine("| Brand:      {0}", i.BrandName);
                                                            Console.WriteLine("| Quantity:   {0}", i.ShoeQuantity);
                                                            Console.WriteLine("| MADE IN {0}", i.ShoeDesception);
                                                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                            Console.WriteLine("\t\t\t\t " + i.ShoeName + "-Color \t\t\t\t");
                                                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                            Console.WriteLine("| ID | Color Name                                                                                 |");
                                                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                            sc = ibl.Color(shoeId);
                                                            if (sc == null)
                                                            { Console.WriteLine(" Erorr!!! No Color in Database!!"); }
                                                            else
                                                            {
                                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                                Console.Write("| Input Color ID to show Size table : ");
                                                                while (Int32.TryParse(Console.ReadLine(), out colorId))
                                                                {
                                                                    Console.WriteLine("┌────────────────────────────┐");
                                                                    Console.WriteLine("|  ID   | Size  | Quantity   |");
                                                                    Console.WriteLine("├────────────────────────────┤");
                                                                    sc = ibl.GetSizes(shoeId, colorId);
                                                                    if (sc == null)
                                                                    {
                                                                        Console.WriteLine("|          EMPTY !!!         |");
                                                                        Console.WriteLine("└────────────────────────────┘");
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("└────────────────────────────┘");
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                            Console.WriteLine("No results found for ID: " + shoeId);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Your Choose is wrong!");
                                                    }
                                                    Console.Write("DO YOU WANT TO CONTINUE SEARCH BY ID (Y/N)???");
                                                    cn = Convert.ToString(Console.ReadLine());
                                                } while (cn == "y" || cn == "Y");
                                                break;
                                            case 3: //search brand
                                                string brandName;
                                                string cnue;
                                                do
                                                {
                                                    Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                                                    Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                                                    Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                                                    Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                                                    Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                                                    Console.WriteLine("|                                                                                                 |");
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    Console.WriteLine("|                                  SHOE STORE SYSTEM - Search                                     |");
                                                    Console.WriteLine("|                                       Search By Brand                                           |");
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    Console.Write("| Input Search Brand: ");
                                                    brandName = Convert.ToString(Console.ReadLine());
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    lst = ibl.SearchByBrand(brandName);
                                                    if (lst.Count <= 0)
                                                    {
                                                        Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                        Console.WriteLine("No results found for " + brandName);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("\nFound " + lst.Count + " results with the Brand: " + brandName);
                                                        Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    }
                                                    Console.WriteLine("DO YOU WANT TO CONTINUE SEARCH BY BRAND?? (Y/N)???");
                                                    cnue = Console.ReadLine();
                                                } while (cnue == "y" || cnue == "Y");
                                                break;
                                            case 4://Show All
                                                Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                                                Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                                                Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                                                Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                                                Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                                                Console.WriteLine("|                                                                                                 |");
                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                Console.WriteLine("|                                  SHOE STORE SYSTEM - Search                                     |");
                                                Console.WriteLine("|                                       Show All Shoes                                            |");
                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                lst = ibl.GetAll();
                                                Console.WriteLine("| Do you want show more details ??                                                                |");
                                                Console.Write("| Yes(Y) or No (N)??? : ");
                                                string cneu = Console.ReadLine();
                                                if (cneu == "y" || cneu == "Y")
                                                {
                                                    Console.Write("| Input Shoe Id:");
                                                    if (Int32.TryParse(Console.ReadLine(), out shoeId))
                                                    {
                                                        Shoes i = ibl.SearchById(shoeId);
                                                        if (i != null)
                                                        {
                                                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                            Console.WriteLine("| Shoe Name:  {0}", i.ShoeName);
                                                            Console.WriteLine("| Shoe Price: {0:###,000} VND", i.ShoePrice);
                                                            Console.WriteLine("| Brand:      {0}", i.BrandName);
                                                            Console.WriteLine("| Quantity:   {0}", i.ShoeQuantity);
                                                            Console.WriteLine("| MADE IN {0}", i.ShoeDesception);
                                                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                            Console.WriteLine("\t\t\t\t " + i.ShoeName + "-Color \t\t\t\t");
                                                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                            Console.WriteLine("| ID | Color Name                                                                                 |");
                                                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                            sc = ibl.Color(shoeId);
                                                            if (sc == null)
                                                            { Console.WriteLine(" Erorr!!! No Color in Database!!"); }
                                                            else
                                                            {
                                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                                Console.Write("| Input Color ID to show Size table : ");
                                                                while (Int32.TryParse(Console.ReadLine(), out colorId))
                                                                {
                                                                    Console.WriteLine("┌────────────────────────────┐");
                                                                    Console.WriteLine("|  ID   | Size  | Quantity   |");
                                                                    Console.WriteLine("├────────────────────────────┤");
                                                                    sc = ibl.GetSizes(shoeId, colorId);
                                                                    if (sc == null)
                                                                    {
                                                                        Console.WriteLine("|          EMPTY !!!         |");
                                                                        Console.WriteLine("└────────────────────────────┘");
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("└────────────────────────────┘");
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                            Console.WriteLine("No results found for ID: " + shoeId);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Your Choose is wrong!");
                                                    }
                                                    Console.ReadLine();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Back to search menu....");
                                                    Console.ReadKey();
                                                }
                                                break;
                                            case 5://
                                                break;
                                        }
                                    }
                                    // Console.WriteLine("Invailid Choice !!! PLEASE CHOICE AGAIN OPTION 1-5");
                                } while (choi != 5);
                                break;
                            case 2:
                                int a;
                                do
                                {
                                    Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                                    Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                                    Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                                    Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                                    Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                                    Console.WriteLine("|                                                                                                 |");
                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                    Console.WriteLine("|                                  SHOE STORE SYSTEM - Invoice                                    |");
                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                    Console.WriteLine("|1.Create Invoice                                                                                 |");
                                    Console.WriteLine("|2.Invoice History                                                                                |");
                                    Console.WriteLine("|3.Exit                                                                                           |");
                                    Console.WriteLine("└─────────────────────────────────────────────────────────────────────────────────────────────────┘");
                                    Console.Write("| Your Choice: ");
                                    if (Int32.TryParse(Console.ReadLine(), out a))
                                    {
                                        switch (a)
                                        {
                                            case 1://Create Invoice

                                                Invoice invoice = new Invoice();
                                                string name;
                                                string customerPhone;
                                                string address;
                                                int sId;
                                                int cId;
                                                int szId;
                                                string ca;
                                                int i= 0;
                                                Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                                                Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                                                Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                                                Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                                                Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                                                Console.WriteLine("|                                                                                                 |");
                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                Console.WriteLine("|                                  SHOE STORE SYSTEM - Invoice                                    |");
                                                Console.WriteLine("|                                     Create Invoice                                              |");
                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                Console.WriteLine("|                                  Input Customer Information                                     |");
                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                while (true)
                                                {
                                                    Console.Write("| Phone: ");
                                                    customerPhone = Convert.ToString(Console.ReadLine());
                                                    if (customerPhone.Length == 10 && customerPhone.StartsWith("0") || customerPhone.Length == 12 && customerPhone.StartsWith("+84"))
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("!!! Phone number must be Length = 10 with start = 0\n And length = 12 with start = +84");
                                                    }
                                                }
                                                Console.Write("| Name: ");
                                                name = Convert.ToString(Console.ReadLine());
                                                Console.Write("| Address: ");
                                                address = Convert.ToString(Console.ReadLine());
                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                invoice.OrderCustomer = new Customer { CustomerName = name, CustomerPhone = customerPhone, CustomerAddress = address };
                                                do
                                                {
                                                    Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                                                    Console.WriteLine("|                                  SHOE STORE SYSTEM - Invoice                                    |");
                                                    Console.WriteLine("|                                     Create Invoice                                              |");
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    Console.WriteLine("| Customer Name    :{0,-78}|", name);
                                                    Console.WriteLine("| Customer Phone   :{0,-78}|", customerPhone);
                                                    Console.WriteLine("| Customer Address :{0,-78}|", address);
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    Console.WriteLine("|                             Enter shoe information in the invoice                               |");
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    lst = ibl.GetAll();
                                                    Console.Write("| Input Shoes ID: ");
                                                    if (Int32.TryParse(Console.ReadLine(), out sId))
                                                        Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    {
                                                        Shoes sh = ibl.SearchById(sId);
                                                        if (sh != null)
                                                        {
                                                            Console.WriteLine("| Shoe name: " + sh.ShoeName);
                                                            Console.WriteLine("| Price:     " + sh.ShoePrice + " VND");
                                                            Console.WriteLine("| Brand:     " + sh.BrandName);
                                                            Console.WriteLine("| Quantity:  " + sh.ShoeQuantity);
                                                            Console.WriteLine("| Made in " + sh.ShoeDesception);
                                                        }
                                                        Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                        Console.WriteLine("|\t\t\t\t " + sh.ShoeName + "-Color \t\t\t\t\t\t");
                                                        Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                        sc = ibl.Color(sId);
                                                        if (sc == null)
                                                        { Console.WriteLine("| Erorr!!! No Color in Database!!"); }
                                                        else
                                                        {
                                                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                            Console.Write("| Input Color ID to show Size table : ");
                                                            if (Int32.TryParse(Console.ReadLine(), out cId))
                                                            {
                                                                Console.WriteLine("┌────────────────────────────┐");
                                                                Console.WriteLine("|  ID   | Size  | Quantity   |");
                                                                Console.WriteLine("├────────────────────────────┤");
                                                                sc = ibl.GetSizes(sId, cId);
                                                                if (sc == null)
                                                                {
                                                                    Console.WriteLine("| No Size of Color ID " + cId + " in Database!!");
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("└────────────────────────────┘");
                                                                    Console.Write("| Choose Your Size (Size ID): ");
                                                                    szId = Convert.ToInt32(Console.ReadLine());
                                                                    Console.Write("| Input Amount: ");
                                                                    invoice.amount = Convert.ToInt32(Console.ReadLine());
                                                                    invoice.ShoesList.Add(ibl.GetToInvocie(sId, cId, szId));
                                                                    invoice.ShoesList[i++].ShoeQuantity = invoice.amount;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    Console.Write("DO YOU WANT TO CONTINUE ADD SHOE (Y/N)???  : ");
                                                    ca = Console.ReadLine();
                                                } while (ca == "Y" || ca == "y");
                                                if (obl.CreateInvoice(invoice) == true)
                                                {
                                                    Console.WriteLine("\t CREATE INVOICE COMPLETE !!!");
                                                    Console.Write("\t Do you want to print the invoice ??? (Y/N)");
                                                    string cni = Console.ReadLine();
                                                    if (cni == "y" || cni == "Y")
                                                    {
                                                        invoice = obl.PrintInvoice(invoice);
                                                    }
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                    Console.WriteLine("|                                                            Export Invoice By Staff {0} : {1} |", staff.StaffId, staff.StaffName);
                                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("\t CREATE INVOICE NOT COMPLETE !!!");
                                                }
                                                Console.ReadKey();
                                                break;
                                            case 2:

                                                Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                                                Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                                                Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                                                Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                                                Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                                                Console.WriteLine("|                                                                                                 |");
                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                Console.WriteLine("|                                  SHOE STORE SYSTEM - Invoice                                    |");
                                                Console.WriteLine("|                                      Invoice History                                            |");
                                                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                                Console.Write("| Input Invoice No: ");
                                                int invoiceNo = Convert.ToInt32(Console.ReadLine());
                                                invoice = obl.PrintInvoiceNo(invoiceNo);
                                                if (invoice == null)
                                                {
                                                    Console.WriteLine("\n !!! NOT FOUND BY INVOICE NO : " + invoiceNo);
                                                }
                                                else
                                                {

                                                }
                                                Console.ReadLine();
                                                break;
                                            default:
                                                Console.WriteLine(" !!! choose option 1-3 !!!");
                                                break;
                                        }
                                    }
                                } while (a != 3);
                                break;
                            case 3:

                                Console.WriteLine("DO YOU WANT TO EXIT SYSTEM...(Y/N)???");
                                string o = Console.ReadLine();
                                if (o == "Y" || o == "y")
                                {
                                    Console.Write("GOOD BYE...");
                                    Environment.Exit(0);
                                }
                                break;
                            default:
                                Console.WriteLine("PLEASE CHOOSE OPTION 1-3: ");
                                Console.Write("PRESS ENTER TO RE-CHOICE....");
                                Console.ReadKey();
                                break;
                        }
                    }
                }

            }
            static string GetPassword()
            {
                var pass = string.Empty;
                ConsoleKey key;
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;
                    if (key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        Console.Write("\b \b");
                        pass = pass[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        pass += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
                return pass;
            }
        }
    }
}