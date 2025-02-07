using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    [DefaultProperty("Text")]
    [ToolboxBitmap(typeof(TextBox))]
    public class RoundTextBoxWithWaterMark : Control
    {
        // Fields
        private int borderSize = 2;
        private int radius = 20;
        private Color borderColor = Color.Gray;
        private Color waterMarkColor = Color.Gray;
        private string waterMarkText;
        private TextBox textBox = new TextBox();
        private Color backColor = Color.White;

        public RoundTextBoxWithWaterMark()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Size = new Size(200, 50);

            textBox.Parent = this;
            textBox.BorderStyle = BorderStyle.None;
            textBox.Font = Font;
            textBox.ForeColor = ForeColor;
            textBox.BackColor = backColor;
            textBox.Location = new Point(10, 15); // Adjust Location to center vertically
            textBox.Size = new Size(Width - 20, Height - 20);
            textBox.GotFocus += RemoveWatermark;
            textBox.LostFocus += ApplyWatermark;
            textBox.Padding = new Padding(0, 10, 0, 0); // Add Padding to center the text vertically
            Controls.Add(textBox);

            ApplyWatermark(null, EventArgs.Empty);
        }

        [Category("Appearance")]
        public new Color BackColor
        {
            get { return backColor; }
            set
            {
                backColor = value;
                textBox.BackColor = backColor;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public int BorderSize
        {
            get => borderSize;
            set
            {
                borderSize = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public int Radius
        {
            get => radius;
            set
            {
                radius = value;
                Invalidate();
            }
        }

        [Category("Behavior")]
        public char PasswordChar
        {
            get => textBox.PasswordChar;
            set
            {
                textBox.PasswordChar = value;
                Invalidate();
            }
        }

        [Category("Behavior")]
        public bool UseSystemPasswordChar
        {
            get => textBox.UseSystemPasswordChar;
            set
            {
                textBox.UseSystemPasswordChar = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public string WaterMark
        {
            get => waterMarkText;
            set
            {
                waterMarkText = value;
                if (!textBox.Focused)
                {
                    ApplyWatermark(null, EventArgs.Empty); // Apply watermark when property changes
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            textBox.Size = new Size(Width - 20, Height - 20);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            textBox.Font = Font;
            Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            textBox.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (textBox.Text != Text)
            {
                textBox.Text = Text;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (GraphicsPath path = CreateRoundedRectanglePath(new Rectangle(0, 0, Width - 1, Height - 1), radius))
            using (Pen pen = new Pen(borderColor, borderSize))
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            }
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

        private void ApplyWatermark(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = waterMarkText;
                textBox.ForeColor = waterMarkColor;
            }
        }

        private void RemoveWatermark(object sender, EventArgs e)
        {
            if (textBox.Text == waterMarkText)
            {
                textBox.Text = "";
                textBox.ForeColor = ForeColor;
            }
        }
    }
}
