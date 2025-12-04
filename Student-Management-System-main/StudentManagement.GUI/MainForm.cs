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

        private void treeViewNav_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            switch (e.Node.Name)
            {
                case "nodeSinhVien":
                    tabControl1.SelectedIndex = 0;
                    sinhVienListControl.LoadData();
                    break;
                case "nodeLop":
                    // TODO: switch to Lop tab when implemented
                    MessageBox.Show("Chưa triển khai màn hình Lớp", "Thông báo");
                    break;
                case "nodeMonHoc":
                    MessageBox.Show("Chưa triển khai màn hình Môn Học", "Thông báo");
                    break;
                case "nodeThongKe":
                    tabControl1.SelectedIndex = 1;
                    thongKeControl.LoadChart();
                    break;
                default:
                    break;
            }
        }
    }
}
