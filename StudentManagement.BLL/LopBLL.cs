using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class LopBLL
    {
        private readonly LopDAL _dal = new LopDAL();
        private readonly SinhVienDAL _svDal = new SinhVienDAL(); // Cần để check ràng buộc

        public List<Lop> GetAll() => _dal.GetAll();
        public List<Lop> GetByKhoa(string maKhoa) => _dal.GetByKhoa(maKhoa);

        public string AddLop(Lop lop)
        {
            if (string.IsNullOrEmpty(lop.MaLop) || string.IsNullOrEmpty(lop.TenLop))
                return "Thông tin lớp không hợp lệ.";

            if (_dal.GetById(lop.MaLop) != null)
                return "Mã lớp đã tồn tại.";

            return _dal.Insert(lop) ? "Thêm thành công" : "Thêm thất bại";
        }

        public string DeleteLop(string maLop)
        {
            // Kiểm tra xem lớp có sinh viên không trước khi xóa
            var svList = _svDal.GetByClass(maLop);
            if (svList.Count > 0)
                return $"Lớp này đang có {svList.Count} sinh viên. Không thể xóa!";

            return _dal.Delete(maLop) ? "Xóa thành công" : "Xóa thất bại";
        }
    }
}