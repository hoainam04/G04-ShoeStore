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
            List<Shoes> lst = new List<Shoes>();

            while (true)
            {
                string pass;
                string userName;
                do
                {
                    Console.WriteLine("+──────────────────────────────────────────────────────────────────────────────────+");
                    Console.WriteLine("|                               ...LOGIN TO SYSTEM...                              |");
                    Console.WriteLine("|                                 G04_ND_SHOES STORE                               |");
                    Console.WriteLine("|──────────────────────────────────────────────────────────────────────────────────|");
                    Console.Write("| User Name: ");
                    userName = Console.ReadLine();
                    Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────");
                    Console.Write("| Password: ");
                    pass = GetPassword();
                    Console.WriteLine("\n────────────────────────────────────────────────────────────────────────────────────");
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
                }
                else
                {
                    break;
                }
            }
            while (true)
            {
                Console.WriteLine("┌──────────────────────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("|                         Shoes Store System - Menu                                |");
                Console.WriteLine("|──────────────────────────────────────────────────────────────────────────────────|");
                Console.WriteLine("| 1.Search                                                                         |");
                Console.WriteLine("| 2.Invoices                                                                       |");
                Console.WriteLine("| 3.Exit System                                                                    |");
                Console.WriteLine("└──────────────────────────────────────────────────────────────────────────────────┘");
                Console.Write(" * Enter choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("+──────────────────────────────────────────────────────────────────────────────────+");
                switch (choice)
                {
                    case 1:
                        int choi;
                        do
                        {
                            Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                            Console.WriteLine("|                                   Shoe Store System - Search                                    |");
                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                            Console.WriteLine("| 1.Search Name                                                                                   |");
                            Console.WriteLine("| 2.Search Id                                                                                     |");
                            Console.WriteLine("| 3.Search Brand                                                                                  |");
                            Console.WriteLine("| 4.Show All Shoes                                                                                |");
                            Console.WriteLine("| 5.Exits Search                                                                                  |");
                            Console.WriteLine("└─────────────────────────────────────────────────────────────────────────────────────────────────┘");
                            Console.Write(" # Your choice: ");
                            choi = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("+──────────────────────────────────────────────────────────────────────────────────+");

                            switch (choi)
                            {
                                case 1: //search name
                                    string shoeName;
                                    Console.Write("Input Search Name: ");
                                    shoeName = Convert.ToString(Console.ReadLine());
                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                    lst = ibl.SearchByName(shoeName);
                                    if (lst == null)
                                    {
                                        Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");

                                        Console.WriteLine("No results found for " + shoeName);

                                    }
                                    else
                                    {
                                        Console.WriteLine("\nFound " + lst.Count + " results with the Name: " + shoeName);
                                        Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");

                                    }
                                    Console.WriteLine("\n    Press Enter key to back search menu...");
                                    Console.ReadKey();
                                    break;
                                case 2: //search id
                                    int shoeId;
                                    Console.Write("*Input Search Id:");
                                    if (Int32.TryParse(Console.ReadLine(), out shoeId))
                                    {
                                        Shoes i = ibl.SearchById(shoeId);
                                        if (i != null)
                                        {
                                            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────");
                                            Console.WriteLine("| Shoe ID: " + i.ShoeId);
                                            Console.WriteLine("| Shoe Name:  " + i.ShoeName);
                                            Console.WriteLine("| Shoe Price: " + i.ShoePrice + "$");
                                            Console.WriteLine("| Brand:      " + i.BrandName);
                                            Console.WriteLine("| MADE IN " + i.ShoeDesception);
                                            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────");
                                            Console.WriteLine("| Size and Color");
                                        }
                                        else
                                        {
                                            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────");
                                            Console.WriteLine(" !!! No results found for id " + shoeId);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Your Choose is wrong!");
                                    }
                                    Console.WriteLine("\n    Press Enter key to back Search Menu...");
                                    Console.ReadLine();
                                    break;
                                case 3: //search brand
                                    string brandName;
                                    Console.Write("Input Search Brand: ");
                                    brandName = Convert.ToString(Console.ReadLine());
                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                    lst = ibl.SearchByBrand(brandName);
                                    // List<Shoes> n= ibl.SearchByBrand(brandName);
                                    if (lst == null)
                                    {
                                        Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                        Console.WriteLine("No results found for " + brandName);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nFound " + lst.Count + " results with the Brand: " + brandName);
                                        Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                    }

                                    Console.WriteLine("\n    Press Enter key to back search menu...");
                                    Console.ReadKey();
                                    break;
                                case 4://Show All
                                    lst = ibl.GetAll();
                                    Console.ReadLine();
                                    break;
                                case 5://
                                    break;

                            }
                        } while (choi != 5);
                        break;
                    case 2:
                        int a;
                        do
                        {
                            Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                            Console.WriteLine("|                                  Shoe Stors System - Invoice                                    |");
                            Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                            Console.WriteLine("|1.Create Invoice                                                                                 |");
                            Console.WriteLine("|2.Invoice History                                                                                |");
                            Console.WriteLine("|3.Exit                                                                                           |");
                            Console.WriteLine("└─────────────────────────────────────────────────────────────────────────────────────────────────┘");
                            Console.Write("# Your Choice: ");
                            a = Convert.ToInt32(Console.ReadLine());
                            switch (a)
                            {
                                case 1://Create Invoice
                                    string name;
                                    int phone;
                                    string address;
                                    Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                                    Console.WriteLine("| Input Customer Information");
                                    Console.Write("# Name: ");
                                    name = Convert.ToString(Console.ReadLine());
                                    Console.Write("# Phone: ");
                                    phone = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("# Address: ");
                                    address = Convert.ToString(Console.ReadLine());
                                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");

                                    Console.ReadKey();
                                    break;

                                case 2:
                                    int i;
                                    do
                                    {

                                        Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                                        Console.WriteLine("|                            Shoe Store System - Invoice History                                  |");
                                        Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                                        Console.WriteLine("| 1.Search by invoice no                                                                          |");
                                        Console.WriteLine("| 2.Search by phone number                                                                        |");
                                        Console.WriteLine("| 3.Exit invoice history                                                                          |");
                                        Console.WriteLine("└─────────────────────────────────────────────────────────────────────────────────────────────────┘");
                                        Console.Write("| #Your choice:");
                                        i = Convert.ToInt32(Console.ReadLine());
                                        switch (i)
                                        {
                                            case 1://search by invoice no
                                                break;

                                            case 2://search by phone number
                                                int p;
                                                Console.Write("Input Customer Phone Number: ");
                                                p = Convert.ToInt32(Console.ReadLine());
                                                Console.ReadKey();
                                                break;
                                            case 3://exit
                                                break;
                                            default:
                                                break;
                                        }
                                    } while (i != 3);
                                    break;

                                default:
                                    break;
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