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
    public class InvertedTriangleShape : Shape
    {
        #region Constructor
        public InvertedTriangleShape() : base() { }

        public InvertedTriangleShape(RectangleF rect) : base(rect) { }

        public InvertedTriangleShape(RectangleShape rectangle) : base(rectangle) { }

        #endregion

        /// <summary>
        /// Проверка за принадлежност на точка point към правоъгълника.
        /// В случая на правоъгълник този метод може да не бъде пренаписван, защото
        /// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
        /// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
        /// елемента в този случай).
        /// </summary>
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

            PointF bottomP = new PointF(left + width/2, top + height);
            PointF topRightP = new PointF(left + width, top);
            PointF topLeftP = new PointF(left, top);

            path.StartFigure();
            path.AddPolygon(new PointF[] { topLeftP, topRightP, bottomP});


            PointF middle = new PointF(left + width / 2, top + height / 2);
            path.StartFigure();
            path.AddLine(middle, bottomP);
            path.AddLine(middle, topLeftP);
            path.AddLine(middle, topRightP);
            //path.AddLine(middle, midddleRightDiagonal);

            return path;
        }
    }
}
