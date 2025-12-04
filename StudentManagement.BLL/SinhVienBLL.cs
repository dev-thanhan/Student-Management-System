using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class SinhVienBLL
    {
        private readonly SinhVienDAL _dal = new SinhVienDAL();

        public List<SinhVien> GetAllStudents()
        {
            return _dal.GetAll();
        }

        // Bạn có thể thêm Insert, Update, Delete gọi xuống DAL tương tự
    }
}