namespace StudentManagement.DTO
{
    public class DangKyHocPhan
    {
        public string MaSV { get; set; } = "";
        public string MaLopHP { get; set; } = "";
        public DateTime NgayDangKy { get; set; } = DateTime.Now;
        public RegistrationStatus TrangThai { get; set; } = RegistrationStatus.DangKyMoi;
    }
    public enum RegistrationStatus : byte
    {
        Huy = 0,
        DangKyMoi = 1,
        ChapNhan = 2
    }
}
