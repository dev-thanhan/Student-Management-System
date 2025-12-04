using StudentManagement.DAL.Implementations;
using StudentManagement.DTO;
using System.Collections.Generic;

namespace StudentManagement.BLL
{
    public class NganhBLL
    {
        private readonly NganhDAL _dal = new NganhDAL();

        public List<Nganh> GetAll() => _dal.GetAll();
        public Nganh GetById(string maNganh) => _dal.GetById(maNganh);
        public bool Insert(Nganh nganh) => _dal.Insert(nganh);
        public bool Update(Nganh nganh) => _dal.Update(nganh);
        public bool Delete(string maNganh) => _dal.Delete(maNganh);
    }
}
