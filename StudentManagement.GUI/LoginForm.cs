using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement.GUI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Text = "Đăng nhập Hệ thống Quản lý Sinh viên";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = btnLogin; // Nhấn Enter sẽ kích hoạt nút Login
            this.CancelButton = btnExit;   // Nhấn Esc sẽ kích hoạt nút Exit
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // --- LOGIC XÁC THỰC (Tạm thời hardcode) ---
            if (username == "admin" && password == "123456")
            {
                // Đăng nhập thành công
                this.DialogResult = DialogResult.OK; 
                this.Close();
            }
            else
            {
                // Đăng nhập thất bại
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi Đăng nhập", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Thoát hoàn toàn ứng dụng
            Application.Exit(); 
        }
    }
}