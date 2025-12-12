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
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            var frmSV = new SinhVienForm();
            frmSV.ShowDialog();
        }

        private void btnLocSinhVien_Click(object sender, EventArgs e)
        {
            var frmLM = new LopMonForm(); 
            frmLM.ShowDialog();
        }

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