using MySql.Data.MySqlClient;
using StudentManagement.DTO;
using System.Collections.Generic;
using StudentManagement.DAL.Interfaces;

namespace StudentManagement.DAL.Implementations
{
    public class KhoaDAL : IKhoaDAL
    {
        public List<Khoa> GetAll()
        {
            var list = new List<Khoa>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Khoa";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Khoa
                        {
                            MaKhoa = reader["MaKhoa"].ToString(),
                            TenKhoa = reader["TenKhoa"].ToString(),
                            DienThoai = reader["DienThoai"].ToString(),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public Khoa GetById(string maKhoa)
        {
            Khoa k = null;
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "SELECT * FROM Khoa WHERE MaKhoa = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", maKhoa);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        k = new Khoa
                        {
                            MaKhoa = reader["MaKhoa"].ToString(),
                            TenKhoa = reader["TenKhoa"].ToString(),
                            DienThoai = reader["DienThoai"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                }
            }
            return k;
        }

        public bool Insert(Khoa khoa)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = @"INSERT INTO Khoa(MaKhoa, TenKhoa, DienThoai, Email)
                               VALUES(@Id, @Name, @Phone, @Email)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                AddParams(cmd, khoa);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(Khoa khoa)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = @"UPDATE Khoa
                               SET TenKhoa = @Name,
                                   DienThoai = @Phone,
                                   Email = @Email
                               WHERE MaKhoa=@Id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                AddParams(cmd, khoa);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string maKhoa)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "DELETE FROM Khoa WHERE MaKhoa = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", maKhoa);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool HasChildData(string maKhoa)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                // Kiểm tra trong bảng Nganh
                string sql = "SELECT COUNT(*) FROM Nganh WHERE MaKhoa = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", maKhoa);

                conn.Open();
                long count = Convert.ToInt64(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private void AddParams(MySqlCommand cmd, Khoa khoa)
        {
            cmd.Parameters.AddWithValue("@Id", khoa.MaKhoa);
            cmd.Parameters.AddWithValue("@Name", khoa.TenKhoa);
            cmd.Parameters.AddWithValue("@Phone", khoa.DienThoai);
            cmd.Parameters.AddWithValue("@Email", khoa.Email);
        }
    }
}