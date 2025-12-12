using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace StudentManagement.GUI.Components
{
    public class CircularPictureBox : PictureBox
    {
        private int _borderSize = 2;

        [Category("Appearance")]
        [DefaultValue(2)] 
        public int BorderSize
        {
            get { return _borderSize; }
            set { _borderSize = value; this.Invalidate(); }
        }

        private Color _borderColor = Color.RoyalBlue;

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "RoyalBlue")] 
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; this.Invalidate(); }
        }

        private Color _borderColor2 = Color.HotPink;

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "HotPink")]
        public Color BorderColor2
        {
            get { return _borderColor2; }
            set { _borderColor2 = value; this.Invalidate(); }
        }

        private DashStyle _borderLineStyle = DashStyle.Solid;

        [Category("Appearance")]
        [DefaultValue(DashStyle.Solid)] 
        public DashStyle BorderLineStyle
        {
            get { return _borderLineStyle; }
            set { _borderLineStyle = value; this.Invalidate(); }
        }

        private bool _borderGradient = false;

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool BorderGradient
        {
            get { return _borderGradient; }
            set { _borderGradient = value; this.Invalidate(); }
        }

        public CircularPictureBox()
        {
            this.Size = new Size(100, 100);
            this.SizeMode = PictureBoxSizeMode.StretchImage;

            _borderSize = 2;
            _borderColor = Color.RoyalBlue;
            _borderColor2 = Color.HotPink;
            _borderLineStyle = DashStyle.Solid;
            _borderGradient = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new Size(this.Width, this.Width);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            var graph = pe.Graphics;
            var rectContourSmooth = Rectangle.Inflate(this.ClientRectangle, -1, -1);
            var rectBorder = Rectangle.Inflate(rectContourSmooth, -_borderSize, -_borderSize);
            var smoothSize = _borderSize > 0 ? _borderSize * 3 : 1;

            using (var borderGColor = new LinearGradientBrush(rectBorder, _borderColor, _borderColor2, 50f))
            using (var pathRegion = new GraphicsPath())
            using (var penSmooth = new Pen(this.Parent.BackColor, smoothSize))
            using (var penBorder = new Pen(borderGColor, _borderSize))
            {
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                penBorder.DashStyle = _borderLineStyle;

                pathRegion.AddEllipse(rectContourSmooth);
                this.Region = new Region(pathRegion);

                graph.DrawEllipse(penSmooth, rectContourSmooth);

                if (_borderSize > 0)
                {
                    if (_borderGradient)
                        graph.DrawEllipse(penBorder, rectBorder);
                    else
                    {
                        using (var solidPen = new Pen(_borderColor, _borderSize))
                        {
                            solidPen.DashStyle = _borderLineStyle;
                            graph.DrawEllipse(solidPen, rectBorder);
                        }
                    }
                }
            }
        }
    }
}