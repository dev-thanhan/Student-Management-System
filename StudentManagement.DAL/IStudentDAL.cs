using System;
using System.Collections.Generic;
using System.Text;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Interfaces
{
    public interface IStudentDAL
    {
        List<Student> GetAll();
        Student GetById(string maSV);
        bool Insert(Student sv);
        bool Update(Student sv);
        bool Delete(string maSV);
        List<Student> Search(string keyword);
    }
}
