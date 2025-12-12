using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.BLL
{
    public class MonHocBLL
    {
        private readonly MonHocDAL _dal = new MonHocDAL();

        /// <summary>
        /// Phương thức mới để giải quyết lỗi CS1061 trong LopMonForm.cs.
        /// Hàm này gọi hàm GetAll() hiện tại của bạn.
        /// </summary>
        public List<MonHoc> GetAllMonHocs() => _dal.GetAll();
        
        public List<MonHoc> GetAll() => _dal.GetAll();
    }
}