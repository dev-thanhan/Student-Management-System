
namespace StudentManagement.DTO
{
    public class SinhVien
    {
        public string MaSV { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; } = DateTime.Now;

        // True: Nam, False: Nữ
        public bool GioiTinh { get; set; } = true;

        public string DiaChi { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MaLop { get; set; } = string.Empty;

        public StudentStatus TrangThai { get; set; } = StudentStatus.DangHoc;
    }

    public enum StudentStatus : byte
    {
        NghiHoc = 0,
        DangHoc = 1, // Default trong SQL là 1
        BaoLuu = 2,
        TotNghiep = 3
    }
}