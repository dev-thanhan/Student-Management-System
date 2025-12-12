using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class HocKyBLL
    {
        private readonly HocKyDAL _dal = new HocKyDAL();
        private readonly LopHocPhanDAL _lhpDal = new LopHocPhanDAL();

        public List<HocKy> GetAll()
        {
            return _dal.GetAll();
        }

        public string AddHocKy(HocKy hk)
        {
            if (string.IsNullOrWhiteSpace(hk.MaHocKy) || string.IsNullOrWhiteSpace(hk.TenHocKy))
                return "Mã học kỳ và Tên học kỳ không được thiếu.";

            // Check trùng
            var all = _dal.GetAll();
            if (all.Exists(x => x.MaHocKy == hk.MaHocKy))
                return "Mã học kỳ đã tồn tại.";

            if (_dal.Insert(hk))
                return "Thêm học kỳ thành công.";

            return "Thêm thất bại.";
        }

        public string UpdateHocKy(HocKy hk)
        {
            if (_dal.Update(hk))
                return "Cập nhật thành công.";
            return "Cập nhật thất bại.";
        }

        public string DeleteHocKy(string maHK)
        {
            // Business Rule: Không xóa Học kỳ nếu đã có Lớp HP mở trong kỳ đó
            var lhpList = _lhpDal.GetBySemester(maHK);
            if (lhpList != null && lhpList.Count > 0)
            {
                return "Không thể xóa Học kỳ này vì đã có dữ liệu Lớp học phần.";
            }

            if (_dal.Delete(maHK))
                return "Xóa thành công.";

            return "Xóa thất bại.";
        }
    }
}