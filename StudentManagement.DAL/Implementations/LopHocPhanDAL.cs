using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;
// Giả định DbHelper nằm trong namespace StudentManagement.DAL.Implementations hoặc được truy cập qua using này
using StudentManagement.DAL.Implementations; 

namespace StudentManagement.DAL.Implementations
{
    public class LopHocPhanDAL
    {
        // ================================================================
        // HÀM CHÍNH: LẤY DANH SÁCH LỚP HỌC PHẦN (GIỮ NGUYÊN)
        // ================================================================

        public List<LopHocPhan> GetAll()
        {
            var list = new List<LopHocPhan>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM LopHocPhan";
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

        public List<LopHocPhan> GetBySemester(string maHocKy)
        {
            var list = new List<LopHocPhan>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM LopHocPhan WHERE MaHocKy = @MaHK";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHK", maHocKy);
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
        // HÀM LỌC SINH VIÊN THEO LỚP QUẢN LÝ (ĐÃ SỬA LỖI SQL)
        // ================================================================
        
        /// <summary>
        /// Lấy danh sách Sinh viên chỉ lọc theo Mã Lớp Quản lý (MaLop).
        /// </summary>
        public List<SinhVien> GetStudentsByLop(string maLop)
        {
            var list = new List<SinhVien>();
            
            // SỬA SQL: Chỉ SELECT từ bảng SinhVien (không JOIN DangKyHocPhan hay LopHocPhan)
            string sql = @"
                SELECT SV.* FROM SinhVien SV
                WHERE 1 = 1 "; 

            // Thêm điều kiện lọc theo Mã Lớp Quản lý Sinh viên (MaLop)
            if (!string.IsNullOrEmpty(maLop))
            {
                sql += " AND SV.MaLop = @MaLop";
            }

            sql += " ORDER BY SV.MaSV";

            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(maLop))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToSinhVien(reader));
                        }
                    }
                }
            }
            return list;
        }

        // ================================================================
        // HÀM HỖ TRỢ MAP DỮ LIỆU
        // ================================================================

        private LopHocPhan MapReaderToObj(MySqlDataReader reader)
        {
            return new LopHocPhan
            {
                MaLopHP = reader["MaLopHP"].ToString(),
                MaMon = reader["MaMon"].ToString(),
                MaHocKy = reader["MaHocKy"].ToString(),
                SiSoToiDa = Convert.ToInt32(reader["SiSoToiDa"]),
                GiangVien = reader["GiangVien"].ToString(),
                Thu = reader["Thu"] != DBNull.Value ? Convert.ToByte(reader["Thu"]) : (byte)0,
                TietBD = reader["TietBD"] != DBNull.Value ? Convert.ToByte(reader["TietBD"]) : (byte)0,
                SoTiet = reader["SoTiet"] != DBNull.Value ? Convert.ToByte(reader["SoTiet"]) : (byte)0,
                PhongHoc = reader["PhongHoc"].ToString()
            };
        }
        
        private SinhVien MapReaderToSinhVien(MySqlDataReader reader)
        {
            // Đảm bảo tên cột khớp với bảng SinhVien trong DB của bạn
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
                
                // Các thuộc tính không được SELECT:
                TenKhoa = string.Empty, 
                GPA = 0, 
                // Giả định cột TrangThai tồn tại trong bảng SinhVien và là byte
                TrangThai = (StudentStatus)Convert.ToByte(reader["TrangThai"]) 
            };
        }
    }
}