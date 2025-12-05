using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Implementations
{
    public class DangKyHocPhanDAL
    {
        // Đăng ký mới môn học
        public bool Register(DangKyHocPhan dk)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO DangKyHocPhan(MaSV, MaLopHP, NgayDangKy, TrangThai) VALUES(@MaSV, @MaLopHP, @NgayDK, @TrangThai)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", dk.MaSV);
                    cmd.Parameters.AddWithValue("@MaLopHP", dk.MaLopHP);
                    cmd.Parameters.AddWithValue("@NgayDK", dk.NgayDangKy);
                    cmd.Parameters.AddWithValue("@TrangThai", dk.TrangThai); // Enum tự convert sang int/byte
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Hủy đăng ký (Xóa)
        public bool CancelRegistration(string maSV, string maLopHP)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM DangKyHocPhan WHERE MaSV = @MaSV AND MaLopHP = @MaLopHP";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    cmd.Parameters.AddWithValue("@MaLopHP", maLopHP);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Lấy danh sách lớp học phần đã đăng ký của 1 sinh viên
        public List<DangKyHocPhan> GetByStudent(string maSV)
        {
            var list = new List<DangKyHocPhan>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM DangKyHocPhan WHERE MaSV = @MaSV";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DangKyHocPhan
                            {
                                MaSV = reader["MaSV"].ToString(),
                                MaLopHP = reader["MaLopHP"].ToString(),
                                NgayDangKy = Convert.ToDateTime(reader["NgayDangKy"]),
                                TrangThai = (RegistrationStatus)Convert.ToByte(reader["TrangThai"])
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}