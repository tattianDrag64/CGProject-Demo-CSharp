using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    [Serializable]
    public class SplitTrapezoidShape : Shape
    {
        #region Constructor
        public SplitTrapezoidShape() : base() { }

        public SplitTrapezoidShape(RectangleF rect) : base(rect) { }

        public SplitTrapezoidShape(RectangleShape rectangle) : base(rectangle) { }
        #endregion

        /// <summary>
        /// Построение трапеции, разделенной вертикальной линией посередине
        /// </summary>
        public override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();

            float left = Rectangle.X;
            float top = Rectangle.Y;
            float width = Rectangle.Width;
            float height = Rectangle.Height;

            float topWidth = width * 0.6f;
            float dxTop = (width - topWidth) / 2;

            // Вершины трапеции
            PointF topLeft = new PointF(left + dxTop, top);
            PointF topRight = new PointF(left + width - dxTop, top);
            PointF bottomRight = new PointF(left + width, top + height);
            PointF bottomLeft = new PointF(left, top + height);

            // Основной контур трапеции
            path.StartFigure();
            path.AddPolygon(new PointF[] { topLeft, topRight, bottomRight, bottomLeft });

            // Вертикальная линия в центре (разделение)
            PointF middle = new PointF(left + width/2, top + height/2);
            PointF middleTop = new PointF(left + width / 2, top);
            PointF middleBottom = new PointF(left + width / 2, top + height);
            PointF midddleRightDiagonal = new PointF(left + width - dxTop/2, top + height / 2);
            path.StartFigure();
            path.AddLine(middle, topLeft);
            path.AddLine(middle, middleBottom);
            path.AddLine(middle, midddleRightDiagonal);

            return path;
        }

        /// <summary>  
        /// Проверка попадания точки внутрь трапеции с использованием алгоритма Ray Casting.  
        /// </summary>  
        public override bool Contains(PointF point)
        {
            PointF[] trapezoidVertices = new PointF[]
            {
               new PointF(Rectangle.X + (Rectangle.Width - Rectangle.Width * 0.6f) / 2, Rectangle.Y), // TopLeft  
               new PointF(Rectangle.X + Rectangle.Width - (Rectangle.Width - Rectangle.Width * 0.6f) / 2, Rectangle.Y), // TopRight  
               new PointF(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height), // BottomRight  
               new PointF(Rectangle.X, Rectangle.Y + Rectangle.Height) // BottomLeft  
            };

            return IsPointInPolygon(trapezoidVertices, point);
        }

        /// <summary>
        /// Отрисовка фигуры с заливкой и границей
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
    }

}
