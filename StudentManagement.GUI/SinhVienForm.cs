using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using StudentManagement.BLL;
using StudentManagement.DTO;
using System.Globalization; // Cần cho định dạng số

namespace StudentManagement.GUI
{
    public partial class SinhVienForm : Form
    {
        private readonly SinhVienBLL _bll = new SinhVienBLL();

        public SinhVienForm()
        {
            InitializeComponent();
            // Đăng ký sự kiện: 
            dgvStudents.CellFormatting += dgvStudents_CellFormatting;
            dgvStudents.DataError += dgvStudents_DataError; 
        }

        private void SinhVienForm_Load(object sender, EventArgs e)
        {
            LoadData();
            CustomizeDataGridView();
        }

        private void CustomizeDataGridView()
        {
            if (dgvStudents.DataSource == null || dgvStudents.Columns.Count == 0) return;
            
            const string GIOITINH_COL = "GioiTinh";

            // 1. SỬA CỘT GIOITINH TỪ CHECKBOX SANG TEXT
            if (dgvStudents.Columns.Contains(GIOITINH_COL) && dgvStudents.Columns[GIOITINH_COL] is DataGridViewCheckBoxColumn)
            {
                var oldColumn = dgvStudents.Columns[GIOITINH_COL];
                int columnIndex = oldColumn.Index;
                string dataPropertyName = oldColumn.DataPropertyName;
                dgvStudents.Columns.Remove(GIOITINH_COL);

                var newColumn = new DataGridViewTextBoxColumn
                {
                    Name = GIOITINH_COL,
                    HeaderText = "Giới Tính",
                    DataPropertyName = dataPropertyName,
                    ReadOnly = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                dgvStudents.Columns.Insert(columnIndex, newColumn);
            }

            // 2. Đặt tên Header cho các cột quan trọng
            if (dgvStudents.Columns.Contains("MaSV")) dgvStudents.Columns["MaSV"].HeaderText = "Mã SV";
            if (dgvStudents.Columns.Contains("HoTen")) dgvStudents.Columns["HoTen"].HeaderText = "Họ Tên";
            if (dgvStudents.Columns.Contains("NgaySinh")) dgvStudents.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            if (dgvStudents.Columns.Contains("MaLop")) dgvStudents.Columns["MaLop"].HeaderText = "Mã Lớp";
            if (dgvStudents.Columns.Contains("GPA")) dgvStudents.Columns["GPA"].HeaderText = "GPA"; // Đặt tên để dễ nhận dạng
        }


        private void LoadData()
        {
            try
            {
                var list = _bll.GetAllStudents();
                dgvStudents.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi");
            }
        }

        /// <summary>
        /// Xử lý định dạng ô (cell) để chuyển đổi các giá trị boolean/enum/số/ngày
        /// </summary>
        private void dgvStudents_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Xử lý cột GioiTinh (chuyển bool sang Nam/Nữ)
            if (dgvStudents.Columns[e.ColumnIndex].Name.Equals("GioiTinh", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value is bool gioitinhValue)
                {
                    e.Value = gioitinhValue ? "Nam" : "Nữ";
                    e.FormattingApplied = true;
                }
            }
            
            // 2. Xử lý cột TrangThai (chuyển Enum sang chuỗi tiếng Việt)
            else if (dgvStudents.Columns[e.ColumnIndex].Name.Equals("TrangThai", StringComparison.OrdinalIgnoreCase))
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
            
            // 3. Xử lý cột NgaySinh (Đảm bảo định dạng ngày tháng nhất quán)
            else if (dgvStudents.Columns[e.ColumnIndex].Name.Equals("NgaySinh", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value is DateTime ngaySinhValue)
                {
                    e.Value = ngaySinhValue.ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                }
            }
            
            // 4. Xử lý cột GPA (Đảm bảo định dạng số thập phân dùng dấu chấm hoặc phẩy chính xác)
            // Lỗi System.FormatException thường xảy ra ở đây do Culture Info.
            else if (dgvStudents.Columns[e.ColumnIndex].Name.Equals("GPA", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value is decimal gpaValue)
                {
                    // Định dạng luôn dùng dấu chấm thập phân và 2 chữ số (Culture Invariant)
                    // Hoặc sử dụng định dạng hiện tại của hệ thống để tránh lỗi:
                    e.Value = gpaValue.ToString("N2", CultureInfo.CurrentCulture);
                    e.FormattingApplied = true;
                }
            }
        }
        
