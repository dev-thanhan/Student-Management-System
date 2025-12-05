using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class SinhVienBLL
    {
        private readonly StudentManagement.DAL.Implementations.SinhVienDAL _dal = new StudentManagement.DAL.Implementations.SinhVienDAL();

        public List<SinhVien> GetAllStudents()
        {
            return _dal.GetAll();
        }

        public List<SinhVien> Search(string keyword)
        {
            return _dal.Search(keyword);
        }

        public SinhVien GetById(string maSV)
        {
            return _dal.GetById(maSV);
        }

        public bool Insert(SinhVien sv)
        {
            return _dal.Insert(sv);
        }

        public bool Update(SinhVien sv)
        {
            return _dal.Update(sv);
        }

        public bool Delete(string maSV)
        {
            return _dal.Delete(maSV);
        }
    }
}