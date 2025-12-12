using StudentManagement.DAL.Implementations;
using StudentManagement.DAL.Interfaces;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class DiemBLL
    {
        private readonly IDiemDAL _diemDAL = new DiemDAL();

        public List<Diem> GetListDiemByLopHP(string maLopHP)
        {
            return _diemDAL.GetByLopHP(maLopHP);
        }

        // Hàm này xử lý logic tính điểm trước khi lưu
        public bool SaveBangDiem(List<Diem> listDiem)
        {
            foreach (var diem in listDiem)
            {
                // Business Rule: Kiểm tra điểm hợp lệ (0-10)
                if (!IsValidScore(diem.DiemQT) || !IsValidScore(diem.DiemThi))
                {
                    // Tùy chọn: Throw exception hoặc bỏ qua dòng lỗi
                    continue;
                }

                // Business Rule: Tính điểm tổng kết
                // Công thức ví dụ: QT * 0.3 + Thi * 0.7
                if (diem.DiemQT.HasValue && diem.DiemThi.HasValue)
                {
                    diem.DiemTongKet = (diem.DiemQT.Value * 0.3m) + (diem.DiemThi.Value * 0.7m);

                    // Làm tròn 2 chữ số thập phân
                    diem.DiemTongKet = Math.Round(diem.DiemTongKet.Value, 2);

                    // Business Rule: Xét kết quả
                    // Ví dụ: >= 4.0 là Đạt
                    diem.KetQua = diem.DiemTongKet >= 4.0m ? "Đạt" : "Rớt";
                }
            }

            // Gọi Transaction bên DAL để lưu tất cả
            return _diemDAL.SaveScoreList(listDiem);
        }

        private bool IsValidScore(decimal? score)
        {
            if (!score.HasValue) return true; // Null chấp nhận (chưa nhập)
            return score >= 0 && score <= 10;
        }
    }
}