using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;
using StudentManagement.DAL.Implementations; 

namespace StudentManagement.DAL.Implementations
{
    public class LopHocPhanDAL
    {
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
        
        public List<SinhVien> GetStudentsByLop(string maLop)
        {
            var list = new List<SinhVien>();
            string sql = @"
                SELECT SV.* FROM SinhVien SV
                WHERE 1 = 1 "; 
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
                TenKhoa = string.Empty, 
                GPA = 0, 
                TrangThai = (StudentStatus)Convert.ToByte(reader["TrangThai"]) 
            };
        }
    }
}