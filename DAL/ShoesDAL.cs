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
            // SizesColors cls = null;

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
            catch (Exception e)
            { Console.WriteLine(e); }
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
            shoe.ShoePrice = reader.GetDouble("shoe_price");
            shoe.ShoeQuantity = reader.GetInt32("shoe_quantity");
            shoe.ShoeDesception = reader.GetString("shoe_desception");
            return shoe;
        }
        internal Shoes GetShoeMany(MySqlDataReader reader)
        {
            Shoes shoe = new Shoes();
            shoe.ShoeId = reader.GetInt32("shoe_id");
            shoe.ShoeName = reader.GetString("shoe_name");
            shoe.BrandName = reader.GetString("brand_name");
            shoe.ShoePrice = reader.GetDouble("shoe_price");
            shoe.ShoeQuantity = reader.GetInt32("shoe_quantity");
            shoe.ShoeDesception = reader.GetString("shoe_desception");
            Console.WriteLine("| {0,-2} | {1,-20} | {2,10:###,000} VND| {3,-15}| {4,-16} | MADE IN {5,-7} |", shoe.ShoeId, shoe.ShoeName, shoe.ShoePrice, shoe.BrandName, shoe.ShoeQuantity, shoe.ShoeDesception);
            return shoe;
        }

        public List<Shoes> GetShoes(int shoeFilter, Shoes shoe)
        {
            lock (connection)
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
                                     from Shoes where shoe_name like concat('%',@shoeName,'%');";
                            command.Parameters.AddWithValue("@shoeName", shoe.ShoeName);
                            break;
                        case ShoeFilter.FILTER_BY_BRAND_NAME:
                            query = @"select shoe_id, shoe_name, shoe_price, brand_name,shoe_quantity,
                                    ifnull(shoe_desception, '') as shoe_desception 
                                     from Shoes where brand_name like concat('%',@brandName,'%');";
                            command.Parameters.AddWithValue("@brandName", shoe.BrandName);
                            break;
                    }
                    command.CommandText = query;
                    MySqlDataReader reader = command.ExecuteReader();
                    lst = new List<Shoes>();
                    Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                    Console.WriteLine("| ID | Name \t\t    | Price  \t    | Brand\t     | Quantity \t|  Description    |");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    while (reader.Read())
                    {
                        lst.Add(GetShoeMany(reader));
                    }
                    reader.Close();
                    Console.WriteLine("└─────────────────────────────────────────────────────────────────────────────────────────────────┘");

                }
                catch { }
                finally
                {
                    connection.Close();
                }
                return lst;
            }
        }
        public SizesColors Color(int shoeId)
        {
            lock (connection)
            {
                SizesColors clc = null;
                try
                {
                    connection.Open();
                    query = @"SELECT DISTINCT colors.color_id,color_name FROM shoesdetails inner join Colors on colors.color_id = shoesdetails.color_id inner join shoes on shoes.shoe_id =shoesdetails.shoe_id where shoes.shoe_id=@shoeId;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@shoeId", shoeId);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        clc = PrColor(reader);
                    }
                    reader.Close();
                }
                catch
                { }
                finally
                {
                    connection.Close();
                }
                return clc;
            }
        }
        public SizesColors GetSizes(int shoeId, int colorId)
        {
            lock (connection)
            {
                SizesColors cls = null;
                try
                {
                    connection.Open();
                    query = @"select Sizes.size_id,size_number,quantity from shoesdetails inner join Colors 
                                on colors.color_id = shoesdetails.color_id inner join Sizes 
                                on sizes.size_id =shoesdetails.size_id
                                inner join shoes  on shoes.shoe_id = shoesdetails.shoe_id where shoes.shoe_id=@shoeId
                                and Colors.color_id = @colorId;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@shoeId", shoeId);
                    command.Parameters.AddWithValue("@colorId", colorId);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cls = PrSizes(reader);
                    }
                    reader.Close();
                }
                catch
                { }
                finally
                {
                    connection.Close();
                }
                return cls;
            }
        }
        public SizesColors PrSizes(MySqlDataReader reader)
        {
            SizesColors colors = new SizesColors();
            colors.SizeId = reader.GetInt32("size_id");
            colors.SizeNumber = reader.GetInt32("size_number");
            colors.Quantity = reader.GetInt32("quantity");
            Console.WriteLine("| {0,-5} | {1,-5} | {2,-10} |", colors.SizeId, colors.SizeNumber, colors.Quantity);
            return colors;
        }

        public SizesColors PrColor(MySqlDataReader reader)
        {
            SizesColors colors = new SizesColors();
            colors.ColorId = reader.GetInt32("color_id");
            colors.ColorName = reader.GetString("color_name");
            Console.WriteLine("| {0,-2} | {1,-20}", colors.ColorId, colors.ColorName);
            return colors;
        }
        public Shoes GetToInvocie(int sId,int cId,int szId)
        {
            Shoes shoe = null;
            try
            {
                connection.Open();
                query = @"select shoe_id,color_id,size_id from shoesdetails where shoe_id=@sId and color_id=@cId and size_id=@szId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@sId", sId);
                command.Parameters.AddWithValue("@cId", cId);
                command.Parameters.AddWithValue("@szId", szId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    shoe = GetShoeToInvoice(reader);
                }
                reader.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e); }
            finally
            {
                connection.Close();
            }
            return shoe;
        }
        internal Shoes GetShoeToInvoice(MySqlDataReader reader)
        {
            Shoes shoe = new Shoes();
            shoe.ShoeId = reader.GetInt32("shoe_id");
            shoe.sc.ColorId = reader.GetInt32("color_id");
            shoe.sc.SizeId = reader.GetInt32("size_id");
            return shoe;
        }

    }
}