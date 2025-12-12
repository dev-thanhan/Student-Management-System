using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StudentManagement.GUI.Components
{
    public class RoundedPanel : Panel
    {
        private int _radius = 30;

        [Category("Appearance")]
        [Description("Radius of corners")]
        [DefaultValue(30)]
        public int BorderRadius
        {
            get { return _radius; }
            set { _radius = value; this.Invalidate(); }
        }

        private Color _backgroundColor = Color.White;

        [Category("Appearance")]
        [Description("Background color")]
        [DefaultValue(typeof(Color), "White")]
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; this.Invalidate(); }
        }

        public RoundedPanel()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent; // Nền trong suốt
            _radius = 30;
            _backgroundColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            {
                int d = _radius * 2;
                Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
                path.AddArc(r.X, r.Y, d, d, 180, 90);
                path.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
                path.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
                path.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
                path.CloseFigure();

                using (SolidBrush brush = new SolidBrush(_backgroundColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }
    }
}