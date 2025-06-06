using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Draw
{
    [Serializable]
    public class StarShape : Shape
    {
        #region Constructor
        [JsonConstructor]
        public StarShape(RectangleF rect) : base(rect)
        {
        }
        [JsonConstructor]
        public StarShape(EllipseShape rectangle) : base(rectangle)
        {
        }
        #endregion

        public override bool Contains(PointF point)
        {
            var m = TransformationMatrix.Clone();
            m.Invert();
            PointF[] points = new PointF[] { point };
            m.TransformPoints(points);
            point = points[0];

            PointF[] starPoints = CalculateStarPoints(5,
                Rectangle.X + Rectangle.Width / 2,
                Rectangle.Y + Rectangle.Height / 2,
                Math.Min(Rectangle.Width, Rectangle.Height) / 2,
                Math.Min(Rectangle.Width, Rectangle.Height) / 4);

            int intersections = 0;
            for (int i = 0; i < starPoints.Length; i++)
            {
                PointF p1 = starPoints[i];
                PointF p2 = starPoints[(i + 1) % starPoints.Length];

                if ((p1.Y > point.Y) != (p2.Y > point.Y) &&
                    point.X < (p2.X - p1.X) * (point.Y - p1.Y) / (p2.Y - p1.Y) + p1.X)
                {
                    intersections++;
                }
            }

            return intersections % 2 != 0;
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            var state = grfx.Save();
            grfx.Transform = TransformationMatrix;

            float centerX = Rectangle.X + Rectangle.Width / 2;
            float centerY = Rectangle.Y + Rectangle.Height / 2;
            float outerRadius = Math.Min(Rectangle.Width, Rectangle.Height) / 2;
            float innerRadius = outerRadius * 0.5f;

            PointF[] starPoints = CalculateStarPoints(5, centerX, centerY, outerRadius, innerRadius);
            using (Pen pen = new Pen(StrokeColor, 1.5f))
            {
                grfx.DrawPolygon(pen, starPoints);
            } 
            using (Pen linePen = new Pen(StrokeColor, 1.0f))
            {
                for (int i = 0; i < starPoints.Length; i++)
                {
                    grfx.DrawLine(linePen, starPoints[i], new PointF(centerX, centerY));
                }
            }

            if (!string.IsNullOrEmpty(Name))
            {
                using (Font font = new Font("Arial", 10))
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    SizeF textSize = grfx.MeasureString(Name, font);
                    float textX = Rectangle.X + (Rectangle.Width - textSize.Width) / 2;
                    float textY = Rectangle.Y + (Rectangle.Height - textSize.Height)+10;
                    grfx.DrawString(Name, font, brush, textX, textY);
                }
            }

            grfx.Restore(state);
        }

        public override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();

            float centerX = Rectangle.X + Rectangle.Width / 2;
            float centerY = Rectangle.Y + Rectangle.Height / 2;
            float outerRadius = Math.Min(Rectangle.Width, Rectangle.Height) / 2;
            float innerRadius = outerRadius * 0.5f;

            PointF[] starPoints = CalculateStarPoints(5, centerX, centerY, outerRadius, innerRadius);

            path.AddPolygon(starPoints);
            return path;
        }

        private PointF[] CalculateStarPoints(int numPoints, float centerX, float centerY, float outerRadius, float innerRadius)
        {
            PointF[] points = new PointF[numPoints * 2];
            double angle = -Math.PI / 2; 

            double step = Math.PI / numPoints;

            for (int i = 0; i < numPoints * 2; i++)
            {
                float r = (i % 2 == 0) ? outerRadius : innerRadius;
                points[i] = new PointF(
                    centerX + (float)(r * Math.Cos(angle)),
                    centerY + (float)(r * Math.Sin(angle))
                );
                angle += step;
            }

            return points;
        }


    }
}
