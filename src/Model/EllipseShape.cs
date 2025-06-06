using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.Json.Serialization;

namespace Draw
{
    /// <summary>
    /// Класът елипса е основен примитив, който е наследник на базовия Shape.
    /// </summary>
    [Serializable]
    public class EllipseShape : Shape
    {
        #region Constructor
        [JsonConstructor]
        public EllipseShape() : base() { }
        [JsonConstructor]
        public EllipseShape(RectangleF rect) : base(rect) { }
        [JsonConstructor]
        public EllipseShape(EllipseShape ellipse) : base(ellipse) { }

        #endregion

        /// <summary>
        /// Проверка за принадлежност на точка point към елипсата.
        /// </summary>
        public override bool Contains(PointF point)
        {
            var m = TransformationMatrix.Clone();
            m.Invert();
            PointF[] points = new PointF[] { point };
            m.TransformPoints(points);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(Location.X, Location.Y, Width, Height);
                return path.IsVisible(points[0]);
            }
        }

        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            GraphicsState state = grfx.Save();

            if (TransformationMatrix != null)
            {
                grfx.MultiplyTransform(TransformationMatrix, MatrixOrder.Prepend);
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
                    int clampedOpacity = Math.Max(0, Math.Min(255, Opacity));
                    Color colorWithAlpha = Color.FromArgb(clampedOpacity, FillColor.R, FillColor.G, FillColor.B);
                    using (var brush = new SolidBrush(colorWithAlpha))
                    {
                        grfx.FillPath(brush, path);
                    }
                }

                using (Pen pen = new Pen(StrokeColor, BorderWidth))
                {
                    grfx.DrawPath(pen, path);
                }

                if (!string.IsNullOrEmpty(Name))
                {
                    using (Font font = new Font("Arial", 10))
                    using (Brush brush = new SolidBrush(Color.Black))
                    {
                        SizeF textSize = grfx.MeasureString(Name, font);
                        RectangleF bounds = path.GetBounds();
                        float textX = bounds.X + (bounds.Width - textSize.Width) / 2;
                        float textY = bounds.Y + (bounds.Height - textSize.Height) / 2;
                        grfx.DrawString(Name, font, brush, textX, textY);
                    }
                }
            }

            grfx.Restore(state);
        }

        /// <summary>
        /// Връща пътя на елипсата, използван за рендериране.
        /// </summary>
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
