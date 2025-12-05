using System;
using System.Windows.Forms;
using StudentManagement.BLL;

namespace StudentManagement.GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                SinhVienBLL bll = new SinhVienBLL();
                var listSinhVien = bll.GetAllStudents();
                MessageBox.Show($"Kết nối thành công! Tìm thấy {listSinhVien.Count} sinh viên trong DB.", "Thông báo");

                // Load dữ liệu vào control
                // Populate navigation tree
                treeViewNav.Nodes.Clear();
                var nSinhVien = new TreeNode("Sinh Viên") { Name = "nodeSinhVien" };
                var nLop = new TreeNode("Lớp") { Name = "nodeLop" };
                var nMonHoc = new TreeNode("Môn Học") { Name = "nodeMonHoc" };
                var nThongKe = new TreeNode("Thống kê") { Name = "nodeThongKe" };

                // khoa 
                treeViewNav.Nodes.AddRange(new TreeNode[] { nSinhVien, nLop, nMonHoc, nThongKe });
                treeViewNav.ExpandAll();

                sinhVienListControl.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi Nghiêm Trọng");
            }
        }

        private void tsmiSinhVien_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Tìm đến hàm treeViewNav_AfterSelect và sửa lại:
        private void treeViewNav_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;

            // 1. Tìm xem đã có control Khoa chưa
            var kControl = this.Controls.Find("KhoaListControl", true).Length > 0
                           ? this.Controls.Find("KhoaListControl", true)[0] as UserControl
                           : null;

            // 2. Ẩn TẤT CẢ các màn hình đi trước
            sinhVienListControl.Visible = false;
            thongKeControl.Visible = false;
            if (kControl != null) kControl.Visible = false; // Ẩn luôn bảng Khoa

            // 3. Hiện màn hình tương ứng với nút được bấm
            switch (e.Node.Name)
            {
                case "nodeSinhVien":
                    sinhVienListControl.Visible = true;
                    sinhVienListControl.LoadData(); // Tải lại dữ liệu 15 sinh viên
                    break;

                case "nodeKhoa":
                    // Nếu chưa có bảng Khoa thì tạo mới
                    if (kControl == null)
                    {
                        var ctl = new KhoaListControl();
                        ctl.Name = "KhoaListControl";
                        ctl.Dock = DockStyle.Fill;
                        // Thêm vào cùng chỗ với sinhVienListControl
                        sinhVienListControl.Parent.Controls.Add(ctl);
                        ctl.BringToFront();
                        kControl = ctl;
                    }
                    // Hiện bảng Khoa
                    kControl.Visible = true;
                    kControl.BringToFront();
                    ((KhoaListControl)kControl).LoadData();
                    break;

                case "nodeThongKe":
                    thongKeControl.Visible = true;
                    thongKeControl.LoadChart();
                    break;

                default:
                    // Các nút chưa làm (Lớp, Môn học...)
                    break;
            }
        }

        private void sinhVienListControl_Load(object sender, EventArgs e)
        {

        }
    }
}
