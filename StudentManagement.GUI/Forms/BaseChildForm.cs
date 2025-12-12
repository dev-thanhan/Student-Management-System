using System.Windows.Forms;
using System.Drawing;

namespace StudentManagement.GUI.Forms
{
    public class BaseChildForm : Form
    {
        public BaseChildForm()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}