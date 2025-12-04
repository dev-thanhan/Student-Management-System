using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Implementations
{
    public class NganhDAL
    {
        public List<Nganh> GetAll()
        {
            var list = new List<Nganh>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Nganh";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Nganh
                        {
                            MaNganh = reader["MaNganh"].ToString(),
                            TenNganh = reader["TenNganh"].ToString(),
                            MaKhoa = reader["MaKhoa"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public Nganh GetById(string id)
        {
            Nganh obj = null;
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Nganh WHERE MaNganh = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            obj = new Nganh
                            {
                                MaNganh = reader["MaNganh"].ToString(),
                                TenNganh = reader["TenNganh"].ToString(),
                                MaKhoa = reader["MaKhoa"].ToString()
                            };
                        }
                    }
                }
            }
            return obj;
        }

        public bool Insert(Nganh obj)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Nganh(MaNganh, TenNganh, MaKhoa) VALUES(@Id, @Name, @FacultyId)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", obj.MaNganh);
                    cmd.Parameters.AddWithValue("@Name", obj.TenNganh);
                    cmd.Parameters.AddWithValue("@FacultyId", obj.MaKhoa);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(Nganh obj)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE Nganh SET TenNganh = @Name, MaKhoa = @FacultyId WHERE MaNganh = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", obj.MaNganh);
                    cmd.Parameters.AddWithValue("@Name", obj.TenNganh);
                    cmd.Parameters.AddWithValue("@FacultyId", obj.MaKhoa);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(string id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Nganh WHERE MaNganh = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}