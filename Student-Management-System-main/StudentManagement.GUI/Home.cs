using System;
using System.Windows.Forms;
using StudentManagement.BLL; // Đảm bảo đã reference project BLL

namespace StudentManagement.GUI
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            // Gọi hàm test ngay khi khởi tạo Form
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SinhVienBLL bll = new SinhVienBLL();
                var listSinhVien = bll.GetAllStudents();

                if (listSinhVien.Count > 0)
                {
                    MessageBox.Show($"Kết nối thành công! Tìm thấy {listSinhVien.Count} sinh viên trong DB.", "Thông báo");
                    // Mở form quản lý sinh viên để thuận tiện phát triển
                    var svForm = new SinhVienForm();
                    svForm.Show();
                }
                else
                {
                    MessageBox.Show("Kết nối thành công nhưng Database đang trống!", "Thông báo");
                    var svForm = new SinhVienForm();
                    svForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi Nghiêm Trọng");
            }
        }
    }
}