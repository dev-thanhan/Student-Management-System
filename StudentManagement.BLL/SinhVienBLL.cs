using StudentManagement.DAL.Implementations;
using StudentManagement.DAL.Interfaces;
using StudentManagement.DTO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StudentManagement.BLL
{
    public class SinhVienBLL
    {
        // Sử dụng Interface để dễ dàng unit test hoặc thay đổi DAL sau này
        private readonly ISinhVienDAL _dal = new SinhVienDAL();

        public List<SinhVien> GetAllStudents()
        {
            return _dal.GetAll();
        }

        public List<SinhVien> SearchStudents(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _dal.GetAll();
            return _dal.Search(keyword);
        }

        public string AddStudent(SinhVien sv)
        {
            // 1. Validate dữ liệu đầu vào
            if (string.IsNullOrEmpty(sv.MaSV) || string.IsNullOrEmpty(sv.HoTen))
                return "Mã sinh viên và họ tên không được để trống!";

            if (!IsValidEmail(sv.Email))
                return "Email không đúng định dạng!";

            // 2. Kiểm tra trùng mã (Business Rule)
            if (_dal.IsIdExists(sv.MaSV))
                return "Mã sinh viên đã tồn tại!";

            // 3. Gọi DAL thực hiện
            if (_dal.Insert(sv))
                return "Thêm sinh viên thành công!";
            else
                return "Thêm thất bại (Lỗi hệ thống).";
        }

        public string UpdateStudent(SinhVien sv)
        {
            // Kiểm tra tồn tại trước khi sửa
            if (!_dal.IsIdExists(sv.MaSV))
                return "Sinh viên không tồn tại!";

            if (!IsValidEmail(sv.Email))
                return "Email không đúng định dạng!";

            if (_dal.Update(sv))
                return "Cập nhật thành công!";
            else
                return "Cập nhật thất bại.";
        }

        public string DeleteStudent(string maSV)
        {
            // Business Rule: Có thể cần kiểm tra xem SV có điểm hay chưa trước khi xóa
            // Tuy nhiên, ở mức cơ bản, ta để DB lo foreign key hoặc xử lý try-catch
            try
            {
                if (_dal.Delete(maSV))
                    return "Xóa thành công!";
                else
                    return "Xóa thất bại (Không tìm thấy SV).";
            }
            catch (Exception ex)
            {
                // Bắt lỗi ràng buộc khóa ngoại từ DB (VD: SV đang có điểm)
                return "Không thể xóa sinh viên này do dữ liệu ràng buộc (Điểm, Đăng ký HP).";
            }
        }

        // Hàm phụ trợ kiểm tra Email
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException) { return false; }
        }
    }
}