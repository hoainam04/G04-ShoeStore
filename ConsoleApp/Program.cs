using System;
using Persistence;
using BL;

namespace ConsoleAppPL
{
	class Program
	{
		static void Main(string[] args)
		{
			int login = 0;
			do
			{
				string pass;
				string userName;
				do{
				Console.Write("User Name: ");
				userName = Console.ReadLine();
				Console.Write("Password: ");
				pass = GetPassword();
				Console.WriteLine();
				if (pass.Length<8)
				{
					Console.WriteLine("Password must be 8 characters or more");
				}
				}while (pass.Length<8);
				Staff staff = new Staff() { UserName = userName, Password = pass };
				StaffBl bl = new StaffBl();

				login = bl.Login(staff);
				if (login <= 0)
				{
					Console.WriteLine("Can't Login");

				};
			}
			while (login <= 0);

			int choice;
			Console.WriteLine("\t\t\t...Wellcome to System...");
			Console.WriteLine("+============================================================================+");
			Console.WriteLine("|                                  Menu                                      |");
			Console.WriteLine("+----------------------------------------------------------------------------+");
			Console.WriteLine("| 1.Search                                                                   |");
			Console.WriteLine("| 2.Create invoice                                                           |");
			Console.WriteLine("+============================================================================+");
			Console.Write("Enter choice:");
			choice = Convert.ToInt32(Console.ReadLine());
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
					Console.WriteLine("| 4.Exits                                                               |");
					Console.WriteLine("+-----------------------------------------------------------------------+");
					Console.Write("Input choice:");
					choi = Convert.ToInt32(Console.ReadLine());
						switch (choi)
						{
							case 1: //search name
							Console.Write("Input Search Name:");
							string searchName = Convert.ToString(Console.ReadLine());
								break;
							case 2: //search id
							Console.Write("Input Search Id:");
							string searchId = Convert.ToString(Console.ReadLine());
								break;
							case 3: //search brand
							Console.Write("Input Search Brand:");
							string searchBrand = Convert.ToString(Console.ReadLine());
								break;	
							default:
								break;
						}
					
					break;
				case 2:
					Console.Write("Create invoice");
					break;
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