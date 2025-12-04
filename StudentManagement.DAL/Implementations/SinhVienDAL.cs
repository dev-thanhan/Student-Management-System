using MySql.Data.MySqlClient; 
using StudentManagement.DTO;
using StudentManagement.DAL.Interfaces;

namespace StudentManagement.DAL.Implementations
{
    public class SinhVienDAL : ISinhVienDAL
    {
        public List<SinhVien> GetAll()
        {
            var result = new List<SinhVien>();
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "SELECT * FROM SinhVien";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(GetStudentFromReader(reader));
                    }
                }
            }
            return result;
        }

        public SinhVien GetById(string maSV)
        {
            SinhVien sv = null;
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "SELECT * FROM SinhVien WHERE MaSV = @MaSV";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSV);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sv = GetStudentFromReader(reader);
                    }
                }
            }
            return sv;
        }

        public bool Insert(SinhVien sv)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = @"INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaLop, TrangThai)
                               VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SoDienThoai, @Email, @MaLop, @TrangThai)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                AddParams(cmd, sv);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(SinhVien sv)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = @"UPDATE SinhVien 
                               SET HoTen = @HoTen, 
                                   NgaySinh = @NgaySinh, 
                                   GioiTinh = @GioiTinh, 
                                   DiaChi = @DiaChi, 
                                   SoDienThoai = @SoDienThoai, 
                                   Email = @Email, 
                                   MaLop = @MaLop,
                                   TrangThai = @TrangThai
                               WHERE MaSV = @MaSV";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                AddParams(cmd, sv);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string maSV)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                // Lưu ý: Do có ràng buộc khóa ngoại (Foreign Key), 
                // nếu SV đã đăng ký học phần hoặc có điểm, lệnh này có thể lỗi.
                // Cần xử lý try-catch ở tầng BLL hoặc GUI.
                string sql = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSV);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<SinhVien> Search(string keyword)
        {
            var result = new List<SinhVien>();
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "SELECT * FROM SinhVien WHERE HoTen LIKE @Keyword OR MaSV LIKE @Keyword";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(GetStudentFromReader(reader));
                    }
                }
            }
            return result;
        }

        public List<SinhVien> GetByClass(string maLop)
        {
            var result = new List<SinhVien>();
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "SELECT * FROM SinhVien WHERE MaLop = @MaLop";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaLop", maLop);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(GetStudentFromReader(reader));
                    }
                }
            }
            return result;
        }

        public bool IsIdExists(string maSV)
        {
            using (MySqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSV);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        // --- Helper Methods ---

        private SinhVien GetStudentFromReader(MySqlDataReader reader)
        {
            return new SinhVien
            {
                MaSV = reader["MaSV"].ToString(),
                HoTen = reader["HoTen"].ToString(),
                // Kiểm tra null cho ngày sinh
                NgaySinh = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]) : DateTime.Now,

                // MySQL BIT trả về bool hoặc ulong, Convert.ToBoolean là an toàn nhất
                GioiTinh = reader["GioiTinh"] != DBNull.Value && Convert.ToBoolean(reader["GioiTinh"]),

                DiaChi = reader["DiaChi"].ToString(),
                SoDienThoai = reader["SoDienThoai"].ToString(),
                Email = reader["Email"].ToString(),
                MaLop = reader["MaLop"].ToString(),

                // Convert TINYINT (byte/int) sang Enum
                TrangThai = reader["TrangThai"] != DBNull.Value
                            ? (StudentStatus)Convert.ToByte(reader["TrangThai"])
                            : StudentStatus.DangHoc
            };
        }


        private void AddParams(MySqlCommand cmd, SinhVien sv)
        {
            cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
            cmd.Parameters.AddWithValue("@HoTen", sv.HoTen);
            cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
            cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh);
            cmd.Parameters.AddWithValue("@DiaChi", sv.DiaChi);
            cmd.Parameters.AddWithValue("@SoDienThoai", sv.SoDienThoai);
            cmd.Parameters.AddWithValue("@Email", sv.Email);
            cmd.Parameters.AddWithValue("@MaLop", sv.MaLop);
            // Enum sẽ được convert sang int/byte khi truyền vào parameter
            cmd.Parameters.AddWithValue("@TrangThai", sv.TrangThai);
        }
    }
}