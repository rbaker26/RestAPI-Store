using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using System.Data.SQLite;


using MySql.Data;
using MySql.Data.MySqlClient;

namespace ProductsREST
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
            m_dbConnection = new MySqlConnection("Server=68.5.123.182; database=productsREST_db; UID=recorder; password=recorder0");
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

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            //products.Add(new Product(1, "Hi", 55, 128.20f));

            // SQLiteDataReader sqlite_datareader;
            MySqlDataReader mysql_datareader;
            try
            {
                string query = "SELECT product_id, description, quantity, price FROM products";
                //string query = "SELECT * FROM products";
               // SQLiteCommand command = m_dbConnection.CreateCommand();
                MySqlCommand command = m_dbConnection.CreateCommand();
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
                    Console.WriteLine("\n\n\n\n****************************************************************************");
                    Console.WriteLine(temp);
                    //*************************

                    products.Add(temp);
                    temp = new Product();
                }
                mysql_datareader.Close();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("\n\n**********************************************************************");
                Console.Out.WriteLine(e.Message);
                Console.Out.WriteLine(e.InnerException);
                Console.Out.WriteLine(e.Source);
                Console.Out.WriteLine("**********************************************************************\n\n");

            }
            

            return products;
        }

    }
}
