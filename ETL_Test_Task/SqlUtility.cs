using Microsoft.Data.SqlClient;

namespace ETL_Test_Task;

public class SqlUtility
{
    public static void ExecuteSqlScripts(string connectionString, string[] scriptPaths)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            foreach (var scriptPath in scriptPaths)
            {
                string sqlScript = File.ReadAllText(scriptPath);
                SqlCommand command = new SqlCommand(sqlScript, connection);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}
