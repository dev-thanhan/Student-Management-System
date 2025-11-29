using Microsoft.Data.SqlClient;
using System.Configuration;

namespace StudentManagement.DAL
{
    public static class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            string connStr = ConfigurationManager
                                .ConnectionStrings["StudentDb"].ConnectionString;
            return new SqlConnection(connStr);
        }
    }
}
