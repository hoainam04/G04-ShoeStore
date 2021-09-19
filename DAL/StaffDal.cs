using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class StaffDal
    {
        private MySqlConnection connection = DbHelper.GetConnection();
        public Staff Login(Staff staff)
        {
            lock (connection)
            {
                try
                {
                    connection.Open();

                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "select * from Staffs where user_name=@userName and user_pass=@userPass;";
                    command.Parameters.AddWithValue("@userName", staff.UserName);
                    command.Parameters.AddWithValue("@userPass", Md5Algorithms.CreateMD5(staff.Password));
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        staff.Role = reader.GetInt32("role");
                    }
                    else
                    {
                        staff.Role = 0;
                    }
                    string name = reader["staff_name"].ToString();
                    Console.WriteLine("\n\t\t\t...Wellcome to System "+name+"...\n");
                    reader.Close();
                }
                catch
                {
                    staff.Role = -1;
                }
                finally
                {
                    connection.Close();
                }
            }
            return staff;
        }

        public int Insert(Staff staff)
        {
            int? result = null;
            MySqlConnection connection = DbHelper.GetConnection();
            string sql = @"insert into Staffs(staff_name, user_name, user_pass, role) values 
                      (@staffName, @userName, @userPass, @role);";
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@staffName", staff.StaffName);
                    command.Parameters.AddWithValue("@userName", staff.UserName);
                    command.Parameters.AddWithValue("@userPass", staff.Password);
                    command.Parameters.AddWithValue("@role", staff.Role);
                    result = command.ExecuteNonQuery();
                }
                catch
                {
                    result = -2;
                }
                finally
                {
                    connection.Close();
                }
            }
            return result ?? 0;
        }

        public List<Staff> GetAll()
        {
            return null;
        }
    }
}