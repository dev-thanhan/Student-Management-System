using MySql.Data.MySqlClient;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Interfaces
{
    public interface IDiemDAL
    {
        List<Diem> GetByLopHP(string maLopHP);
        List<Diem> GetByStudent(string maSV);
        bool SaveScoreList(List<Diem> listDiem);
        bool UpdateScore(Diem diem);
        //void AddParams(MySqlCommand cmd, Diem diem);
    }
}