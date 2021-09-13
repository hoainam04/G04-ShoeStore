using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public static class ShoeFilter
    {
        public const int GET_ALL = 0;
        public const int FILTER_BY_SHOE_NAME = 1;
        public const int FILTER_BY_BRAND_NAME = 2;
    }
    public class ShoesDAL
    {
        private string query;
        private MySqlConnection connection = DbHelper.GetConnection();
        public Shoes GetbyID(int shoeId)
        {
            Shoes shoe = null;
            try
            {
                connection.Open();
                query = @"select shoe_id, shoe_name, shoe_price, brand_name, shoe_quantity,
                            ifnull(shoe_desception, '') as shoe_desception 
                            from Shoes where shoe_id = @shoeId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@shoeId", shoeId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    shoe = GetShoe(reader);
                }
                reader.Close();
            }
            catch
            { }
            finally
            {
                connection.Close();
            }
            return shoe;
        }
        internal Shoes GetShoe(MySqlDataReader reader)
        {
            Shoes shoe = new Shoes();
            shoe.ShoeId = reader.GetInt32("shoe_id");
            shoe.ShoeName = reader.GetString("shoe_name");
            shoe.BrandName = reader.GetString("brand_name");
            shoe.ShoePrice = reader["shoe_price"].ToString();
            shoe.ShoeQuantity = reader["shoe_quantity"].ToString();
            shoe.ShoeDesception = reader["shoe_desception"].ToString();
            return shoe;
        }
        internal Shoes GetShoeMany(MySqlDataReader reader)
        {
            Shoes shoe = new Shoes();
            shoe.ShoeId = reader.GetInt32("shoe_id");
            shoe.ShoeName = reader.GetString("shoe_name");
            shoe.BrandName = reader.GetString("brand_name");
            shoe.ShoePrice = reader["shoe_price"].ToString();
            shoe.ShoeQuantity = reader["shoe_quantity"].ToString();
            shoe.ShoeDesception = reader["shoe_desception"].ToString();
            // Console.WriteLine("-------------------------------------");
            Console.WriteLine("| Shoe ID:    " + shoe.ShoeId);
            Console.WriteLine("| Shoe Name:  " + shoe.ShoeName);
            Console.WriteLine("| Shoe Price: " + shoe.ShoePrice);
            Console.WriteLine("| Brand:      " + shoe.BrandName);
            Console.WriteLine("| Quantity:   " + shoe.ShoeQuantity);
            Console.WriteLine("| MADE IN " + shoe.ShoeDesception);
            Console.WriteLine("-------------------------------------");
            return shoe;
        }

        public List<Shoes> GetShoes(int shoeFilter, Shoes shoe)
        {
            List<Shoes> lst = null;
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("", connection);
                switch (shoeFilter)
                {
                    case ShoeFilter.GET_ALL:
                        query = @"select * from Shoes";
                        break;
                    case ShoeFilter.FILTER_BY_SHOE_NAME:
                        query = @"select shoe_id, shoe_name, shoe_price, brand_name,shoe_quantity,
                                    ifnull(shoe_desception, '') as shoe_desception 
                                     from Shoes where shoe_name =@shoeName;";
                        command.Parameters.AddWithValue("@shoeName", shoe.ShoeName);
                        break;
                    case ShoeFilter.FILTER_BY_BRAND_NAME:
                        query = @"select shoe_id, shoe_name, shoe_price, brand_name,shoe_quantity,
                                    ifnull(shoe_desception, '') as shoe_desception 
                                     from Shoes where brand_name =@brandName;";
                        command.Parameters.AddWithValue("@brandName", shoe.BrandName);
                        break;
                }
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                lst = new List<Shoes>();
                while (reader.Read())
                {
                    lst.Add(GetShoeMany(reader));
                }
                reader.Close();
            }
            catch { }
            finally
            {
                connection.Close();
            }
            return lst;
        }
    }
}