namespace StudentManagement.DTO
{
    public class Student
    {
        public string MaSV { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; } = DateTime.Now;
        public string GioiTinh { get; set; } = string.Empty;
        public string MaLop { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool TrangThai { get; set; } = true;

    }
}
