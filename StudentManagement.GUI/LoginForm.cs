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
            this.AcceptButton = btnLogin;
            this.CancelButton = btnExit; 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "admin" && password == "123456")
            {
                this.DialogResult = DialogResult.OK; 
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi Đăng nhập", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
    }
}