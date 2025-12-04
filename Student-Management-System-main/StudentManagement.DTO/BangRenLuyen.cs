namespace StudentManagement.DTO
{
    public class BangRenLuyen
    {
        public int IdRenLuyen { get; set; }
        public string MaSV { get; set; } = ""; 
        public string MaHocKy { get; set; } = "";
        public int DiemSo { get; set; }
        public string XepLoai { get; set; } = "";
        public string NhanXet { get; set; } = "";
        public DateTime NgayDanhGia { get; set; } = DateTime.Now;
    }
}