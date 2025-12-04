using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagement.DTO
{
    public class BangRenLuyen
    {
        public int IdRenLuyen { get; set; } // Auto increment
        public string MaSV { get; set; } = string.Empty;
        public string MaHocKy { get; set; } = string.Empty;
        public int DiemSo { get; set; } = 0;
        public string XepLoai { get; set; } = string.Empty;
        public string NhanXet { get; set; } = string.Empty;
        public DateTime NgayDanhGia { get; set; } = DateTime.Now;
    }
}
