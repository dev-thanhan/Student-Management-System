using System.Collections.Generic;
using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;

namespace StudentManagement.BLL
{
    public class SinhVienBLL
    {
        private readonly SinhVienDAL _dal = new SinhVienDAL();

        /// <summary>
        /// Lấy tất cả sinh viên.
        /// </summary>
        public List<SinhVien> GetAllStudents() => _dal.GetAll();
        public SinhVien GetById(string maSV) => _dal.GetById(maSV);
        public List<SinhVien> Search(string keyword) => _dal.Search(keyword);
        public bool Insert(SinhVien sv) => _dal.Insert(sv);
        public bool Update(SinhVien sv) => _dal.Update(sv);
        public bool Delete(string maSV) => _dal.Delete(maSV);
        public bool Add(SinhVien sv)
        {
            if (string.IsNullOrEmpty(sv.MaSV)) throw new Exception("Mã Sinh viên không được để trống.");
            if (string.IsNullOrEmpty(sv.HoTen)) throw new Exception($"Họ tên của sinh viên {sv.MaSV} không được để trống.");
            SinhVien existingStudent = _dal.GetById(sv.MaSV);

            if (existingStudent == null)
            {
                return Insert(sv);
            }
            else
            {
                return Update(sv);
            }
        }
        
        /// <summary>
        /// Thống kê số lượng sinh viên theo Giới tính.
        /// </summary>
        public Dictionary<string, int> ThongKeTheoGioiTinh()
        {
            return _dal.ThongKeTheoGioiTinh();
        }

        /// <summary>
        /// Thống kê số lượng sinh viên theo Năm sinh.
        /// </summary>
        public Dictionary<int, int> ThongKeTheoNamSinh()
        {
            return _dal.ThongKeTheoNamSinh();
        }

        /// <summary>
        /// Thống kê số lượng sinh viên theo Mức GPA.
        /// </summary>
        public Dictionary<string, int> ThongKeTheoGPA()
        {
            return _dal.ThongKeTheoGPA();
        }
    }
}