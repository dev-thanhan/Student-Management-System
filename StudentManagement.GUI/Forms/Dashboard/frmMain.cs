using StudentManagement.DTO;
using StudentManagement.GUI.Components;
using StudentManagement.GUI.Forms;
using StudentManagement.GUI.Forms.HocTap;
using StudentManagement.GUI.Forms.SinhVien;
using StudentManagement.GUI.Forms.System;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace StudentManagement.GUI
{
    public partial class frmMain : Form
    {
        private Form currentFormChild;

        // Định nghĩa độ rộng của vùng biên để bắt sự kiện resize
        private const int cGrip = 16;      // Kích thước của tay nắm (grip)
        private const int cCaption = 32;   // Chiều cao của thanh tiêu đề ảo (để kéo thả)

        // Mã message của Windows để phát hiện khi người dùng bắt đầu/kết thúc kéo cửa sổ
        private const int WM_ENTERSIZEMOVE = 0x0231;
        private const int WM_EXITSIZEMOVE = 0x0232;

        public frmMain()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            // Đăng ký sự kiện kéo di chuyển
            this.MouseDown += Form_MouseDown;
            if (background != null) background.MouseDown += Form_MouseDown;
            if (panelMenu != null) panelMenu.MouseDown += Form_MouseDown;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // LƯU Ý QUAN TRỌNG: Không gọi RoundedHelper.Apply ở đây nữa để tránh lag khi kéo!
            // Chúng ta chỉ gọi nó khi kết thúc kéo (trong WndProc -> WM_EXITSIZEMOVE)
        }

        private void Home_Load(object sender, EventArgs e)
        {
            // Bo tròn lần đầu khi mở app
            RoundedHelper.Apply(this, 30);
        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        protected override void WndProc(ref Message m)
        {
            // Xử lý logic chống lag khi thay đổi kích thước
            switch (m.Msg)
            {
                case WM_ENTERSIZEMOVE:
                    // Bắt đầu kéo: Dừng layout và ẩn form con để giảm tải GPU
                    this.SuspendLayout();
                    if (currentFormChild != null) currentFormChild.Visible = false;
                    break;

                case WM_EXITSIZEMOVE:
                    // Kết thúc kéo: Tính toán lại layout và hiện form con
                    this.ResumeLayout();
                    if (currentFormChild != null) currentFormChild.Visible = true;
                    // Bo tròn lại góc khi đã chốt kích thước
                    RoundedHelper.Apply(this, 30);
                    break;
            }

            // 0x84 là WM_NCHITTEST - Kiểm tra vị trí chuột để hiển thị mũi tên resize
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);

                // Kiểm tra góc dưới bên phải
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }

                // Kiểm tra biên phải
                if (pos.X >= this.ClientSize.Width - cGrip)
                {
                    m.Result = (IntPtr)11; // HTRIGHT
                    return;
                }

                // Kiểm tra biên dưới
                if (pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)15; // HTBOTTOM
                    return;
                }

                // Kiểm tra vùng tiêu đề ảo (để di chuyển)
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void OpenChildForm(Form childForm)
        {
            // Nếu bấm lại vào nút của form đang mở thì không làm gì cả
            if (currentFormChild != null && currentFormChild.GetType() == childForm.GetType())
            {
                return;
            }

            if (currentFormChild != null)
            {
                currentFormChild.Close();
                currentFormChild.Dispose();
            }

            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelBody.Controls.Add(childForm);
            panelBody.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnHome_Click(object sender, EventArgs e) => OpenChildForm(new frmDashboard());
        private void btnLopHoc_Click(object sender, EventArgs e) => OpenChildForm(new frmLop());
        private void btnMonHoc_Click(object sender, EventArgs e) => OpenChildForm(new frmMonHoc());
        private void btnDiem_Click(object sender, EventArgs e) => OpenChildForm(new frmDiem());
        private void btnThongKe_Click(object sender, EventArgs e) => OpenChildForm(new frmThongKe());
        private void btnSinhVien_Click(object sender, EventArgs e) => OpenChildForm(new frmSinhVien());

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn tắt phần mềm không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Bật Double Buffering cấp hệ điều hành
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}