using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class InvoiceDAL
    {
        private MySqlConnection connection = DbHelper.GetConnection();
        public bool CreateInvoice(Invoice invoice)
        {
            if (invoice == null || invoice.ShoesList == null || invoice.ShoesList.Count == 0)
            {
                return false;
            }
            bool result = false;
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.Connection = connection;

                cmd.CommandText = "lock tables Staffs write,Invoices write, Customers write, Shoes write, sizes write,  colors write, invoicedetails write, shoesdetails write;";
                cmd.ExecuteNonQuery();
                MySqlTransaction trans = connection.BeginTransaction();
                cmd.Transaction = trans;
                MySqlDataReader reader = null;
                if (invoice.OrderCustomer == null || invoice.OrderCustomer.CustomerName == null || invoice.OrderCustomer.CustomerName == "")
                {

                    invoice.OrderCustomer = new Customer() { CustomerId = 1 };
                }
                try
                {
                    if (invoice.OrderCustomer.CustomerId == null)
                    {

                        cmd.CommandText = @"insert into Customers(customer_name,customer_phone ,customer_address)
                         values ('" + invoice.OrderCustomer.CustomerName + "','" + invoice.OrderCustomer.CustomerPhone + "','" + (invoice.OrderCustomer.CustomerAddress ?? "") + "');";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "select customer_id from Customers order by customer_id desc limit 1;";
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            invoice.OrderCustomer.CustomerId = reader.GetInt32("customer_id");
                        }
                        reader.Close();
                    }
                    else
                    {

                        cmd.CommandText = "select * from Customers where customer_id=@customerId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@customerId", invoice.OrderCustomer.CustomerId);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            invoice.OrderCustomer = new CustomerDAL().GetCustomer(reader);
                        }
                        reader.Close();
                    }
                    if (invoice.OrderCustomer == null || invoice.OrderCustomer.CustomerId == null)
                    {
                        throw new Exception("Can't find Customer!");
                    }

                    cmd.CommandText = "insert into Invoices(customer_id, invoice_status) values (@customerId, @invoiceStatus);";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@customerId", invoice.OrderCustomer.CustomerId);
                    cmd.Parameters.AddWithValue("@invoiceStatus", InvoiceStatus.CREATE_NEW_INVOICE);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select LAST_INSERT_ID() as invoice_no";
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        invoice.InvoiceNo = reader.GetInt32("invoice_no");
                    }
                    reader.Close();


                    foreach (var shoe in invoice.ShoesList)
                    {
                        if (shoe.ShoeId == null || shoe.ShoeQuantity <= 0)
                        {
                            throw new Exception("Not Exists Item");
                        }

                        cmd.CommandText = "select shoe_price from Shoes where shoe_id=@shoeId";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@shoeId", shoe.ShoeId);
                        reader = cmd.ExecuteReader();
                        if (!reader.Read())
                        {
                            throw new Exception("Not Exists Item");
                        }
                        shoe.ShoePrice = reader.GetDouble("shoe_price");
                        reader.Close();
                        cmd.CommandText = @"insert into InvoiceDetails(invoice_no, shoe_id, color_id,size_id, amount) values 
                            (" + invoice.InvoiceNo + ", " + shoe.ShoeId + ", " + shoe.sc.ColorId + ", " + shoe.sc.SizeId + ", " + shoe.ShoeQuantity + ");";
                        cmd.ExecuteNonQuery();
                        // update quantity in shoes table
                        cmd.CommandText = "update Shoes set shoe_quantity=shoe_quantity-@amount where shoe_id=" + shoe.ShoeId + ";";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@amount", shoe.ShoeQuantity);
                        cmd.ExecuteNonQuery();
                        // update quantity in shoedetails table
                        cmd.CommandText = "update shoesdetails set quantity = quantity - @amount where shoe_id =" + shoe.ShoeId + " and color_id =" + shoe.sc.ColorId + " and size_id = " + shoe.sc.SizeId + ";";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@amount", shoe.ShoeQuantity);
                        cmd.ExecuteNonQuery();
                        // update total price in invoicedetails
                        cmd.CommandText = "update invoicedetails inner join shoes on shoes.shoe_id = invoicedetails.shoe_id set total_price = shoes.shoe_price  * amount where invoice_no =  " + invoice.InvoiceNo + ";";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@amount", shoe.ShoeQuantity);
                        cmd.ExecuteNonQuery();
                        // update total staff_id
                        cmd.CommandText = "update invoices set invoices.staff_id = 1 where invoice_no =  " + invoice.InvoiceNo + ";";
                        cmd.Parameters.Clear();
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    try
                    {
                        trans.Rollback();
                    }
                    catch { }
                }
                finally
                {
                    cmd.CommandText = "unlock tables;";
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public Invoice GetInvoice(Invoice invoice)
        {
            string query;
            try
            {
                connection.Open();
                query = @"select invoices.invoice_no,invoice_date,customer_name,customer_phone,customer_address,shoe_name,brand_name,shoe_price,
                            color_name,size_number,amount,total_price 
                            from invoicedetails inner join invoices 
                            on invoicedetails.invoice_no = invoices.invoice_no inner join
                            customers on customers.customer_id = invoices.customer_id
                            inner join shoes on shoes.shoe_id = invoicedetails.shoe_id
                            inner join colors on colors.color_id = invoicedetails.color_id
                            inner join sizes on sizes.size_id = invoicedetails.size_id
                            where invoices.invoice_no =@invoiceNo;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invoiceNo", invoice.InvoiceNo);
                MySqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                Console.WriteLine("|                                                                                                 |");
                Console.WriteLine("|                                         INVOICE                                                 |");
                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                Console.WriteLine("|                                                          Address: 18,Tam Trinh,Minh Khai,HBT,HN |");
                Console.WriteLine("|                                                                            Hotline: 024 000 000 |");
                // Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                if (reader.Read())
                {
                    invoice.InvoiceNo = reader.GetInt32("invoice_no");
                    invoice.InvoiceDate = reader.GetDateTime("invoice_date");
                    string CustomerName = reader.GetString("customer_name");
                    string CustomerPhone = reader.GetString("customer_phone");
                    string CustomerAddress = reader.GetString("customer_address");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("| Invoice No : {0,-1}                                                   DateTime: {1,-2} |", invoice.InvoiceNo, invoice.InvoiceDate);
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("| Customer Name    :{0,-78}|", CustomerName);
                    Console.WriteLine("| Customer Phone   :{0,-78}|", CustomerPhone);
                    Console.WriteLine("| Customer Address :{0,-78}|", CustomerAddress);
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("| Shoe Name          | Brand      | Price    | Color           | Size   | Amount |  Total price   |");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                }
                reader.Close();
                MySqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    invoice.InvoiceNo = read.GetInt32("invoice_no");
                    invoice.InvoiceDate = read.GetDateTime("invoice_date");
                    string CustomerName = read.GetString("customer_name");
                    string CustomerPhone = read.GetString("customer_phone");
                    string CustomerAddress = read.GetString("customer_address");
                    string ShoeName = read.GetString("shoe_name");
                    string BrandName = read.GetString("brand_name");
                    double ShoePrice = read.GetDouble("shoe_price");
                    string ColorName = read.GetString("color_name");
                    int SizeNumber = read.GetInt32("size_number");
                    invoice.amount = read.GetInt32("amount");
                    invoice.TotalPrice = read.GetDouble("total_price");
                    Console.WriteLine("| {0,-18} | {1,-10} | {2,-8} | {3,-15} | {4,-6} | {5,-6} | {6,-14} |", ShoeName, BrandName, ShoePrice, ColorName, SizeNumber, invoice.amount, invoice.TotalPrice);
                }
                reader.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            try
            {
                connection.Open();
                query = @"select invoices.invoice_no,invoice_date,customer_name,customer_phone,customer_address,shoe_name,brand_name,shoe_price,
                            color_name,size_number,amount,total_price, SUM(total_price) as total
                            from invoicedetails inner join invoices 
                            on invoicedetails.invoice_no = invoices.invoice_no inner join
                            customers on customers.customer_id = invoices.customer_id
                            inner join shoes on shoes.shoe_id = invoicedetails.shoe_id
                            inner join colors on colors.color_id = invoicedetails.color_id
                            inner join sizes on sizes.size_id = invoicedetails.size_id
                            where invoices.invoice_no =@invoiceNo;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invoiceNo", invoice.InvoiceNo);
                MySqlDataReader r = command.ExecuteReader();
                if (r.Read())
                {
                    invoice.InvoiceNo = r.GetInt32("invoice_no");
                    double total = r.GetDouble("total");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("| Total                                                                          |  Total price   |");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("|                                                                                |  {0,-11}   |", total);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return invoice;
        }
        public Invoice GetInvoiceNo(int invoiceNo)
        {
            Invoice invoice = new Invoice();
            string query;
            try
            {
                connection.Open();
                query = @"select invoices.invoice_no,invoice_date,customer_name,customer_phone,customer_address,shoe_name,brand_name,shoe_price,
                            color_name,size_number,amount,total_price,staff_name 
                            from invoicedetails inner join invoices 
                            on invoicedetails.invoice_no = invoices.invoice_no inner join
                            customers on customers.customer_id = invoices.customer_id
                            inner join shoes on shoes.shoe_id = invoicedetails.shoe_id
                            inner join colors on colors.color_id = invoicedetails.color_id
                            inner join sizes on sizes.size_id = invoicedetails.size_id
                            inner join staffs on staffs.staff_id= invoices.staff_id 
                            where invoices.invoice_no =@invoiceNo;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invoiceNo", invoiceNo);
                MySqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("|          _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ ____           |");
                Console.WriteLine("|         |   __|  |  |     |   __|   __|  |   __|_   _|     | __  |   __|  |   | |    `          |");
                Console.WriteLine("|         |__   |     |  |  |   __|__   |  |__   | | | |  |  |    -|   __|  | | | |  |  |         |");
                Console.WriteLine("|         |_____|__|__|_____|_____|_____|  |_____| |_| |_____|__|__|_____|  |_|___|____/          |");
                Console.WriteLine("|                                                                                                 |");
                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                Console.WriteLine("|                                                          Address: 18,Tam Trinh,Minh Khai,HBT,HN |");
                Console.WriteLine("|                                                                            Hotline: 024 000 000 |");
                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                if (reader.Read())
                {
                    invoice.InvoiceNo = reader.GetInt32("invoice_no");
                    invoice.InvoiceDate = reader.GetDateTime("invoice_date");
                    string CustomerName = reader.GetString("customer_name");
                    string CustomerPhone = reader.GetString("customer_phone");
                    string CustomerAddress = reader.GetString("customer_address");

                    Console.WriteLine("| Invoice No : {0}                                                   DateTime: {1,-2} |", invoice.InvoiceNo, invoice.InvoiceDate);
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("| Customer Name    :{0,-78}|", CustomerName);
                    Console.WriteLine("| Customer Phone   :{0,-78}|", CustomerPhone);
                    Console.WriteLine("| Customer Address :{0,-78}|", CustomerAddress);
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("| Shoe Name          | Brand      | Price    | Color           | Size   | Amount |  Total price   |");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                }
                reader.Close();
                MySqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    invoice.InvoiceNo = read.GetInt32("invoice_no");
                    invoice.InvoiceDate = read.GetDateTime("invoice_date");
                    string CustomerName = read.GetString("customer_name");
                    string CustomerPhone = read.GetString("customer_phone");
                    string CustomerAddress = read.GetString("customer_address");
                    string ShoeName = read.GetString("shoe_name");
                    string BrandName = read.GetString("brand_name");
                    double ShoePrice = read.GetDouble("shoe_price");
                    string ColorName = read.GetString("color_name");
                    int SizeNumber = read.GetInt32("size_number");
                    invoice.amount = read.GetInt32("amount");
                    invoice.TotalPrice = read.GetDouble("total_price");
                    Console.WriteLine("| {0,-18} | {1,-10} | {2,8:###,000} | {3,-15} | {4,-6} | {5,-6} | {6,11:###,000} VND|", ShoeName, BrandName, ShoePrice, ColorName, SizeNumber, invoice.amount, invoice.TotalPrice);

                }
                read.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            try
            {
                connection.Open();
                query = @"select invoices.invoice_no,invoice_date,customer_name,customer_phone,customer_address,shoe_name,brand_name,shoe_price,
                            color_name,size_number,amount,total_price,staff_name, SUM(total_price) as total
                            from invoicedetails inner join invoices 
                            on invoicedetails.invoice_no = invoices.invoice_no inner join
                            customers on customers.customer_id = invoices.customer_id
                            inner join shoes on shoes.shoe_id = invoicedetails.shoe_id
                            inner join colors on colors.color_id = invoicedetails.color_id
                            inner join sizes on sizes.size_id = invoicedetails.size_id
                            inner join staffs on staffs.staff_id= invoices.staff_id 
                            where invoices.invoice_no =@invoiceNo;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invoiceNo", invoice.InvoiceNo);
                MySqlDataReader r = command.ExecuteReader();
                if (r.Read())
                {
                    invoice.InvoiceNo = r.GetInt32("invoice_no");
                    double total = r.GetDouble("total");
                    invoice.InvoiceNo = r.GetInt32("invoice_no");
                    string staffName = r.GetString("staff_name");
                    string CustomerName = r.GetString("customer_name");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("| Total                                                                          |  Total price   |");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("|                                                                                | {0,11:###,000} VND|", total);
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine("|                                                                   Export Invoice By Staff       |");
                    Console.WriteLine("|                                                                                                 |");
                    Console.WriteLine("|                                                                          {0}               |", staffName);
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────┤");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return invoice;
        }
    }
}
