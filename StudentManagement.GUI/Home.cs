using System;
using System.Windows.Forms;
using StudentManagement.GUI;

namespace StudentManagement.GUI
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            this.Load += Home_Load;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            try
            {
                LopMonForm f = new LopMonForm();
                f.Show();

                
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động: " + ex.Message, "Lỗi");
            }
        }
    }
}
