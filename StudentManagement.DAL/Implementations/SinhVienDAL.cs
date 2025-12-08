using MySql.Data.MySqlClient;
using StudentManagement.DTO;
using StudentManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace StudentManagement.DAL.Implementations
{
    public class SinhVienDAL : ISinhVienDAL
    {
        // Khai báo câu lệnh SQL chuẩn để dùng chung cho GetAll, Search, GetById...
        // Giúp code gọn hơn và đồng bộ dữ liệu Khoa, GPA ở mọi nơi
        private const string SQL_SELECT_FULL = @"
            SELECT 
                sv.*, 
                k.TenKhoa,
                COALESCE(
                    (SELECT SUM(d.DiemTongKet * mh.SoTinChi) / SUM(mh.SoTinChi)
                     FROM Diem d
                     JOIN LopHocPhan lhp ON d.MaLopHP = lhp.MaLopHP
                     JOIN MonHoc mh ON lhp.MaMon = mh.MaMon
                     WHERE d.MaSV = sv.MaSV), 0
                ) AS GPA
            FROM SinhVien sv
            LEFT JOIN Lop l ON sv.MaLop = l.MaLop
            LEFT JOIN Nganh n ON l.MaNganh = n.MaNganh
            LEFT JOIN Khoa k ON n.MaKhoa = k.MaKhoa ";

        public List<SinhVien> GetAll()
        {
            var result = new List<SinhVien>();
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                // Sử dụng câu lệnh SQL đầy đủ đã khai báo ở trên
                MySqlCommand cmd = new MySqlCommand(SQL_SELECT_FULL, conn);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(GetStudentFromReader(reader));
                    }
                }
            }
            return result;
        }

        public SinhVien GetById(string maSV)
        {
            SinhVien sv = null;
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                // Thêm điều kiện WHERE vào câu SQL đầy đủ
                string sql = SQL_SELECT_FULL + " WHERE sv.MaSV = @MaSV";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSV);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sv = GetStudentFromReader(reader);
                    }
                }
            }
            return sv;
        }

        public List<SinhVien> Search(string keyword)
        {
            var result = new List<SinhVien>();
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                // Thêm điều kiện tìm kiếm
                string sql = SQL_SELECT_FULL + " WHERE sv.HoTen LIKE @Keyword OR sv.MaSV LIKE @Keyword";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(GetStudentFromReader(reader));
                    }
                }
            }
            return result;
        }

        public List<SinhVien> GetByClass(string maLop)
        {
            var result = new List<SinhVien>();
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = SQL_SELECT_FULL + " WHERE sv.MaLop = @MaLop";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaLop", maLop);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(GetStudentFromReader(reader));
                    }
                }
            }
            return result;
        }

        public bool Insert(SinhVien sv)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = @"INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaLop, TrangThai)
                               VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SoDienThoai, @Email, @MaLop, @TrangThai)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                AddParams(cmd, sv);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(SinhVien sv)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = @"UPDATE SinhVien 
                               SET HoTen = @HoTen, 
                                   NgaySinh = @NgaySinh, 
                                   GioiTinh = @GioiTinh, 
                                   DiaChi = @DiaChi, 
                                   SoDienThoai = @SoDienThoai, 
                                   Email = @Email, 
                                   MaLop = @MaLop,
                                   TrangThai = @TrangThai
                               WHERE MaSV = @MaSV";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                AddParams(cmd, sv);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string maSV)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSV);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool IsIdExists(string maSV)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSV);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        // --- Helper Methods ---
        // Đã sửa lại để map đúng các cột TenKhoa và GPA
        private SinhVien GetStudentFromReader(MySqlDataReader reader)
        {
            var sv = new SinhVien
            {
                MaSV = reader["MaSV"].ToString(),
                HoTen = reader["HoTen"].ToString(),
                NgaySinh = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]) : DateTime.Now,
                GioiTinh = reader["GioiTinh"] != DBNull.Value && Convert.ToBoolean(reader["GioiTinh"]),
                DiaChi = reader["DiaChi"].ToString(),
                SoDienThoai = reader["SoDienThoai"].ToString(),
                Email = reader["Email"].ToString(),
                MaLop = reader["MaLop"].ToString(),
                TrangThai = reader["TrangThai"] != DBNull.Value
                            ? (StudentStatus)Convert.ToByte(reader["TrangThai"])
                            : StudentStatus.DangHoc
            };

            // Kiểm tra và lấy TenKhoa (thay thế cho MaKhoa cũ)
            // Cần kiểm tra cột tồn tại để tránh lỗi nếu câu query không có cột này
            if (ColumnExists(reader, "TenKhoa"))
            {
                sv.TenKhoa = reader["TenKhoa"] != DBNull.Value ? reader["TenKhoa"].ToString() : "";
            }

            // Kiểm tra và lấy GPA
            if (ColumnExists(reader, "GPA"))
            {
                sv.GPA = reader["GPA"] != DBNull.Value ? Convert.ToDecimal(reader["GPA"]) : 0;
                sv.GPA = Math.Round(sv.GPA, 2); // Làm tròn 2 số lẻ
            }

            return sv;
        }

        private void AddParams(MySqlCommand cmd, SinhVien sv)
        {
            cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
            cmd.Parameters.AddWithValue("@HoTen", sv.HoTen);
            cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
            cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh);
            cmd.Parameters.AddWithValue("@DiaChi", sv.DiaChi);
            cmd.Parameters.AddWithValue("@SoDienThoai", sv.SoDienThoai);
            cmd.Parameters.AddWithValue("@Email", sv.Email);
            cmd.Parameters.AddWithValue("@MaLop", sv.MaLop);
            cmd.Parameters.AddWithValue("@TrangThai", sv.TrangThai);
        }

        // Hàm phụ để kiểm tra cột có tồn tại trong DataReader không
        private bool ColumnExists(MySqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}