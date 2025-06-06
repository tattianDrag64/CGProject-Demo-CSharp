using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    [Serializable]
    public class CrossRectangleShape : Shape
    {
        [JsonConstructor]
        public CrossRectangleShape(RectangleF rect) : base(rect)
        {
        }
        [JsonConstructor]
        public CrossRectangleShape(RectangleShape rectangle) : base(rectangle)
        {
        }
        public override bool Contains(PointF point)
        {
            return base.Contains(point) && Rectangle.Contains(point);
        }
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            using(Pen pen = new Pen(StrokeColor, BorderWidth))
            {
                grfx.FillRectangle(new SolidBrush(FillColor), Rectangle);
                grfx.DrawRectangle(pen, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
                grfx.DrawLine(pen, Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
                grfx.DrawLine(pen, Rectangle.Left, Rectangle.Bottom, Rectangle.Right, Rectangle.Top);
            }
        }

        public override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(Rectangle);
            return path;

        }

    }
}
