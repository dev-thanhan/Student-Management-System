using System;
using System.Windows.Forms;
using StudentManagement.DTO; 

namespace StudentManagement.GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
            // Thiết lập vị trí Form (sẽ bị ghi đè bởi WindowState.Maximized)
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // 1. Nút "Danh sách sinh viên" (Gọi Form danh sách)
        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            var frmSV = new SinhVienForm();
            frmSV.ShowDialog();
        }

        // 2. Nút "Lọc sinh viên" (Gọi LopMonForm để lọc theo Lớp/Môn)
        private void btnLocSinhVien_Click(object sender, EventArgs e)
        {
            var frmLM = new LopMonForm(); 
            frmLM.ShowDialog();
        }

        // 3. Nút "Thống kê" (Gọi Form thống kê Pop-up)
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            var frmTK = new ThongKeForm(); 
            frmTK.ShowDialog(); 
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