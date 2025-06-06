using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.Json.Serialization;
using static System.Windows.Forms.AxHost;

namespace Draw
{
    [Serializable]

    public class PointShape : Shape
    {
        #region Constructor
        private const float PointSize = 5;
        [JsonConstructor]
        public PointShape(PointF location)
        : base(new RectangleF(location.X - PointSize / 2, location.Y - PointSize / 2, PointSize, PointSize))
        {
        }
        [JsonConstructor]
        public PointShape(PointShape rectangle) : base(rectangle)
        {
        }

        #endregion


        public override bool Contains(PointF point)
        {
            var center = new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2);
            var radiusX = Rectangle.Width / 2;
            var radiusY = Rectangle.Height / 2;

            float dx = (point.X - center.X) / radiusX;
            float dy = (point.Y - center.Y) / radiusY;

            return dx * dx + dy * dy <= 1;
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            GraphicsState state = grfx.Save();

            if (TransformationMatrix != null)
            {
                grfx.MultiplyTransform(TransformationMatrix, MatrixOrder.Prepend);
            }

            int clampedOpacity = Math.Max(0, Math.Min(255, Opacity));
            Color colorWithAlpha = Color.FromArgb(clampedOpacity, FillColor.R, FillColor.G, FillColor.B);

            using (SolidBrush brush = new SolidBrush(colorWithAlpha))
            {
                grfx.FillEllipse(brush, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            }

            using (GraphicsPath path = GetPath())
            {
                if (UseGradient)
                {
                    if (UseLinearGradient)
                    {
                        using (var brush = new LinearGradientBrush(
                            path.GetBounds(),
                            GradientStartColor,
                            GradientEndColor,
                            LinearGradientMode.ForwardDiagonal))
                        {
                            grfx.FillPath(brush, path);
                        }
                    }
                    else if (UseRadialGradientRadioButton)
                    {
                        using (var brush = new PathGradientBrush(path))
                        {
                            brush.CenterColor = GradientStartColor;
                            brush.SurroundColors = new Color[] { GradientEndColor };
                            grfx.FillPath(brush, path);
                        }
                    }
                }
                else
                {
                    using (var brush = new SolidBrush(FillColor))
                    {
                        grfx.FillPath(brush, path);
                    }
                }

                using (Pen pen = new Pen(StrokeColor, BorderWidth))
                {
                    grfx.DrawEllipse(pen, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
                }

                if (!string.IsNullOrEmpty(Name))
                {
                    using (Font font = new Font("Arial", 10))
                    using (Brush brush = new SolidBrush(Color.Black))
                    {
                        SizeF textSize = grfx.MeasureString(Name, font);
                        float textX = Rectangle.X + (Rectangle.Width - textSize.Width) / 2;
                        float textY = Rectangle.Y + (Rectangle.Height - textSize.Height) / 2;
                        grfx.DrawString(Name, font, brush, textX, textY);
                    }
                }

                if (IsSelected)
                {
                    using (Pen selectionPen = new Pen(Color.Blue, 1))
                    {
                        selectionPen.DashStyle = DashStyle.Dash;
                        grfx.DrawEllipse(selectionPen, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
                    }
                }
            }

            grfx.Restore(state);
        }

        public override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new RectangleF(Location.X, Location.Y, Width, Height));

            if (TransformationMatrix != null)
            {
                path.Transform(TransformationMatrix);
            }

            return path;
        }
    }
}
