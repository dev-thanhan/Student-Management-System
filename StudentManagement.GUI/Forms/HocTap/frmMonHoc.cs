using StudentManagement.GUI.Components;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement.GUI.Forms.HocTap
{
    public partial class frmMonHoc : BaseChildForm
    {
        public frmMonHoc()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.dgvMon = new DataGridView();
            this.btnAdd = new RoundedButton() { Text = "Thêm Môn", BackColor = Color.DodgerBlue };

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.ClientSize = new Size(762, 500);

            lblTitle.Text = "DANH SÁCH MÔN HỌC";
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 60;
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.Padding = new Padding(20, 0, 0, 0);

            btnAdd.Location = new Point(20, 70);

            dgvMon.Location = new Point(20, 130);
            dgvMon.Size = new Size(720, 350);
            dgvMon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Cột của môn học
            dgvMon.Columns.Add("MaMon", "Mã Môn");
            dgvMon.Columns.Add("TenMon", "Tên Môn Học");
            dgvMon.Columns.Add("TinChi", "Số Tín Chỉ");

            this.Controls.Add(btnAdd);
            this.Controls.Add(dgvMon);
            this.Controls.Add(lblTitle);
        }

        private Label lblTitle;
        private DataGridView dgvMon;
        private RoundedButton btnAdd;
    }
}