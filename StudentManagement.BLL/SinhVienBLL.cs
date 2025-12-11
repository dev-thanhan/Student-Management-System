using System.Collections.Generic;
using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;

namespace StudentManagement.BLL
{
    public class SinhVienBLL
    {
        private readonly SinhVienDAL _dal = new SinhVienDAL();

        public List<SinhVien> GetAllStudents() => _dal.GetAll();

        // HÀM MỚI ĐƯỢC THÊM
        public List<SinhVien> GetStudentsByClass(string maLop) => _dal.GetByClass(maLop);

        public SinhVien GetById(string maSV) => _dal.GetById(maSV);
        public List<SinhVien> Search(string keyword) => _dal.Search(keyword);
        public bool Insert(SinhVien sv) => _dal.Insert(sv);
        public bool Update(SinhVien sv) => _dal.Update(sv);
        public bool Delete(string maSV) => _dal.Delete(maSV);
    }
}