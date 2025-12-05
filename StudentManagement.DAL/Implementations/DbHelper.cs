using MySql.Data.MySqlClient;
using System.Configuration;

namespace StudentManagement.DAL
{
    public static class DbHelper
    {
        public static MySqlConnection GetConnection()
        {
            // Ví dụ: Server=localhost;Database=StudentManagement;Uid=root;Pwd=...;
            string connStr = ConfigurationManager.ConnectionStrings["StudentDb"].ConnectionString;
            return new MySqlConnection(connStr);
        }
    }
}