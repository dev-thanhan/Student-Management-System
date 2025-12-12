using StudentManagement.DAL.Implementations;
using StudentManagement.DAL.Interfaces;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class KhoaBLL
    {
        private readonly IKhoaDAL _khoaDAL = new KhoaDAL();

        public List<Khoa> GetAll()
        {
            return _khoaDAL.GetAll();
        }

        public string DeleteKhoa(string maKhoa)
        {
            // Business Rule: Không được xóa Khoa nếu đã có Ngành trực thuộc
            if (_khoaDAL.HasChildData(maKhoa))
            {
                return "Không thể xóa Khoa này vì đang có Ngành học trực thuộc. Hãy xóa Ngành trước.";
            }

            if (_khoaDAL.Delete(maKhoa))
                return "Xóa khoa thành công.";
            else
                return "Lỗi khi xóa khoa.";
        }

        public bool AddKhoa(Khoa khoa)
        {
            // Có thể thêm check trùng ID ở đây tương tự SinhVienBLL
            return _khoaDAL.Insert(khoa);
        }
    }
}