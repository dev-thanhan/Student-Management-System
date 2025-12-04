using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagement.DTO
{
    public class BangDiem
    {
        public string MaSV { get; set; } = string.Empty;
        public string MaLopHP { get; set; } = string.Empty;

        // Dùng decimal cho độ chính xác cao với điểm số
        public decimal DiemQT { get; set; } = 0;
        public decimal DiemThi { get; set; } = 0;
        public decimal DiemTongKet { get; set; } = 0;
        public string KetQua { get; set; } = string.Empty; // Đậu/Rớt hoặc A,B,C...
    }
}
