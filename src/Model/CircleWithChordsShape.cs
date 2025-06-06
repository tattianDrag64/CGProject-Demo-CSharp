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
    public class CircleWithChordsShape : Shape
    {
        [JsonConstructor]
        public CircleWithChordsShape(RectangleF rect) : base(rect)
        {
        }
        [JsonConstructor]
        public CircleWithChordsShape(RectangleShape rectangle) : base(rectangle)
        {
        }
        public override bool Contains(PointF point)
        {
            return base.Contains(point) && Rectangle.Contains(point);
        }
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            using (Pen pen = new Pen(StrokeColor, BorderWidth))
            {
                grfx.FillEllipse(new SolidBrush(FillColor), Rectangle);
                grfx.DrawEllipse(pen, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

                float centerX = Rectangle.X + Rectangle.Width / 2;
                float centerY = Rectangle.Y + Rectangle.Height / 2;
                float radius = Math.Min(Rectangle.Width, Rectangle.Height) / 2;

                grfx.DrawLine(pen, centerX, Rectangle.Top, centerX, Rectangle.Bottom);
                grfx.DrawLine(pen, Rectangle.Left, centerY, Rectangle.Right, centerY);

                float offsetX = Rectangle.Width * 0.25f;
                grfx.DrawLine(pen, new PointF(centerX - offsetX, Rectangle.Top + 12), new PointF(centerX - offsetX, Rectangle.Bottom - 12)); // левая
                grfx.DrawLine(pen, new PointF(centerX + offsetX, Rectangle.Top + 12), new PointF(centerX + offsetX, Rectangle.Bottom - 12)); // правая

                //float offsetX = Rectangle.Width / 4;
                //float leftX = Rectangle.Left + offsetX;
                //float rightX = Rectangle.Right - offsetX;

                //grfx.DrawLine(pen, new PointF(leftX, Rectangle.Top), new PointF(leftX, Rectangle.Bottom));   // левая хорда
                //grfx.DrawLine(pen, new PointF(rightX, Rectangle.Top), new PointF(rightX, Rectangle.Bottom)); // правая хорда;
                ////grfx.DrawLine(pen, Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
                ////grfx.DrawLine(pen, Rectangle.Left, Rectangle.Bottom, Rectangle.Right, Rectangle.Top);
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
