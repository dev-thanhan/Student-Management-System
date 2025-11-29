using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StudentManagement.DTO;
using StudentManagement.DAL.Interfaces;

namespace StudentManagement.DAL.Implementations
{
    public class StudentDAL : IStudentDAL
    {
        public List<Student> GetAll()
        {
            var result = new List<Student>();
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "SELECT * FROM SinhVien";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var sv = new Student
                    {
                        MaSV = reader["MaSV"].ToString(),
                        HoTen = reader["HoTen"].ToString(),
                        NgaySinh = (DateTime)reader["NgaySinh"],
                        GioiTinh = reader["GioiTinh"].ToString(),
                        MaLop = reader["MaLop"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        Email = reader["Email"].ToString(),
                        TrangThai = (bool)reader["TrangThai"]
                    };
                    result.Add(sv);
                }
            }
            return result;
        }

        // Các hàm Insert/Update/Delete/Search tương tự
    }
}
