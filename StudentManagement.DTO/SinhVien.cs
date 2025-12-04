namespace StudentManagement.DTO
{
    public class SinhVien
    {
        public string MaSV { get; set; } = "";
        public string HoTen { get; set; } = "";
        public DateTime NgaySinh { get; set; } = DateTime.Now;
        public bool GioiTinh { get; set; } // True: Nam, False: Nữ
        public string DiaChi { get; set; } = "";
        public string SoDienThoai { get; set; } = "";
        public string Email { get; set; } = "";
        public string MaLop { get; set; } = "";

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