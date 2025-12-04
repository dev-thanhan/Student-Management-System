using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Implementations
{
    public class BangRenLuyenDAL
    {
        public List<BangRenLuyen> GetAll()
        {
            var list = new List<BangRenLuyen>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM BangRenLuyen";
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

        // Lấy điểm rèn luyện của 1 sinh viên cụ thể
        public List<BangRenLuyen> GetByStudent(string maSV)
        {
            var list = new List<BangRenLuyen>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM BangRenLuyen WHERE MaSV = @MaSV";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
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

        public bool Insert(BangRenLuyen obj)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Không insert IdRenLuyen vì là Auto Increment
                string sql = @"INSERT INTO BangRenLuyen(MaSV, MaHocKy, DiemSo, XepLoai, NhanXet, NgayDanhGia) 
                               VALUES(@MaSV, @MaHK, @Score, @Rank, @Comment, @Date)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    AddParams(cmd, obj);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(BangRenLuyen obj)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Cập nhật dựa trên IdRenLuyen
                string sql = @"UPDATE BangRenLuyen 
                               SET MaSV = @MaSV, 
                                   MaHocKy = @MaHK, 
                                   DiemSo = @Score, 
                                   XepLoai = @Rank, 
                                   NhanXet = @Comment, 
                                   NgayDanhGia = @Date 
                               WHERE IdRenLuyen = @Id";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    // Thêm tham số ID riêng vì hàm AddParams không bao gồm ID cho insert
                    cmd.Parameters.AddWithValue("@Id", obj.IdRenLuyen);
                    AddParams(cmd, obj);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM BangRenLuyen WHERE IdRenLuyen = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Helper maps
        private BangRenLuyen MapReaderToObj(MySqlDataReader reader)
        {
            return new BangRenLuyen
            {
                IdRenLuyen = Convert.ToInt32(reader["IdRenLuyen"]),
                MaSV = reader["MaSV"].ToString(),
                MaHocKy = reader["MaHocKy"].ToString(),
                DiemSo = Convert.ToInt32(reader["DiemSo"]), // TINYINT trong MySQL map sang int/byte
                XepLoai = reader["XepLoai"].ToString(),
                NhanXet = reader["NhanXet"].ToString(),
                NgayDanhGia = reader["NgayDanhGia"] != DBNull.Value ? Convert.ToDateTime(reader["NgayDanhGia"]) : DateTime.MinValue
            };
        }

        private void AddParams(MySqlCommand cmd, BangRenLuyen obj)
        {
            cmd.Parameters.AddWithValue("@MaSV", obj.MaSV);
            cmd.Parameters.AddWithValue("@MaHK", obj.MaHocKy);
            cmd.Parameters.AddWithValue("@Score", obj.DiemSo);
            cmd.Parameters.AddWithValue("@Rank", obj.XepLoai);
            cmd.Parameters.AddWithValue("@Comment", obj.NhanXet);
            cmd.Parameters.AddWithValue("@Date", obj.NgayDanhGia);
        }
    }
}