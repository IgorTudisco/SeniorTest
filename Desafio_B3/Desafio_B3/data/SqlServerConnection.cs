using Microsoft.Data.SqlClient;

namespace Desafio_B3.data;

public class SqlServerConnection
{

    private readonly string _connectionString;

    public SqlServerConnection(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void ExecuteQuery()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TableName", connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["ColumnName"]);
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine($"Erro de conexão: {e.Message}");
        }
    }

}
