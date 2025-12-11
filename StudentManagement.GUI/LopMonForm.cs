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
        private readonly LopBLL _lopBLL = new LopBLL();
        private readonly MonHocBLL _monHocBLL = new MonHocBLL();
        private readonly HocPhanBLL _hocPhanBLL = new HocPhanBLL(); 

        // Declare Controls
        ComboBox cboLop;
        // Đã loại bỏ ComboBox cboMon
        DataGridView dgv;
        Button btnExport;
        Button btnImport;

        public LopMonForm()
        {
            InitializeNewUI();
            dgv.DataError += dgv_DataError; 
        }

        private void InitializeNewUI()
        {
            this.Text = "Quản lý Sinh viên theo Lớp"; // Đổi tên form
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // ---------- PANEL TOP (SELECTORS) ----------
            Panel topPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 70, // Giảm chiều cao panel
                Padding = new Padding(10),
                BackColor = Color.WhiteSmoke
            };
            this.Controls.Add(topPanel);

            Label lblLop = new Label()
            {
                Text = "Lớp:",
                AutoSize = true,
                Location = new Point(10, 10)
            };
            topPanel.Controls.Add(lblLop);

            cboLop = new ComboBox()
            {
                Location = new Point(60, 7),
                Size = new Size(200, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboLop.SelectedIndexChanged += cboLop_SelectedIndexChanged;
            topPanel.Controls.Add(cboLop);

            // Đã loại bỏ Label và ComboBox cho Môn học

            // ---------- GRID VIEW ----------
            dgv = new DataGridView()
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, 
                BorderStyle = BorderStyle.Fixed3D,
                AllowUserToAddRows = false,
                ReadOnly = true
            };
            dgv.CellFormatting += dgv_CellFormatting; 
            this.Controls.Add(dgv);

            // ---------- BOTTOM TOOL BAR ----------
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
                Location = new Point(10, 10)
            };
            btnExport.Click += btnExportExcel_Click;
            bottomPanel.Controls.Add(btnExport);

            btnImport = new Button()
            {
                Text = "Nhập Excel",
                Size = new Size(120, 35),
                Location = new Point(140, 10)
            };
            btnImport.Click += btnImportExcel_Click;
            bottomPanel.Controls.Add(btnImport);

            this.Load += LopMonForm_Load;
        }

        // ================== LOGIC TẢI DỮ LIỆU BAN ĐẦU ==================

        private void LopMonForm_Load(object sender, EventArgs e)
        {
            LoadLopData();
            // Đã loại bỏ LoadMonHocData()
            LoadFilteredStudents(); 
        }

        private void LoadLopData()
        {
            try
            {
                var lopList = _lopBLL.GetAllLops().Cast<Lop>().ToList(); 
                lopList.Insert(0, new Lop { MaLop = null, TenLop = "--- Tất cả Lớp ---" }); 
                
                cboLop.DataSource = lopList;
                cboLop.DisplayMember = "TenLop"; 
                cboLop.ValueMember = "MaLop"; 
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách Lớp: " + ex.Message);
            }
        }

        // Đã loại bỏ LoadMonHocData()
        
        // ================== LOGIC LỌC VÀ HIỂN THỊ DỮ LIỆU ==================

        private void LoadFilteredStudents()
        {
            try
            {
                string selectedMaLop = cboLop.SelectedValue?.ToString(); 
                // Đã loại bỏ selectedMaMon

                // SỬA: Chỉ truyền maLop
                var filteredList = _hocPhanBLL.GetStudentsByLop(selectedMaLop);
                
                dgv.DataSource = filteredList;
                
                CustomizeDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc sinh viên: " + ex.Message, "Lỗi"); 
                dgv.DataSource = null;
            }
        }

        private void CustomizeDataGridView()
        {
            if (dgv.DataSource == null || dgv.Columns.Count == 0) return;
            
            const string GIOITINH_COL = "GioiTinh";
            const string TENKHOA_COL = "TenKhoa";

            // 1. SỬA CỘT GIOITINH TỪ CHECKBOX SANG TEXT
            if (dgv.Columns.Contains(GIOITINH_COL) && dgv.Columns[GIOITINH_COL] is DataGridViewCheckBoxColumn)
            {
                var oldColumn = dgv.Columns[GIOITINH_COL];
                int columnIndex = oldColumn.Index;
                string dataPropertyName = oldColumn.DataPropertyName;
                dgv.Columns.Remove(GIOITINH_COL);

                var newColumn = new DataGridViewTextBoxColumn
                {
                    Name = GIOITINH_COL,
                    HeaderText = "Giới Tính",
                    DataPropertyName = dataPropertyName,
                    ReadOnly = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                dgv.Columns.Insert(columnIndex, newColumn);
            }
            
            // 2. ẨN CỘT TenKhoa (vẫn cần ẩn vì nó không được SELECT từ DB nữa)
            if (dgv.Columns.Contains(TENKHOA_COL)) 
            {
                dgv.Columns[TENKHOA_COL].Visible = false;
            }
            
            // 3. Đặt tên Header cho các cột quan trọng
            if (dgv.Columns.Contains("MaSV")) dgv.Columns["MaSV"].HeaderText = "Mã SV";
            if (dgv.Columns.Contains("HoTen")) dgv.Columns["HoTen"].HeaderText = "Họ Tên";
            if (dgv.Columns.Contains("MaLop")) dgv.Columns["MaLop"].HeaderText = "Mã Lớp";
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Xử lý cột GioiTinh (chuyển bool sang Nam/Nữ)
            if (dgv.Columns.Contains("GioiTinh") && dgv.Columns[e.ColumnIndex].Name.Equals("GioiTinh", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value is bool gioitinhValue)
                {
                    e.Value = gioitinhValue ? "Nam" : "Nữ";
                    e.FormattingApplied = true;
                }
            }
            
            // Xử lý cột TrangThai (chuyển Enum sang chuỗi tiếng Việt)
            if (dgv.Columns.Contains("TrangThai") && dgv.Columns[e.ColumnIndex].Name.Equals("TrangThai", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value is StudentStatus statusValue)
                {
                    switch (statusValue)
                    {
                        case StudentStatus.NghiHoc: e.Value = "Nghỉ học"; break;
                        case StudentStatus.DangHoc: e.Value = "Đang học"; break;
                        case StudentStatus.BaoLuu: e.Value = "Bảo lưu"; break;
                        case StudentStatus.TotNghiep: e.Value = "Đã tốt nghiệp"; break;
                        default: e.Value = statusValue.ToString(); break;
                    }
                    e.FormattingApplied = true;
                }
            }
        }

        // ================== EVENT HANDLERS ==================

        private void cboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLop.SelectedValue != null) 
            {
                LoadFilteredStudents();
            }
        }

        // Đã loại bỏ cboMon_SelectedIndexChanged()
        
        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false; 
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
             MessageBox.Show("Chức năng Xuất Excel sẽ được triển khai tại đây.");
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Nhập Excel sẽ được triển khai tại đây.");
        }
    }
}