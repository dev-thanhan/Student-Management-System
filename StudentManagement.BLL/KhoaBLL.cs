using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class KhoaBLL
    {
        private readonly KhoaDAL _dal = new KhoaDAL();

        public List<Khoa> GetAll() => _dal.GetAll();
    }
}
