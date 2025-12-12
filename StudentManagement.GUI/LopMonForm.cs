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
        DataGridView dgv;
        public LopMonForm()
        {
            InitializeNewUI();
            dgv.DataError += dgv_DataError; 
        }

        private void InitializeNewUI()
        {
            this.Text = "Quản lý Sinh viên theo Lớp"; 
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // ---------- PANEL TOP (SELECTORS) ----------
            Panel topPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 30, 
                Padding = new Padding(0), 
                BackColor = Color.WhiteSmoke
            };
            this.Controls.Add(topPanel);

            Label lblLop = new Label()
            {
                Text = "Lớp:",
                AutoSize = true,
                Location = new Point(10, 8) 
            };
            topPanel.Controls.Add(lblLop);

            cboLop = new ComboBox()
            {
                Location = new Point(60, 5), 
                Size = new Size(200, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboLop.SelectedIndexChanged += cboLop_SelectedIndexChanged;
            topPanel.Controls.Add(cboLop);

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

            this.Load += LopMonForm_Load;
        }
        
        private void LopMonForm_Load(object sender, EventArgs e)
        {
            LoadLopData();
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

        private void LoadFilteredStudents()
        {
            try
            {
                string selectedMaLop = cboLop.SelectedValue?.ToString(); 
                if (selectedMaLop == "--- Tất cả Lớp ---") selectedMaLop = null;
                
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
            
            // Logic Customization
            const string GIOITINH_COL = "GioiTinh";
            const string TENKHOA_COL = "TenKhoa";

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
            
            if (dgv.Columns.Contains(TENKHOA_COL)) 
            {
                dgv.Columns[TENKHOA_COL].Visible = false;
            }
            
            if (dgv.Columns.Contains("MaSV")) dgv.Columns["MaSV"].HeaderText = "Mã SV";
            if (dgv.Columns.Contains("HoTen")) dgv.Columns["HoTen"].HeaderText = "Họ Tên";
            if (dgv.Columns.Contains("MaLop")) dgv.Columns["MaLop"].HeaderText = "Mã Lớp";
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.Columns.Contains("GioiTinh") && dgv.Columns[e.ColumnIndex].Name.Equals("GioiTinh", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value is bool gioitinhValue)
                {
                    e.Value = gioitinhValue ? "Nam" : "Nữ";
                    e.FormattingApplied = true;
                }
            }
            
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
        
        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false; 
        }
        
    }
}