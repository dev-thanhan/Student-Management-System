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
        public bool Insert(MonHoc mh)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO MonHoc(MaMon, TenMon, SoTinChi, MonTienQuyet) VALUES(@Id, @Name, @Credits, @Prereq)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    AddParams(cmd, mh);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(MonHoc mh)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE MonHoc 
                               SET TenMon = @Name, 
                                   SoTinChi = @Credits, 
                                   MonTienQuyet = @Prereq 
                               WHERE MaMon = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    AddParams(cmd, mh);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(string maMon)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Lưu ý: Nếu môn học này là môn tiên quyết của môn khác, lệnh này sẽ lỗi do khóa ngoại.
                // Cần xử lý ở BLL hoặc Try-Catch
                string sql = "DELETE FROM MonHoc WHERE MaMon = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", maMon);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Helper: Map dữ liệu từ SQL Reader sang Object
        private MonHoc MapReaderToObj(MySqlDataReader reader)
        {
            return new MonHoc
            {
                MaMon = reader["MaMon"].ToString(),
                TenMon = reader["TenMon"].ToString(),
                SoTinChi = Convert.ToInt32(reader["SoTinChi"]),
                MonTienQuyet = reader["MonTienQuyet"] != DBNull.Value ? reader["MonTienQuyet"].ToString() : ""
            };
        }

        // Helper: Thêm tham số vào Command
        private void AddParams(MySqlCommand cmd, MonHoc mh)
        {
            cmd.Parameters.AddWithValue("@Id", mh.MaMon);
            cmd.Parameters.AddWithValue("@Name", mh.TenMon);
            cmd.Parameters.AddWithValue("@Credits", mh.SoTinChi);
            // Xử lý Null cho Môn tiên quyết
            cmd.Parameters.AddWithValue("@Prereq", string.IsNullOrEmpty(mh.MonTienQuyet) ? DBNull.Value : mh.MonTienQuyet);
        }
    }
}