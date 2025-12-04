using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;

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

        // Tìm lớp học phần theo Học kỳ
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

        private LopHocPhan MapReaderToObj(MySqlDataReader reader)
        {
            return new LopHocPhan
            {
                MaLopHP = reader["MaLopHP"].ToString(),
                MaMon = reader["MaMon"].ToString(),
                MaHocKy = reader["MaHocKy"].ToString(),
                SiSoToiDa = Convert.ToInt32(reader["SiSoToiDa"]),
                GiangVien = reader["GiangVien"].ToString(),
                // Xử lý null và convert TINYINT -> byte
                Thu = reader["Thu"] != DBNull.Value ? Convert.ToByte(reader["Thu"]) : (byte)0,
                TietBD = reader["TietBD"] != DBNull.Value ? Convert.ToByte(reader["TietBD"]) : (byte)0,
                SoTiet = reader["SoTiet"] != DBNull.Value ? Convert.ToByte(reader["SoTiet"]) : (byte)0,
                PhongHoc = reader["PhongHoc"].ToString()
            };
        }
    }
}