using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class MonHocBLL
    {
        private readonly MonHocDAL _dal = new MonHocDAL();
        // Cần thêm LopHocPhanDAL để kiểm tra ràng buộc khi xóa
        private readonly LopHocPhanDAL _lhpDal = new LopHocPhanDAL();

        public List<MonHoc> GetAll()
        {
            return _dal.GetAll();
        }

        public string AddMonHoc(MonHoc mh)
        {
            if (string.IsNullOrWhiteSpace(mh.MaMon) || string.IsNullOrWhiteSpace(mh.TenMon))
                return "Mã môn và Tên môn không được để trống.";

            if (mh.SoTinChi <= 0)
                return "Số tín chỉ phải lớn hơn 0.";

            // Kiểm tra trùng mã (Giả sử DAL chưa có hàm GetById thì dùng GetAll check tạm)
            // Tốt nhất nên bổ sung GetById vào DAL
            var existing = _dal.GetAll().Find(m => m.MaMon == mh.MaMon);
            if (existing != null) return "Mã môn học đã tồn tại.";

            if (_dal.Insert(mh))
                return "Thêm môn học thành công.";

            return "Thêm thất bại.";
        }

        public string UpdateMonHoc(MonHoc mh)
        {
            if (mh.SoTinChi <= 0) return "Số tín chỉ phải lớn hơn 0.";

            if (_dal.Update(mh))
                return "Cập nhật thành công.";

            return "Cập nhật thất bại.";
        }

        public string DeleteMonHoc(string maMon)
        {
            // Business Rule: Không xóa môn học nếu đã có Lớp học phần mở cho môn này
            // Logic này giúp tránh lỗi Foreign Key trong DB
            var lhpList = _lhpDal.GetAll(); // Hoặc GetByMonHoc nếu có
            if (lhpList.Exists(x => x.MaMon == maMon))
            {
                return "Không thể xóa môn học này vì đã có Lớp học phần sử dụng.";
            }

            if (_dal.Delete(maMon))
                return "Xóa thành công.";

            return "Xóa thất bại.";
        }
    }
}