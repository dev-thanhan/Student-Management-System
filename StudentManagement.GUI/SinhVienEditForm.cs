using System;
using System.Windows.Forms;
using StudentManagement.BLL;
using StudentManagement.DTO;
using StudentManagement.DAL; // Thêm dòng này để dùng DbHelper

namespace StudentManagement.GUI
{
    public partial class SinhVienEditForm : Form
    {
        private readonly SinhVienBLL _sinhVienBll = new SinhVienBLL();
        private readonly LopBLL _lopBll = new LopBLL();
        private readonly bool _isEdit;

        public SinhVienEditForm()
        {
            InitializeComponent();
            _isEdit = false;

            // Tải danh sách lớp
            LoadMaLopComboBox();

            // SỰ KIỆN: Khi chọn lớp -> Tự động hiện tên Khoa tương ứng
            this.cboMaLop.SelectedIndexChanged += (s, e) =>
            {
                if (cboMaLop.SelectedValue != null)
                {
                    txtTenKhoa.Text = GetTenKhoaByMaLop(cboMaLop.SelectedValue.ToString());
                }
            };
        }

        // Constructor dùng khi bấm nút "Sửa"
        public SinhVienEditForm(SinhVien sv) : this()
        {
            if (sv != null)
            {
                _isEdit = true;
                // Đổ dữ liệu lên các ô
                txtMaSV.Text = sv.MaSV;
                txtMaSV.ReadOnly = true; // Không cho sửa Mã SV
                txtHoTen.Text = sv.HoTen;
                dtpNgaySinh.Value = sv.NgaySinh;
                cboGioiTinh.SelectedIndex = sv.GioiTinh ? 0 : 1; // 0:Nam, 1:Nữ
                txtDiaChi.Text = sv.DiaChi;
                txtSDT.Text = sv.SoDienThoai;
                txtEmail.Text = sv.Email;
                cboMaLop.SelectedValue = sv.MaLop;
                cboTrangThai.SelectedIndex = (int)sv.TrangThai;

                // Hiển thị Khoa và GPA
                txtTenKhoa.Text = sv.TenKhoa;
                txtGPA.Text = sv.GPA.ToString("N2"); // Hiện 2 số lẻ (VD: 8.50)
            }
        }

        private void LoadMaLopComboBox()
        {
            try
            {
                var lopList = _lopBll.GetAll();
                cboMaLop.DataSource = lopList;
                cboMaLop.DisplayMember = "MaLop";
                cboMaLop.ValueMember = "MaLop";

                // Mặc định chọn lớp đầu tiên và hiện tên khoa luôn
                if (lopList.Count > 0)
                {
                    cboMaLop.SelectedIndex = 0;
                    if (cboMaLop.SelectedValue != null)
                        txtTenKhoa.Text = GetTenKhoaByMaLop(cboMaLop.SelectedValue.ToString());
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải lớp: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra nhập liệu
                if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập Mã SV và Họ Tên!", "Thiếu thông tin"); return;
                }

                // 2. Tạo đối tượng sinh viên
                var sv = new SinhVien
                {
                    MaSV = txtMaSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cboGioiTinh.SelectedIndex == 0,
                    DiaChi = txtDiaChi.Text.Trim(),
                    SoDienThoai = txtSDT.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    MaLop = cboMaLop.SelectedValue?.ToString() ?? "",
                    TrangThai = (StudentStatus)cboTrangThai.SelectedIndex
                    // GPA không lưu ở đây vì GPA được tính từ bảng Điểm
                };

                // 3. Gọi hàm Lưu
                bool ok;
                if (_isEdit) ok = _sinhVienBll.Update(sv);
                else ok = _sinhVienBll.Insert(sv);

                if (ok)
                {
                    MessageBox.Show("Lưu thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lưu thất bại! (Có thể Mã SV bị trùng hoặc lỗi kết nối)", "Lỗi");
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hệ thống: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // HÀM LẤY TÊN KHOA TỪ CSDL
        private string GetTenKhoaByMaLop(string maLop)
        {
            if (string.IsNullOrEmpty(maLop)) return "";
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    // Join bảng Lop -> Nganh -> Khoa
                    string sql = @"SELECT k.TenKhoa FROM Lop l 
                                   JOIN Nganh n ON l.MaNganh = n.MaNganh 
                                   JOIN Khoa k ON n.MaKhoa = k.MaKhoa 
                                   WHERE l.MaLop = @MaLop";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                        var res = cmd.ExecuteScalar();
                        return res != null ? res.ToString() : "";
                    }
                }
            }
            catch { return ""; }
        }
    }
}