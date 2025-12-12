using MySql.Data.MySqlClient;
using StudentManagement.DAL;
using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;

namespace StudentManagement.BLL
{
    public class LopHocPhanBLL
    {
        private readonly LopHocPhanDAL _dal = new LopHocPhanDAL();

        public List<LopHocPhan> GetBySemester(string maHK)
        {
            return _dal.GetBySemester(maHK);
        }

        public string AddLopHocPhan(LopHocPhan lhp)
        {
            // 1. Validate dữ liệu cơ bản
            if (lhp.SiSoToiDa <= 0) return "Sĩ số tối đa phải lớn hơn 0.";
            if (lhp.SoTiet <= 0) return "Số tiết phải lớn hơn 0.";
            if (lhp.TietBD < 1 || lhp.TietBD > 12) return "Tiết bắt đầu không hợp lệ.";
            if (lhp.Thu < 2 || lhp.Thu > 8) return "Thứ không hợp lệ (2-CN).";

            // 2. Validate Logic trùng lịch phòng học (Nâng cao)
            // Lấy tất cả lớp trong cùng học kỳ để kiểm tra
            var listLop = _dal.GetBySemester(lhp.MaHocKy);

            // Kiểm tra xem có lớp nào học cùng Phòng, cùng Thứ và trùng kíp giờ không
            bool isConflict = listLop.Any(x =>
                x.PhongHoc == lhp.PhongHoc &&
                x.Thu == lhp.Thu &&
                // Logic trùng giờ: (StartA <= EndB) and (EndA >= StartB)
                (lhp.TietBD <= (x.TietBD + x.SoTiet - 1)) &&
                ((lhp.TietBD + lhp.SoTiet - 1) >= x.TietBD)
            );

            if (isConflict)
                return $"Phòng {lhp.PhongHoc} đã có lớp học vào thời gian này!";

            // 3. Gọi DAL lưu
            // Lưu ý: Bạn cần viết thêm hàm Insert trong LopHocPhanDAL
            if (_dal.Insert(lhp))
                return "Tạo lớp học phần thành công!";

            return "Lỗi hệ thống khi tạo lớp.";
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

        public string Delete(string maLopHP)
        {
            // Kiểm tra xem ID có hợp lệ không (ví dụ: không rỗng)
            if (string.IsNullOrEmpty(maLopHP)) return "Mã lớp học phần không hợp lệ.";

            try
            {
                // Gọi xuống DAL để thực hiện xóa
                if (_dal.Delete(maLopHP))
                    return "Xóa thành công.";
                else
                    return "Không tìm thấy lớp để xóa.";
            }
            catch (MySqlException ex)
            {
                // Bắt lỗi ràng buộc khóa ngoại (Foreign Key) từ MySQL
                if (ex.Number == 1451)
                    return "Không thể xóa lớp học phần này vì đã có sinh viên đăng ký hoặc có điểm.";

                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        // --- Helper Methods ---

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