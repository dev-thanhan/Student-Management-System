using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Implementations
{
    public class LopDAL
    {
        public List<Lop> GetAll()
        {
            var list = new List<Lop>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Join để lấy tên ngành nếu cần hiển thị, ở đây lấy raw data bảng Lop
                string sql = "SELECT * FROM Lop";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Lop
                        {
                            MaLop = reader["MaLop"].ToString(),
                            TenLop = reader["TenLop"].ToString(),
                            KhoaHoc = reader["KhoaHoc"].ToString(),
                            MaNganh = reader["MaNganh"].ToString(),
                            CoVanHocTap = reader["CoVanHocTap"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public bool Insert(Lop lop)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Lop(MaLop, TenLop, KhoaHoc, MaNganh, CoVanHocTap) VALUES(@Id, @Name, @Course, @MajorId, @Advisor)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", lop.MaLop);
                    cmd.Parameters.AddWithValue("@Name", lop.TenLop);
                    cmd.Parameters.AddWithValue("@Course", lop.KhoaHoc);
                    cmd.Parameters.AddWithValue("@MajorId", lop.MaNganh);
                    cmd.Parameters.AddWithValue("@Advisor", lop.CoVanHocTap);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Bạn tự bổ sung hàm Update và Delete tương tự KhoaDAL nhé
    }
}