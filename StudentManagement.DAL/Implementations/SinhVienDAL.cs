using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;
using System.Globalization;
using System.Linq;

namespace StudentManagement.DAL.Implementations
{
    public class SinhVienDAL
    {
        public List<SinhVien> GetAll()
        {
            var list = new List<SinhVien>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        SV.*, K.TenKhoa, IFNULL(T.GPA, 0) AS GPA
                    FROM SinhVien SV
                    LEFT JOIN Lop L ON SV.MaLop = L.MaLop
                    LEFT JOIN Nganh N ON L.MaNganh = N.MaNganh
                    LEFT JOIN Khoa K ON N.MaKhoa = K.MaKhoa
                    LEFT JOIN (
                        SELECT MaSV, AVG(DiemTongKet) AS GPA
                        FROM Diem
                        GROUP BY MaSV
                    ) T ON SV.MaSV = T.MaSV";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapReaderToObj(reader));
                    }
                }
            }
            return list;
        }

        public SinhVien GetById(string maSV)
        {
            SinhVien sv = null;
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM SinhVien WHERE MaSV = @MaSV";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sv = MapReaderToObj(reader);
                        }
                    }
                }
            }
            return sv; 
        }

        public List<SinhVien> Search(string keyword)
        {
            var list = new List<SinhVien>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM SinhVien WHERE MaSV LIKE @Keyword OR HoTen LIKE @Keyword";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToObj(reader));
                        }
                    }
                }
            }
            return list;
        }

        public bool Insert(SinhVien sv)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaLop, TrangThai)
                    VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @Email, @MaLop, @TrangThai)";
                
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
                    cmd.Parameters.AddWithValue("@HoTen", sv.HoTen);
                    cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh.ToString("yyyy-MM-dd")); 
                    cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh ? 1 : 0); 
                    cmd.Parameters.AddWithValue("@DiaChi", sv.DiaChi);
                    cmd.Parameters.AddWithValue("@SDT", sv.SoDienThoai); 
                    cmd.Parameters.AddWithValue("@Email", sv.Email);
                    cmd.Parameters.AddWithValue("@MaLop", sv.MaLop);
                    cmd.Parameters.AddWithValue("@TrangThai", sv.TrangThai);
                    
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(SinhVien sv)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    UPDATE SinhVien SET 
                        HoTen = @HoTen, 
                        NgaySinh = @NgaySinh, 
                        GioiTinh = @GioiTinh,
                        DiaChi = @DiaChi, 
                        SoDienThoai = @SDT,
                        Email = @Email, 
                        MaLop = @MaLop,
                        TrangThai = @TrangThai
                    WHERE MaSV = @MaSV";
                
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HoTen", sv.HoTen);
                    cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh ? 1 : 0);
                    cmd.Parameters.AddWithValue("@DiaChi", sv.DiaChi);
                    cmd.Parameters.AddWithValue("@SDT", sv.SoDienThoai);
                    cmd.Parameters.AddWithValue("@Email", sv.Email);
                    cmd.Parameters.AddWithValue("@MaLop", sv.MaLop);
                    cmd.Parameters.AddWithValue("@TrangThai", sv.TrangThai);
                    cmd.Parameters.AddWithValue("@MaSV", sv.MaSV); 
                    
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(string maSV)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public Dictionary<string, int> ThongKeTheoGioiTinh()
        {
            var data = new Dictionary<string, int>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        CASE 
                            WHEN GioiTinh = 1 THEN 'Nam' 
                            ELSE 'Nữ' 
                        END AS GioiTinhLabel,
                        COUNT(MaSV) AS SoLuong
                    FROM SinhVien
                    GROUP BY GioiTinhLabel;";
                
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(
                            reader["GioiTinhLabel"].ToString(), 
                            Convert.ToInt32(reader["SoLuong"])
                        );
                    }
                }
            }
            return data;
        }

        public Dictionary<int, int> ThongKeTheoNamSinh()
        {
            var data = new Dictionary<int, int>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        YEAR(NgaySinh) AS NamSinh,
                        COUNT(MaSV) AS SoLuong
                    FROM SinhVien
                    GROUP BY NamSinh
                    ORDER BY NamSinh DESC;";
                
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(
                            Convert.ToInt32(reader["NamSinh"]), 
                            Convert.ToInt32(reader["SoLuong"])
                        );
                    }
                }
            }
            return data;
        }

        public Dictionary<string, int> ThongKeTheoGPA()
        {
            var data = new Dictionary<string, int>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    WITH StudentGPA AS (
                        SELECT 
                            MaSV, 
                            AVG(DiemTongKet) AS AvgGPA 
                        FROM Diem
                        GROUP BY MaSV
                    )
                    SELECT 
                        CASE 
                            WHEN T.AvgGPA IS NULL OR T.AvgGPA < 5.0 THEN 'Dưới 5.0 (Rớt)'
                            WHEN T.AvgGPA < 7.0 THEN '5.0 - 6.9 (TB/Khá)'
                            WHEN T.AvgGPA < 8.0 THEN '7.0 - 7.9 (Khá/Giỏi)'
                            ELSE '8.0+ (Giỏi/Xuất sắc)'
                        END AS XepLoai,
                        COUNT(SV.MaSV) AS SoLuong
                    FROM SinhVien SV
                    LEFT JOIN StudentGPA T ON SV.MaSV = T.MaSV
                    GROUP BY XepLoai;";
                
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(
                            reader["XepLoai"].ToString(), 
                            Convert.ToInt32(reader["SoLuong"])
                        );
                    }
                }
            }
            return data;
        }

        
        private SinhVien MapReaderToObj(MySqlDataReader reader)
        {
            return new SinhVien
            {
                MaSV = reader["MaSV"].ToString(),
                HoTen = reader["HoTen"].ToString(),
                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                GioiTinh = Convert.ToBoolean(reader["GioiTinh"]),
                DiaChi = reader["DiaChi"].ToString(),
                SoDienThoai = reader["SoDienThoai"].ToString(),
                Email = reader["Email"].ToString(),
                MaLop = reader["MaLop"].ToString(),
                
                TenKhoa = ContainsColumn(reader, "TenKhoa") ? reader["TenKhoa"].ToString() : string.Empty,
                GPA = ContainsColumn(reader, "GPA") && reader["GPA"] != DBNull.Value ? Convert.ToDecimal(reader["GPA"]) : 0,
                
                TrangThai = ContainsColumn(reader, "TrangThai") && reader["TrangThai"] != DBNull.Value ? (StudentStatus)Convert.ToByte(reader["TrangThai"]) : StudentStatus.DangHoc
            };
        }

        public static bool ContainsColumn(MySqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}