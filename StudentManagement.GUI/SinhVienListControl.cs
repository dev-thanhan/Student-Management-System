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

        // Xử lý lỗi hiển thị DataGridView (nếu có)
        private void dgvStudents_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
        }

        // Hàm tải dữ liệu lên lưới
        public void LoadData()
        {
            try
            {
                var list = _bll.GetAllStudents();

                // Chuyển đổi sang danh sách hiển thị (thêm cột Khoa và GPA)
                var displayList = new List<dynamic>();
                foreach (var sv in list)
                {
                    displayList.Add(new
                    {
                        Mã_SV = sv.MaSV,
                        Họ_Tên = sv.HoTen,
                        Ngày_Sinh = sv.NgaySinh,
                        Giới_Tính = sv.GioiTinh ? "Nam" : "Nữ",
                        Địa_Chỉ = sv.DiaChi,
                        SĐT = sv.SoDienThoai,
                        Email = sv.Email,
                        Lớp = sv.MaLop,

                        // --- THÊM 2 CỘT MỚI Ở ĐÂY ---
                        Khoa = sv.TenKhoa,
                        GPA = sv.GPA,
                        // ----------------------------

                        Trạng_Thái = sv.TrangThai
                    });
                }

                dgvStudents.DataSource = displayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi");
            }
        }

        // Sự kiện tìm kiếm
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var kw = txtSearch.Text.Trim();
            try
            {
                List<SinhVien> list;
                if (string.IsNullOrEmpty(kw)) list = _bll.GetAllStudents();
                else list = _bll.Search(kw);

                // Cũng phải cập nhật chỗ hiển thị tìm kiếm cho đồng bộ
                var displayList = new List<dynamic>();
                foreach (var sv in list)
                {
                    displayList.Add(new
                    {
                        Mã_SV = sv.MaSV,
                        Họ_Tên = sv.HoTen,
                        Ngày_Sinh = sv.NgaySinh,
                        Giới_Tính = sv.GioiTinh ? "Nam" : "Nữ",
                        Địa_Chỉ = sv.DiaChi,
                        SĐT = sv.SoDienThoai,
                        Email = sv.Email,
                        Lớp = sv.MaLop,

                        // --- THÊM 2 CỘT MỚI Ở ĐÂY ---
                        Khoa = sv.TenKhoa,
                        GPA = sv.GPA,
                        // ----------------------------

                        Trạng_Thái = sv.TrangThai
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
            // Kiểm tra xem có dòng nào được chọn không
            if (dgvStudents.CurrentRow == null) return;

            // Lưu ý: Cần lấy đúng tên cột (property name trong anonymous object ở trên)
            // Vì ở trên mình đặt tên là "Mã_SV" nên ở đây phải lấy theo "Mã_SV"
            string maSV = dgvStudents.CurrentRow.Cells["Mã_SV"].Value.ToString();

            // Gọi BLL để lấy thông tin sinh viên đầy đủ từ Database
            SinhVien sv = _bll.GetById(maSV);

            if (sv == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên này!", "Lỗi");
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null) return;

            // Lấy MaSV và HoTen từ cột tương ứng
            string maSV = dgvStudents.CurrentRow.Cells["Mã_SV"].Value.ToString();
            string hoTen = dgvStudents.CurrentRow.Cells["Họ_Tên"].Value.ToString();

            var confirm = MessageBox.Show($"Xóa sinh viên {maSV} - {hoTen}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (_bll.Delete(maSV))
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