using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagement.DTO
{
    public class DangKyHocPhan
    {
        public string MaSV { get; set; } = string.Empty;
        public string MaLopHP { get; set; } = string.Empty;
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
