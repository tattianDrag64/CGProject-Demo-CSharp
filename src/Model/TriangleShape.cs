using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.Json.Serialization;

namespace Draw
{
    [Serializable]
    public class TriangleShape : Shape
    {
        [JsonConstructor]
        public TriangleShape(RectangleF rect) : base(rect) { }

        [JsonConstructor]
        public TriangleShape(RectangleShape rectangle) : base(rectangle) { }

        private PointF[] GetTrianglePoints()
        {
            return new PointF[]
            {
                new PointF(Location.X + Width / 2, Location.Y),
                new PointF(Location.X, Location.Y + Height),
                new PointF(Location.X + Width, Location.Y + Height)
            };
        }

        public override bool Contains(PointF point)
        {
            if (!base.Contains(point)) return false;

            PointF[] pointsToConvert = new PointF[] { point };
            var m = TransformationMatrix.Clone();
            m.Invert();
            m.TransformPoints(pointsToConvert);
            PointF pointBase = pointsToConvert[0];

            PointF[] trianglePoints = GetTrianglePoints();

            int intersectCount = 0;
            int vertexCount = trianglePoints.Length;
            for (int i = 0; i < vertexCount; i++)
            {
                PointF v1 = trianglePoints[i];
                PointF v2 = trianglePoints[(i + 1) % vertexCount];

                if (pointBase.Y > Math.Min(v1.Y, v2.Y) && pointBase.Y <= Math.Max(v1.Y, v2.Y) &&
                    pointBase.X <= Math.Max(v1.X, v2.X) && v1.Y != v2.Y)
                {
                    float xIntersection = (pointBase.Y - v1.Y) * (v2.X - v1.X) / (v2.Y - v1.Y) + v1.X;
                    if (v1.X == v2.X || pointBase.X <= xIntersection)
                        intersectCount++;
                }
            }

            return (intersectCount % 2 == 1);
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            GraphicsState state = grfx.Save();

            if (TransformationMatrix != null)
                grfx.MultiplyTransform(TransformationMatrix, MatrixOrder.Prepend);

            PointF[] trianglePoints = GetTrianglePoints();

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(trianglePoints);

                int clampedOpacity = Math.Max(0, Math.Min(255, Opacity));

                if (UseGradient)
                {
                    if (UseLinearGradient)
                    {
                        using (var brush = new LinearGradientBrush(
                            path.GetBounds(),
                            Color.FromArgb(clampedOpacity, GradientStartColor),
                            Color.FromArgb(clampedOpacity, GradientEndColor),
                            LinearGradientMode.ForwardDiagonal))
                        {
                            grfx.FillPath(brush, path);
                        }
                    }
                    else if (UseRadialGradientRadioButton)
                    {
                        using (var brush = new PathGradientBrush(path))
                        {
                            brush.CenterColor = Color.FromArgb(clampedOpacity, GradientStartColor);
                            brush.SurroundColors = new Color[] { Color.FromArgb(clampedOpacity, GradientEndColor) };
                            grfx.FillPath(brush, path);
                        }
                    }
                }
                else
                {
                    using (var brush = new SolidBrush(Color.FromArgb(clampedOpacity, FillColor)))
                    {
                        grfx.FillPath(brush, path);
                    }
                }

                using (Pen pen = new Pen(Color.FromArgb(clampedOpacity, StrokeColor), BorderWidth))
                {
                    grfx.DrawPolygon(pen, trianglePoints);
                }

                if (!string.IsNullOrEmpty(Name))
                {
                    using (Font font = new Font("Arial", 10))
                    using (Brush textBrush = new SolidBrush(Color.Black))
                    {
                        SizeF textSize = grfx.MeasureString(Name, font);
                        float textX = Location.X + (Width - textSize.Width) / 2;
                        float textY = Location.Y + (Height - textSize.Height) / 2;
                        grfx.DrawString(Name, font, textBrush, textX, textY);
                    }
                }

                if (IsSelected)
                {
                    using (Pen selectionPen = new Pen(Color.Blue, 1))
                    {
                        selectionPen.DashStyle = DashStyle.Dash;
                        grfx.DrawPolygon(selectionPen, trianglePoints);
                    }
                }
            }

            grfx.Restore(state);
        }

        public override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(GetTrianglePoints());

            if (TransformationMatrix != null)
            {
                path.Transform(TransformationMatrix);
            }

            return path;
        }
    }
}
