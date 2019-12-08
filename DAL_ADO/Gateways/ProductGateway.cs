using DAL_ADO.Entities;
using DAL_ADO.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ADO.Gateways
{
    public class ProductGateway : IGateway<Product>
    {
        private readonly string ConnectionString;
        private readonly string TableName = "Product";

        public ProductGateway()
            : this(ConfigurationManager.ConnectionStrings["CatalogADO"].ToString())
        {

        }

        public ProductGateway(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool Create(Product item)
        {
            SqlParameter ProductId = new SqlParameter("@ProductId", item.ProductId);
            SqlParameter Name = new SqlParameter("@Name", item.Name);
            SqlParameter Price = new SqlParameter("@Price", item.Price);
            SqlParameter DateOfCreating = new SqlParameter("@DateOfCreating", item.DateOfCreating);
            SqlParameter CategoryId = new SqlParameter("@CategoryId", item.CategoryId);
            SqlParameter ProviderId = new SqlParameter("@ProviderId", item.ProviderId);
            string SqlCreateQuery = $"INSERT INTO {TableName} " +
                $"VALUES(@ProductId, @Name, @Price, @DateOfCreating, @CategoryId, @ProviderId);";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlTransaction transaction;
                SqlCommand command = new SqlCommand(SqlCreateQuery, connection);
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.Parameters.AddRange(new SqlParameter[] { ProductId, Name, Price,
                    DateOfCreating, CategoryId, ProviderId});
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return false;
        }

        public Product Get(int id)
        {
            SqlParameter Id = new SqlParameter("@Id", id);
            SqlDataReader reader;
            Product product = new Product();
            string SqlGetQuery = $"SELECT * FROM {TableName} WHERE ProductId = @Id;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(SqlGetQuery, connection);
                command.Parameters.Add(Id);
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    product.ProductId = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Price = Convert.ToDouble(reader.GetDecimal(2));
                    product.DateOfCreating = reader.GetDateTime(3);
                    product.CategoryId = reader.GetInt32(4);
                    product.ProviderId = reader.GetInt32(5);
                }

                reader.Close();
            }

            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            SqlDataReader reader;
            List<Product> products = new List<Product>();
            string SqlGetAllQuery = $"SELECT * FROM {TableName};";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(SqlGetAllQuery, connection);
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Product product = new Product()
                        {
                            ProductId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = Convert.ToDouble(reader.GetDecimal(2)),
                            DateOfCreating = reader.GetDateTime(3),
                            CategoryId = reader.GetInt32(4),
                            ProviderId = reader.GetInt32(5)
                        };
                        products.Add(product);
                    }

                    reader.NextResult();
                }

                reader.Close();
            }

            return products;
        }

        public bool Remove(int id)
        {
            SqlParameter ProductId = new SqlParameter("@ProductId", id);
            string SqlRemoveQuery = $"DELETE FROM {TableName} WHERE ProductId = @ProductId;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlTransaction transaction;
                SqlCommand command = new SqlCommand(SqlRemoveQuery, connection);
                command.Parameters.Add(ProductId);
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return false;
        }

        public bool Remove(Product item)
        {
            return Remove(item.ProductId);
        }

        public bool Update(Product item)
        {
            SqlParameter ProductId = new SqlParameter("@ProductId", item.ProductId);
            SqlParameter Name = new SqlParameter("@Name", item.Name);
            SqlParameter Price = new SqlParameter("@Price", item.Price);
            SqlParameter DateOfCreating = new SqlParameter("@DateOfCreating", item.DateOfCreating);
            SqlParameter CategoryId = new SqlParameter("@CategoryId", item.CategoryId);
            SqlParameter ProviderId = new SqlParameter("@ProviderId", item.ProviderId);

            string SqlUpdateQuery = $"UPDATE {TableName} SET Name = @Name, Price = @Price, " +
                $"DateOfCreating = @DateOfCreating, CategoryId = @CategoryId, ProviderId = @ProviderId," +
                $" WHERE ProductId = @ProductId;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlTransaction transaction;
                SqlCommand command = new SqlCommand(SqlUpdateQuery, connection);
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.Parameters.AddRange(new SqlParameter[] { ProductId, Name, Price, DateOfCreating,
                    CategoryId, ProviderId });

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return false;
        }
    }
}
