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
    [Serializable]
    public class EllipseXShape : Shape
    {

        #region Constructor
        [JsonConstructor]
        public EllipseXShape(RectangleF rect) : base(rect)
        {
        }
        [JsonConstructor]
        public EllipseXShape(EllipseShape rectangle) : base(rectangle)
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

            // Заливка круга
            using (LinearGradientBrush brush = new LinearGradientBrush(
                Rectangle,
                Color.White,
                FillColor,
                LinearGradientMode.ForwardDiagonal))
            {
                grfx.FillEllipse(brush, Rectangle);
            }

            using (Pen pen = new Pen(StrokeColor, BorderWidth))
            {
                // Контур круга
                grfx.DrawEllipse(pen, Rectangle);

                // Центр и радиусы
                float centerX = Rectangle.X + Rectangle.Width / 2;
                float centerY = Rectangle.Y + Rectangle.Height / 2;
                float radiusX = Rectangle.Width / 2;
                float radiusY = Rectangle.Height / 2;

                // Смещение от центра по нормали к диагонали (для параллельных хорд)
                float offsetFactor = 0.5f; // можешь менять для регулировки расстояния между хордами

                // Угол наклона диагонали (45°)
                float angle = (float)(Math.PI / 4); // 45 градусов в радианах

                // Вектор направления диагонали
                float dx = (float)Math.Cos(angle);
                float dy = (float)Math.Sin(angle);

                // Нормаль (перпендикуляр) к диагонали
                float nx = dy;
                float ny = -dx;

                // Смещение вверх и вниз от центра по нормали
                float offsetX = nx * radiusX * offsetFactor;
                float offsetY = ny * radiusY * offsetFactor;

                // Параллельные хорды: считаем точки на окружности
                PointF[] chord1 = GetChordPoints(centerX + offsetX, centerY + offsetY, dx, dy, radiusX, radiusY);
                PointF[] chord2 = GetChordPoints(centerX - offsetX, centerY - offsetY, dx, dy, radiusX, radiusY);

                grfx.DrawLine(pen, chord1[0], chord1[1]);
                grfx.DrawLine(pen, chord2[0], chord2[1]);

                //// Первая хорда (↘)
                //PointF p1 = new PointF(centerX - radiusX / (float)Math.Sqrt(2), centerY - radiusY / (float)Math.Sqrt(2));
                //PointF p2 = new PointF(centerX + radiusX / (float)Math.Sqrt(2), centerY + radiusY / (float)Math.Sqrt(2));

                //// Вторая хорда (↗)
                //PointF p3 = new PointF(centerX - radiusX / (float)Math.Sqrt(2), centerY + radiusY / (float)Math.Sqrt(2));
                //PointF p4 = new PointF(centerX + radiusX / (float)Math.Sqrt(2), centerY - radiusY / (float)Math.Sqrt(2));

                //grfx.DrawLine(pen, p1, p2);
                //grfx.DrawLine(pen, p3, p4);
            }
        }

        // Вспомогательный метод для нахождения концов хорды
        private PointF[] GetChordPoints(float cx, float cy, float dx, float dy, float rx, float ry)
        {
            // Находим t, при котором точка лежит на эллипсе: (x/rx)^2 + (y/ry)^2 = 1
            float scale = 1 / (float)Math.Sqrt(
                (dx * dx) / (rx * rx) + (dy * dy) / (ry * ry)
            );

            float x1 = cx - dx * scale;
            float y1 = cy - dy * scale;
            float x2 = cx + dx * scale;
            float y2 = cy + dy * scale;

            return new PointF[] { new PointF(x1+10, y1 + 10), new PointF(x2-10, y2- 10) };
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
