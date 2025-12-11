using System.Collections.Generic;
using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Linq;

namespace StudentManagement.BLL
{
    public class SinhVienBLL
    {
        private readonly SinhVienDAL _dal = new SinhVienDAL();

        /// <summary>
        /// Sửa lỗi nếu tên hàm không khớp. Lấy tất cả sinh viên.
        /// </summary>
        public List<SinhVien> GetAllStudents() => _dal.GetAll();

        // Giữ nguyên các hàm cũ của bạn (nếu có)
        public SinhVien GetById(string maSV) => _dal.GetById(maSV);
        public List<SinhVien> Search(string keyword) => _dal.Search(keyword);
        public bool Insert(SinhVien sv) => _dal.Insert(sv);
        public bool Update(SinhVien sv) => _dal.Update(sv);
        public bool Delete(string maSV) => _dal.Delete(maSV);
    }
}