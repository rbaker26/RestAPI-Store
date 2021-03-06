﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

using REST_lib;

namespace cartREST
{
    public sealed class SQL_Interface
    {
        //*****************************************************************************************
        #region sql config region
        private static MySqlConnection m_dbConnection = null;

        private static readonly Lazy<SQL_Interface>
        m_lazy = new Lazy<SQL_Interface>(() => new SQL_Interface());

        public static SQL_Interface Instance { get { lock (m_lazy) { return m_lazy.Value; } } }
        //*****************************************************************************************


        //*****************************************************************************************
        private SQL_Interface()
        {
            // SQLiteConnection.CreateFile("MyDatabase.sqlite");
            // this.m_dbConnection = new SQLiteConnection("Data Source=C:\\Users\\007ds\\Documents\\GitHub\\RestAPI-Store\\ProductsREST\\ProductsREST\\products.db;Version=3;");
            m_dbConnection = MakeConnectionPool();
        }
        //*****************************************************************************************


        //*****************************************************************************************
        ~SQL_Interface()
        {
            try
            {
                m_dbConnection.Close();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }

        }

        //*****************************************************************************************
        private static MySqlConnection MakeConnectionPool()
        {
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder
            {
                Server = "68.5.123.182",
                Database = "cartREST_db",
                UserID = "recorder",
                Password = "recorder0",
                MinimumPoolSize = 20,
                MaximumPoolSize = 50
            };

            return new MySqlConnection(mscsb.ToString());
        }
        #endregion
        //*****************************************************************************************

        public void AddProductToCart(string email, ProductUpdate productUpdate)
        {
            int ProductId = productUpdate.ProductId;
            int Quantity = productUpdate.QuantityToBeRemoved;

            MySqlCommand command = null;
            MySqlDataReader mysql_datareader = null;
            try
            {
                // set if item exits
                command = m_dbConnection.CreateCommand();
                command.Connection.Open();
                string query0 = "SELECT * FROM cart WHERE user_id = (@userID) and product_id = (@productID) and purchased = false;";
                command.CommandText = query0;
                command.Parameters.Add("@userID", MySqlDbType.String).Value = email;
                command.Parameters.Add("@productID", MySqlDbType.Int32).Value = ProductId;


                mysql_datareader = command.ExecuteReader();
                ProductUpdate temp = new ProductUpdate();
                temp.ProductId = -1;
                temp.QuantityToBeRemoved = -1;
                while (mysql_datareader.Read())
                {
                    temp.ProductId = mysql_datareader.GetInt32(2);
                    temp.QuantityToBeRemoved = mysql_datareader.GetInt32(3);
                }
                command.Connection.Close();
                command = null;



                if (temp.ProductId == -1)
                {
                    // Get Quantity of item in database
                    string query1 = "INSERT into cart( user_id, product_id, quantity, purchased ) VALUES ( (@user_id), (@product_id), (@quantity), (@purchased) );";
                    command = m_dbConnection.CreateCommand();
                    command.Connection.Open();
                    command.CommandText = query1;
                    command.Parameters.Add("@user_id", MySqlDbType.String).Value = email;
                    command.Parameters.Add("@product_id", MySqlDbType.Int32).Value = ProductId;
                    command.Parameters.Add("@quantity", MySqlDbType.Int32).Value = Quantity;
                    command.Parameters.Add("@purchased", MySqlDbType.Int32).Value = false;

                    int rows_affected = command.ExecuteNonQuery();
                }
                else
                {
                    string query1 = "UPDATE cart SET quantity = (@newQuantity) WHERE product_id = (@productID) AND user_id = (@userID) ;";
                    command = m_dbConnection.CreateCommand();
                    command.Connection.Open();
                    command.CommandText = query1;
                    command.Parameters.Add("@productID", MySqlDbType.Int32).Value = ProductId;
                    command.Parameters.Add("@newQuantity", MySqlDbType.Int32).Value = Quantity + temp.QuantityToBeRemoved;
                    command.Parameters.Add("@userID", MySqlDbType.String).Value = email;
                    int rows_affected = command.ExecuteNonQuery();
                }






            }
            catch (Exception e)
            {
                Console.Out.WriteLine("\n\n**********************************************************************");
                Console.Out.WriteLine(e.Message);
                Console.Out.WriteLine(e.InnerException);
                Console.Out.WriteLine(e.Source);
                Console.Out.WriteLine("**********************************************************************\n\n");
            }
            finally
            {
                command?.Connection?.Close();
                mysql_datareader?.Close();
            }

        }


