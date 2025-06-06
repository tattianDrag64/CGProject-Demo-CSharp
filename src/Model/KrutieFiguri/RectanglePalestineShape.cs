using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model.KrutieFiguri
{
    [Serializable]
    public class RectanglePalestineShape : Shape
    {
        #region Constructor
        public RectanglePalestineShape() : base() { }

        public RectanglePalestineShape(RectangleF rect) : base(rect) { }

        public RectanglePalestineShape(RectangleShape rectangle) : base(rectangle) { }

        #endregion
        public override bool Contains(PointF point)
        {
            using (GraphicsPath path = GetPath())
            {
                return path.IsVisible(point);
            }
        }

        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            using (GraphicsPath path = GetPath())
            {
                using (Brush brush = new SolidBrush(FillColor))
                {
                    grfx.FillPath(brush, path);
                }

                using (Pen pen = new Pen(StrokeColor, BorderWidth))
                {
                    grfx.DrawPath(pen, path);
                }
            }
        }



        public override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();

            float left = Rectangle.X;
            float top = Rectangle.Y;
            float width = Rectangle.Width;
            float height = Rectangle.Height;

            // Вершины трапеции
            PointF topLeft = new PointF(left, top);
            PointF topRight = new PointF(left + width, top);
            PointF bottomRight = new PointF(left + width, top + height);
            PointF bottomLeft = new PointF(left, top + height);

            // Основной контур трапеции
            path.StartFigure();
            path.AddPolygon(new PointF[] { topLeft, topRight, bottomRight, bottomLeft });


            // Вертикальная линия в центре (разделение)
            PointF middle = new PointF(left + width / 2, top + height / 2);
            PointF middleLeft = new PointF(left, top + height / 2);
            path.StartFigure();
            path.AddLine(middle, topRight);
            path.AddLine(middle, bottomRight);
            path.AddLine(middle, middleLeft);
            //path.AddLine(leftP, rightP);
            //path.AddLine(middle, midddleRightDiagonal);

            return path;
        }
    }
}
