using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using StudentManagement.BLL;
using StudentManagement.DTO;
using System.Globalization;
using System.Data; // Cần cho DataTable, DataRow
using ExcelDataReader; // Cần cho thư viện đọc Excel

namespace StudentManagement.GUI
{
    public partial class SinhVienForm : Form
    {
        private readonly SinhVienBLL _bll = new SinhVienBLL();

        public SinhVienForm()
        {
            InitializeComponent();
            
            // Đăng ký sự kiện định dạng lưới
            dgvStudents.CellFormatting += dgvStudents_CellFormatting;
            dgvStudents.DataError += dgvStudents_DataError;
            
            // Đăng ký sự kiện cho nút Import (Nếu chưa gán trong Design thì bỏ comment dòng dưới)
            // btnImport.Click += btnImport_Click;
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
            if (dgvStudents.Columns.Contains("GPA")) dgvStudents.Columns["GPA"].HeaderText = "GPA";
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

        // ================================================================
        // XỬ LÝ FORMATTING & ERROR
        // ================================================================

        private void dgvStudents_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvStudents.Columns[e.ColumnIndex].Name.Equals("GioiTinh", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value is bool gioitinhValue)
                {
                    e.Value = gioitinhValue ? "Nam" : "Nữ";
                    e.FormattingApplied = true;
                }
            }
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
            else if (dgvStudents.Columns[e.ColumnIndex].Name.Equals("NgaySinh", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value is DateTime ngaySinhValue)
                {
                    e.Value = ngaySinhValue.ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                }
            }
            else if (dgvStudents.Columns[e.ColumnIndex].Name.Equals("GPA", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value is decimal gpaValue)
                {
                    e.Value = gpaValue.ToString("N2", CultureInfo.CurrentCulture);
                    e.FormattingApplied = true;
                }
            }
        }
        
