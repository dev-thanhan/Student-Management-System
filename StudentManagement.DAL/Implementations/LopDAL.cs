using MySql.Data.MySqlClient;
using StudentManagement.DTO;
using StudentManagement.DAL.Interfaces;

namespace StudentManagement.DAL.Implementations
{
    public class LopDAL : ILopDAL
    {
        public List<Lop> GetAll()
        {
            var list = new List<Lop>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Lop";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Lop
                        {
                            MaLop = reader["MaLop"].ToString(),
                            TenLop = reader["TenLop"].ToString(),
                            KhoaHoc = reader["KhoaHoc"].ToString(),
                            MaNganh = reader["MaNganh"].ToString(),
                            CoVanHocTap = reader["CoVanHocTap"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public List<Lop> GetByKhoa(string maKhoa)
        {
            var list = new List<Lop>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT l.* FROM Lop l 
                           JOIN Nganh n ON l.MaNganh = n.MaNganh 
                           WHERE n.MaKhoa = @MaKhoa";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Lop
                            {
                                MaLop = reader["MaLop"].ToString(),
                                TenLop = reader["TenLop"].ToString(),
                                KhoaHoc = reader["KhoaHoc"].ToString(),
                                MaNganh = reader["MaNganh"].ToString(),
                                CoVanHocTap = reader["CoVanHocTap"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public Lop GetById(string maLop)
        {
            Lop lop = null;
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Lop WHERE MaLop = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", maLop);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lop = new Lop
                            {
                                MaLop = reader["MaLop"].ToString(),
                                TenLop = reader["TenLop"].ToString(),
                                KhoaHoc = reader["KhoaHoc"].ToString(),
                                MaNganh = reader["MaNganh"].ToString(),
                                CoVanHocTap = reader["CoVanHocTap"].ToString()
                            };
                        }
                    }
                }
            }
            return lop;
        }

        public bool Insert(Lop lop)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Lop(MaLop, TenLop, KhoaHoc, MaNganh, CoVanHocTap) VALUES(@Id, @Name, @Course, @MajorId, @Advisor)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", lop.MaLop);
                    cmd.Parameters.AddWithValue("@Name", lop.TenLop);
                    cmd.Parameters.AddWithValue("@Course", lop.KhoaHoc);
                    cmd.Parameters.AddWithValue("@MajorId", lop.MaNganh);
                    cmd.Parameters.AddWithValue("@Advisor", lop.CoVanHocTap);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

        }

        public bool Update(Lop lop)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE Lop
                               SET TenLop = @Name,
                                   KhoaHoc = @Course,
                                   MaNganh = @MajorId,
                                   CoVanHocTap = @Advisor
                               WHERE MaLop = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", lop.MaLop);
                    cmd.Parameters.AddWithValue("@Name", lop.TenLop);
                    cmd.Parameters.AddWithValue("@Course", lop.KhoaHoc);
                    cmd.Parameters.AddWithValue("@MajorId", lop.MaNganh);
                    cmd.Parameters.AddWithValue("@Advisor", lop.CoVanHocTap);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(string maLop)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Lop WHERE MaLop = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", maLop);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private void AddParams(MySqlCommand cmd, Lop lop)
        {
            cmd.Parameters.AddWithValue("@Id", lop.MaLop);
            cmd.Parameters.AddWithValue("@Name", lop.TenLop);
            cmd.Parameters.AddWithValue("@Course", lop.KhoaHoc);
            cmd.Parameters.AddWithValue("@MajorId", lop.MaNganh);
            cmd.Parameters.AddWithValue("@Advisor", lop.CoVanHocTap);
        }
    }
}