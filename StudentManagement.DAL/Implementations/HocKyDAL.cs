using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StudentManagement.DTO;

namespace StudentManagement.DAL.Implementations
{
    public class HocKyDAL
    {
        public List<HocKy> GetAll()
        {
            var list = new List<HocKy>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM HocKy";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new HocKy
                        {
                            MaHocKy = reader["MaHocKy"].ToString(),
                            TenHocKy = reader["TenHocKy"].ToString(),
                            NamHoc = reader["NamHoc"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public bool Insert(HocKy obj)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO HocKy(MaHocKy, TenHocKy, NamHoc) VALUES(@Id, @Name, @Year)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", obj.MaHocKy);
                    cmd.Parameters.AddWithValue("@Name", obj.TenHocKy);
                    cmd.Parameters.AddWithValue("@Year", obj.NamHoc);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(HocKy obj)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE HocKy SET TenHocKy = @Name, NamHoc = @Year WHERE MaHocKy = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", obj.MaHocKy);
                    cmd.Parameters.AddWithValue("@Name", obj.TenHocKy);
                    cmd.Parameters.AddWithValue("@Year", obj.NamHoc);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(string id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM HocKy WHERE MaHocKy = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}