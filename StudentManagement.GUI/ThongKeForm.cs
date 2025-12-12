using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement.GUI
{
    public partial class ThongKeForm : Form
    {
        private ThongKeControl thongKeControl;

        public ThongKeForm()
        {
            InitializeComponent();
            this.Text = "Thống kê Sinh viên theo Lớp";
            this.Size = new Size(1600, 900);  
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimizeBox = true;
            this.MaximizeBox = true;
            
            thongKeControl = new ThongKeControl();
            thongKeControl.Dock = DockStyle.Fill;
            this.Controls.Add(thongKeControl);
            this.Load += ThongKeForm_Load;
        }

        private void ThongKeForm_Load(object sender, EventArgs e)
        {
            thongKeControl.LoadChart();
        }
    }
}