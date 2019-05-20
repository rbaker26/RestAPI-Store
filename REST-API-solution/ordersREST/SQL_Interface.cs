using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

using REST_lib;

namespace ordersREST
{
    public sealed class SQL_Interface
    {

        //*****************************************************************************************
        #region sql config region#region sql config region
        private static MySqlConnection m_dbConnection = null;

        private static readonly Lazy<SQL_Interface>
        m_lazy = new Lazy<SQL_Interface>(() => new SQL_Interface());

        public static SQL_Interface Instance { get { return m_lazy.Value; } }
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


        //*****************************************************************************************
        private static MySqlConnection MakeConnectionPool()
        {
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder
            {
                Server = "68.5.123.182",
                Database = "ordersREST_db",
                UserID = "recorder",
                Password = "recorder0",
                MinimumPoolSize = 20,
                MaximumPoolSize = 50
            };

            return new MySqlConnection(mscsb.ToString());
        }
        #endregion
        //*****************************************************************************************

        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            MySqlDataReader mysql_datareader = null; ;
            MySqlCommand command = null;
            try
            {
                string query = "SELECT * FROM orders";
                command = m_dbConnection.CreateCommand();
                command.Connection.Open();
                command.CommandText = query;

                mysql_datareader = command.ExecuteReader();

                Order temp = new Order();


                while (mysql_datareader.Read())
                {
                    //temp.ProductId = mysql_datareader.GetInt32(0);
                    //temp.Description = mysql_datareader.GetString(1);
                    //temp.Quantity = mysql_datareader.GetInt32(2);
                    //temp.Price = mysql_datareader.GetFloat(3);


                    //*************************
                    //* Debug Code            *
                    //*************************
                    //Console.WriteLine("\n\n\n\n****************************************************************************");
                    //Console.WriteLine(temp);
                    //*************************

                    orders.Add(temp);
                    temp = new Order();
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
                mysql_datareader?.Close();
                command?.Connection?.Close();
            }


            return orders;
        }

        public void AddNewOrder(string email, List<ProductUpdate> orderList)
        {
            MySqlCommand command1 = null;
            MySqlCommand command2 = null;
            MySqlCommand command3 = null;
            try
            {
                //string query = "SELECT * FROM products";
                // SQLiteCommand command = m_dbConnection.CreateCommand();
                using (MySqlTransaction trans = m_dbConnection.BeginTransaction())
                {
                    command1 = m_dbConnection.CreateCommand();
                    command1.Connection.Open();


                    // Inset into `orders` table
                    string query1 = "INSERT INTO orders (user_id) VALUES ( (@email) );";
                    command1.CommandText = query1;
                    command1.Parameters.Add("@email", MySqlDbType.String).Value = email;

                    int rows_affected = command1.ExecuteNonQuery();


                    // Get new order_id from the above query
                    command2 = m_dbConnection.CreateCommand();
                    command2.Connection.Open();
                    string query2 = "SELECT order_id FROM orders WHERE user_id = (@email) ORDER BY order_id DESC limit 1;";
                    command2.CommandText = query2;
                    command2.Parameters.Add("@email", MySqlDbType.String).Value = email;
                    MySqlDataReader mysql_datareader;

                    mysql_datareader = command2.ExecuteReader();
                    int orderId = -1;
                    while (mysql_datareader.Read())
                    {
                        orderId = mysql_datareader.GetInt32(0);
                    }
                    mysql_datareader.Close();


                    // Insert the list of product updates into the `orders_list` tables
                    command3 = m_dbConnection.CreateCommand();
                    command3.Connection.Open();
                    string query3 = "INSERT INTO orders_list (order_id, product_id, quantity) VALUES ( (@orderID), (@productID), (@quantity) );";
                    command3.CommandText = query3;

                    // ya i know, shut up
                    bool first = true;
                    foreach(ProductUpdate productUpdate in orderList)
                    {
                        if(first)
                        {
                            command3.Parameters.Add("@orderID", MySqlDbType.Int32).Value = orderId;
                            command3.Parameters.Add("@productID", MySqlDbType.Int32).Value = productUpdate.ProductId;
                            command3.Parameters.Add("@quantity", MySqlDbType.Int32).Value = productUpdate.QuantityToBeRemoved;
                            first = false;
                        }
                        else
                        {
                            command3.Parameters["@orderID"].Value = orderId;
                            command3.Parameters["@productID"].Value = productUpdate.ProductId; 
                            command3.Parameters["@quantity"].Value = productUpdate.QuantityToBeRemoved; 
                        }
                        
                        int row_affected = command3.ExecuteNonQuery();
                    }
                    

                    trans.Commit();
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
                command1?.Connection?.Close();
                command2?.Connection?.Close();
                command3?.Connection?.Close();
            }

        }


    }
}