        public Cart PurchaseCart(string email, bool purchase)
        {
            List<ProductUpdate> updates = new List<ProductUpdate>();

            MySqlCommand command1 = null;
            MySqlCommand command2 = null;
            MySqlDataReader mysql_datareader = null;

            try
            {
                // Get Quantity of item in database
                string query1 = "SELECT product_id, quantity FROM cart WHERE user_id = (@user_id) AND purchased = 0;";
                command1 = m_dbConnection.CreateCommand();
                command1.Connection.Open();

                command1.CommandText = query1;
                command1.Parameters.Add("@user_id", MySqlDbType.String).Value = email;


                mysql_datareader = command1.ExecuteReader();
                ProductUpdate temp = new ProductUpdate();
                while (mysql_datareader.Read())
                {
                    temp.ProductId = mysql_datareader.GetInt32(0);
                    temp.QuantityToBeRemoved = mysql_datareader.GetInt32(1);

                    //*************************
                    //* Debug Code            *
                    //*************************
                    //Console.WriteLine("\n\n\n\n****************************************************************************");
                    //Console.WriteLine(temp);
                    //*************************

                    updates.Add(temp);
                    temp = new ProductUpdate();
                }
                

                if (purchase)
                {
                    command1.Connection?.Close();
                    string query2 = "UPDATE cart SET purchased = 1 WHERE user_id = (@user_id) AND purchased = 0;";
                    command2 = m_dbConnection.CreateCommand();
                    command2.Connection.Open();
                    command2.CommandText = query2;
                    command2.Parameters.Add("@user_id", MySqlDbType.String).Value = email;
                    int row_affected = command2.ExecuteNonQuery();
                }

               // Console.Out.WriteLine("Rows affected:\t" + row_affected);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("\n\n**********************************************************************");
                Console.Out.WriteLine(e.Message);
                Console.Out.WriteLine(e.InnerException);
                Console.Out.WriteLine(e.Source);
                Console.Out.WriteLine("**********************************************************************\n\n");
            }
            finally
            {
                mysql_datareader?.Close();
                command1?.Connection?.Close();
                command2?.Connection?.Close();
            }

            return new Cart(email, updates);

        }
        public void RemoveProduct(string email, int productID)
        {
            MySqlCommand command = null;
            try
            {
            string query = "DELETE FROM cart WHERE user_id = (@user_id) AND product_id = (@product_id);";
            command = m_dbConnection.CreateCommand();
            command.Connection.Open();
            command.CommandText = query;
            command.Parameters.Add("@user_id", MySqlDbType.String).Value = email;
            command.Parameters.Add("@product_id", MySqlDbType.Int32).Value = productID;

            int rows_affected = command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Console.Out.WriteLine("\n\n**********************************************************************");
                Console.Out.WriteLine(e.Message);
                Console.Out.WriteLine(e.InnerException);
                Console.Out.WriteLine(e.Source);
                Console.Out.WriteLine("**********************************************************************\n\n");
            }
            finally
            {
                command?.Connection?.Close();
            }
        }
        //*****************************************************************************************

        //public List<Product> GetProducts()
        //{
        //    List<Product> products = new List<Product>();
        //    //products.Add(new Product(1, "Hi", 55, 128.20f));

        //    // SQLiteDataReader sqlite_datareader;
        //    MySqlDataReader mysql_datareader;
        //    try
        //    {
        //        string query = "SELECT product_id, description, quantity, price FROM products";
        //        //string query = "SELECT * FROM products";
        //        // SQLiteCommand command = m_dbConnection.CreateCommand();
        //        MySqlCommand command = m_dbConnection.CreateCommand();
        //        command.CommandText = query;

        //        mysql_datareader = command.ExecuteReader();

        //        Product temp = new Product();


        //        while (mysql_datareader.Read())
        //        {
        //            temp.ProductId = mysql_datareader.GetInt32(0);
        //            temp.Description = mysql_datareader.GetString(1);
        //            temp.Quantity = mysql_datareader.GetInt32(2);
        //            temp.Price = mysql_datareader.GetFloat(3);


