using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

using REST_lib;

namespace ProductsREST
{
    public sealed class SQL_Interface
    {
        #region sql config region
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



        //*****************************************************************************************
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            //products.Add(new Product(1, "Hi", 55, 128.20f));

            MySqlDataReader mysql_datareader = null;
            MySqlCommand command = null;
            try
            {
                string query = "SELECT product_id, description, quantity, price FROM products";
                //string query = "SELECT * FROM products";
                //SQLiteCommand command = m_dbConnection.CreateCommand();
                command = m_dbConnection.CreateCommand();
                command.Connection.Open();
                command.CommandText = query;

                mysql_datareader = command.ExecuteReader();

                Product temp = new Product();


                while (mysql_datareader.Read())
                {
                    temp.ProductId = mysql_datareader.GetInt32(0);
                    temp.Description = mysql_datareader.GetString(1);
                    temp.Quantity = mysql_datareader.GetInt32(2);
                    temp.Price = mysql_datareader.GetFloat(3);


                    //*************************
                    //* Debug Code            *
                    //*************************
                    //Console.WriteLine("\n\n\n\n****************************************************************************");
                    //Console.WriteLine(temp);
                    //*************************

                    products.Add(temp);
                    temp = new Product();
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


            return products;
        }
        //*****************************************************************************************



        //*****************************************************************************************
        public Product GetProductById(int productId)
        {
            Product temp = new Product();

            MySqlDataReader mysql_datareader = null;
            MySqlCommand command = null;
            try
            {
                string query = "SELECT product_id, description, quantity, price FROM products WHERE product_id = (@id)";

                command = m_dbConnection.CreateCommand();
                command.Connection.Open();
                command.CommandText = query;
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = productId;

                mysql_datareader = command.ExecuteReader();

                while (mysql_datareader.Read())
                {
                    temp.ProductId = mysql_datareader.GetInt32(0);
                    temp.Description = mysql_datareader.GetString(1);
                    temp.Quantity = mysql_datareader.GetInt32(2);
                    temp.Price = mysql_datareader.GetFloat(3);


                    //*************************
                    //* Debug Code            *
                    //*************************
                    //Console.WriteLine("\n\n\n\n****************************************************************************");
                    //Console.WriteLine(temp);
                    //*************************

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


            return temp;
        }
        //*****************************************************************************************




        //*****************************************************************************************
		public int AddNewItem(Product newProduct) {

			try {
				int resultID;

				MySqlCommand insertCommand = m_dbConnection.CreateCommand();
				try {
					//MakeConnectionPool(insertCommand);
					insertCommand.Connection.Open();

					insertCommand.CommandText = "INSERT INTO products (description, quantity, price, photo) VALUES (@desc, @qty, @price, 0)";
					insertCommand.Parameters.Add("@desc", MySqlDbType.String).Value = newProduct.Description;
					insertCommand.Parameters.Add("@qty", MySqlDbType.Int32).Value = newProduct.Quantity;
					insertCommand.Parameters.Add("@price", MySqlDbType.Float).Value = newProduct.Price;

					insertCommand.ExecuteNonQuery();
				}
				finally {
					insertCommand?.Connection?.Close();
				}


				MySqlCommand readCommand = m_dbConnection.CreateCommand();
				try {
					//MakeConnectionPool(readCommand);
					readCommand.Connection.Open();

					readCommand.CommandText = "SELECT product_id FROM products WHERE description = @desc ORDER BY product_id DESC LIMIT 1";
					readCommand.Parameters.Add("@desc", MySqlDbType.String).Value = newProduct.Description;

					using(MySqlDataReader reader = readCommand.ExecuteReader()) {
						if(reader.Read()) {
							resultID = reader.GetInt32(0);
						}
						else {
							throw new Exception("Unable to retrieve newly inserted item. WUT?");
						}
					}
				}
				finally {
					readCommand?.Connection?.Close();
				}

				return resultID;
			}
			catch(Exception e) {
				Console.Out.WriteLine("\n\n**********************************************************************");
				Console.Out.WriteLine(e.Message);
				Console.Out.WriteLine(e.InnerException);
				Console.Out.WriteLine(e.Source);
				Console.Out.WriteLine("**********************************************************************\n\n");

				throw new InvalidOperationException("Failed to insert item", e);
			}
		}
        //*****************************************************************************************

        //*****************************************************************************************
		public void ChangeItem(int id, Product changedProduct) {
			try {
				if(id != changedProduct.ProductId) {
					throw new InvalidOperationException("Chosen ID to update doesn't match the ID of the product passed in");
				}

				MySqlCommand updateCommand = m_dbConnection.CreateCommand();
				try {
					//MakeConnectionPool(updateCommand);
					updateCommand.Connection.Open();

					updateCommand.CommandText = "UPDATE products SET description = @desc, quantity = @qty, price = @price WHERE product_id = @id";
					updateCommand.Parameters.Add("@desc", MySqlDbType.String).Value = changedProduct.Description;
					updateCommand.Parameters.Add("@qty", MySqlDbType.Int32).Value = changedProduct.Quantity;
					updateCommand.Parameters.Add("@price", MySqlDbType.Float).Value = changedProduct.Price;
					updateCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

					int rowsEffected = updateCommand.ExecuteNonQuery();

					if(rowsEffected == 0) {
						throw new ArgumentOutOfRangeException("id");
					}
				}
				finally {
					updateCommand?.Connection?.Close();
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

				throw new InvalidOperationException("Failed to update item", e);
			}
		}
        //*****************************************************************************************

        //*****************************************************************************************
        public void ReduceItemQuantity(ProductUpdate productUpdate)
        {
            int ProductId = productUpdate.ProductId;
            int Quantity = productUpdate.QuantityToBeRemoved;

            MySqlDataReader mysql_datareader = null;
            MySqlCommand command = null;
            int orgQuantity = -1;
            try
            {
                // Get Quantity of item in database
                string query1 = "SELECT quantity FROM products WHERE product_id = (@id) LIMIT 1;";
                command = m_dbConnection.CreateCommand();
                command.Connection.Open();
                command.CommandText = query1;
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = ProductId;

                mysql_datareader = command.ExecuteReader();
                while (mysql_datareader.Read())
                {
                    orgQuantity = mysql_datareader.GetInt32(0);
                }
                

                // Make sure the is enough inventory
                if (orgQuantity < Quantity)
                {
                    // throw new ArgumentException("Purchased qantity is greater than stored quantity", "Quantity");
                }
                command.Connection.Close();


                // Reduce the quantity of the item by n
                string query2 = "UPDATE products SET quantity = (@newQuantity) WHERE product_id = (@id);";
                command = m_dbConnection.CreateCommand();
                command.Connection.Open();
                command.CommandText = query2;

                command.Parameters.Add("@id", MySqlDbType.Int32).Value = ProductId;
                command.Parameters.Add("@newQuantity", MySqlDbType.Int32).Value = orgQuantity - Quantity;



                int rows_affected = command.ExecuteNonQuery();
                //Console.Out.WriteLine("Rows Affected:\t" + rows_affected);
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
        }
        //*****************************************************************************************

    }
}
