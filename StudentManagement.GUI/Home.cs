using System;
using System.Windows.Forms;
using StudentManagement.GUI;

namespace StudentManagement.GUI
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            // Gán sự kiện Load
            this.Load += Home_Load;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            try
            {
                // Mở giao diện quản lý Lớp – Môn
                LopMonForm f = new LopMonForm();
                f.Show();

                // Ẩn form Home nếu bạn không dùng nó
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động: " + ex.Message, "Lỗi");
            }
        }
    }
}
