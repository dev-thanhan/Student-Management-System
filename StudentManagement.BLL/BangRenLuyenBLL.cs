using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class BangRenLuyenBLL
    {
        private readonly BangRenLuyenDAL _dal = new BangRenLuyenDAL();

        public List<BangRenLuyen> GetByStudent(string maSV)
        {
            return _dal.GetByStudent(maSV);
        }

        public string SaveRenLuyen(BangRenLuyen rl)
        {
            if (rl.DiemSo < 0 || rl.DiemSo > 100)
                return "Điểm rèn luyện phải từ 0 đến 100.";

            // Tự động xếp loại (Business Rule)
            rl.XepLoai = CalcXepLoai(rl.DiemSo);

            if (rl.IdRenLuyen > 0) // Đã có ID -> Update
            {
                if (_dal.Update(rl)) return "Cập nhật thành công!";
            }
            else // Chưa có ID -> Insert
            {
                if (_dal.Insert(rl)) return "Thêm đánh giá thành công!";
            }
            return "Lưu thất bại.";
        }

        private string CalcXepLoai(int diem)
        {
            if (diem >= 90) return "Xuất sắc";
            if (diem >= 80) return "Giỏi";
            if (diem >= 65) return "Khá";
            if (diem >= 50) return "Trung bình";
            if (diem >= 35) return "Yếu";
            return "Kém";
        }
    }
}