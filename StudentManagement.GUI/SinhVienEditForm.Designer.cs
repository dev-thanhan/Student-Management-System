namespace StudentManagement.GUI
{
    partial class SinhVienEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMaSV;
        private System.Windows.Forms.TextBox txtMaSV;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblMaLop;
        private System.Windows.Forms.ComboBox cboMaLop;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblMaSV = new Label();
            txtMaSV = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblNgaySinh = new Label();
            dtpNgaySinh = new DateTimePicker();
            lblGioiTinh = new Label();
            cboGioiTinh = new ComboBox();
            lblDiaChi = new Label();
            txtDiaChi = new TextBox();
            lblSDT = new Label();
            txtSDT = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblMaLop = new Label();
            cboMaLop = new ComboBox();
            lblTrangThai = new Label();
            cboTrangThai = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            txtTenKhoa = new TextBox();
            txtGPA = new TextBox();
            SuspendLayout();
            // 
            // lblMaSV
            // 
            lblMaSV.AutoSize = true;
            lblMaSV.Font = new Font("Segoe UI", 9F);
            lblMaSV.Location = new Point(14, 20);
            lblMaSV.Name = "lblMaSV";
            lblMaSV.Size = new Size(0, 20);
            lblMaSV.TabIndex = 0;
            // 
            // txtMaSV
            // 
            txtMaSV.Font = new Font("Segoe UI", 9F);
            txtMaSV.Location = new Point(137, 16);
            txtMaSV.Margin = new Padding(3, 4, 3, 4);
            txtMaSV.Name = "txtMaSV";
            txtMaSV.Size = new Size(228, 27);
            txtMaSV.TabIndex = 1;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI", 9F);
            lblHoTen.Location = new Point(14, 59);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(0, 20);
            lblHoTen.TabIndex = 2;
            // 
            // txtHoTen
            // 
            txtHoTen.Font = new Font("Segoe UI", 9F);
            txtHoTen.Location = new Point(137, 55);
            txtHoTen.Margin = new Padding(3, 4, 3, 4);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(228, 27);
            txtHoTen.TabIndex = 3;
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Font = new Font("Segoe UI", 9F);
            lblNgaySinh.Location = new Point(14, 97);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(0, 20);
            lblNgaySinh.TabIndex = 4;
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Font = new Font("Segoe UI", 9F);
            dtpNgaySinh.Location = new Point(137, 93);
            dtpNgaySinh.Margin = new Padding(3, 4, 3, 4);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(228, 27);
            dtpNgaySinh.TabIndex = 5;
            dtpNgaySinh.ValueChanged += dtpNgaySinh_ValueChanged;
            // 
            // lblGioiTinh
            // 
            lblGioiTinh.AutoSize = true;
            lblGioiTinh.Font = new Font("Segoe UI", 9F);
            lblGioiTinh.Location = new Point(14, 136);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new Size(0, 20);
            lblGioiTinh.TabIndex = 6;
            // 
            // cboGioiTinh
            // 
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.Font = new Font("Segoe UI", 9F);
            cboGioiTinh.FormattingEnabled = true;
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGioiTinh.Location = new Point(137, 136);
            cboGioiTinh.Margin = new Padding(3, 4, 3, 4);
            cboGioiTinh.Name = "cboGioiTinh";
            cboGioiTinh.Size = new Size(228, 28);
            cboGioiTinh.TabIndex = 7;
            // 
            // lblDiaChi
            // 
            lblDiaChi.AutoSize = true;
            lblDiaChi.Font = new Font("Segoe UI", 9F);
            lblDiaChi.Location = new Point(14, 169);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new Size(0, 20);
            lblDiaChi.TabIndex = 8;
            // 
            // txtDiaChi
            // 
            txtDiaChi.Font = new Font("Segoe UI", 9F);
            txtDiaChi.Location = new Point(137, 165);
            txtDiaChi.Margin = new Padding(3, 4, 3, 4);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(228, 27);
            txtDiaChi.TabIndex = 9;
            txtDiaChi.TextChanged += txtDiaChi_TextChanged;
            // 
            // lblSDT
            // 
            lblSDT.AutoSize = true;
            lblSDT.Font = new Font("Segoe UI", 9F);
            lblSDT.Location = new Point(14, 208);
            lblSDT.Name = "lblSDT";
            lblSDT.Size = new Size(0, 20);
            lblSDT.TabIndex = 10;
            // 
            // txtSDT
            // 
            txtSDT.Font = new Font("Segoe UI", 9F);
            txtSDT.Location = new Point(137, 208);
            txtSDT.Margin = new Padding(3, 4, 3, 4);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(228, 27);
            txtSDT.TabIndex = 11;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F);
            lblEmail.Location = new Point(14, 247);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 20);
            lblEmail.TabIndex = 12;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.Location = new Point(137, 243);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(228, 27);
            txtEmail.TabIndex = 13;
            // 
            // lblMaLop
            // 
            lblMaLop.AutoSize = true;
            lblMaLop.Font = new Font("Segoe UI", 9F);
            lblMaLop.Location = new Point(14, 285);
            lblMaLop.Name = "lblMaLop";
            lblMaLop.Size = new Size(0, 20);
            lblMaLop.TabIndex = 14;
            // 
            // cboMaLop
            // 
            cboMaLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMaLop.Font = new Font("Segoe UI", 9F);
            cboMaLop.FormattingEnabled = true;
            cboMaLop.Location = new Point(137, 281);
            cboMaLop.Margin = new Padding(3, 4, 3, 4);
            cboMaLop.Name = "cboMaLop";
            cboMaLop.Size = new Size(228, 28);
            cboMaLop.TabIndex = 15;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Font = new Font("Segoe UI", 9F);
            lblTrangThai.Location = new Point(14, 324);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(0, 20);
            lblTrangThai.TabIndex = 16;
            // 
            // cboTrangThai
            // 
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Font = new Font("Segoe UI", 9F);
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Items.AddRange(new object[] { "NghiHoc", "DangHoc", "BaoLuu", "TotNghiep" });
            cboTrangThai.Location = new Point(137, 425);
            cboTrangThai.Margin = new Padding(3, 4, 3, 4);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(228, 28);
            cboTrangThai.TabIndex = 17;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 9F);
            btnSave.Location = new Point(137, 489);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(86, 40);
            btnSave.TabIndex = 18;
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 9F);
            btnCancel.Location = new Point(279, 489);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(86, 40);
            btnCancel.TabIndex = 19;
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtTenKhoa
            // 
            txtTenKhoa.Font = new Font("Segoe UI", 9F);
            txtTenKhoa.Location = new Point(137, 324);
            txtTenKhoa.Margin = new Padding(3, 4, 3, 4);
            txtTenKhoa.Name = "txtTenKhoa";
            txtTenKhoa.Size = new Size(228, 27);
            txtTenKhoa.TabIndex = 20;
            // 
            // txtGPA
            // 
            txtGPA.Font = new Font("Segoe UI", 9F);
            txtGPA.Location = new Point(137, 375);
            txtGPA.Margin = new Padding(3, 4, 3, 4);
            txtGPA.Name = "txtGPA";
            txtGPA.Size = new Size(228, 27);
            txtGPA.TabIndex = 21;
            // 
            // SinhVienEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(544, 560);
            Controls.Add(txtGPA);
            Controls.Add(txtTenKhoa);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lblTrangThai);
            Controls.Add(cboTrangThai);
            Controls.Add(lblMaLop);
            Controls.Add(cboMaLop);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblSDT);
            Controls.Add(txtSDT);
            Controls.Add(lblDiaChi);
            Controls.Add(txtDiaChi);
            Controls.Add(lblGioiTinh);
            Controls.Add(cboGioiTinh);
            Controls.Add(lblNgaySinh);
            Controls.Add(dtpNgaySinh);
            Controls.Add(lblHoTen);
            Controls.Add(txtHoTen);
            Controls.Add(lblMaSV);
            Controls.Add(txtMaSV);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SinhVienEditForm";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtTenKhoa;
        private TextBox txtGPA;
    }
}
