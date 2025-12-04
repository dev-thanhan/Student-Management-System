using MySql.Data.MySqlClient;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Interfaces
{
    public interface ISinhVienDAL
    {
        List<SinhVien> GetAll();
        List<SinhVien> GetByClass(string maLop);
        List<SinhVien> Search(string keyword);
        SinhVien GetById(string maSV);
        bool Insert(SinhVien sv);
        bool Update(SinhVien sv);
        bool Delete(string maSV);
        bool IsIdExists(string maSV);
        //void AddParams(MySqlCommand cmd, SinhVien sv);
    }
}