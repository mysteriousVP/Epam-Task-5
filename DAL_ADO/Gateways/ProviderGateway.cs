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
    public class ProviderGateway : IGateway<Provider>
    {
        private readonly string ConnectionString;
        private readonly string TableName = "Provider";

        public ProviderGateway() 
            : this(ConfigurationManager.ConnectionStrings["CatalogADO"].ToString()) 
        {

        }

        public ProviderGateway(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool Create(Provider item)
        {
            SqlParameter ProviderId = new SqlParameter("@ProviderId", item.ProviderId);
            SqlParameter Name = new SqlParameter("@Name", item.Name);
            SqlParameter Email = new SqlParameter("@Email", item.Email);
            string SqlCreateQuery = $"INSERT INTO {TableName} VALUES(@ProviderId, @Name, @Email);";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlTransaction transaction;
                SqlCommand command = new SqlCommand(SqlCreateQuery, connection);
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.Parameters.AddRange(new SqlParameter[] { ProviderId, Name, Email });
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

        public Provider Get(int id)
        {
            SqlParameter Id = new SqlParameter("@Id", id);
            SqlDataReader reader;
            Provider provider = new Provider();
            string SqlGetQuery = $"SELECT * FROM {TableName} WHERE ProviderId = @Id;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(SqlGetQuery, connection);
                command.Parameters.Add(Id);
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    provider.ProviderId = reader.GetInt32(0);
                    provider.Name = reader.GetString(1);
                    provider.Email = reader.GetString(2);
                }

                reader.Close();
            }

            return provider;
        }

        public IEnumerable<Provider> GetAll()
        {
            SqlDataReader reader;
            List<Provider> providers = new List<Provider>();
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
                        Provider provider = new Provider()
                        {
                            ProviderId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2)
                        };
                        providers.Add(provider);
                    }

                    reader.NextResult();
                }

                reader.Close();
            }

            return providers;
        }

        public bool Remove(int id)
        {
            SqlParameter ProviderId = new SqlParameter("@ProviderId", id);
            string SqlRemoveQuery = $"DELETE FROM {TableName} WHERE ProviderId = @ProviderId;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlTransaction transaction;
                SqlCommand command = new SqlCommand(SqlRemoveQuery, connection);
                command.Parameters.Add(ProviderId);
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

        public bool Remove(Provider item)
        {
            return Remove(item.ProviderId);
        }

        public bool Update(Provider item)
        {
            SqlParameter ProviderId = new SqlParameter("@ProviderId", item.ProviderId);
            SqlParameter Name = new SqlParameter("@Name", item.Name);
            SqlParameter Email = new SqlParameter("@Email", item.Email);
            string SqlUpdateQuery = $"UPDATE {TableName} SET Name = @Name, Email = @Email " +
                $"WHERE ProviderId = @ProviderId;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlTransaction transaction;
                SqlCommand command = new SqlCommand(SqlUpdateQuery, connection);
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.Parameters.AddRange(new SqlParameter[] { ProviderId, Name, Email });

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
