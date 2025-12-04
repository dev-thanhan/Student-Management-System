using System;
using System.Collections.Generic;
using System.Text;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Interfaces
{
    public interface ISinhVienDAL
    {
        List<SinhVien> GetAll();
        SinhVien GetById(string maSV);
        bool Insert(SinhVien sv);
        bool Update(SinhVien sv);
        bool Delete(string maSV);
        List<SinhVien> Search(string keyword);
    }
}
