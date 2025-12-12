using StudentManagement.GUI.Components;
using StudentManagement.GUI.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement.GUI
{
    public partial class frmDashboard : BaseChildForm
    {
        // --- KHAI BÁO CÁC CONTROL GIAO DIỆN ---
        private RoundedPanel panelBanner;
        private Label lblSchoolName;
        private Label lblWelcome;
        private PictureBox picLogo;

        // Các thẻ thống kê
        private RoundedPanel cardSinhVien;
        private RoundedPanel cardLopHoc;
        private RoundedPanel cardMonHoc;

        public frmDashboard()
        {
            // 1. Gọi hàm khởi tạo cơ bản (Đã được viết lại bên dưới để sửa lỗi)
            InitializeComponent();

            // 2. Cấu hình Form nâng cao
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.DoubleBuffered = true; // Giúp giao diện mượt, không nháy

            // 3. Vẽ giao diện Dashboard
            SetupDashboardUI();
        }

        // --- HÀM KHỞI TẠO CƠ BẢN (Thay thế cho file Designer bị lỗi) ---
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Cấu hình Form cơ bản
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(762, 500); // Kích thước chuẩn
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDashboard";
            this.Text = "Dashboard";
            this.ResumeLayout(false);
        }

        // --- HÀM TẠO GIAO DIỆN DASHBOARD (Vẽ Banner, Thẻ...) ---
        private void SetupDashboardUI()
        {
            // A. Tạo Banner Chào Mừng
            panelBanner = new RoundedPanel();
            panelBanner.BackgroundColor = Color.FromArgb(40, 40, 60); // Màu nền tối sang trọng
            panelBanner.BorderRadius = 30;
            panelBanner.Size = new Size(700, 150);

            // Logo trường
            picLogo = new PictureBox();
            picLogo.Size = new Size(120, 120);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.BackColor = Color.Transparent;
            // picLogo.Image = Properties.Resources.Logo_SGU; // Bỏ comment nếu đã có ảnh
            picLogo.Location = new Point(560, 15);

            // Tên trường
            lblSchoolName = new Label();
            lblSchoolName.Text = "TRƯỜNG ĐẠI HỌC SÀI GÒN";
            lblSchoolName.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblSchoolName.ForeColor = Color.White;
            lblSchoolName.BackColor = Color.Transparent;
            lblSchoolName.AutoSize = true;
            lblSchoolName.Location = new Point(30, 30);

            // Lời chào
            lblWelcome = new Label();
            lblWelcome.Text = "Hệ Thống Quản Lý Sinh Viên\nNiên khóa: 2024 - 2025";
            lblWelcome.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            lblWelcome.ForeColor = Color.LightGray;
            lblWelcome.BackColor = Color.Transparent;
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(30, 80);

            // Thêm vào Banner
            panelBanner.Controls.Add(lblSchoolName);
            panelBanner.Controls.Add(lblWelcome);
            panelBanner.Controls.Add(picLogo);
            this.Controls.Add(panelBanner);

            // B. Tạo các Thẻ Thống Kê (Card)
            cardSinhVien = CreateStatCard("TỔNG SINH VIÊN", "1,250", Color.RoyalBlue);
            this.Controls.Add(cardSinhVien);

            cardLopHoc = CreateStatCard("LỚP HỌC", "45", Color.SeaGreen);
            this.Controls.Add(cardLopHoc);

            cardMonHoc = CreateStatCard("MÔN HỌC", "120", Color.DarkOrange);
            this.Controls.Add(cardMonHoc);
        }

        // Hàm hỗ trợ tạo thẻ thống kê nhanh
        private RoundedPanel CreateStatCard(string title, string value, Color color)
        {
            RoundedPanel pnl = new RoundedPanel();
            pnl.Size = new Size(220, 120);
            pnl.BackgroundColor = color;
            pnl.BorderRadius = 20;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.ForeColor = Color.WhiteSmoke;
            lblTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;

            Label lblValue = new Label();
            lblValue.Text = value;
            lblValue.ForeColor = Color.White;
            lblValue.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblValue.BackColor = Color.Transparent;
            lblValue.Location = new Point(20, 50);
            lblValue.AutoSize = true;

            pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(lblValue);
            return pnl;
        }

        // --- SỰ KIỆN RESIZE: TỰ ĐỘNG CĂN CHỈNH VỊ TRÍ ---
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // 1. Bo tròn lại Form chính
            RoundedHelper.Apply(this, 30);

            // 2. Canh giữa Banner
            if (panelBanner != null)
            {
                int bannerX = (this.ClientSize.Width - panelBanner.Width) / 2;
                panelBanner.Location = new Point(bannerX, 30);
            }

            // 3. Canh vị trí các thẻ Card (Dàn hàng ngang)
            if (cardSinhVien != null && cardLopHoc != null && cardMonHoc != null)
            {
                int gap = 30; // Khoảng cách giữa các thẻ
                int totalWidth = (cardSinhVien.Width * 3) + (gap * 2);
                int startX = (this.ClientSize.Width - totalWidth) / 2;
                int startY = 200; // Vị trí Y dưới banner

                cardSinhVien.Location = new Point(startX, startY);
                cardLopHoc.Location = new Point(startX + cardSinhVien.Width + gap, startY);
                cardMonHoc.Location = new Point(startX + (cardSinhVien.Width + gap) * 2, startY);
            }
        }
    }
}