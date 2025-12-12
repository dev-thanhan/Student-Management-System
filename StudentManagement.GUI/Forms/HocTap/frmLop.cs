using StudentManagement.GUI.Components;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement.GUI.Forms.HocTap
{
    public partial class frmLop : BaseChildForm
    {
        public frmLop()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // --- Phần này copy từ frmTemplate và sửa lại ---
            this.lblTitle = new Label();
            this.dgvLop = new DataGridView();
            this.btnAdd = new RoundedButton() { Text = "Thêm Lớp", BackColor = Color.SeaGreen };
            this.btnEdit = new RoundedButton() { Text = "Sửa", BackColor = Color.Orange };
            this.btnDelete = new RoundedButton() { Text = "Xóa", BackColor = Color.Firebrick };

            // Cấu hình Form
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.ClientSize = new Size(762, 500);

            // 1. Tiêu đề
            lblTitle.Text = "QUẢN LÝ LỚP HỌC";
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 60;
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.Padding = new Padding(20, 0, 0, 0);

            // 2. Các nút chức năng (Đặt tạm vị trí, bạn có thể chỉnh lại)
            btnAdd.Location = new Point(20, 70);
            btnEdit.Location = new Point(180, 70);
            btnDelete.Location = new Point(340, 70);

            // 3. Bảng dữ liệu (DataGridView)
            dgvLop.Location = new Point(20, 130);
            dgvLop.Size = new Size(720, 350);
            dgvLop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Thêm cột mẫu
            dgvLop.Columns.Add("MaLop", "Mã Lớp");
            dgvLop.Columns.Add("TenLop", "Tên Lớp");
            dgvLop.Columns.Add("Khoa", "Khoa");

            // Thêm vào Form
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnDelete);
            this.Controls.Add(dgvLop);
            this.Controls.Add(lblTitle);
        }

        // Khai báo biến
        private Label lblTitle;
        private DataGridView dgvLop;
        private RoundedButton btnAdd, btnEdit, btnDelete;
    }
}