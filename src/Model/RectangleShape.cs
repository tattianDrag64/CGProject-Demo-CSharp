using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.Json.Serialization;

namespace Draw
{
    /// <summary>
    /// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
    /// </summary>
    [Serializable]

    public class RectangleShape : Shape
	{
        #region Constructor
        public RectangleShape() : base() { }

        public RectangleShape(RectangleF rect) : base(rect) { }

        public RectangleShape(RectangleShape rectangle) : base(rectangle) { }

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
			var m = TransformationMatrix.Clone();
			m.Invert();
			PointF[] points = new PointF[] { point };
			m.TransformPoints(points);

            if (base.Contains(points[0]))
				// Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
				// В случая на правоъгълник - директно връщаме true
				return true;
			else
				// Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
				return false;
		}

        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            GraphicsState state = grfx.Save();

            if (TransformationMatrix != null)
            {
                grfx.MultiplyTransform(TransformationMatrix, MatrixOrder.Prepend);
            }


            using (GraphicsPath path = GetPath())
            {
                if (UseGradient)
                {
                    if (UseLinearGradient)
                    {
                        using (var brush = new LinearGradientBrush(
                            path.GetBounds(),
                            GradientStartColor,
                            GradientEndColor,
                            LinearGradientMode.ForwardDiagonal))
                        {
                            grfx.FillPath(brush, path);
                        }
                    }
                    else if (UseRadialGradientRadioButton)
                    {
                        using (var brush = new PathGradientBrush(path))
                        {
                            brush.CenterColor = GradientStartColor;
                            brush.SurroundColors = new Color[] { GradientEndColor };
                            grfx.FillPath(brush, path);
                        }
                    }
                }
                else
                {
                    int clampedOpacity = Math.Max(0, Math.Min(255, Opacity));
                    Color colorWithAlpha = Color.FromArgb(clampedOpacity, FillColor.R, FillColor.G, FillColor.B);
                    using (var brush = new SolidBrush(colorWithAlpha))
                    {
                        grfx.FillPath(brush, path);
                    }
                }

                using (Pen pen = new Pen(StrokeColor, BorderWidth))
                {
                    grfx.DrawPath(pen, path); // ← правильная граница
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    using (Font font = new Font("Arial", 10))
                    using (Brush brush = new SolidBrush(Color.Black))
                    {
                        SizeF textSize = grfx.MeasureString(Name, font);
                        RectangleF bounds = path.GetBounds();
                        float textX = bounds.X + (bounds.Width - textSize.Width) / 2;
                        float textY = bounds.Y + (bounds.Height - textSize.Height) / 2;
                        grfx.DrawString(Name, font, brush, textX, textY);
                    }
                }
            }

            //using (Pen pen = new Pen(StrokeColor, BorderWidth))
            //{
            //    grfx.DrawRectangle(pen, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            //}

           
grfx.Restore(state);
            }

            



        public override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(Location.X, Location.Y, Width, Height));

            if (TransformationMatrix != null)
            {
                path.Transform(TransformationMatrix);
            }

            return path;
        }


    }
}
