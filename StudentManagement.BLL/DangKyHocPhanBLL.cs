using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;

namespace StudentManagement.BLL
{
    public class DangKyHocPhanBLL
    {
        private readonly DangKyHocPhanDAL _dkDAL = new DangKyHocPhanDAL();
        private readonly LopHocPhanDAL _lhpDAL = new LopHocPhanDAL();

        // Lấy danh sách đăng ký của SV
        public List<DangKyHocPhan> GetRegisteredCourses(string maSV)
        {
            return _dkDAL.GetByStudent(maSV);
        }

        // Đăng ký mới
        public string RegisterCourse(DangKyHocPhan dk)
        {
            // 1. Kiểm tra lớp học phần có tồn tại không và lấy thông tin sĩ số
            // Lưu ý: LopHocPhanDAL của bạn hiện tại chưa có hàm GetById, 
            // nên ta tạm dùng GetAll() và lọc (hoặc bạn nên bổ sung GetById vào DAL)
            var allLHP = _lhpDAL.GetAll();
            var lhp = allLHP.FirstOrDefault(x => x.MaLopHP == dk.MaLopHP);

            if (lhp == null) return "Lớp học phần không tồn tại!";

            // 2. Kiểm tra sinh viên đã đăng ký môn này chưa (hoặc trùng giờ học - logic nâng cao)
            var currentRegs = _dkDAL.GetByStudent(dk.MaSV);
            if (currentRegs.Any(x => x.MaLopHP == dk.MaLopHP))
            {
                return "Sinh viên đã đăng ký lớp học phần này rồi!";
            }

            // 3. (Nâng cao) Kiểm tra sĩ số tối đa
            // Cần query đếm số lượng đăng ký hiện tại của lớp HP đó trong DB
            // Giả sử DAL hỗ trợ CountStudents(maLopHP), nếu chưa có thì tạm bỏ qua bước này

            // 4. Thực hiện đăng ký
            dk.TrangThai = RegistrationStatus.DangKyMoi;
            dk.NgayDangKy = DateTime.Now;

            if (_dkDAL.Register(dk))
                return "Đăng ký thành công! Chờ duyệt.";

            return "Đăng ký thất bại.";
        }

        public bool CancelRegistration(string maSV, string maLopHP)
        {
            return _dkDAL.CancelRegistration(maSV, maLopHP);
        }
    }
}