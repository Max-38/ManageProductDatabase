using ManageProductDatabase.NsApplication.Interfaces;
using System.Data.SqlClient;

namespace ManageProductDatabase.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<List<Product>> GetAll()
        {
            List<Product> products = new List<Product>();
            string sqlExpression = "SELECT * FROM Products";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new Product(reader.GetInt32(0),
                                                 reader.GetString(1),
                                                 reader.GetDecimal(2),
                                                 reader.GetDecimal(3)));
                    }
                }
            }
            return products;
        }

        public async Task Add(string name, decimal price, decimal itemsCount)
        {
            string sqlExpression = $"INSERT INTO Products (name, price, itemsCount) VALUES (@name, @price, @itemsCount)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("price", price);
                command.Parameters.AddWithValue("itemsCount", itemsCount);

                await command.ExecuteNonQueryAsync();
            }

        }

        public async Task Update(Product product)
        {
            string sqlExpression = $"UPDATE Products SET name = @name, price = @price, itemsCount = @itemsCount WHERE id = {product.Id}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("name", product.Name);
                command.Parameters.AddWithValue("price", product.Price);
                command.Parameters.AddWithValue("itemsCount", product.ItemsCount);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Delete(int id)
        {
            string sqlExpression = $"DELETE FROM Products WHERE id = {id}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
