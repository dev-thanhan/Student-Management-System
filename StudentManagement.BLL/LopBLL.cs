using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.BLL
{
    public class LopBLL
    {
        private readonly LopDAL _dal = new LopDAL();

        /// <summary>
        /// Phương thức mới để giải quyết lỗi CS1061 trong LopMonForm.cs.
        /// Hàm này gọi hàm GetAll() hiện tại của bạn.
        /// </summary>
        public List<Lop> GetAllLops() => _dal.GetAll();
        
        // --- Các phương thức hiện có (Giữ nguyên) ---
        public List<Lop> GetAll() => _dal.GetAll();
        public Lop GetById(string maLop) => _dal.GetById(maLop);
        public List<Lop> Search(string keyword) => _dal.GetAll().Where(x => x.TenLop.Contains(keyword)).ToList();
        public bool Insert(Lop lop) => _dal.Insert(lop);
        public bool Update(Lop lop) => _dal.Update(lop);
        public List<Lop> GetByKhoa(string maKhoa) => _dal.GetByKhoa(maKhoa);
    }
}