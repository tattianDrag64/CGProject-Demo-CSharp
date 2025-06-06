using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.Json.Serialization;
using static System.Windows.Forms.AxHost;

namespace Draw
{
    [Serializable]
    public class GroupShape : Shape
    {
        public List<Shape> SubShapes = new List<Shape>();
        [JsonConstructor]
        public GroupShape(RectangleF rect) : base(rect) { }
        [JsonConstructor]
        public GroupShape(GroupShape group) : base(group)
        {
            foreach (Shape shape in group.SubShapes)
            {
                SubShapes.Add((Shape)shape.Clone());
            }
        }

        public override bool Contains(PointF point)
        {
            foreach (var shape in SubShapes)
            {
                Matrix combined = this.TransformationMatrix.Clone();
                combined.Multiply(shape.TransformationMatrix, MatrixOrder.Append);
                Matrix inverse = combined.Clone();
                inverse.Invert();
                PointF[] pts = new PointF[] { point };
                inverse.TransformPoints(pts);

                if (shape.Contains(pts[0]))
                    return true;
            }

            return false;
        }

        public override void DrawSelf(Graphics grfx)
        {
            GraphicsState state = grfx.Save();
            grfx.MultiplyTransform(this.TransformationMatrix, MatrixOrder.Prepend);
            base.DrawSelf(grfx);

            using (SolidBrush brush = new SolidBrush(FillColor))
                grfx.FillRectangle(brush, Rectangle);

            using (Pen pen = new Pen(Color.Black, 2))
                grfx.DrawRectangle(pen, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

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

        public override PointF Location
        {
            get { return base.Location; }
            set
            {
                foreach (Shape shape in SubShapes)
                {
                    shape.Location = new PointF(
                        shape.Location.X + value.X - base.Location.X,
                        shape.Location.Y + value.Y - base.Location.Y);
                }
                base.Location = value;
            }
        }

        public override object Clone()
        {
            GroupShape clone = new GroupShape(this);
            clone.SubShapes = new List<Shape>();
            foreach (Shape shape in this.SubShapes)
            {
                clone.SubShapes.Add((Shape)shape.Clone());
            }
            return clone;
        }

        public override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();

            foreach (var shape in SubShapes)
            {
                var subPath = shape.GetPath();
                if (subPath != null)
                {
                    path.AddPath(subPath, false);
                }
            }

            return path;
        }
    }
}
