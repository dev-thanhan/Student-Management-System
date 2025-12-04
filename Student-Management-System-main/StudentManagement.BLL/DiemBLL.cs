using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class DiemBLL
    {
        private readonly DiemDAL _dal = new DiemDAL();

        public List<Diem> GetByStudent(string maSV) => _dal.GetByStudent(maSV);
        public List<Diem> GetByLopHP(string maLopHP) => _dal.GetByLopHP(maLopHP);
        public Diem? GetBySVAndLopHP(string maSV, string maLopHP) => _dal.GetBySVAndLopHP(maSV, maLopHP);
        public bool SaveScoreList(List<Diem> listDiem) => _dal.SaveScoreList(listDiem);
        public bool UpdateScore(Diem diem) => _dal.UpdateScore(diem);
    }
}
