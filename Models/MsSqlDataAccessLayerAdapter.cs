using System.Data.SqlClient;

namespace Filter.Models
{
    public class MsSqlDataAccessLayerAdapter : IDataAccessLayer
    {
        private readonly string connStr;

        public MsSqlDataAccessLayerAdapter(string connectionString)
        {
            connStr = connectionString;
        }

        public bool CheckAddress(string address)
        {
            string query = $"select RangeAlias from ips where RangeStart <= dbo.strtobinary('{address}') and RangeEnd >= dbo.strtobinary('{address}')";

            using (var connection = new SqlConnection(connStr))
            {
                connection.Open();

                using (var command = new SqlCommand(query))
                {
                    command.Connection = connection;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        string alias = string.Empty;

                        if (reader.Read())
                        {
                            alias = reader.GetValue(0).ToString();
                        }

                        return !string.IsNullOrEmpty(alias);
                    }
                }
            }
        }
    }
}
