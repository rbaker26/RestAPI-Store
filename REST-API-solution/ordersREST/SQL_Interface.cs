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
                MinimumPoolSize = 10,
                MaximumPoolSize = 20
            };

            return new MySqlConnection(mscsb.ToString());
        }
        #endregion
        //*****************************************************************************************

			/*
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
		*/

		public List<Order> GetOrders(string email) {
			// I considered changing this to return a list of lists of ProductUpdates, but I think
			// it's better to return a list of orders. We can bundle order-specific things this way.

			List<Order> orders = new List<Order>();

			MySqlDataReader reader = null;
			MySqlCommand idCommand = null;		// Used to get the relevant IDs
			MySqlCommand orderCommand = null;   // Used to get the orders
			try {
				string idQuery = "SELECT order_id FROM orders WHERE user_id = @email";

				idCommand = m_dbConnection.CreateCommand();
				idCommand.Connection.Open();
				idCommand.CommandText = idQuery;
				idCommand.Parameters.Add("@email", MySqlDbType.String).Value = email;

				List<int> orderIDs = new List<int>();

				reader = idCommand.ExecuteReader();

				Console.WriteLine("Reading IDs");

				while(reader.Read()) {
					int id = reader.GetInt32(0);
					Console.WriteLine(id);
					orderIDs.Add(id);

					//*************************
					//* Debug Code            *
					//*************************
					//Console.WriteLine("\n\n\n\n****************************************************************************");
					//Console.WriteLine(orderIDs[orderIDs.Count - 1]);
					//*************************
				} // End of while

				reader.Close();
				idCommand.Connection.Close();

				Console.WriteLine(orderIDs.Count);

				foreach(int id in orderIDs) {
					string orderQuery = "SELECT product_id, quantity, price FROM orders_list where order_id = (@id)";

					orderCommand = m_dbConnection.CreateCommand();
					orderCommand.Connection.Open();
					orderCommand.CommandText = orderQuery;
					orderCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

					Order resultOrder = new Order(id, email, TimeStamp: 0); // TODO TimeStamp lawl

					reader = orderCommand.ExecuteReader();
					while(reader.Read()) {
						int productID = reader.GetInt32(0);
						int quantity = reader.GetInt32(1);
						float price = 0;
						if(!reader.IsDBNull(2)) {
							price = reader.GetFloat(2);
						}

						//resultOrder.ShoppingCart.Add(new ProductUpdate(productID, quantity));
						resultOrder.ShoppingCart.Add(new Product(productID, "", quantity, price));
					} // End while(reader.Read())

					reader.Close();
					orderCommand.Connection.Close();

					orders.Add(resultOrder);

				} // End foreach(id)

			}
			catch(Exception e) {
				Console.Out.WriteLine("\n\n**********************************************************************");
				Console.Out.WriteLine(e.Message);
				Console.Out.WriteLine(e.InnerException);
				Console.Out.WriteLine(e.Source);
				Console.Out.WriteLine(e.StackTrace);
				Console.Out.WriteLine("**********************************************************************\n\n");

				// Clean this out; we don't want to return anything if we bumped into an exception.
				orders = new List<Order>();
			}
			finally {
				idCommand?.Connection?.Close();
				orderCommand?.Connection?.Close();
				reader?.Close();
			}


			return orders;
		}



		public void AddNewOrder(string email, List<ProductUpdate> orderList)
        {
			Dictionary<int, float> prices = GetAllPrices();


			MySqlCommand command = null;
            try
            {
				//string query = "SELECT * FROM products";
				// SQLiteCommand command = m_dbConnection.CreateCommand();

				command = m_dbConnection.CreateCommand();
				command.Connection.Open();
				using(MySqlTransaction trans = command.Connection.BeginTransaction())
				{

					//command1.Connection.Open();


					// Inset into `orders` table

					string query1 = "INSERT INTO orders (user_id) VALUES ( (@email) );";
					command.CommandText = query1;
					command.Parameters.Add("@email", MySqlDbType.String).Value = email;

					int rows_affected = command.ExecuteNonQuery();
					command.Parameters.Clear();


					// Get new order_id from the above query

					string query2 = "SELECT order_id FROM orders WHERE user_id = (@email) ORDER BY order_id DESC limit 1;";
					command.CommandText = query2;
					command.Parameters.Add("@email", MySqlDbType.String).Value = email;
					MySqlDataReader mysql_datareader;

					mysql_datareader = command.ExecuteReader();
					int orderId = -1;
					while(mysql_datareader.Read()) {
						orderId = mysql_datareader.GetInt32(0);
					}
					mysql_datareader.Close();
					command.Parameters.Clear();


					// Insert the list of product updates into the `orders_list` tables
					string query3 = "INSERT INTO orders_list (order_id, product_id, quantity, price) VALUES ( (@orderID), (@productID), (@quantity), (@price) );";
					command.CommandText = query3;

					// ya i know, shut up
					bool first = true;
					foreach(ProductUpdate productUpdate in orderList) {

						if(first) {
							command.Parameters.Add("@orderID", MySqlDbType.Int32).Value = orderId;
							command.Parameters.Add("@productID", MySqlDbType.Int32).Value = productUpdate.ProductId;
							command.Parameters.Add("@quantity", MySqlDbType.Int32).Value = productUpdate.QuantityToBeRemoved;
							command.Parameters.Add("@price", MySqlDbType.Float).Value = prices.GetValueOrDefault(productUpdate.ProductId, 0);
							first = false;
						}
						else {
							command.Parameters["@orderID"].Value = orderId;
							command.Parameters["@productID"].Value = productUpdate.ProductId;
							command.Parameters["@quantity"].Value = productUpdate.QuantityToBeRemoved;
							command.Parameters["@price"].Value = prices.GetValueOrDefault(productUpdate.ProductId, 0);
						}

						int row_affected = command.ExecuteNonQuery();
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
                command?.Connection?.Close();
            }

        }


		public void SetProductInfo(Product p) {

			//MySqlDataReader mysql_datareader = null;
			MySqlCommand command = null;
			try {

				bool newProduct = !HasProduct(p.ProductId);
				Console.WriteLine("Is this product new? " + newProduct);

				command = m_dbConnection.CreateCommand();
				command.Connection.Open();
				if(newProduct) {
					command.CommandText = "INSERT INTO products(id, price) VALUES(@id, @price)";
				}
				else {
					command.CommandText = "UPDATE products SET price = @price WHERE id = @id";
				}
				command.Parameters.Add("@id", MySqlDbType.Int32).Value = p.ProductId;
				command.Parameters.Add("@price", MySqlDbType.Float).Value = p.Price;

				//mysql_datareader = command.ExecuteReader();
				int rowsEffected = command.ExecuteNonQuery();

				Console.WriteLine("Set the product info. Rows effected: " + rowsEffected);
			}
			catch(Exception e) {
				Console.Out.WriteLine("\n\n**********************************************************************");
				Console.Out.WriteLine(e.Message);
				Console.Out.WriteLine(e.InnerException);
				Console.Out.WriteLine(e.Source);
				Console.Out.WriteLine("**********************************************************************\n\n");

			}
			finally {
				//mysql_datareader?.Close();
				command?.Connection?.Close();
			}

		}

		public Dictionary<int, float> GetAllPrices() {
			Dictionary<int, float> result = new Dictionary<int, float>();

			MySqlDataReader mysql_datareader = null;
			MySqlCommand command = null;
			try {
				string query = "SELECT id, price FROM products";
				command = m_dbConnection.CreateCommand();
				command.Connection.Open();
				command.CommandText = query;

				mysql_datareader = command.ExecuteReader();

				while(mysql_datareader.Read()) {
					int id = mysql_datareader.GetInt32(0);
					float price = mysql_datareader.GetFloat(1);

					result[id] = price;
				}
			}
			catch(Exception e) {
				Console.Out.WriteLine("\n\n**********************************************************************");
				Console.Out.WriteLine(e.Message);
				Console.Out.WriteLine(e.InnerException);
				Console.Out.WriteLine(e.Source);
				Console.Out.WriteLine("**********************************************************************\n\n");

			}
			finally {
				mysql_datareader?.Close();
				command?.Connection?.Close();
			}

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="productID"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if productID is not found.</exception>
		public float GetPrice(int productID) {

			float result = -1;

			MySqlDataReader mysql_datareader = null;
			MySqlCommand command = null;
			try {
				string query = "SELECT price FROM products WHERE id = @id";
				command = m_dbConnection.CreateCommand();
				command.Connection.Open();
				command.CommandText = query;
				command.Parameters.Add("@id", MySqlDbType.Int32).Value = productID;

				mysql_datareader = command.ExecuteReader();

				if(mysql_datareader.Read()) {
					result = mysql_datareader.GetFloat(0);
				}
				else {
					throw new ArgumentOutOfRangeException("productID");
				}
			}
			catch(ArgumentOutOfRangeException e) {
				Console.Out.WriteLine("\n\n**********************************************************************");
				Console.Out.WriteLine(e.Message);
				Console.Out.WriteLine(e.InnerException);
				Console.Out.WriteLine(e.Source);
				Console.Out.WriteLine("**********************************************************************\n\n");

				throw e;
			}
			catch(Exception e) {
				Console.Out.WriteLine("\n\n**********************************************************************");
				Console.Out.WriteLine(e.Message);
				Console.Out.WriteLine(e.InnerException);
				Console.Out.WriteLine(e.Source);
				Console.Out.WriteLine("**********************************************************************\n\n");

			}
			finally {
				mysql_datareader?.Close();
				command?.Connection?.Close();
			}

			return result;
		}

		public bool HasProduct(int productID) {

			bool result = false;

			MySqlDataReader mysql_datareader = null;
			MySqlCommand command = null;
			try {
				string query = "SELECT price FROM products WHERE id = @id";
				command = m_dbConnection.CreateCommand();
				command.Connection.Open();
				command.CommandText = query;
				command.Parameters.Add("@id", MySqlDbType.Int32).Value = productID;

				mysql_datareader = command.ExecuteReader();

				if(mysql_datareader.Read()) {
					result = true;
				}
			}
			catch(Exception e) {
				Console.Out.WriteLine("\n\n**********************************************************************");
				Console.Out.WriteLine(e.Message);
				Console.Out.WriteLine(e.InnerException);
				Console.Out.WriteLine(e.Source);
				Console.Out.WriteLine("**********************************************************************\n\n");

			}
			finally {
				mysql_datareader?.Close();
				command?.Connection?.Close();
			}

			return result;
		}


	}
}
