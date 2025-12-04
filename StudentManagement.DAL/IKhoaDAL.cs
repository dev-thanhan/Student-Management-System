using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.DAL.Interfaces
{
    public interface IKhoaDAL
    {
        List<Khoa> GetAll();
        bool Insert(Khoa khoa);
        bool Update(Khoa khoa);
        bool Delete(string maKhoa);
    }
}