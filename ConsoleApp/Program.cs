
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
            List<Shoes> lst;

            while (true)
            {
                string pass;
                string userName;
                do
                {
                    Console.Write("User Name: ");
                    userName = Console.ReadLine();
                    Console.Write("Password: ");
                    pass = GetPassword();
                    Console.WriteLine();
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
                Console.WriteLine("+============================================================================+");
                Console.WriteLine("|                                  Menu                                      |");
                Console.WriteLine("+----------------------------------------------------------------------------+");
                Console.WriteLine("| 1.Search                                                                   |");
                Console.WriteLine("| 2.Create invoice                                                           |");
                Console.WriteLine("| 3.Exit System                                                              |");
                Console.WriteLine("+============================================================================+");
                Console.Write("Enter choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        int choi;
                        do
                        {
                            Console.WriteLine("+=======================================================================+");
                            Console.WriteLine("|                              Search                                   |");
                            Console.WriteLine("|-----------------------------------------------------------------------|");
                            Console.WriteLine("| 1.Search name                                                         |");
                            Console.WriteLine("| 2.Search id                                                           |");
                            Console.WriteLine("| 3.Search brand                                                        |");
                            Console.WriteLine("| 4.Exits Search                                                        |");
                            Console.WriteLine("+-----------------------------------------------------------------------+");
                            Console.Write("Input choice: ");
                            choi = Convert.ToInt32(Console.ReadLine());
                            switch (choi)
                            {
                                case 1: //search name
                                    string shoeName;
                                    Console.Write("Input Search Name: ");
                                    shoeName = Convert.ToString(Console.ReadLine());
                                    Console.WriteLine("-------------------------------------");
                                    lst = ibl.SearchByName(shoeName);
                                    if (lst.Count <= 0)
                                    {
                                        Console.WriteLine("-------------------------------------");
                                        Console.WriteLine("There is no item with id " + shoeName);

                                    }
                                    else
                                    {
                                        Console.WriteLine("\nItem Count By Name: " + lst.Count);
                                        Console.WriteLine("-------------------------------------");
                                    }
                                    Console.WriteLine("\n    Press Enter key to back menu...");
                                    Console.ReadKey();
                                    break;
                                case 2: //search id
                                    int shoeId;
                                    Console.Write("Input Search Id:");
                                    if (Int32.TryParse(Console.ReadLine(), out shoeId))
                                    {
                                        Shoes i = ibl.SearchById(shoeId);
                                        if (i != null)
                                        {
                                            Console.WriteLine("-------------------------------------");
                                            //Console.WriteLine("Item ID: " + i.ShoeId);
                                            Console.WriteLine("| Shoe Name:  " + i.ShoeName);
                                            Console.WriteLine("| Shoe Price: " + i.ShoePrice);
                                            Console.WriteLine("| Brand:      " + i.BrandName);
                                            Console.WriteLine("| Quantity: " + i.ShoeQuantity);
                                            Console.WriteLine("| MADE IN " + i.ShoeDesception);
                                            Console.WriteLine("-------------------------------------");
                                            Console.WriteLine("Do you want to show Size and Color ???");
                                        }
                                        else
                                        {
                                            Console.WriteLine("-------------------------------------");
                                            Console.WriteLine("There is no item with id " + shoeId);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Your Choose is wrong!");
                                    }
                                    Console.WriteLine("\n    Press Enter key to back menu...");
                                    Console.ReadLine();
                                    break;
                                case 3: //search brand
                                    string brandName;
                                    Console.Write("Input Search Brand: ");
                                    brandName = Convert.ToString(Console.ReadLine());
                                    Console.WriteLine("-------------------------------------");
                                    lst = ibl.SearchByBrand(brandName);
                                    if (lst.Count <= 0)
                                    {
                                        Console.WriteLine("-------------------------------------");
                                        Console.WriteLine("There is no item with " + brandName);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nResult Count By Brand: " + lst.Count);
                                        Console.WriteLine("-------------------------------------");
                                    }

                                    Console.WriteLine("\n    Press Enter key to back menu...");
                                    Console.ReadKey();
                                    break;
                                case 4://Exits
                                    break;

                            }
                        } while (choi == 3);
                        break;
                    case 2:
                        Console.WriteLine("+=======================================================================+");
                        Console.WriteLine("|                              Create Invoice                           |");
                        Console.WriteLine("|-----------------------------------------------------------------------|");
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