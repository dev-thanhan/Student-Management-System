using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;
using StudentManagement.DAL.Interfaces; // Nếu bạn không dùng Interface thì xóa dòng này

namespace StudentManagement.DAL.Implementations
{
    public class SinhVienDAL
    {
        // ================================================================
        // HÀM: Lấy tất cả Sinh viên
        // ================================================================
        public List<SinhVien> GetAll()
        {
            var list = new List<SinhVien>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT sv.*, k.TenKhoa,
                           (SELECT AVG(DiemTongKet) FROM Diem d 
                            JOIN LopHocPhan lhp ON d.MaLopHP = lhp.MaLopHP 
                            WHERE d.MaSV = sv.MaSV) as CalculatedGPA
                    FROM SinhVien sv
                    LEFT JOIN Lop l ON sv.MaLop = l.MaLop
                    LEFT JOIN Nganh n ON l.MaNganh = n.MaNganh
                    LEFT JOIN Khoa k ON n.MaKhoa = k.MaKhoa
                    ORDER BY sv.MaSV DESC";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(MapReaderToObj(reader));
                }
            }
            return list;
        }

        // ================================================================
        // HÀM MỚI: Lấy Sinh viên theo Lớp (Fix lỗi logic lọc lớp)
        // ================================================================
        public List<SinhVien> GetByClass(string maLop)
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
                    WHERE sv.MaLop = @MaLop";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) list.Add(MapReaderToObj(reader));
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
                string sql = @"SELECT sv.*, k.TenKhoa, 
                               (SELECT AVG(DiemTongKet) FROM Diem d WHERE d.MaSV = sv.MaSV) as CalculatedGPA
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
                        if (reader.Read()) sv = MapReaderToObj(reader);
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
                string sql = @"SELECT sv.*, k.TenKhoa, 
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
                        while (reader.Read()) list.Add(MapReaderToObj(reader));
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
                string sql = @"INSERT INTO SinhVien(MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaLop, TrangThai) 
                               VALUES(@MaSV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @Email, @MaLop, @TrangThai)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    AddParams(cmd, sv);
                    try { return cmd.ExecuteNonQuery() > 0; } catch { return false; }
                }
            }
        }

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
                    try { return cmd.ExecuteNonQuery() > 0; } catch { return false; }
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
                    try { return cmd.ExecuteNonQuery() > 0; } catch { return false; }
                }
            }
        }

        // Check ID tồn tại (Hỗ trợ validation)
        public bool IsIdExists(string maSV)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    return Convert.ToInt64(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        private void AddParams(MySqlCommand cmd, SinhVien sv)
        {
            cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
            cmd.Parameters.AddWithValue("@HoTen", sv.HoTen);
            cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
            cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh);
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
                TenKhoa = ContainsColumn(reader, "TenKhoa") && reader["TenKhoa"] != DBNull.Value ? reader["TenKhoa"].ToString() : "",
                TrangThai = ContainsColumn(reader, "TrangThai") && reader["TrangThai"] != DBNull.Value ? (StudentStatus)Convert.ToByte(reader["TrangThai"]) : StudentStatus.DangHoc
            };

            if (ContainsColumn(reader, "CalculatedGPA") && reader["CalculatedGPA"] != DBNull.Value)
                sv.GPA = Convert.ToDecimal(reader["CalculatedGPA"]);

            return sv;
        }

        public static bool ContainsColumn(MySqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }
    }
}