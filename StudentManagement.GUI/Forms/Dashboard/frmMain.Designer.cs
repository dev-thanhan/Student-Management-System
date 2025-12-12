using StudentManagement.GUI.Components;

namespace StudentManagement.GUI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            imageList1 = new ImageList(components);
            background = new RoundedPanel();
            panelBody = new RoundedPanel();
            panelMenu = new RoundedPanel();
            btnCloseApp = new RoundedButton();
            btnThongKe = new RoundedButton();
            btnDiem = new RoundedButton();
            btnMonHoc = new RoundedButton();
            btnLopHoc = new RoundedButton();
            circularPictureBox1 = new CircularPictureBox();
            btnSinhVien = new RoundedButton();
            btnHome = new RoundedButton();
            background.SuspendLayout();
            panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // background
            // 
            background.BackColor = Color.Transparent;
            background.BackgroundColor = Color.FromArgb(0, 162, 232);
            background.Controls.Add(panelBody);
            background.Controls.Add(panelMenu);
            background.Dock = DockStyle.Fill;
            background.Location = new Point(6, 6);
            background.Name = "background";
            background.Size = new Size(1012, 500);
            background.TabIndex = 0;
            // 
            // panelBody
            // 
            panelBody.BackColor = Color.Transparent;
            panelBody.BackgroundColor = Color.FromArgb(239, 250, 252);
            panelBody.Dock = DockStyle.Fill;
            panelBody.Location = new Point(250, 0);
            panelBody.Name = "panelBody";
            panelBody.Size = new Size(762, 500);
            panelBody.TabIndex = 1;
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.Transparent;
            panelMenu.BackgroundColor = Color.FromArgb(0, 162, 232);
            panelMenu.Controls.Add(btnCloseApp);
            panelMenu.Controls.Add(btnThongKe);
            panelMenu.Controls.Add(btnDiem);
            panelMenu.Controls.Add(btnMonHoc);
            panelMenu.Controls.Add(btnLopHoc);
            panelMenu.Controls.Add(circularPictureBox1);
            panelMenu.Controls.Add(btnSinhVien);
            panelMenu.Controls.Add(btnHome);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(250, 500);
            panelMenu.TabIndex = 0;
            // 
            // btnCloseApp
            // 
            btnCloseApp.BackColor = Color.Red;
            btnCloseApp.FlatAppearance.BorderSize = 0;
            btnCloseApp.FlatStyle = FlatStyle.Flat;
            btnCloseApp.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCloseApp.ForeColor = Color.White;
            btnCloseApp.HoverColor = Color.FromArgb(255, 128, 128);
            btnCloseApp.Location = new Point(22, 447);
            btnCloseApp.Name = "btnCloseApp";
            btnCloseApp.Size = new Size(209, 50);
            btnCloseApp.TabIndex = 5;
            btnCloseApp.Text = "ĐÓNG ỨNG DỤNG";
            btnCloseApp.UseVisualStyleBackColor = false;
            btnCloseApp.Click += btnCloseApp_Click;
            // 
            // btnThongKe
            // 
            btnThongKe.BackColor = Color.Transparent;
            btnThongKe.FlatAppearance.BorderSize = 0;
            btnThongKe.FlatStyle = FlatStyle.Flat;
            btnThongKe.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnThongKe.ForeColor = Color.White;
            btnThongKe.Location = new Point(-15, 392);
            btnThongKe.Name = "btnThongKe";
            btnThongKe.Size = new Size(246, 50);
            btnThongKe.TabIndex = 2;
            btnThongKe.Text = "THỐNG KÊ";
            btnThongKe.UseVisualStyleBackColor = false;
            btnThongKe.Click += btnThongKe_Click;
            // 
            // btnDiem
            // 
            btnDiem.BackColor = Color.Transparent;
            btnDiem.FlatAppearance.BorderSize = 0;
            btnDiem.FlatStyle = FlatStyle.Flat;
            btnDiem.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDiem.ForeColor = Color.White;
            btnDiem.Location = new Point(-15, 336);
            btnDiem.Name = "btnDiem";
            btnDiem.Size = new Size(246, 50);
            btnDiem.TabIndex = 4;
            btnDiem.Text = "BẢNG ĐIỂM";
            btnDiem.UseVisualStyleBackColor = false;
            btnDiem.Click += btnDiem_Click;
            // 
            // btnMonHoc
            // 
            btnMonHoc.BackColor = Color.Transparent;
            btnMonHoc.FlatAppearance.BorderSize = 0;
            btnMonHoc.FlatStyle = FlatStyle.Flat;
            btnMonHoc.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMonHoc.ForeColor = Color.White;
            btnMonHoc.Location = new Point(-15, 280);
            btnMonHoc.Name = "btnMonHoc";
            btnMonHoc.Size = new Size(246, 50);
            btnMonHoc.TabIndex = 3;
            btnMonHoc.Text = "MÔN HỌC";
            btnMonHoc.UseVisualStyleBackColor = false;
            btnMonHoc.Click += btnMonHoc_Click;
            // 
            // btnLopHoc
            // 
            btnLopHoc.BackColor = Color.Transparent;
            btnLopHoc.FlatAppearance.BorderSize = 0;
            btnLopHoc.FlatStyle = FlatStyle.Flat;
            btnLopHoc.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLopHoc.ForeColor = Color.White;
            btnLopHoc.Location = new Point(-15, 224);
            btnLopHoc.Name = "btnLopHoc";
            btnLopHoc.Size = new Size(246, 50);
            btnLopHoc.TabIndex = 2;
            btnLopHoc.Text = "LỚP HỌC";
            btnLopHoc.UseVisualStyleBackColor = false;
            btnLopHoc.Click += btnLopHoc_Click;
            // 
            // circularPictureBox1
            // 
            circularPictureBox1.BorderColor = SystemColors.ActiveBorder;
            circularPictureBox1.Image = (Image)resources.GetObject("circularPictureBox1.Image");
            circularPictureBox1.Location = new Point(22, 9);
            circularPictureBox1.Name = "circularPictureBox1";
            circularPictureBox1.Size = new Size(88, 88);
            circularPictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            circularPictureBox1.TabIndex = 0;
            circularPictureBox1.TabStop = false;
            // 
            // btnSinhVien
            // 
            btnSinhVien.BackColor = Color.Transparent;
            btnSinhVien.FlatAppearance.BorderSize = 0;
            btnSinhVien.FlatStyle = FlatStyle.Flat;
            btnSinhVien.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSinhVien.ForeColor = Color.White;
            btnSinhVien.Location = new Point(-15, 168);
            btnSinhVien.Name = "btnSinhVien";
            btnSinhVien.Size = new Size(246, 50);
            btnSinhVien.TabIndex = 1;
            btnSinhVien.Text = "SINH VIÊN";
            btnSinhVien.UseVisualStyleBackColor = false;
            btnSinhVien.Click += btnSinhVien_Click;
            // 
            // btnHome
            // 
            btnHome.BackColor = Color.FromArgb(255, 193, 0);
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHome.ForeColor = Color.White;
            btnHome.Location = new Point(-15, 112);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(246, 50);
            btnHome.TabIndex = 0;
            btnHome.Text = "TRANG CHỦ";
            btnHome.UseVisualStyleBackColor = false;
            btnHome.Click += btnHome_Click;
            // 
            // Home
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Gray;
            ClientSize = new Size(1024, 512);
            Controls.Add(background);
            Font = new Font("Century Gothic", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Home";
            Padding = new Padding(6);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TRANG CHỦ";
            Load += Home_Load;
            background.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ImageList imageList1;
        private RoundedPanel background;
        private RoundedPanel panelMenu;
        private RoundedPanel panelBody;
        private RoundedButton btnHome;
        private RoundedButton btnSinhVien;
        private CircularPictureBox circularPictureBox1;
        private RoundedButton btnDiem;
        private RoundedButton btnMonHoc;
        private RoundedButton btnLopHoc;
        private RoundedButton btnThongKe;
        private RoundedButton btnCloseApp;
    }
}