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
            m_dbConnection = new MySqlConnection("Server=68.5.123.182; database=ordersREST_db; UID=recorder; password=recorder0");
            m_dbConnection.Open();
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

        public List<Order> GetOrders(string email)
        {
			// I considered changing this to return a list of lists of ProductUpdates, but I think
			// it's better to return a list of orders. We can bundle order-specific things this way.

            List<Order> orders = new List<Order>();
            //products.Add(new Product(1, "Hi", 55, 128.20f));

            // SQLiteDataReader sqlite_datareader;
            try
            {
                string idQuery = "SELECT order_id FROM orders WHERE user_id = (@email)";
				//string query = "SELECT * FROM products";
				// SQLiteCommand command = m_dbConnection.CreateCommand();
				MySqlCommand idCommand = m_dbConnection.CreateCommand();
				idCommand.CommandText = idQuery;
				idCommand.Parameters.Add("@email", MySqlDbType.String).Value = email;

				List<int> orderIDs = new List<int>();

				//Order temp = new Order();

				using(MySqlDataReader mysql_datareader = idCommand.ExecuteReader()) {

					while(mysql_datareader.Read()) {
						orderIDs.Add(mysql_datareader.GetInt32(0));

						//*************************
						//* Debug Code            *
						//*************************
						//Console.WriteLine("\n\n\n\n****************************************************************************");
						//Console.WriteLine(orderIDs[orderIDs.Count - 1]);
						//*************************
					} // End of while
				} // End of using

				foreach(int id in orderIDs) {
					string orderQuery = "SELECT product_id, quantity, price FROM orders_list where order_id = (@id)";

					MySqlCommand orderCommand = m_dbConnection.CreateCommand();
					orderCommand.CommandText = orderQuery;
					orderCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

					Order resultOrder = new Order(id, email, TimeStamp: 0); // TODO TimeStamp lawl

					using(MySqlDataReader reader = orderCommand.ExecuteReader()) {
						while(reader.Read()) {
							int productID = reader.GetInt32(0);
							int quantity = reader.GetInt32(1);
							float price = -1;
							if(!reader.IsDBNull(2)) {
								price = reader.GetFloat(2);   // TODO This needs to be used somehow
							}

							resultOrder.ShoppingCart.Add(new ProductUpdate(productID, quantity));
						} // End while(reader.Read())
					} // End using(reader)

					orders.Add(resultOrder);

				} // End foreach(id)

            }
            catch (Exception e)
            {
                Console.Out.WriteLine("\n\n**********************************************************************");
                Console.Out.WriteLine(e.Message);
                Console.Out.WriteLine(e.InnerException);
                Console.Out.WriteLine(e.Source);
                Console.Out.WriteLine(e.StackTrace);
                Console.Out.WriteLine("**********************************************************************\n\n");

				// Clean this out; we don't want to return anything if we bumped into an exception.
				orders = new List<Order>();
            }


            return orders;
        }

        public void AddNewOrder(string email, List<ProductUpdate> orderList)
        {
            try
            {
                //string query = "SELECT * FROM products";
                // SQLiteCommand command = m_dbConnection.CreateCommand();
                using (MySqlTransaction trans = m_dbConnection.BeginTransaction())
                {
                    MySqlCommand command1 = m_dbConnection.CreateCommand();

                    // Inset into `orders` table
                    string query1 = "INSERT INTO orders (user_id) VALUES ( (@email) );";
                    command1.CommandText = query1;
                    command1.Parameters.Add("@email", MySqlDbType.String).Value = email;

                    int rows_affected = command1.ExecuteNonQuery();


                    // Get new order_id from the above query
                    MySqlCommand command2 = m_dbConnection.CreateCommand();
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
                    MySqlCommand command3 = m_dbConnection.CreateCommand();
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

        }


    }
}
