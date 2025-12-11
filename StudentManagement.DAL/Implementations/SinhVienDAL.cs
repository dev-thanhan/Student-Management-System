using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;
using System.Globalization;

namespace StudentManagement.DAL.Implementations
{
    public class SinhVienDAL
    {
        // ================================================================
        // HÀM: Lấy tất cả Sinh viên (Kèm Tên Khoa và GPA)
        // ================================================================
        public List<SinhVien> GetAll()
        {
            var list = new List<SinhVien>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // SQL: Lấy thông tin SV + Tên Khoa + Tính điểm trung bình (GPA)
                string sql = @"
                    SELECT sv.*, k.TenKhoa,
                           (SELECT AVG(DiemTongKet) 
                            FROM Diem d 
                            JOIN LopHocPhan lhp ON d.MaLopHP = lhp.MaLopHP 
                            WHERE d.MaSV = sv.MaSV) as CalculatedGPA
                    FROM SinhVien sv
                    LEFT JOIN Lop l ON sv.MaLop = l.MaLop
                    LEFT JOIN Nganh n ON l.MaNganh = n.MaNganh
                    LEFT JOIN Khoa k ON n.MaKhoa = k.MaKhoa
                    ORDER BY sv.MaSV DESC"; // Sắp xếp sinh viên mới nhất lên đầu

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
        // HÀM: Lấy 1 Sinh viên theo Mã (Dùng cho chức năng Sửa)
        // ================================================================
        public SinhVien GetById(string maSV)
        {
            SinhVien sv = null;
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT sv.*, k.TenKhoa,
                           (SELECT AVG(DiemTongKet) 
                            FROM Diem d 
                            WHERE d.MaSV = sv.MaSV) as CalculatedGPA
                    FROM SinhVien sv
                    LEFT JOIN Lop l ON sv.MaLop = l.MaLop
                    LEFT JOIN Nganh n ON l.MaNganh = n.MaNganh
                    LEFT JOIN Khoa k ON n.MaKhoa = k.MaKhoa
                    WHERE sv.MaSV = @Id";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", maSV);
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

        // ================================================================
        // HÀM: Tìm kiếm Sinh viên (Theo Mã hoặc Tên)
        // ================================================================
        public List<SinhVien> Search(string keyword)
        {
            var list = new List<SinhVien>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT sv.*, k.TenKhoa,
                           (SELECT AVG(DiemTongKet) FROM Diem d WHERE d.MaSV = sv.MaSV) as CalculatedGPA
                    FROM SinhVien sv
                    LEFT JOIN Lop l ON sv.MaLop = l.MaLop
                    LEFT JOIN Nganh n ON l.MaNganh = n.MaNganh
                    LEFT JOIN Khoa k ON n.MaKhoa = k.MaKhoa
                    WHERE sv.MaSV LIKE @Kw OR sv.HoTen LIKE @Kw";

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

        // ================================================================
        // HÀM: Thêm mới Sinh viên (Insert)
        // ================================================================
        public bool Insert(SinhVien sv)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO SinhVien(MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaLop, TrangThai) 
                               VALUES(@MaSV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @Email, @MaLop, @TrangThai)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    AddParams(cmd, sv);
                    // ExecuteNonQuery trả về số dòng bị ảnh hưởng, > 0 nghĩa là thành công
                    try { return cmd.ExecuteNonQuery() > 0; }
                    catch { return false; } // Trả về false nếu trùng mã SV hoặc lỗi khác
                }
            }
        }

        // ================================================================
        // HÀM: Cập nhật Sinh viên (Update)
        // ================================================================
        public bool Update(SinhVien sv)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE SinhVien 
                               SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, 
                                   DiaChi=@DiaChi, SoDienThoai=@SDT, Email=@Email, 
                                   MaLop=@MaLop, TrangThai=@TrangThai 
                               WHERE MaSV=@MaSV";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    AddParams(cmd, sv);
                    try { return cmd.ExecuteNonQuery() > 0; }
                    catch { return false; }
                }
            }
        }

        // ================================================================
        // HÀM: Xóa Sinh viên (Delete)
        // ================================================================
        public bool Delete(string maSV)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    try { return cmd.ExecuteNonQuery() > 0; }
                    catch { return false; } // Lỗi nếu sinh viên đang có điểm (ràng buộc khóa ngoại)
                }
            }
        }

        // ================================================================
        // CÁC HÀM HỖ TRỢ (PRIVATE)
        // ================================================================

        private void AddParams(MySqlCommand cmd, SinhVien sv)
        {
            cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
            cmd.Parameters.AddWithValue("@HoTen", sv.HoTen);
            cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
            cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh); // 1: Nam, 0: Nữ
            cmd.Parameters.AddWithValue("@DiaChi", sv.DiaChi);
            cmd.Parameters.AddWithValue("@SDT", sv.SoDienThoai);
            cmd.Parameters.AddWithValue("@Email", sv.Email);
            cmd.Parameters.AddWithValue("@MaLop", sv.MaLop);
            cmd.Parameters.AddWithValue("@TrangThai", (int)sv.TrangThai);
        }

        private SinhVien MapReaderToObj(MySqlDataReader reader)
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

                // Lấy tên khoa nếu có join
                TenKhoa = ContainsColumn(reader, "TenKhoa") && reader["TenKhoa"] != DBNull.Value
                          ? reader["TenKhoa"].ToString() : string.Empty,

                // Lấy trạng thái
                TrangThai = ContainsColumn(reader, "TrangThai") && reader["TrangThai"] != DBNull.Value
                            ? (StudentStatus)Convert.ToByte(reader["TrangThai"]) : StudentStatus.DangHoc
            };

            // Lấy GPA tính toán (CalculatedGPA)
            if (ContainsColumn(reader, "CalculatedGPA") && reader["CalculatedGPA"] != DBNull.Value)
            {
                sv.GPA = Convert.ToDecimal(reader["CalculatedGPA"]);
            }
            else if (ContainsColumn(reader, "GPA") && reader["GPA"] != DBNull.Value) // Dự phòng trường hợp cột tên là GPA
            {
                sv.GPA = Convert.ToDecimal(reader["GPA"]);
            }

            return sv;
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