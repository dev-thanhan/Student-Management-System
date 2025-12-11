using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;
using StudentManagement.DAL.Interfaces; // Đảm bảo có namespace này nếu dùng interface

namespace StudentManagement.DAL.Implementations
{
    public class SinhVienDAL
    {
        // Câu lệnh SQL cơ bản để lấy đầy đủ thông tin (kèm Tên Khoa và GPA)
        // Dùng chung cho GetAll và Search để hiển thị lên lưới không bị mất dữ liệu
        private const string SELECT_FULL_INFO = @"
            SELECT 
                SV.*, K.TenKhoa, IFNULL(T.GPA, 0) AS GPA
            FROM SinhVien SV
            JOIN Lop L ON SV.MaLop = L.MaLop
            JOIN Nganh N ON L.MaNganh = N.MaNganh
            JOIN Khoa K ON N.MaKhoa = K.MaKhoa
            LEFT JOIN (
                SELECT MaSV, AVG(DiemTongKet) AS GPA
                FROM Diem
                GROUP BY MaSV
            ) T ON SV.MaSV = T.MaSV ";

        public List<SinhVien> GetAll()
        {
            var list = new List<SinhVien>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(SELECT_FULL_INFO, conn))
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

        // --- 1. SỬA LỖI HÀM GET BY ID (Lấy sinh viên theo Mã) ---
        public SinhVien GetById(string id)
        {
            SinhVien sv = null;
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Dùng câu lệnh SELECT đơn giản để lấy thông tin gốc nhanh chóng
                string sql = "SELECT * FROM SinhVien WHERE MaSV = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Lưu ý: MapReaderToObj có thể thiếu TenKhoa/GPA nếu dùng SELECT *, 
                            // nhưng không ảnh hưởng logic Sửa/Xóa
                            sv = MapReaderToObj(reader);
                        }
                    }
                }
            }
            return sv;
        }

        // --- 2. SỬA LỖI HÀM INSERT (Thêm sinh viên) ---
        public bool Insert(SinhVien sv)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO SinhVien(MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaLop, TrangThai) 
                               VALUES(@MaSV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @Email, @MaLop, @TrangThai)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    AddParams(cmd, sv); // Gọi hàm thêm tham số
                    try
                    {
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch
                    {
                        return false; // Trả về false nếu trùng khóa chính hoặc lỗi SQL
                    }
                }
            }
        }

        // --- 3. SỬA LỖI HÀM UPDATE (Cập nhật sinh viên) ---
        public bool Update(SinhVien sv)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE SinhVien 
                               SET HoTen = @HoTen, 
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
                    AddParams(cmd, sv);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // --- 4. SỬA LỖI HÀM DELETE (Xóa sinh viên) ---
        public bool Delete(string id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Lưu ý: Nếu sinh viên đã có điểm hoặc đăng ký học phần,
                // database sẽ chặn xóa nếu không có ON DELETE CASCADE.
                // Ở đây ta thực hiện xóa trực tiếp, nếu lỗi FK thì UI sẽ bắt exception.
                string sql = "DELETE FROM SinhVien WHERE MaSV = @Id";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // --- 5. SỬA LỖI HÀM SEARCH (Tìm kiếm) ---
        public List<SinhVien> Search(string keyword)
        {
            var list = new List<SinhVien>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Tìm theo Mã SV hoặc Họ Tên, kết hợp với QUERY đầy đủ để hiện GPA/Khoa
                string sql = SELECT_FULL_INFO + " WHERE SV.MaSV LIKE @Kw OR SV.HoTen LIKE @Kw";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Kw", "%" + keyword + "%");
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

        // --- CÁC HÀM THỐNG KÊ (GIỮ NGUYÊN) ---
        public Dictionary<string, int> ThongKeTheoGioiTinh()
        {
            var data = new Dictionary<string, int>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT CASE WHEN GioiTinh = 1 THEN 'Nam' ELSE 'Nữ' END AS GioiTinhLabel, COUNT(MaSV) AS SoLuong FROM SinhVien GROUP BY GioiTinhLabel";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader["GioiTinhLabel"].ToString(), Convert.ToInt32(reader["SoLuong"]));
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
                string sql = "SELECT YEAR(NgaySinh) AS NamSinh, COUNT(MaSV) AS SoLuong FROM SinhVien GROUP BY NamSinh ORDER BY NamSinh DESC";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(Convert.ToInt32(reader["NamSinh"]), Convert.ToInt32(reader["SoLuong"]));
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
                    WITH StudentGPA AS (SELECT MaSV, AVG(DiemTongKet) AS AvgGPA FROM Diem GROUP BY MaSV)
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
                    GROUP BY XepLoai";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader["XepLoai"].ToString(), Convert.ToInt32(reader["SoLuong"]));
                    }
                }
            }
            return data;
        }

        // --- CÁC HÀM HỖ TRỢ (PRIVATE) ---

        // Hàm thêm tham số để tránh lặp code và SQL Injection
        private void AddParams(MySqlCommand cmd, SinhVien sv)
        {
            cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
            cmd.Parameters.AddWithValue("@HoTen", sv.HoTen);
            cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
            cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh ? 1 : 0); // Bit: 1=Nam, 0=Nữ
            cmd.Parameters.AddWithValue("@DiaChi", sv.DiaChi);
            cmd.Parameters.AddWithValue("@SDT", sv.SoDienThoai);
            cmd.Parameters.AddWithValue("@Email", sv.Email);
            cmd.Parameters.AddWithValue("@MaLop", sv.MaLop);
            cmd.Parameters.AddWithValue("@TrangThai", (int)sv.TrangThai);
        }

        private SinhVien MapReaderToObj(MySqlDataReader reader)
        {
            return new SinhVien
            {
                MaSV = reader["MaSV"].ToString(),
                HoTen = reader["HoTen"].ToString(),
                NgaySinh = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]) : DateTime.MinValue,
                GioiTinh = reader["GioiTinh"] != DBNull.Value && Convert.ToBoolean(reader["GioiTinh"]),
                DiaChi = reader["DiaChi"].ToString(),
                SoDienThoai = reader["SoDienThoai"].ToString(),
                Email = reader["Email"].ToString(),
                MaLop = reader["MaLop"].ToString(),
                // Kiểm tra cột tồn tại trước khi đọc để tránh lỗi khi dùng SELECT * đơn giản
                TenKhoa = ContainsColumn(reader, "TenKhoa") ? reader["TenKhoa"].ToString() : "",
                GPA = ContainsColumn(reader, "GPA") && reader["GPA"] != DBNull.Value ? Convert.ToDecimal(reader["GPA"]) : 0,
                TrangThai = ContainsColumn(reader, "TrangThai") ? (StudentStatus)Convert.ToByte(reader["TrangThai"]) : StudentStatus.DangHoc
            };
        }

        private bool ContainsColumn(MySqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }
    }
}