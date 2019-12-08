using DAL_ADO.Entities;
using DAL_ADO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL_ADO.Gateways
{
    public class CategoryGateway : IGateway<Category>
    {
        private readonly string ConnectionString;
        private const string TableName = "Category";

        public CategoryGateway()
            : this(ConfigurationManager.ConnectionStrings["CatalogADO"].ToString())
        {
        }

        public CategoryGateway(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public bool Create(Category item)
        {
            SqlParameter CategoryId = new SqlParameter("@CategoryId", item.CategoryId);
            SqlParameter Name = new SqlParameter("@Name", item.Name);
            string SqlCreateQuery = $"INSERT INTO {TableName} VALUES(@CategoryId, @Name);";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlTransaction transaction;
                SqlCommand command = new SqlCommand(SqlCreateQuery, connection);
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.Parameters.AddRange(new SqlParameter[] { CategoryId, Name });
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    transaction.Commit();

                    return true;
                }
                catch(Exception)
                {
                    transaction.Rollback();
                }
            }

            return false;
        }

        public Category Get(int id)
        {
            SqlParameter Id = new SqlParameter("@Id", id);
            SqlDataReader reader;
            Category category = new Category();
            string SqlGetQuery = $"SELECT * FROM {TableName} WHERE CategoryId = @Id;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(SqlGetQuery, connection);
                command.Parameters.Add(Id);
                connection.Open();
                reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    category.CategoryId = reader.GetInt32(0);
                    category.Name = reader.GetString(1);
                }

                reader.Close();
            }

            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            SqlDataReader reader;
            List<Category> categories = new List<Category>();
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
                        Category category = new Category()
                        {
                            CategoryId = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        categories.Add(category);
                    }

                    reader.NextResult();
                }

                reader.Close();
            }

            return categories;
        }

        public bool Remove(int id)
        {
            SqlParameter CategoryId = new SqlParameter("@CategoryId", id);
            string SqlRemoveQuery = $"DELETE FROM {TableName} WHERE CategoryId = @CategoryId;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlTransaction transaction;
                SqlCommand command = new SqlCommand(SqlRemoveQuery, connection);
                command.Parameters.Add(CategoryId);
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    transaction.Commit();

                    return true;
                }
                catch(Exception)
                {
                    transaction.Rollback();
                }
            }

            return false;
        }

        public bool Remove(Category item)
        {
            return Remove(item.CategoryId);
        }

        public bool Update(Category item)
        {
            SqlParameter CategoryId = new SqlParameter("@CategoryId", item.CategoryId);
            SqlParameter Name = new SqlParameter("@Name", item.Name);
            string SqlUpdateQuery = $"UPDATE {TableName} SET Name = @Name WHERE CategoryId = @CategoryId;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlTransaction transaction;
                SqlCommand command = new SqlCommand(SqlUpdateQuery, connection);
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.Parameters.AddRange(new SqlParameter[] { CategoryId, Name });

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    transaction.Commit();

                    return true;
                }
                catch(Exception)
                {
                    transaction.Rollback();
                }
            }

            return false;
        }
    }
}
