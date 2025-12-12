using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StudentManagement.GUI.Components
{
    public class RoundedButton : Button
    {
        private int _radius = 20;
        private bool _isHovered = false;
        [Category("Appearance")]
        [Description("Độ bo tròn của nút")]
        [DefaultValue(20)]
        public int BorderRadius
        {
            get { return _radius; }
            set { _radius = value; this.Invalidate(); }
        }
        private Color _hoverColor = Color.MediumPurple;
        [Category("Appearance")]
        [Description("Màu nền khi di chuột vào")]
        [DefaultValue(typeof(Color), "MediumPurple")]
        public Color HoverColor
        {
            get { return _hoverColor; }
            set { _hoverColor = value; this.Invalidate(); }
        }

        public RoundedButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.White;
            this.Cursor = Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            using (GraphicsPath path = new GraphicsPath())
            {
                int d = _radius * 2;

                path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                path.AddArc(rect.X + rect.Width - d, rect.Y, d, d, 270, 90);
                path.AddArc(rect.X + rect.Width - d, rect.Y + rect.Height - d, d, d, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - d, d, d, 90, 90);
                path.CloseFigure();

                this.Region = new Region(path);

                Color paintColor = _isHovered ? _hoverColor : this.BackColor;

                using (SolidBrush brush = new SolidBrush(paintColor))
                {
                    pevent.Graphics.FillPath(brush, path);
                }

                TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, rect, this.ForeColor,
                                      TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
    }
}