using MySql.Data.MySqlClient;
using StudentManagement.DTO;
using StudentManagement.DAL.Interfaces;

namespace StudentManagement.DAL.Implementations
{
    public class DiemDAL : IDiemDAL
    {
        public List<Diem> GetByLopHP(string maLopHP)
        {
            var list = new List<Diem>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Diem WHERE MaLopHP = @MaLopHP";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLopHP", maLopHP);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToGrade(reader));
                        }
                    }
                }
            }
            return new List<Diem>();
        }

        public List<Diem> GetByStudent(string maSV)
        {
            var list = new List<Diem>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Diem WHERE MaSV = @MaSV";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToGrade(reader));
                        }
                    }
                }
            }
            return list;
        }

        public bool SaveScoreList(List<Diem> listDiem)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Tạo Transaction: Nếu lưu 10 sv mà lỗi 1 người thì rollback hết
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = @"UPDATE Diem 
                                   SET DiemQT = @DQT, DiemThi = @DT, 
                                       DiemTongKet = @DTK, KetQua = @KQ
                                   WHERE MaSV = @MaSV AND MaLopHP = @MaLopHP";

                        foreach (var diem in listDiem)
                        {
                            using (var cmd = new MySqlCommand(sql, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaSV", diem.MaSV);
                                cmd.Parameters.AddWithValue("@MaLopHP", diem.MaLopHP);
                                cmd.Parameters.AddWithValue("@DQT", (object)diem.DiemQT ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@DT", (object)diem.DiemThi ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@DTK", (object)diem.DiemTongKet ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@KQ", (object)diem.KetQua ?? DBNull.Value);

                                cmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public Diem? GetBySVAndLopHP(string maSV, string maLopHP)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Diem WHERE MaSV = @MaSV AND MaLopHP = @MaLopHP";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    cmd.Parameters.AddWithValue("@MaLopHP", maLopHP);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToGrade(reader);
                        }
                    }
                }
            }
            return null;
        }

        public bool UpdateScore(Diem diem)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE Diem 
                               SET DiemQT = @DQT, 
                                   DiemThi = @DT, 
                                   DiemTongKet = @DTK, 
                                   KetQua = @KQ
                               WHERE MaSV = @MaSV AND MaLopHP = @MaLopHP";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", diem.MaSV);
                    cmd.Parameters.AddWithValue("@MaLopHP", diem.MaLopHP);
                    cmd.Parameters.AddWithValue("@DQT", (object)diem.DiemQT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DT", (object)diem.DiemThi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DTK", (object)diem.DiemTongKet ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@KQ", (object)diem.KetQua ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private Diem MapReaderToGrade(MySqlDataReader reader)
        {
            return new Diem
            {
                MaSV = reader["MaSV"].ToString(),
                MaLopHP = reader["MaLopHP"].ToString(),
                DiemQT = reader["DiemQT"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["DiemQT"]) : null,
                DiemThi = reader["DiemThi"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["DiemThi"]) : null,
                DiemTongKet = reader["DiemTongKet"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["DiemTongKet"]) : null,
                KetQua = reader["KetQua"] != DBNull.Value ? reader["KetQua"].ToString() : string.Empty
            };
        }
    }
}