        //            //*************************
        //            //* Debug Code            *
        //            //*************************
        //            Console.WriteLine("\n\n\n\n****************************************************************************");
        //            Console.WriteLine(temp);
        //            //*************************

        //            products.Add(temp);
        //            temp = new Product();
        //        }
        //        mysql_datareader.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Out.WriteLine("\n\n**********************************************************************");
        //        Console.Out.WriteLine(e.Message);
        //        Console.Out.WriteLine(e.InnerException);
        //        Console.Out.WriteLine(e.Source);
        //        Console.Out.WriteLine("**********************************************************************\n\n");

        //    }


        //    return products;
        //}

        //public Product GetProductById(int productId)
        //{
        //    Product temp = new Product();
        //    //products.Add(new Product(1, "Hi", 55, 128.20f));

        //    // SQLiteDataReader sqlite_datareader;
        //    MySqlDataReader mysql_datareader;
        //    try
        //    {
        //        string query = "SELECT product_id, description, quantity, price FROM products WHERE product_id = (@id)";
        //        //string query = "SELECT * FROM products";
        //        // SQLiteCommand command = m_dbConnection.CreateCommand();
        //        MySqlCommand command = m_dbConnection.CreateCommand();
        //        command.CommandText = query;
        //        command.Parameters.Add("@id", MySqlDbType.Int32).Value = productId;

        //        mysql_datareader = command.ExecuteReader();

        //        while (mysql_datareader.Read())
        //        {
        //            temp.ProductId = mysql_datareader.GetInt32(0);
        //            temp.Description = mysql_datareader.GetString(1);
        //            temp.Quantity = mysql_datareader.GetInt32(2);
        //            temp.Price = mysql_datareader.GetFloat(3);


        //            //*************************
        //            //* Debug Code            *
        //            //*************************
        //            //Console.WriteLine("\n\n\n\n****************************************************************************");
        //            //Console.WriteLine(temp);
        //            //*************************

        //        }
        //        mysql_datareader.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Out.WriteLine("\n\n**********************************************************************");
        //        Console.Out.WriteLine(e.Message);
        //        Console.Out.WriteLine(e.InnerException);
        //        Console.Out.WriteLine(e.Source);
        //        Console.Out.WriteLine("**********************************************************************\n\n");
        //    }

        //    return temp;
        //}


        //public void ReduceItemQuantity(ProductUpdate productUpdate)
        //{
        //    int ProductId = productUpdate.ProductId;
        //    int Quantity = productUpdate.QuantityToBeRemoved;

        //    MySqlDataReader mysql_datareader;
        //    int orgQuantity = -1;
        //    try
        //    {
        //        // Get Quantity of item in database
        //        string query1 = "SELECT quantity FROM products WHERE product_id = (@id) LIMIT 1;";
        //        MySqlCommand command = m_dbConnection.CreateCommand();
        //        command.CommandText = query1;
        //        command.Parameters.Add("@id", MySqlDbType.Int32).Value = ProductId;

        //        mysql_datareader = command.ExecuteReader();
        //        while (mysql_datareader.Read())
        //        {
        //            orgQuantity = mysql_datareader.GetInt32(0);
        //        }
        //        mysql_datareader.Close();

        //        // Make sure the is enough inventory
        //        if (orgQuantity < Quantity)
        //        {
        //            throw new ArgumentException("Purchased qantity is greater than stored quantity", "Quantity");
        //        }


        //        // Reduce the quantity of the item by n
        //        string query2 = "UPDATE products SET quantity = (@newQuantity) WHERE product_id = (@id);";
        //        command = m_dbConnection.CreateCommand();
        //        command.CommandText = query2;

        //        command.Parameters.Add("@id", MySqlDbType.Int32).Value = ProductId;
        //        command.Parameters.Add("@newQuantity", MySqlDbType.Int32).Value = orgQuantity - Quantity;



        //        int rows_affected = command.ExecuteNonQuery();
        //        //Console.Out.WriteLine("Rows Affected:\t" + rows_affected);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Out.WriteLine("\n\n**********************************************************************");
        //        Console.Out.WriteLine(e.Message);
        //        Console.Out.WriteLine(e.InnerException);
        //        Console.Out.WriteLine(e.Source);
        //        Console.Out.WriteLine("**********************************************************************\n\n");
        //    }



        //}

    }
}
