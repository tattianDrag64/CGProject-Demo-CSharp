using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.Json.Serialization;

namespace Draw.src.Model
{
    [Serializable]
    public class LineShape : Shape
    {
        [JsonConstructor]
        public LineShape(RectangleF rect) : base(rect) { }

        [JsonConstructor]
        public LineShape(RectangleShape rectangle) : base(rectangle) { }

        public override bool Contains(PointF point)
        {
            if (!base.Contains(point)) return false;

            // Преобразуем точку в локальные координаты
            PointF[] pointsToConvert = new PointF[] { point };
            var m = TransformationMatrix.Clone();
            m.Invert();
            m.TransformPoints(pointsToConvert);
            PointF p = pointsToConvert[0];

            PointF start = new PointF(Rectangle.X, Rectangle.Y);
            PointF end = new PointF(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height);

            const float tolerance = 5f; // допустимая погрешность

            using (var path = new GraphicsPath())
            {
                path.AddLine(start, end);
                using (var pen = new Pen(Color.Black, BorderWidth + tolerance))
                {
                    return path.IsOutlineVisible(p, pen);
                }
            }
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            GraphicsState state = grfx.Save();

            if (TransformationMatrix != null)
                grfx.MultiplyTransform(TransformationMatrix, MatrixOrder.Prepend);

            PointF p1 = new PointF(Rectangle.X, Rectangle.Y);
            PointF p2 = new PointF(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height);

            using (Pen pen = new Pen(StrokeColor, BorderWidth))
            {
                // Прозрачность
                int clampedOpacity = Math.Max(0, Math.Min(255, Opacity));
                Color strokeWithAlpha = Color.FromArgb(clampedOpacity, StrokeColor);
                pen.Color = strokeWithAlpha;

                // Градиент для линии (линейный или радиальный как эффект через кисть)
                if (UseGradient)
                {
                    if (UseLinearGradient)
                    {
                        using (var brush = new LinearGradientBrush(Rectangle, GradientStartColor, GradientEndColor, LinearGradientMode.ForwardDiagonal))
                        {
                            pen.Brush = brush;
                            grfx.DrawLine(pen, p1, p2);
                        }
                    }
                    else if (UseRadialGradientRadioButton)
                    {
                        using (var path = new GraphicsPath())
                        {
                            path.AddLine(p1, p2);
                            using (var brush = new PathGradientBrush(path))
                            {
                                brush.CenterColor = GradientStartColor;
                                brush.SurroundColors = new Color[] { GradientEndColor };
                                pen.Brush = brush;
                                grfx.DrawLine(pen, p1, p2);
                            }
                        }
                    }
                }
                else
                {
                    grfx.DrawLine(pen, p1, p2);
                }
            }

            // Отрисовка текста (в центре линии)
            if (!string.IsNullOrEmpty(Name))
            {
                using (Font font = new Font("Arial", 10))
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    float midX = (p1.X + p2.X) / 2;
                    float midY = (p1.Y + p2.Y) / 2;

                    SizeF textSize = grfx.MeasureString(Name, font);
                    grfx.DrawString(Name, font, brush, midX - textSize.Width / 2, midY - textSize.Height / 2);
                }
            }

            // Если выделена — пунктирный прямоугольник
            if (IsSelected)
            {
                using (Pen selectionPen = new Pen(Color.Blue, 1))
                {
                    selectionPen.DashStyle = DashStyle.Dash;
                    grfx.DrawRectangle(selectionPen, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
                }
            }

            grfx.Restore(state);
        }

        public override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(Rectangle.X, Rectangle.Y, Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height);

            if (TransformationMatrix != null)
                path.Transform(TransformationMatrix);

            return path;
        }
    }
}
