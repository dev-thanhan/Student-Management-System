using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StudentManagement.BLL;
using StudentManagement.DTO;

namespace StudentManagement.GUI
{
    public partial class SinhVienListControl : UserControl
    {
        private readonly SinhVienBLL _bll = new SinhVienBLL();

        public SinhVienListControl()
        {
            InitializeComponent();
        }

        private void dgvStudents_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Suppress the default DataGridView error dialog and show a friendly message
            e.ThrowException = false;
            e.Cancel = true;
            // Optional: log or show minimal message
            MessageBox.Show("Lỗi dữ liệu trong bảng. Vui lòng kiểm tra lại dữ liệu.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void LoadData()
        {
            try
            {
                var list = _bll.GetAllStudents();
                
                // Convert to display-friendly format
                var displayList = new List<dynamic>();
                foreach (var sv in list)
                {
                    displayList.Add(new
                    {
                        sv.MaSV,
                        sv.HoTen,
                        sv.NgaySinh,
                        GioiTinh = sv.GioiTinh ? "Nam" : "Nữ",
                        sv.DiaChi,
                        sv.SoDienThoai,
                        sv.Email,
                        sv.MaLop,
                        sv.TrangThai
                    });
                }
                
                dgvStudents.DataSource = displayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var kw = txtSearch.Text.Trim();
            try
            {
                List<SinhVien> list;
                if (string.IsNullOrEmpty(kw)) list = _bll.GetAllStudents();
                else list = _bll.Search(kw);
                
                // Convert to display-friendly format
                var displayList = new List<dynamic>();
                foreach (var sv in list)
                {
                    displayList.Add(new
                    {
                        sv.MaSV,
                        sv.HoTen,
                        sv.NgaySinh,
                        GioiTinh = sv.GioiTinh ? "Nam" : "Nữ",
                        sv.DiaChi,
                        sv.SoDienThoai,
                        sv.Email,
                        sv.MaLop,
                        sv.TrangThai
                    });
                }
                
                dgvStudents.DataSource = displayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var f = new SinhVienEditForm())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null) return;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null) return;
            var sv = dgvStudents.CurrentRow.DataBoundItem as SinhVien;
            if (sv == null) return;

            var confirm = MessageBox.Show($"Xóa sinh viên {sv.MaSV} - {sv.HoTen}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (_bll.Delete(sv.MaSV))
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công", "Lỗi");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi");
                }
            }
        }
    }
}
