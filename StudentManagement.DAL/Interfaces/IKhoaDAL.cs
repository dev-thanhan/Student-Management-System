using MySql.Data.MySqlClient;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Interfaces
{
    public interface IKhoaDAL
    {
        List<Khoa> GetAll();
        Khoa GetById(string maKhoa);
        bool Insert(Khoa khoa);
        bool Update(Khoa khoa);
        bool Delete(string maKhoa);
        bool HasChildData(string maKhoa);
        //void AddParams(MySqlCommand cmd, Khoa khoa);
    }
}