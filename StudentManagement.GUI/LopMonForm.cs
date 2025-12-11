using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using StudentManagement.BLL;
using StudentManagement.DTO;
using System.Linq;

namespace StudentManagement.GUI
{
    public partial class LopMonForm : Form
    {
        // Khai báo các tầng xử lý nghiệp vụ
        private readonly LopBLL _lopBLL = new LopBLL();
        private readonly SinhVienBLL _sinhVienBLL = new SinhVienBLL();

        // Khai báo các Control giao diện
        ComboBox cboLop;
        DataGridView dgv;
        Button btnExport;
        Button btnImport;

        public LopMonForm()
        {
            // Gọi hàm Designer (giờ đã sạch lỗi)
            InitializeComponent();
            // Gọi hàm tự tạo giao diện của chúng ta
            InitializeNewUI();
        }

        // --- HÀM TẠO GIAO DIỆN (CODE TAY) ---
        private void InitializeNewUI()
        {
            this.Text = "Quản lý Sinh viên theo Lớp";
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // 1. Panel trên cùng (Chứa ComboBox chọn lớp)
            Panel topPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10),
                BackColor = Color.WhiteSmoke
            };
            this.Controls.Add(topPanel);

            Label lblLop = new Label()
            {
                Text = "Chọn Lớp:",
                AutoSize = true,
                Location = new Point(20, 18)
            };
            topPanel.Controls.Add(lblLop);

            cboLop = new ComboBox()
            {
                Location = new Point(100, 15),
                Size = new Size(250, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            // Gán sự kiện khi người dùng chọn lớp khác
            cboLop.SelectedIndexChanged += cboLop_SelectedIndexChanged;
            topPanel.Controls.Add(cboLop);

            // 2. Bảng dữ liệu (DataGridView)
            dgv = new DataGridView()
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BorderStyle = BorderStyle.Fixed3D,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            // Gán sự kiện để định dạng hiển thị (Nam/Nữ,...)
            dgv.CellFormatting += dgv_CellFormatting;
            dgv.DataError += dgv_DataError;
            this.Controls.Add(dgv);

            // 3. Panel dưới cùng (Chứa nút bấm)
            Panel bottomPanel = new Panel()
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                Padding = new Padding(10),
                BackColor = Color.WhiteSmoke
            };
            this.Controls.Add(bottomPanel);

            btnExport = new Button()
            {
                Text = "Xuất Excel",
                Size = new Size(120, 35),
                Location = new Point(10, 10),
                BackColor = Color.White
            };
            btnExport.Click += btnExportExcel_Click;
            bottomPanel.Controls.Add(btnExport);

            btnImport = new Button()
            {
                Text = "Nhập Excel",
                Size = new Size(120, 35),
                Location = new Point(140, 10),
                BackColor = Color.White
            };
            btnImport.Click += btnImportExcel_Click;
            bottomPanel.Controls.Add(btnImport);

            // Gán sự kiện Load Form (QUAN TRỌNG: Tên hàm là LopMonForm_Load)
            this.Load += LopMonForm_Load;
        }

        // ================== LOGIC TẢI DỮ LIỆU ==================

        // Hàm này chạy khi form bắt đầu hiện lên
        private void LopMonForm_Load(object sender, EventArgs e)
        {
            LoadLopData();
        }

        private void LoadLopData()
        {
            try
            {
                // Lấy danh sách lớp từ CSDL
                var lopList = _lopBLL.GetAllLops().Cast<Lop>().ToList();

                cboLop.DataSource = lopList;
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";

                // Mặc định chọn lớp đầu tiên nếu có
                if (lopList.Count > 0)
                {
                    cboLop.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách Lớp: " + ex.Message);
            }
        }

        private void LoadFilteredStudents()
        {
            try
            {
                if (cboLop.SelectedValue == null) return;
                string selectedMaLop = cboLop.SelectedValue.ToString();

                // Lấy sinh viên thuộc lớp đã chọn bằng SinhVienBLL
                var filteredList = _sinhVienBLL.GetStudentsByClass(selectedMaLop);

                dgv.DataSource = filteredList;
                CustomizeDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc sinh viên: " + ex.Message, "Lỗi");
            }
        }

        private void CustomizeDataGridView()
        {
            if (dgv.DataSource == null) return;

            // Ẩn cột Mã Lớp (vì đã hiển thị trên ComboBox rồi)
            if (dgv.Columns.Contains("MaLop")) dgv.Columns["MaLop"].Visible = false;

            // Đặt tên cột tiếng Việt cho đẹp
            if (dgv.Columns.Contains("MaSV")) dgv.Columns["MaSV"].HeaderText = "Mã SV";
            if (dgv.Columns.Contains("HoTen")) dgv.Columns["HoTen"].HeaderText = "Họ và Tên";
            if (dgv.Columns.Contains("NgaySinh")) dgv.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            if (dgv.Columns.Contains("GioiTinh")) dgv.Columns["GioiTinh"].HeaderText = "Giới Tính";
            if (dgv.Columns.Contains("TenKhoa")) dgv.Columns["TenKhoa"].HeaderText = "Khoa";
            if (dgv.Columns.Contains("GPA")) dgv.Columns["GPA"].HeaderText = "Điểm TB";
            if (dgv.Columns.Contains("TrangThai")) dgv.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }

        // ================== XỬ LÝ SỰ KIỆN & ĐỊNH DẠNG ==================

        private void cboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFilteredStudents();
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Giới tính: true -> Nam, false -> Nữ
            if (dgv.Columns[e.ColumnIndex].Name == "GioiTinh" && e.Value is bool gender)
            {
                e.Value = gender ? "Nam" : "Nữ";
                e.FormattingApplied = true;
            }

            // 2. Trạng thái: Enum -> Tiếng Việt
            if (dgv.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value is StudentStatus status)
            {
                switch (status)
                {
                    case StudentStatus.NghiHoc: e.Value = "Nghỉ học"; break;
                    case StudentStatus.DangHoc: e.Value = "Đang học"; break;
                    case StudentStatus.BaoLuu: e.Value = "Bảo lưu"; break;
                    case StudentStatus.TotNghiep: e.Value = "Đã tốt nghiệp"; break;
                    default: e.Value = status.ToString(); break;
                }
                e.FormattingApplied = true;
            }

            // 3. GPA: Hiển thị 2 số lẻ (VD: 8.50)
            if (dgv.Columns[e.ColumnIndex].Name == "GPA" && e.Value != null)
            {
                e.Value = string.Format("{0:N2}", e.Value);
                e.FormattingApplied = true;
            }
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false; // Bỏ qua lỗi hiển thị nếu có
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Xuất Excel sẽ được triển khai sau.");
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Nhập Excel sẽ được triển khai sau.");
        }
    }
}