using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.Json.Serialization;

namespace Draw
{
    [Serializable]
    public class SnowflakeShape : Shape
    {
        [JsonConstructor]
        public SnowflakeShape(PointF pointF, RectangleF rect) : base(rect) { }

        [JsonConstructor]
        public SnowflakeShape(RectangleF rect) : base(rect) { }

        public override bool Contains(PointF point)
        {
            using (GraphicsPath path = GetPath())
            {
                return path.IsVisible(point);
            }
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            var state = grfx.Save();

            if (TransformationMatrix != null)
            {
                grfx.MultiplyTransform(TransformationMatrix, MatrixOrder.Prepend);
            }

            float centerX = Rectangle.X + Rectangle.Width / 2;
            float centerY = Rectangle.Y + Rectangle.Height / 2;
            float radius = Math.Min(Rectangle.Width, Rectangle.Height) / 2;

            using (Pen pen = new Pen(StrokeColor))
            {
                for (int i = 0; i < 6; i++)
                {
                    float angle = i * 60;
                    DrawBranch(grfx, pen, centerX, centerY, radius, angle, false);
                }
            }

            grfx.Restore(state);
        }

        public override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();

            float centerX = Rectangle.X + Rectangle.Width / 2;
            float centerY = Rectangle.Y + Rectangle.Height / 2;
            float radius = Math.Min(Rectangle.Width, Rectangle.Height) / 2;

            for (int i = 0; i < 6; i++)
            {
                float angle = i * 60;
                DrawBranch(path, centerX, centerY, radius, angle);
            }

            if (TransformationMatrix != null)
            {
                path.Transform(TransformationMatrix);
            }

            return path;
        }


        private void DrawBranch(object target, Pen pen, float cx, float cy, float length, float angleDegrees, bool isPath = true)
        {
            double angleRad = angleDegrees * Math.PI / 180.0;
            float ex = cx + (float)(length * Math.Cos(angleRad));
            float ey = cy + (float)(length * Math.Sin(angleRad));

            if (isPath && target is GraphicsPath path)
            {
                path.AddLine(cx, cy, ex, ey);
            }
            else if (target is Graphics g)
            {
                g.DrawLine(pen, cx, cy, ex, ey);
            }

            float branchLength = length / 3f;
            float offset = length / 3f;

            for (int i = 1; i <= 2; i++)
            {
                float bx = cx + (float)(offset * i * Math.Cos(angleRad));
                float by = cy + (float)(offset * i * Math.Sin(angleRad));

                DrawBranchLine(target, pen, bx, by, branchLength, angleDegrees - 45, isPath);
                DrawBranchLine(target, pen, bx, by, branchLength, angleDegrees + 45, isPath);
            }
        }

        private void DrawBranchLine(object target, Pen pen, float bx, float by, float branchLength, float branchAngle, bool isPath)
        {
            float ex = bx + (float)(branchLength * Math.Cos(branchAngle * Math.PI / 180));
            float ey = by + (float)(branchLength * Math.Sin(branchAngle * Math.PI / 180));

            if (isPath && target is GraphicsPath path)
            {
                path.AddLine(bx, by, ex, ey);
            }
            else if (target is Graphics g)
            {
                g.DrawLine(pen, bx, by, ex, ey);
            }
        }

        private void DrawBranch(Graphics g, Pen pen, float cx, float cy, float length, float angleDegrees, bool isPath = false)
        {
            DrawBranch((object)g, pen, cx, cy, length, angleDegrees, isPath);
        }

        private void DrawBranch(GraphicsPath path, float cx, float cy, float length, float angleDegrees)
        {
            DrawBranch((object)path, null, cx, cy, length, angleDegrees, true);
        }
    }
}
