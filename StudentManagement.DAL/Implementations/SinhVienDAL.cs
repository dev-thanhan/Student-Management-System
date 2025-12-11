using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;
using System.Globalization; 

namespace StudentManagement.DAL.Implementations
{
    public class SinhVienDAL
    {
        // ================================================================
        // HÀM: Lấy tất cả Sinh viên (GetAll)
        // ================================================================
        public List<SinhVien> GetAll()
        {
            var list = new List<SinhVien>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM SinhVien"; 
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapReaderToObj(reader));
                    }
                }
            }
            return list;
        }
        
        // ================================================================
        // HÀM BỔ SUNG: CRUD (ĐỂ KHẮC PHỤC LỖI BIÊN DỊCH CS1061)
        // ================================================================

        public SinhVien GetById(string maSV)
        {
            // Cần logic SQL để lấy sinh viên theo ID
            return null; // Trả về null hoặc đối tượng mặc định
        }

        public List<SinhVien> Search(string keyword)
        {
            // Cần logic SQL để tìm kiếm sinh viên
            return new List<SinhVien>();
        }

        public bool Insert(SinhVien sv)
        {
            // Cần logic SQL để thêm sinh viên
            return false;
        }

        public bool Update(SinhVien sv)
        {
            // Cần logic SQL để cập nhật sinh viên
            return false;
        }

        public bool Delete(string maSV)
        {
            // Cần logic SQL để xóa sinh viên
            return false;
        }

        // ================================================================
        // HÀM HỖ TRỢ MAP DỮ LIỆU
        // ================================================================

        private SinhVien MapReaderToObj(MySqlDataReader reader)
        {
            return new SinhVien
            {
                MaSV = reader["MaSV"].ToString(),
                HoTen = reader["HoTen"].ToString(),
                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                GioiTinh = Convert.ToBoolean(reader["GioiTinh"]),
                DiaChi = reader["DiaChi"].ToString(),
                SoDienThoai = reader["SoDienThoai"].ToString(),
                Email = reader["Email"].ToString(),
                MaLop = reader["MaLop"].ToString(),
                
                TenKhoa = ContainsColumn(reader, "TenKhoa") ? reader["TenKhoa"].ToString() : string.Empty,
                GPA = ContainsColumn(reader, "GPA") && reader["GPA"] != DBNull.Value ? Convert.ToDecimal(reader["GPA"]) : 0,
                
                TrangThai = ContainsColumn(reader, "TrangThai") && reader["TrangThai"] != DBNull.Value ? (StudentStatus)Convert.ToByte(reader["TrangThai"]) : StudentStatus.DangHoc
            };
        }

        public static bool ContainsColumn(MySqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}