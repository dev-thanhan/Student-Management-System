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

        public bool Insert(LopHocPhan lhp)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO LopHocPhan(MaLopHP, MaMon, MaHocKy, SiSoToiDa, GiangVien, Thu, TietBD, SoTiet, PhongHoc) 
                       VALUES(@MaLopHP, @MaMon, @MaHocKy, @SiSoToiDa, @GiangVien, @Thu, @TietBD, @SoTiet, @PhongHoc)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    AddParams(cmd, lhp);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(LopHocPhan lhp)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE LopHocPhan 
                       SET MaMon=@MaMon, MaHocKy=@MaHocKy, SiSoToiDa=@SiSoToiDa, 
                           GiangVien=@GiangVien, Thu=@Thu, TietBD=@TietBD, SoTiet=@SoTiet, PhongHoc=@PhongHoc
                       WHERE MaLopHP=@MaLopHP";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    AddParams(cmd, lhp);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(string maLopHP)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM LopHocPhan WHERE MaLopHP = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", maLopHP);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Hàm phụ trợ thêm tham số (để tránh lặp code)
        private void AddParams(MySqlCommand cmd, LopHocPhan lhp)
        {
            cmd.Parameters.AddWithValue("@MaLopHP", lhp.MaLopHP);
            cmd.Parameters.AddWithValue("@MaMon", lhp.MaMon);
            cmd.Parameters.AddWithValue("@MaHocKy", lhp.MaHocKy);
            cmd.Parameters.AddWithValue("@SiSoToiDa", lhp.SiSoToiDa);
            cmd.Parameters.AddWithValue("@GiangVien", lhp.GiangVien);
            cmd.Parameters.AddWithValue("@Thu", lhp.Thu);
            cmd.Parameters.AddWithValue("@TietBD", lhp.TietBD);
            cmd.Parameters.AddWithValue("@SoTiet", lhp.SoTiet);
            cmd.Parameters.AddWithValue("@PhongHoc", lhp.PhongHoc);
        }
    }
}