        private void dgvStudents_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false; 
        }

        // ================================================================
        // CÁC CHỨC NĂNG CHÍNH (THÊM, SỬA, XÓA, TÌM KIẾM, REFRESH)
        // ================================================================

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
                if (sv == null) return;

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
                if (sv == null) return;

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

        // ================================================================
        // TÍNH NĂNG IMPORT TỪ EXCEL (MỚI THÊM)
        // ================================================================

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx;*.xls", ValidateNames = true })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        // Hiển thị cursor chờ
                        Cursor.Current = Cursors.WaitCursor;
                        
                        var importedCount = ImportFromExcel(ofd.FileName);
                        
                        Cursor.Current = Cursors.Default;

                        if (importedCount > 0)
                        {
                            MessageBox.Show($"Đã import thành công {importedCount} sinh viên!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData(); 
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu nào được import. Có thể file rỗng hoặc tất cả mã SV đã tồn tại/bị lỗi.", "Thông báo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Lỗi khi import: " + ex.Message, "Lỗi");
            }
        }

        private int ImportFromExcel(string filePath)
        {
            int countSuccess = 0;
            int countError = 0;
            StringBuilder errorLog = new StringBuilder(); // Dùng để ghi lại lỗi chi tiết

            // Đăng ký encoding
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    if (result.Tables.Count == 0) return 0;
                    DataTable table = result.Tables[0];

                    foreach (DataRow row in table.Rows)
                    {
                        string currentMaSV = "";
                        try
                        {
                            // 1. Kiểm tra Mã SV rỗng
                            if (row["MaSV"] == DBNull.Value || string.IsNullOrWhiteSpace(row["MaSV"].ToString()))
                                continue;

                            currentMaSV = row["MaSV"].ToString().Trim();

                            // Kiểm tra trùng lặp trước khi thêm (Optional)
                            // Nếu Mã SV này đã có trong list hiện tại -> Báo lỗi trùng
                            // (Logic này tùy thuộc vào việc bạn muốn Update hay Báo lỗi)

                            SinhVien sv = new SinhVien();
                            sv.MaSV = currentMaSV;

                            // 2. Map dữ liệu
                            sv.HoTen = table.Columns.Contains("HoTen") ? row["HoTen"].ToString() : "";
                            sv.DiaChi = table.Columns.Contains("DiaChi") ? row["DiaChi"].ToString() : "";
                            sv.Email = table.Columns.Contains("Email") ? row["Email"].ToString() : "";
                            sv.MaLop = table.Columns.Contains("MaLop") ? row["MaLop"].ToString() : "";

                            // --- XỬ LÝ SỐ ĐIỆN THOẠI (FIX LỖI MẤT SỐ 0) ---
                            if (table.Columns.Contains("SoDienThoai"))
                            {
                                string sdt = row["SoDienThoai"].ToString().Trim();
                                // Nếu Excel lưu dạng số (ví dụ 909123...), tự động thêm số 0 vào đầu
                                if (!string.IsNullOrEmpty(sdt) && !sdt.StartsWith("0"))
                                {
                                    sdt = "0" + sdt;
                                }
                                sv.SoDienThoai = sdt;
                            }
                            else sv.SoDienThoai = "";

                            // --- XỬ LÝ NGÀY SINH ---
                            sv.NgaySinh = DateTime.Now;
                            if (table.Columns.Contains("NgaySinh") && row["NgaySinh"] != DBNull.Value)
                            {
                                if (row["NgaySinh"] is DateTime dt)
                                {
                                    sv.NgaySinh = dt;
                                }
                                else
                                {
                                    // Thử parse nhiều định dạng
                                    string dateStr = row["NgaySinh"].ToString();
                                    if (DateTime.TryParse(dateStr, out DateTime parsedDate))
                                        sv.NgaySinh = parsedDate;
                                    // Thử parse định dạng yyyy-MM-dd nếu TryParse thường thất bại
                                    else if (DateTime.TryParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                                        sv.NgaySinh = parsedDate;
                                }
                            }

                            // --- XỬ LÝ GIỚI TÍNH ---
                            sv.GioiTinh = true; // Default Nam
                            if (table.Columns.Contains("GioiTinh") && row["GioiTinh"] != DBNull.Value)
                            {
                                string gtStr = row["GioiTinh"].ToString().ToLower().Trim();
                                if (gtStr == "nữ" || gtStr == "nu" || gtStr == "0" || gtStr == "false")
                                    sv.GioiTinh = false;
                            }

                            // --- XỬ LÝ TRẠNG THÁI ---
                            sv.TrangThai = StudentStatus.DangHoc;
                            if (table.Columns.Contains("TrangThai") && row["TrangThai"] != DBNull.Value)
                            {
                                string statusStr = row["TrangThai"].ToString().ToLower();
                                if (statusStr.Contains("nghỉ")) sv.TrangThai = StudentStatus.NghiHoc;
                                else if (statusStr.Contains("bảo lưu")) sv.TrangThai = StudentStatus.BaoLuu;
                                else if (statusStr.Contains("tốt nghiệp")) sv.TrangThai = StudentStatus.TotNghiep;
                            }

                            // 3. Gọi INSERT
                            if (_bll.Insert(sv))
                            {
                                countSuccess++;
                            }
                            else
                            {
                                countError++;
                                errorLog.AppendLine($"- SV {currentMaSV}: Lỗi Insert vào CSDL (Có thể trùng mã).");
                            }
                        }
                        catch (Exception ex)
                        {
                            countError++;
                            // Ghi lại lý do lỗi cụ thể để hiện lên MessageBox
                            errorLog.AppendLine($"- SV {currentMaSV}: {ex.Message}");
                        }
                    }
                }
            }

            // HIỆN THÔNG BÁO LỖI CHI TIẾT
            if (countError > 0)
            {
                MessageBox.Show($"Thành công: {countSuccess}\nThất bại: {countError}\n\nChi tiết lỗi:\n{errorLog.ToString()}", 
                                "Kết quả Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return countSuccess;
        }

        // ================================================================
        // TÍNH NĂNG EXPORT CSV (GIỮ NGUYÊN)
        // ================================================================

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

                    // Header
                    var visibleCols = new List<int>();
                    for (int i = 0; i < dgvStudents.Columns.Count; i++)
                    {
                        string headerText = dgvStudents.Columns[i].HeaderText;
                        if (dgvStudents.Columns[i].Name.Equals("GioiTinh", StringComparison.OrdinalIgnoreCase))
                            headerText = "Giới Tính";
                        
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
                            
                            if (dgvStudents.Columns[c].Name.Equals("GioiTinh", StringComparison.OrdinalIgnoreCase) && val is bool gioitinhExportValue)
                            {
                                s = gioitinhExportValue ? "Nam" : "Nữ";
                            }
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