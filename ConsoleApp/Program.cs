
using System;
using Persistence;
using BL;

namespace ConsoleAppPL
{
    class Program
    {
        static void Main(string[] args)
        {

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
                Console.Write("Enter choice:");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        int choi;
                        Console.WriteLine("+=======================================================================+");
                        Console.WriteLine("|                              Search                                   |");
                        Console.WriteLine("|-----------------------------------------------------------------------|");
                        Console.WriteLine("| 1.Search name                                                         |");
                        Console.WriteLine("| 2.Search id                                                           |");
                        Console.WriteLine("| 3.Search brand                                                        |");
                        Console.WriteLine("| 4.Exits Search                                                        |");
                        Console.WriteLine("+-----------------------------------------------------------------------+");
                        Console.Write("Input choice:");
                        choi = Convert.ToInt32(Console.ReadLine());
                        switch (choi)
                        {
                            case 1: //search name
                                string name;
                                Console.Write("Input Search Name:");
                                name = Convert.ToString(Console.ReadLine());
                                Shoes shoes = new Shoes() { ShoeName = name };
                                ShoesBL blname = new ShoesBL();
                                shoes = blname.SearchByName(shoes);
                                Console.ReadKey();
                                break;
                            case 2: //search id
                                int id;
                                Console.Write("Input Search Id:");
                                id = Convert.ToInt32(Console.ReadLine());
                                Shoes shoesid = new Shoes() { ShoeId = id };
                                ShoesBL blid = new ShoesBL();
                                shoes = blid.SearchById(shoesid);
                                if (shoes.ShoeId <= 0)
                                {
                                    Console.WriteLine("Not result");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.ReadKey();
                                }

                                break;
                            case 3: //search brand
                                Console.Write("Input Search Brand:");
                                string br = Convert.ToString(Console.ReadLine());
                                Shoes shoesbr = new Shoes() { BrandName = br };
                                ShoesBL blbr = new ShoesBL();
                                shoesbr = blbr.SearchByBrand(shoesbr);
                                Console.ReadKey();
                                break;
                            case 4://Exits
                                break;

                        }
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