using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Implementations
{
    public class MonHocDAL
    {
        public List<MonHoc> GetAll()
        {
            var list = new List<MonHoc>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM MonHoc";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new MonHoc
                        {
                            MaMon = reader["MaMon"].ToString(),
                            TenMon = reader["TenMon"].ToString(),
                            SoTinChi = Convert.ToInt32(reader["SoTinChi"]),
                            MonTienQuyet = reader["MonTienQuyet"] != DBNull.Value ? reader["MonTienQuyet"].ToString() : ""
                        });
                    }
                }
            }
            return list;
        }
        // Insert/Update/Delete tương tự...
    }
}