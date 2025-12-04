using MySql.Data.MySqlClient;
using StudentManagement.DTO;


namespace StudentManagement.DAL.Implementations
{
    public class DiemDAL
    {
        // Lấy bảng điểm của một lớp học phần
        public List<Diem> GetByCourse(string maLopHP)
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
            return list;
        }

        // Cập nhật điểm (Thường giáo vụ sẽ nhập điểm chứ ít khi Insert từng dòng)
        public bool UpdateScore(Diem diem)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                // Tự động tính điểm tổng kết và kết quả ngay tại SQL hoặc tính ở BLL trước khi truyền vào
                string sql = @"UPDATE Diem 
                               SET DiemQT = @DQT, DiemThi = @DT, DiemTongKet = @DTK, KetQua = @KQ
                               WHERE MaSV = @MaSV AND MaLopHP = @MaLopHP";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", diem.MaSV);
                    cmd.Parameters.AddWithValue("@MaLopHP", diem.MaLopHP);
                    cmd.Parameters.AddWithValue("@DQT", diem.DiemQT);
                    cmd.Parameters.AddWithValue("@DT", diem.DiemThi);
                    cmd.Parameters.AddWithValue("@DTK", diem.DiemTongKet);
                    cmd.Parameters.AddWithValue("@KQ", diem.KetQua);
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
                DiemQT = Convert.ToDecimal(reader["DiemQT"]),
                DiemThi = Convert.ToDecimal(reader["DiemThi"]),
                DiemTongKet = Convert.ToDecimal(reader["DiemTongKet"]),
                KetQua = reader["KetQua"].ToString()
            };
        }
    }
}