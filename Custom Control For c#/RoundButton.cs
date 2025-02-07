using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    [ToolboxBitmap(typeof(Button))]
    public class RoundButton : Button
    {
        private int radius = 20;
        private Color borderColor = Color.Gray;
        private int borderSize = 2;
        private Color hoverColor = Color.LightGray;
        private Color clickColor = Color.DarkGray;
        private Color backgroundColor = Color.White;

        public RoundButton()
        {
            FlatStyle = FlatStyle.Flat;
            BackColor = backgroundColor;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Arial", 10, FontStyle.Bold);
            Size = new Size(100, 40);
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
                base.BackColor = Color.Transparent; // Make default BackColor transparent
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
                Region = new Region(path); // Clip the button to rounded shape
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            }

            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
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

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            backgroundColor = clickColor;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
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
