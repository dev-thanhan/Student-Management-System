using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using StudentManagement.GUI.Components;

namespace StudentManagement.GUI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            // Xử lý di chuyển cửa sổ khi kéo chuột vào panel
            this.MouseDown += Form_MouseDown;
            panelContainer.MouseDown += Form_MouseDown;
            lblTitle.MouseDown += Form_MouseDown;

            // Bo tròn form login
            RoundedHelper.Apply(this, 30);
        }

        // --- CODE DI CHUYỂN FORM (Tương tự frmMain) ---
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        // --- SỰ KIỆN NÚT ---
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // KIỂM TRA ĐĂNG NHẬP (Tạm thời hardcode, sau này bạn gọi xuống BLL/Database)
            if (username == "admin" && password == "123")
            {
                // Ẩn form Login
                this.Hide();

                // Mở form Main
                frmMain mainForm = new frmMain();
                mainForm.ShowDialog(); // Dùng ShowDialog để khi Main đóng, code sẽ chạy tiếp dòng dưới

                // Khi Main đóng thì đóng luôn Login để thoát app
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}