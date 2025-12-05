using MySql.Data.MySqlClient;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Interfaces
{
    public interface ILopDAL
    {
        List<Lop> GetAll();
        List<Lop> GetByKhoa(string maKhoa);
        Lop GetById(string maLop);
        bool Insert(Lop lop);
        bool Update(Lop lop);
        bool Delete(string maLop);
        //void AddParams(MySqlCommand cmd, Lop lop);
    }
}