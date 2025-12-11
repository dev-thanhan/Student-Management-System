using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;
using System.Globalization;
using System.Linq; // Thêm thư viện này để sử dụng LINQ (chuyển đổi Dictionary)

namespace StudentManagement.DAL.Implementations
{
    public class SinhVienDAL
    {
        // ... (Giữ nguyên các hàm GetAll, MapReaderToObj, ContainsColumn)
        // ... (Giữ nguyên các hàm GetById, Search, Insert, Update, Delete)

        // ================================================================
        // HÀM: Lấy tất cả Sinh viên (GetAll) - Giữ nguyên
        // ================================================================
        public List<SinhVien> GetAll()
        {
            var list = new List<SinhVien>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Sửa SQL để JOIN với Khoa (nếu cần hiển thị TenKhoa) và tính GPA
                string sql = @"
                    SELECT 
                        SV.*, K.TenKhoa, IFNULL(T.GPA, 0) AS GPA
                    FROM SinhVien SV
                    JOIN Lop L ON SV.MaLop = L.MaLop
                    JOIN Nganh N ON L.MaNganh = N.MaNganh
                    JOIN Khoa K ON N.MaKhoa = K.MaKhoa
                    LEFT JOIN (
                        -- Tính GPA trung bình (Giả sử bạn đã có logic tính GPA)
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

        // ================================================================
        // HÀM THỐNG KÊ (YÊU CẦU 6)
        // ================================================================

        // THỐNG KÊ 1: Theo Giới tính (string: Nam/Nữ, int: Số lượng)
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

        // THỐNG KÊ 2: Theo Năm sinh (int: Năm, int: Số lượng)
        public Dictionary<int, int> ThongKeTheoNamSinh()
        {
            var data = new Dictionary<int, int>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Lấy số lượng theo Năm sinh, sắp xếp từ mới nhất (DESC)
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

        // THỐNG KÊ 3: Theo GPA/Mức Điểm (string: Xếp loại, int: Số lượng)
        public Dictionary<string, int> ThongKeTheoGPA()
        {
            var data = new Dictionary<string, int>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Truy vấn phức tạp hơn: Phải tính điểm TB trước, sau đó phân loại
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
                        COUNT(T.MaSV) AS SoLuong
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

        // ================================================================
        // HÀM HỖ TRỢ MAP DỮ LIỆU
        // ================================================================
        
        // ... (Giữ nguyên MapReaderToObj và ContainsColumn)
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
        
        public SinhVien GetById(string maSV)
 	    {
 		    // Cần logic SQL để lấy sinh viên theo ID
 		    return null; // Trả về null hoặc đối tượng mặc định
 	    }

 	    public List<SinhVien> Search(string keyword)
 	    {
 		    // Cần logic SQL để tìm kiếm sinh viên
 		    return new List<SinhVien>();
 	    }

 	    public bool Insert(SinhVien sv)
 	    {
 		    // Cần logic SQL để thêm sinh viên
 		    return false;
 	    }

 	    public bool Update(SinhVien sv)
 	    {
 		    // Cần logic SQL để cập nhật sinh viên
 		    return false;
 	    }

 	    public bool Delete(string maSV)
 	    {
 		    // Cần logic SQL để xóa sinh viên
 		    return false;
 	    }
    }
}