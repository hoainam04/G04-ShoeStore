using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class InvoiceDAL
    {

    private MySqlConnection connection = DbConfig.GetConnection();
    public bool CreateOrder(Invoice invoice)
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
        //Lock update all tables
        cmd.CommandText = "lock tables Customers write, Invoices write, Shoes write, InvoiceDetails write;";
        cmd.ExecuteNonQuery();
        MySqlTransaction trans = connection.BeginTransaction();
        cmd.Transaction = trans;
        MySqlDataReader reader = null;
        if (invoice.Customer == null || invoice.Customer.CustomerName == null || invoice.Customer.CustomerName == "")
        {
          //set default customer with customer id = 1
          invoice.Customer = new Customer() { CustomerId = 1 };
        }
        try
        {
          if (invoice.Customer.CustomerId == null)
          {
            //Insert new Customer
            cmd.CommandText = @"insert into Customers(customer_name, customer_phone ,customer_address)
                            values ('" + invoice.Customer.CustomerName + "','"+invoice.Customer.CustomerPhone +"','" + (invoice.Customer.CustomerAddress ?? "") + "');";
            cmd.ExecuteNonQuery();
            //Get new customer id
            cmd.CommandText = "select customer_id from Customers order by customer_id desc limit 1;";
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
              invoice.Customer.CustomerId = reader.GetInt32("customer_id");
            }
            reader.Close();
          }
          else
          {
            //get Customer by Id
            cmd.CommandText = "select * from Customers where customer_id=@customerId;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@customerId", invoice.Customer.CustomerId);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
              invoice.Customer = new CustomerDAL().GetCustomer(reader);
            }
            reader.Close();
          }
          if (invoice.Customer == null || invoice.Customer.CustomerId == null)
          {
            throw new Exception("Can't find Customer!");
          }
          //Insert Order
          cmd.CommandText = "insert into Orders(customer_id, invoice_status) values (@customerId, @invoiceStatus);";
          cmd.Parameters.Clear();
          cmd.Parameters.AddWithValue("@customerId", invoice.Customer.CustomerId);
          cmd.Parameters.AddWithValue("@invoiceStatus", InvoiceStatus.CREATE_NEW_INVOICE);
          cmd.ExecuteNonQuery();
          //get new Order_ID
          cmd.CommandText = "select LAST_INSERT_ID() as invoice_no";
          reader = cmd.ExecuteReader();
          if (reader.Read())
          {
            invoice.InvoiceNo = reader.GetInt32("invoice_no");
          }
          reader.Close();

          //insert Order Details table
          foreach (var shoe in invoice.ShoesList)
          {
            if (shoe.ShoeId == null || shoe.ShoeQuantity <= 0)
            {
              throw new Exception("Not Exists Item");
            }
            //get unit_price
            cmd.CommandText = "select shoe_price from Items where shoe_id=@shoeId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@shoeId", shoe.ShoeId);
            reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
              throw new Exception("Not Exists Item");
            }
            shoe.ShoePrice = reader.GetDouble("shoe_price");
            reader.Close();

            //insert to Order Details
            cmd.CommandText = @"insert into OrderDetails(invoice_no, shoe_id, shoe_price, amount) values 
                            (" + invoice.InvoiceNo + ", " + shoe.ShoeId + ", " + shoe.ShoePrice + ", " + shoe.ShoeQuantity + ");";
            cmd.ExecuteNonQuery();

            //update amount in Items
            cmd.CommandText = "update Shoes set shoe_quantity=shoe_quantity-@amount where shoe_id=" + shoe.ShoeId + ";";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@amount", shoe.ShoeQuantity);
            cmd.ExecuteNonQuery();
          }
          //commit transaction
          trans.Commit();
          result = true;
        }
        catch
        {
          try
          {
            trans.Rollback();
          }
          catch { }
        }
        finally
        {
          //unlock all tables;
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
  }
}