using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement.GUI.Forms.System
{
    public partial class frmThongKe : BaseChildForm
    {
        public frmThongKe()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.ClientSize = new Size(762, 500);

            Label lbl = new Label();
            lbl.Text = "BÁO CÁO THỐNG KÊ\n(Chức năng đang phát triển)";
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font("Segoe UI", 20, FontStyle.Italic);
            lbl.ForeColor = Color.Gray;

            this.Controls.Add(lbl);
        }
    }
}