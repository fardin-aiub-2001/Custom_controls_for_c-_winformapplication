using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    [ToolboxBitmap(typeof(Panel))]
    public class RoundPanel : Panel
    {
        private int radius = 20;
        private Color borderColor = Color.Gray;
        private int borderSize = 2;
        private Color hoverColor = Color.LightGray;
        private Color clickColor = Color.DarkGray;
        private Color backgroundColor = Color.White;

        public RoundPanel()
        {
            BackColor = backgroundColor;
            Size = new Size(200, 100);
            DoubleBuffered = true; // Reduce flickering
        }

        [Category("Appearance")]
        public int Radius
        {
            get => radius;
            set { radius = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color HoverColor
        {
            get => hoverColor;
            set { hoverColor = value; }
        }

        [Category("Appearance")]
        public Color ClickColor
        {
            get => clickColor;
            set { clickColor = value; }
        }

        [Category("Appearance")]
        public new Color BackColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                base.BackColor = Color.Transparent; // Keep default transparent
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = CreateRoundedRectanglePath(ClientRectangle, radius))
            using (Pen pen = new Pen(borderColor, borderSize))
            using (SolidBrush brush = new SolidBrush(backgroundColor))
            {
                Region = new Region(path); // Set panel clipping to rounded
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            backgroundColor = hoverColor;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            backgroundColor = BackColor;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            backgroundColor = clickColor;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            backgroundColor = hoverColor;
            Invalidate();
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int arcWidth = radius * 2;

            path.AddArc(rect.X, rect.Y, arcWidth, arcWidth, 180, 90);
            path.AddArc(rect.Right - arcWidth, rect.Y, arcWidth, arcWidth, 270, 90);
            path.AddArc(rect.Right - arcWidth, rect.Bottom - arcWidth, arcWidth, arcWidth, 0, 90);
            path.AddArc(rect.X, rect.Bottom - arcWidth, arcWidth, arcWidth, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
