using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement.GUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSinhVien;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        
        // KHAI BÁO CÁC CONTROLS MỚI
        private System.Windows.Forms.Button btnDanhSach; 
        private System.Windows.Forms.Button btnLocSinhVien; 
        private System.Windows.Forms.Button btnThongKe; 
        private System.Windows.Forms.Label lblTitle; // Tiêu đề lớn bên trái

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
            // Khởi tạo các Controls
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            tsmiSinhVien = new ToolStripMenuItem();
            tsmiExit = new ToolStripMenuItem();
            btnDanhSach = new Button();
            btnLocSinhVien = new Button();
            btnThongKe = new Button();
            lblTitle = new Label();
            
            menuStrip1.SuspendLayout();
            this.SuspendLayout();

            // MenuStrip (Giữ nguyên)
            menuStrip1.Items.AddRange(new ToolStripItem[] {
                fileToolStripMenuItem
            });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(800, 24);
            menuStrip1.TabIndex = 0;
            
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                tsmiSinhVien,
                tsmiExit
            });
            
            tsmiSinhVien.Text = "Sinh Viên";
            tsmiSinhVien.Click += tsmiSinhVien_Click;

            tsmiExit.Text = "Thoát";
            tsmiExit.Click += tsmiExit_Click;

            // =====================================================================
            // CẤU HÌNH LABEL VÀ CÁC NÚT TRUNG TÂM
            // =====================================================================
            
            // lblTitle (Tiêu đề lớn bên trái: ỨNG DỤNG QUẢN LÝ SINH VIÊN)
            lblTitle.Anchor = AnchorStyles.Left;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(50, 150); 
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(250, 150);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "ỨNG DỤNG QUẢN LÝ SINH VIÊN";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter; // Căn giữa
            
            // 1. btnDanhSach (Danh sách sinh viên)
            btnDanhSach.Anchor = AnchorStyles.None;
            btnDanhSach.Font = new System.Drawing.Font("Segoe UI", 14F);
            btnDanhSach.Location = new System.Drawing.Point(400, 100);
            btnDanhSach.Name = "btnDanhSach";
            btnDanhSach.Size = new System.Drawing.Size(300, 70);
            btnDanhSach.TabIndex = 1;
            btnDanhSach.Text = "Danh sách sinh viên";
            btnDanhSach.UseVisualStyleBackColor = true;
            btnDanhSach.Click += btnDanhSach_Click; 

            // 2. btnLocSinhVien (Lọc sinh viên)
            btnLocSinhVien.Anchor = AnchorStyles.None;
            btnLocSinhVien.Font = new System.Drawing.Font("Segoe UI", 14F);
            btnLocSinhVien.Location = new System.Drawing.Point(400, 200);
            btnLocSinhVien.Name = "btnLocSinhVien";
            btnLocSinhVien.Size = new System.Drawing.Size(300, 70);
            btnLocSinhVien.TabIndex = 2;
            btnLocSinhVien.Text = "Lọc sinh viên";
            btnLocSinhVien.UseVisualStyleBackColor = true;
            btnLocSinhVien.Click += btnLocSinhVien_Click; 

            // 3. btnThongKe (Thống kê)
            btnThongKe.Anchor = AnchorStyles.None;
            btnThongKe.Font = new System.Drawing.Font("Segoe UI", 14F);
            btnThongKe.Location = new System.Drawing.Point(400, 300);
            btnThongKe.Name = "btnThongKe";
            btnThongKe.Size = new System.Drawing.Size(300, 70);
            btnThongKe.TabIndex = 3;
            btnThongKe.Text = "Thống kê";
            btnThongKe.UseVisualStyleBackColor = true;
            btnThongKe.Click += btnThongKe_Click; 

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450); 
            
            // THÊM CÁC CONTROLS VÀO FORM
            this.Controls.Add(lblTitle); 
            this.Controls.Add(btnThongKe);
            this.Controls.Add(btnLocSinhVien);
            this.Controls.Add(btnDanhSach);
            this.Controls.Add(menuStrip1); 
            
            this.MainMenuStrip = menuStrip1;
            this.Name = "MainForm";
            this.Text = "Quản lý Sinh viên"; 
            this.WindowState = FormWindowState.Maximized; 

            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}