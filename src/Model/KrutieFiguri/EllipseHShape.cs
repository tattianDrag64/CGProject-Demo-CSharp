using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Draw.src.Model.KrutieFiguri
{
    public class EllipseHShape : Shape
    {

        #region Constructor
        [JsonConstructor]
        public EllipseHShape(RectangleF rect) : base(rect)
        {
        }
        [JsonConstructor]
        public EllipseHShape(EllipseShape rectangle) : base(rectangle)
        {
        }

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
            float a = Width / 2;
            float b = Height / 2;
            float xc = Location.X + a;
            float yc = Location.Y + b;

            // Клонируем матрицу, чтобы не портить исходную
            using (Matrix invertedMatrix = TransformationMatrix.Clone())
            {
                invertedMatrix.Invert();

                PointF[] pointsToConvert = new PointF[] { point };
                invertedMatrix.TransformPoints(pointsToConvert);

                PointF pointNew = pointsToConvert[0];

                // Эллиптическая формула
                return Math.Pow((pointNew.X - xc) / a, 2) + Math.Pow((pointNew.Y - yc) / b, 2) <= 1;
            }
        }


        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>


        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            if (TransformationMatrix != null)
            {
                grfx.MultiplyTransform(TransformationMatrix, MatrixOrder.Prepend);
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(
                Rectangle,
                Color.White,
                FillColor,
                LinearGradientMode.ForwardDiagonal))
            {
                grfx.FillEllipse(brush, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            }
            grfx.DrawEllipse(new Pen(StrokeColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            float centerX = Rectangle.X + Rectangle.Width / 2;
            float centerY = Rectangle.Y + Rectangle.Height / 2;
            float radius = Math.Min(Rectangle.Width, Rectangle.Height) / 2;
            using (Pen pen = new Pen(StrokeColor, BorderWidth))
            {
                //grfx.DrawLine(pen, centerX, Rectangle.Top, centerX, Rectangle.Bottom);
                grfx.DrawLine(pen, Rectangle.Left, centerY, Rectangle.Right, centerY);
                grfx.DrawLine(pen, Rectangle.X, Rectangle.Top, Rectangle.X, Rectangle.Bottom);
                grfx.DrawLine(pen, Rectangle.Right, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);

                //float left = Rectangle.X;
                //float top = Rectangle.Y;
                //float width = Rectangle.Width;
                //float height = Rectangle.Height;

                //PointF topLeft = new PointF(left, top);
                //PointF topRight = new PointF(left + width, top);
                //PointF bottomRight = new PointF(left + width, top + height);
                //PointF bottomLeft = new PointF(left, top + height);
            }


            //using (Pen pen = new Pen(StrokeColor, BorderWidth))
            //{
            //    grfx.DrawRectangle(pen, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            //}
        }

        public override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new RectangleF(Location.X, Location.Y, Width, Height));
            if (TransformationMatrix != null)
            {
                path.Transform(TransformationMatrix);
            }
            return path;
        }

    }
}
