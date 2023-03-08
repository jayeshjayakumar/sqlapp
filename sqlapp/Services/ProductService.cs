using System.Data.SqlClient;
using sqlapp.Models; 

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {

        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            //referring to connection string storedi in azure app service
            //return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));

            //this is how we access the configuration from Azure App config
            return new SqlConnection(_configuration["SQLConnection"]);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> _product_lst = new List<Product>();
            string statement = "SELECT ProductId, ProductName, Quantity from Products";

            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using SqlDataReader _reader = cmd.ExecuteReader();
            {
                while (_reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = _reader.GetInt32(0),
                        ProductName = _reader.GetString(1),
                        Quantity = _reader.GetInt32(2)
                    };

                    _product_lst.Add(product);
                }
            }
            conn.Close();
            return _product_lst;
        }
    }
}
