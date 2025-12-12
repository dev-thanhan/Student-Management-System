using System;
using System.Drawing;
using System.Windows.Forms;
using StudentManagement.BLL; // Cần using BLL và DTO
using StudentManagement.DTO;
using StudentManagement.GUI.Components;

namespace StudentManagement.GUI.Forms.SinhVien
{
    public partial class frmSinhVien : BaseChildForm
    {
        // 1. Khai báo BLL để giao tiếp
        private readonly SinhVienBLL _bll = new SinhVienBLL();

        public frmSinhVien()
        {
            InitializeComponent();
            // 2. Đăng ký các sự kiện sau khi khởi tạo giao diện
            this.Load += FrmSinhVien_Load;
            this.btnSearch.Click += BtnSearch_Click;
            this.btnDelete.Click += BtnDelete_Click;

            // Các nút Thêm/Sửa cần mở Form chi tiết (chưa có code form con nên tôi tạm hiển thị thông báo)
            this.btnAdd.Click += (s, e) => MessageBox.Show("Chức năng đang phát triển: Mở form thêm mới.");
            this.btnEdit.Click += (s, e) => MessageBox.Show("Chức năng đang phát triển: Mở form sửa.");
        }

        // --- Sự kiện Load Form ---
        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            LoadData(); // Tải dữ liệu khi mở form
        }

        // --- Hàm tải dữ liệu lên GridView ---
        private void LoadData()
        {
            try
            {
                dgvSinhVien.DataSource = _bll.GetAllStudents();
                CustomizeGridView(); // Định dạng lại cột cho đẹp
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- Sự kiện Tìm kiếm ---
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            dgvSinhVien.DataSource = _bll.SearchStudents(keyword);
        }

        // --- Sự kiện Xóa ---
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count > 0)
            {
                // Lấy MaSV từ dòng đang chọn
                string maSV = dgvSinhVien.SelectedRows[0].Cells["MaSV"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa sinh viên {maSV}?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string message = _bll.DeleteStudent(maSV);
                    MessageBox.Show(message);
                    LoadData(); // Refresh lại lưới sau khi xóa
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên để xóa!");
            }
        }

        private void CustomizeGridView()
        {
            // Ẩn các cột không cần thiết nếu DataGridView tự generate cột từ List<SinhVien>
            // Nếu bạn đã Add Columns thủ công ở InitializeComponent, thì cần map DataPropertyName
            if (dgvSinhVien.Columns["MaSV"] != null) dgvSinhVien.Columns["MaSV"].DataPropertyName = "MaSV";
            if (dgvSinhVien.Columns["HoTen"] != null) dgvSinhVien.Columns["HoTen"].DataPropertyName = "HoTen";
            if (dgvSinhVien.Columns["NgaySinh"] != null) dgvSinhVien.Columns["NgaySinh"].DataPropertyName = "NgaySinh";
            if (dgvSinhVien.Columns["GioiTinh"] != null) dgvSinhVien.Columns["GioiTinh"].DataPropertyName = "GioiTinh";
            if (dgvSinhVien.Columns["Lop"] != null) dgvSinhVien.Columns["Lop"].DataPropertyName = "MaLop"; // Lưu ý tên Property trong DTO
            if (dgvSinhVien.Columns["SDT"] != null) dgvSinhVien.Columns["SDT"].DataPropertyName = "SoDienThoai";
        }

        private void InitializeComponent()
        {
            // --- GIỮ NGUYÊN CODE GIAO DIỆN CỦA BẠN ---
            this.lblTitle = new Label();
            this.dgvSinhVien = new DataGridView();
            this.txtSearch = new TextBox();

            this.btnSearch = new RoundedButton() { Text = "Tìm kiếm", BackColor = Color.MediumSlateBlue, ForeColor = Color.White };
            this.btnAdd = new RoundedButton() { Text = "Thêm Mới", BackColor = Color.SeaGreen, ForeColor = Color.White };
            this.btnEdit = new RoundedButton() { Text = "Sửa", BackColor = Color.Orange, ForeColor = Color.White };
            this.btnDelete = new RoundedButton() { Text = "Xóa", BackColor = Color.Firebrick, ForeColor = Color.White };

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.ClientSize = new Size(762, 500);

            lblTitle.Text = "QUẢN LÝ SINH VIÊN";
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 60;
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.Padding = new Padding(20, 0, 0, 0);

            txtSearch.Location = new Point(20, 80);
            txtSearch.Size = new Size(300, 30);
            txtSearch.Font = new Font("Segoe UI", 10);
            // txtSearch.PlaceholderText = "Nhập tên hoặc mã sinh viên..."; // Bỏ comment nếu dùng .NET Core

            btnSearch.Location = new Point(330, 75);
            btnSearch.Size = new Size(100, 35);

            btnAdd.Location = new Point(450, 75);
            btnAdd.Size = new Size(90, 35);

            btnEdit.Location = new Point(550, 75);
            btnEdit.Size = new Size(90, 35);

            btnDelete.Location = new Point(650, 75);
            btnDelete.Size = new Size(90, 35);

            dgvSinhVien.Location = new Point(20, 130);
            dgvSinhVien.Size = new Size(720, 350);
            dgvSinhVien.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dgvSinhVien.BackgroundColor = Color.WhiteSmoke;
            dgvSinhVien.BorderStyle = BorderStyle.None;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSinhVien.RowTemplate.Height = 30;

            // ĐỊNH NGHĨA CỘT (Cần map đúng tên Property của DTO nếu dùng DataSource)
            dgvSinhVien.Columns.Add("MaSV", "Mã SV");
            dgvSinhVien.Columns.Add("HoTen", "Họ và Tên");
            dgvSinhVien.Columns.Add("NgaySinh", "Ngày Sinh");
            dgvSinhVien.Columns.Add("GioiTinh", "Giới Tính");
            dgvSinhVien.Columns.Add("Lop", "Lớp");
            dgvSinhVien.Columns.Add("SDT", "Số ĐT");

            this.Controls.Add(txtSearch);
            this.Controls.Add(btnSearch);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnDelete);
            this.Controls.Add(dgvSinhVien);
            this.Controls.Add(lblTitle);
        }

        private Label lblTitle;
        private DataGridView dgvSinhVien;
        private TextBox txtSearch;
        private RoundedButton btnSearch, btnAdd, btnEdit, btnDelete;
    }
}