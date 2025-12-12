using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StudentManagement.GUI.Components
{
    // Class này là STATIC (công cụ hỗ trợ), KHÔNG kế thừa từ Panel
    public static class RoundedHelper
    {
        public static void Apply(Control control, int radius)
        {
            // KIỂM TRA kích thước quá nhỏ hoặc bằng 0 thì không làm gì
            if (control.Width <= 0 || control.Height <= 0) return;
            using (GraphicsPath path = new GraphicsPath())
            {
                int diameter = radius * 2;
                if (diameter > control.Width) diameter = control.Width;
                if (diameter > control.Height) diameter = control.Height;
                Rectangle rect = new Rectangle(0, 0, control.Width, control.Height);
                path.AddArc(new Rectangle(0, 0, diameter, diameter), 180, 90);
                path.AddArc(new Rectangle(control.Width - diameter, 0, diameter, diameter), 270, 90);
                path.AddArc(new Rectangle(control.Width - diameter, control.Height - diameter, diameter, diameter), 0, 90);
                path.AddArc(new Rectangle(0, control.Height - diameter, diameter, diameter), 90, 90);
                path.CloseFigure();
                control.Region = new Region(path);
            }
        }
    }
}