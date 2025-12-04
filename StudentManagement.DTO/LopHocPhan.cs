using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagement.DTO
{
    public class LopHocPhan
    {
        public string MaLopHP { get; set; } = string.Empty;
        public string MaMon { get; set; } = string.Empty;
        public string MaHocKy { get; set; } = string.Empty;
        public int SiSoToiDa { get; set; } = 40;
        public string GiangVien { get; set; } = string.Empty;

        // Thứ 2-8, Tiết bắt đầu, Số tiết dùng byte cho nhẹ (TINYINT)
        public byte Thu { get; set; }
        public byte TietBD { get; set; }
        public byte SoTiet { get; set; }
        public string PhongHoc { get; set; } = string.Empty;
    }
}
