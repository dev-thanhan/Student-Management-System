using System.Collections.Generic;
using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;

namespace StudentManagement.BLL
{
    public class HocPhanBLL
    {
        private readonly LopHocPhanDAL _lopHocPhanDAL = new LopHocPhanDAL(); 
        
        /// <summary>
        /// SỬA: Lấy danh sách sinh viên đã đăng ký học phần, chỉ lọc theo Mã Lớp Quản lý.
        /// </summary>
        /// <param name="maLop">Mã Lớp Quản lý Sinh viên (có thể là null nếu chọn "Tất cả").</param>
        public List<SinhVien> GetStudentsByLop(string maLop)
        {
            // Gọi DAL để thực hiện truy vấn SQL phức tạp
            return _lopHocPhanDAL.GetStudentsByLop(maLop);
        }
        
        // Đã loại bỏ hàm GetStudentsByLopAndMonHoc()
    }
}