        /// <summary>
        /// Bắt lỗi dữ liệu (DataError) để ngăn hộp thoại lỗi System.FormatException mặc định xuất hiện
        /// </summary>
        private void dgvStudents_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false; 
        }

        // --- CÁC PHƯƠNG THỨC NÚT BẤM (GIỮ NGUYÊN) ---

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var kw = txtSearch.Text.Trim();
                List<SinhVien> list;
                if (string.IsNullOrEmpty(kw)) list = _bll.GetAllStudents();
                else list = _bll.Search(kw);
                dgvStudents.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var f = new SinhVienEditForm())
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form thêm: " + ex.Message, "Lỗi");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudents.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn sinh viên để sửa.");
                    return;
                }

                var sv = dgvStudents.CurrentRow.DataBoundItem as SinhVien;
                if (sv == null)
                {
                    MessageBox.Show("Dữ liệu chọn không hợp lệ.");
                    return;
                }

                using (var f = new SinhVienEditForm(sv))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form sửa: " + ex.Message, "Lỗi");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudents.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn sinh viên để xóa.");
                    return;
                }

                var sv = dgvStudents.CurrentRow.DataBoundItem as SinhVien;
                if (sv == null)
                {
                    MessageBox.Show("Dữ liệu chọn không hợp lệ.");
                    return;
                }

                var confirm = MessageBox.Show($"Xóa sinh viên {sv.MaSV} - {sv.HoTen}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;

                if (_bll.Delete(sv.MaSV))
                {
                    MessageBox.Show("Xóa thành công.", "Thông báo");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại.", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi");
            }
        }
        
        // --- Export CSV (Giữ nguyên) ---

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudents.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo");
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV file (*.csv)|*.csv";
                    sfd.FileName = "DanhSachSinhVien.csv";
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    var sb = new StringBuilder();

                    // Build header from visible columns
                    var visibleCols = new List<int>();
                    for (int i = 0; i < dgvStudents.Columns.Count; i++)
                    {
                        string headerText = dgvStudents.Columns[i].HeaderText;
                        
                        if (dgvStudents.Columns[i].Name.Equals("GioiTinh", StringComparison.OrdinalIgnoreCase))
                        {
                            headerText = "Giới Tính";
                        }
                        
                        if (!dgvStudents.Columns[i].Visible) continue;
                        visibleCols.Add(i);
                        sb.Append(CsvEscape(headerText));
                        if (i < dgvStudents.Columns.Count - 1) sb.Append(",");
                    }
                    sb.AppendLine();

                    // Rows
                    foreach (DataGridViewRow row in dgvStudents.Rows)
                    {
                        if (row.IsNewRow) continue;
                        bool first = true;
                        foreach (int c in visibleCols)
                        {
                            if (!first) sb.Append(",");
                            
                            var val = row.Cells[c].Value;
                            string s;
                            
                            // XỬ LÝ NỘI DUNG GIỚI TÍNH KHI EXPORT
                            if (dgvStudents.Columns[c].Name.Equals("GioiTinh", StringComparison.OrdinalIgnoreCase) && val is bool gioitinhExportValue)
                            {
                                s = gioitinhExportValue ? "Nam" : "Nữ";
                            }
                            // XỬ LÝ NỘI DUNG TRẠNG THÁI KHI EXPORT
                            else if (dgvStudents.Columns[c].Name.Equals("TrangThai", StringComparison.OrdinalIgnoreCase) && val is StudentStatus statusValue)
                            {
                                s = statusValue switch
                                {
                                    StudentStatus.NghiHoc => "Nghỉ học",
                                    StudentStatus.DangHoc => "Đang học",
                                    StudentStatus.BaoLuu => "Bảo lưu",
                                    StudentStatus.TotNghiep => "Đã tốt nghiệp",
                                    _ => statusValue.ToString()
                                };
                            }
                            else if (val == null) s = "";
                            else if (val is DateTime dt) s = dt.ToString("yyyy-MM-dd");
                            else s = val.ToString();

                            sb.Append(CsvEscape(s));
                            first = false;
                        }
                        sb.AppendLine();
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                    MessageBox.Show("Xuất CSV thành công:\n" + sfd.FileName, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất CSV: " + ex.Message, "Lỗi");
            }
        }

        private string CsvEscape(string field)
        {
            if (string.IsNullOrEmpty(field)) return "";
            var s = field.Replace("\"", "\"\"");
            bool mustQuote = s.Contains(",") || s.Contains("\"") || s.Contains("\r") || s.Contains("\n");
            return mustQuote ? $"\"{s}\"" : s;
        }
    }
}