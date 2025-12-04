using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StudentManagement.BLL;
using StudentManagement.DTO;

namespace StudentManagement.GUI
{
    public partial class SinhVienEditForm : Form
    {
        private readonly SinhVienBLL _sinhVienBll = new SinhVienBLL();
        private readonly LopBLL _lopBll = new LopBLL();
        private readonly bool _isEdit;
        private SinhVien _entity;

        public SinhVienEditForm()
        {
            InitializeComponent();
            _isEdit = false;
            SetupLabels();
            LoadMaLopComboBox();
        }

        private void LoadMaLopComboBox()
        {
            try
            {
                var lopList = _lopBll.GetAll();
                cboMaLop.DataSource = lopList;
                cboMaLop.DisplayMember = "MaLop";
                cboMaLop.ValueMember = "MaLop";
                if (lopList.Count > 0) cboMaLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp: " + ex.Message, "Lỗi");
            }
        }

        private void SetupLabels()
        {
            lblMaSV.Text = "Mã SV:";
            lblHoTen.Text = "Họ Tên:";
            lblNgaySinh.Text = "Ngày Sinh:";
            lblGioiTinh.Text = "Giới Tính:";
            lblDiaChi.Text = "Địa Chỉ:";
            lblSDT.Text = "Số Điện Thoại:";
            lblEmail.Text = "Email:";
            lblMaLop.Text = "Mã Lớp:";
            lblTrangThai.Text = "Trạng Thái:";
            btnSave.Text = "Lưu";
            btnCancel.Text = "Hủy";
            this.Text = "Thêm / Sửa Sinh Viên";
        }

        public SinhVienEditForm(SinhVien sv) : this()
        {
            if (sv != null)
            {
                _isEdit = true;
                _entity = sv;
                LoadEntity(sv);
                txtMaSV.ReadOnly = true;
            }
        }

        private void LoadEntity(SinhVien sv)
        {
            txtMaSV.Text = sv.MaSV;
            txtHoTen.Text = sv.HoTen;
            dtpNgaySinh.Value = sv.NgaySinh;
            cboGioiTinh.SelectedIndex = sv.GioiTinh ? 0 : 1;
            txtDiaChi.Text = sv.DiaChi;
            txtSDT.Text = sv.SoDienThoai;
            txtEmail.Text = sv.Email;
            cboMaLop.SelectedValue = sv.MaLop;
            cboTrangThai.SelectedIndex = (int)sv.TrangThai;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var sv = new SinhVien
                {
                    MaSV = txtMaSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cboGioiTinh.SelectedIndex == 0,
                    DiaChi = txtDiaChi.Text.Trim(),
                    SoDienThoai = txtSDT.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    MaLop = cboMaLop.SelectedValue.ToString(),
                    TrangThai = (StudentStatus)cboTrangThai.SelectedIndex
                };

                bool ok;
                if (_isEdit)
                {
                    ok = _sinhVienBll.Update(sv);
                }
                else
                {
                    ok = _sinhVienBll.Insert(sv);
                }

                if (ok)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lưu thất bại", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
