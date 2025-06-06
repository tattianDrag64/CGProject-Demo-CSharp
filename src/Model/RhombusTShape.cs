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
    public class RhombusTShape : Shape
    {
        #region Constructor
        public RhombusTShape() : base() { }

        public RhombusTShape(RectangleF rect) : base(rect) { }

        public RhombusTShape(RectangleShape rectangle) : base(rectangle) { }

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

            PointF topP = new PointF(left + width / 2, top);
            PointF bottomP = new PointF(left + width / 2, top + height);
            PointF leftP = new PointF(left, top + height / 2);
            PointF rightP = new PointF(left + width, top + height / 2);


            // Основной контур трапеции
            path.StartFigure();
            path.AddPolygon(new PointF[] { topP, rightP, bottomP, leftP });

            // Вертикальная линия в центре (разделение)
            PointF middle = new PointF(left + width / 2, top + height / 2);
            path.StartFigure();
            path.AddLine(middle, topP);
            path.AddLine(leftP, rightP);
            //path.AddLine(middle, midddleRightDiagonal);

            return path;
        }

    }
}