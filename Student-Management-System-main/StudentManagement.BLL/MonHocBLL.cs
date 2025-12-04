using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.BLL
{
    public class MonHocBLL
    {
        private readonly MonHocDAL _dal = new MonHocDAL();

        public List<MonHoc> GetAll() => _dal.GetAll();
    }
}
