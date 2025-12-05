using System;
using System.Windows.Forms;
using StudentManagement.BLL;

namespace StudentManagement.GUI
{
    public partial class KhoaListControl : UserControl
    {
        private readonly KhoaBLL _bll = new KhoaBLL();

        public KhoaListControl()
        {
            InitializeComponent();

            // Cấu hình bảng (Tự động tạo cột bằng code)
            dgvKhoa.AutoGenerateColumns = false;

            // Cột Mã Khoa
            DataGridViewColumn colMa = new DataGridViewTextBoxColumn();
            colMa.DataPropertyName = "MaKhoa";
            colMa.HeaderText = "Mã Khoa";
            dgvKhoa.Columns.Add(colMa);

            // Cột Tên Khoa
            DataGridViewColumn colTen = new DataGridViewTextBoxColumn();
            colTen.DataPropertyName = "TenKhoa";
            colTen.HeaderText = "Tên Khoa";
            colTen.Width = 200; // Cho rộng ra chút
            dgvKhoa.Columns.Add(colTen);

            // Cột Điện Thoại
            DataGridViewColumn colDT = new DataGridViewTextBoxColumn();
            colDT.DataPropertyName = "DienThoai";
            colDT.HeaderText = "Điện Thoại";
            dgvKhoa.Columns.Add(colDT);
        }

        // Hàm này để MainForm gọi khi bấm vào menu
        public void LoadData()
        {
            try
            {
                dgvKhoa.DataSource = _bll.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách Khoa: " + ex.Message);
            }
        }
    }
}