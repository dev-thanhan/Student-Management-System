using System;
using System.Windows.Forms;
// Đảm bảo có using cho DTO nếu cần tham chiếu
using StudentManagement.DTO; 

namespace StudentManagement.GUI
{
    public partial class MainForm : Form
    {
        // ❌ ĐÃ LOẠI BỎ: Biến _thongKeControl (vì ta dùng ThongKeForm Pop-up)

        public MainForm()
        {
            InitializeComponent();
            LoadTreeView();
            
            // ❌ ĐÃ LOẠI BỎ: InitializeThongKeControl()
        }

        private void LoadTreeView()
        {
            var root = new TreeNode("Quản lý");

            root.Nodes.Add(new TreeNode("Sinh viên"));
            root.Nodes.Add(new TreeNode("Lớp - Môn"));

            treeViewNav.Nodes.Add(root);

            treeViewNav.Nodes.Add(new TreeNode("Thống kê"));

            treeViewNav.ExpandAll();
        }
        
        // ❌ ĐÃ LOẠI BỎ: Hàm InitializeThongKeControl()

        private void treeViewNav_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Kiểm tra Node được chọn có phải là lá (leaf) không
            if (e.Node.Nodes.Count > 0) return;

            switch (e.Node.Text)
            {
                case "Sinh viên":
                    var frmSV = new SinhVienForm();
                    frmSV.ShowDialog();
                    break;

                case "Lớp - Môn":
                    var frmLM = new LopMonForm();
                    frmLM.ShowDialog();
                    break;

                case "Thống kê":
                    // SỬA: Chuyển sang gọi Form Pop-up Thống kê
                    var frmTK = new ThongKeForm(); 
                    frmTK.ShowDialog(); 
                    break;
            }
        }

        private void tsmiSinhVien_Click(object sender, EventArgs e)
            {
            var frm = new SinhVienForm();
            frm.ShowDialog();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}