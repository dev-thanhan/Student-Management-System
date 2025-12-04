using MySql.Data.MySqlClient; // Cần cài NuGet: MySql.Data
using System.Configuration;

namespace StudentManagement.DAL
{
    public static class DbHelper
    {
        public static MySqlConnection GetConnection()
        {
            // Đảm bảo ConnectionString trong App.config đã đổi sang format của MySQL
            // Ví dụ: Server=localhost;Database=StudentManagement;Uid=root;Pwd=...;
            string connStr = ConfigurationManager.ConnectionStrings["StudentDb"].ConnectionString;
            return new MySqlConnection(connStr);
        }
    }
}