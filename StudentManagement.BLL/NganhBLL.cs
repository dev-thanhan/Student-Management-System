using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class NganhBLL
    {
        private readonly NganhDAL _dal = new NganhDAL();
        private readonly LopDAL _lopDal = new LopDAL(); // Để check ràng buộc

        public List<Nganh> GetAll()
        {
            return _dal.GetAll();
        }

        public string AddNganh(Nganh nganh)
        {
            if (string.IsNullOrWhiteSpace(nganh.MaNganh) || string.IsNullOrWhiteSpace(nganh.TenNganh))
                return "Mã ngành và Tên ngành không được để trống.";

            if (string.IsNullOrWhiteSpace(nganh.MaKhoa))
                return "Phải chọn Khoa trực thuộc.";

            // Check trùng ID
            var all = _dal.GetAll();
            if (all.Exists(x => x.MaNganh == nganh.MaNganh))
                return "Mã ngành đã tồn tại.";

            if (_dal.Insert(nganh))
                return "Thêm ngành thành công.";

            return "Thêm thất bại.";
        }

        public string UpdateNganh(Nganh nganh)
        {
            if (_dal.Update(nganh))
                return "Cập nhật thành công.";
            return "Cập nhật thất bại.";
        }

        public string DeleteNganh(string maNganh)
        {
            // Business Rule: Không xóa Ngành nếu đang có Lớp sinh hoạt thuộc ngành đó
            var lopList = _lopDal.GetAll();
            if (lopList.Exists(x => x.MaNganh == maNganh))
            {
                return "Không thể xóa Ngành này vì đang có Lớp trực thuộc.";
            }

            if (_dal.Delete(maNganh))
                return "Xóa thành công.";

            return "Xóa thất bại.";
        }
    }
}