using System.Data.SqlClient;
using sqlapp.Models; 

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "jjlearningappserver.database.windows.net";

        private static string db_user = "jjadmin";
        private static string db_password = "Password@1234";
        private static string db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID= db_user;
            _builder.Password= db_password;
            _builder.InitialCatalog= db_database;
            return new SqlConnection( _builder.ConnectionString );
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> _product_lst = new List<Product>();
            string statement = "SELECT ProductId, ProductName, Quantity from Products";

            conn.Open();
            SqlCommand cmd = new SqlCommand( statement, conn );
            using SqlDataReader _reader = cmd.ExecuteReader();
            {
                while( _reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = _reader.GetInt32(0),
                        ProductName = _reader.GetString(1),
                        Quantity = _reader.GetInt32(2)
                    };

                   _product_lst.Add( product ); 
                }
            }
            conn.Close();
            return _product_lst;
        }
    }
}
