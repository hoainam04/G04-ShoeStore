using System;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class ShoesDAL
    {
        private MySqlConnection connection = DbHelper.GetConnection();
        public Shoes GetbyID(Shoes shoes)
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from Shoes where shoe_id = @id";
                command.Parameters.AddWithValue("@id", shoes.ShoeId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    shoes.ShoeId = reader.GetInt32("shoe_id");
                    string id = reader["shoe_id"].ToString();
                    string name = reader["shoe_name"].ToString();
                    string price = reader["shoe_price"].ToString();
                    string brand = reader["brand_name"].ToString();
                    string quantity = reader["shoe_quantity"].ToString();
                    string description = reader["shoe_desception"].ToString();
                    Console.WriteLine("ShoeId:    " + id);
                    Console.WriteLine("ShoeName:  " + name);
                    Console.WriteLine("ShoePrice: " + price + "$");
                    Console.WriteLine("Brand:     " + brand);
                    Console.WriteLine("Quantity:  " + quantity);
                    Console.WriteLine("Made in " + description);
                }
                else
                {
                    shoes.ShoeId = 0;
                }
                reader.Close();
            }
            catch
            {
                shoes.ShoeId = -1;
            }
            finally
            {
                connection.Close();
            }
            return shoes;
        }
        public Shoes GetByBrand(Shoes shoes)
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from Shoes where brand_name = @br";
                command.Parameters.AddWithValue("@br", shoes.BrandName);
                MySqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("---------------------------------------------------------------");
                if (reader.Read())
                {
                    shoes.ShoeId = reader.GetInt32("shoe_id");
                    while (reader.Read())
                    {
                        string id = reader["shoe_id"].ToString();
                        string name = reader["shoe_name"].ToString();
                        string price = reader["shoe_price"].ToString();
                        string brand = reader["brand_name"].ToString();
                        string quantity = reader["shoe_quantity"].ToString();
                        string description = reader["shoe_desception"].ToString();
                        Console.WriteLine("ShoeId:    " + id);
                        Console.WriteLine("ShoeName:  " + name);
                        Console.WriteLine("ShoePrice: " + price + "$");
                        Console.WriteLine("Brand:     " + brand);
                        Console.WriteLine("Quantity:  " + quantity);
                        Console.WriteLine("Made in " + description);
                        Console.WriteLine("---------------------------------------------------------------");
                    }
                }
                else
                {
                    shoes.ShoeId = 0;
                }

                reader.Close();
            }
            finally
            {

            }
            return shoes;
        }
        public Shoes GetByName(Shoes shoes)
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from Shoes where shoe_name = @name";
                command.Parameters.AddWithValue("@name", shoes.ShoeName);
                MySqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("---------------------------------------------------------------");

                    while (reader.Read())
                    {
                        string id = reader["shoe_id"].ToString();
                        string name = reader["shoe_name"].ToString();
                        string price = reader["shoe_price"].ToString();
                        string brand = reader["brand_name"].ToString();
                        string quantity = reader["shoe_quantity"].ToString();
                        string description = reader["shoe_desception"].ToString();
                        Console.WriteLine("ShoeId:    " + id);
                        Console.WriteLine("ShoeName:  " + name);
                        Console.WriteLine("ShoePrice: " + price + "$");
                        Console.WriteLine("Brand:     " + brand);
                        Console.WriteLine("Quantity:  " + quantity);
                        Console.WriteLine("Made in " + description);
                        Console.WriteLine("---------------------------------------------------------------");
                    }

                reader.Close();
            }
            finally
            {

            }
            return shoes;
        }
    }
}