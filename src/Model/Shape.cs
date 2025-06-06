
using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace Draw
{
	[Serializable]
	/// <summary>
	/// Базовия клас на примитивите, който съдържа общите характеристики на примитивите.
	/// </summary>
	public abstract class Shape
	{
        public Shape()
        {
            transformationMatrix = new Matrix(); // Обязательно, иначе будет null при десериализации
        }
        #region Constructors
        public bool IsSelected { get; set; } = false;
        public virtual string Type => this.GetType().Name;

        public abstract GraphicsPath GetPath();
        public Shape(RectangleF rect)
		{
			rectangle = rect;
		}
      
        public Shape(Shape shape)
		{
			this.Height = shape.Height;
			this.Width = shape.Width;
			this.Location = shape.Location;
			this.rectangle = shape.rectangle;

			this.FillColor = shape.FillColor;
		}
		#endregion

		#region Properties

		/// <summary>
		/// Обхващащ правоъгълник на елемента.
		/// </summary>
		private RectangleF rectangle;
		public virtual RectangleF Rectangle
		{
			get { return rectangle; }
			set { rectangle = value; }
		}

		/// <summary>
		/// Широчина на елемента.
		/// </summary>
		public virtual float Width
		{
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}

		/// <summary>
		/// Височина на елемента.
		/// </summary>
		public virtual float Height
		{
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}

		/// <summary>
		/// Горен ляв ъгъл на елемента.
		/// </summary>
		public virtual PointF Location
		{
			get { return Rectangle.Location; }
			set { rectangle.Location = value; }
		}

		/// <summary>
		/// Цвят на елемента.
		/// </summary>
		private Color fillColor;
		public virtual Color FillColor
		{
			get { return fillColor; }
			set { fillColor = value; }
		}
        [NonSerialized]
        private Matrix transformationMatrix = new Matrix();
        public Matrix TransformationMatrix
        {
            get
            {
                if (transformationMatrix == null)
                    transformationMatrix = new Matrix(); // ← безопасная инициализация

                return transformationMatrix;
            }
            set
            {
                transformationMatrix = value; // ← исправлено: обращение к полю, а не к свойству
            }
        }

        public bool UseGradient { get; set; } = false;
        public bool UseLinearGradient { get; set; } = true;
		public bool UseRadialGradientRadioButton { get; set; } = true;
        public Color GradientStartColor { get; set; } = Color.White;
        public Color GradientEndColor { get; set; } = Color.Black;


        public SerializableMatrix MatrixData
        {
            get => new SerializableMatrix(transformationMatrix);
            set => transformationMatrix = value?.ToMatrix() ?? new Matrix();
        }
        public virtual void Translate(float dx, float dy)
        {
            Rectangle = new RectangleF(Rectangle.X + dx, Rectangle.Y + dy, Rectangle.Width, Rectangle.Height);
        }


        public void Rotate(float angle, PointF center)
		{
			// Създаваме матрица за въртене
			Matrix rotationMatrix = new Matrix();
			rotationMatrix.RotateAt(angle, center);
			// Прилагаме въртенето към трансформационната матрица
			transformationMatrix.Multiply(rotationMatrix);
		}

		public void Scale(float scaleX, float scaleY, PointF center)
		{
			// Създаваме матрица за мащабиране
			Matrix scaleMatrix = new Matrix();
			scaleMatrix.Scale(scaleX, scaleY, MatrixOrder.Append);
			// Прилагаме мащабирането към трансформационната матрица
			transformationMatrix.Multiply(scaleMatrix);
		}
		/// <summary>
		/// Цвят на контура на елемента.
		/// </summary>

		private Color strokeColor = Color.Black;
		public virtual Color StrokeColor
		{
			get { return strokeColor; }
			set { strokeColor = value; }
		}
		private int opacity = 255;

		public virtual int Opacity
		{
			get { return opacity; }
			set { opacity = value; }
		}

		public int Clamp(int value, int min, int max)
		{
			if (value < min) return min;
			if (value > max) return max;
			return value;
		}

		private string name;
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

    
		private float borderWidth = 2.0f;
		public virtual float BorderWidth
		{
			get { return borderWidth; }
			set { borderWidth = value; }
		}



		#endregion


		/// <summary>
		/// Проверка дали точка point принадлежи на елемента.
		/// </summary>
		/// <param name="point">Точка</param>
		/// <returns>Връща true, ако точката принадлежи на елемента и
		/// false, ако не пренадлежи</returns>
		public virtual bool Contains(PointF point)
		{
			return Rectangle.Contains(point.X, point.Y);
		}

		/// <summary>
		/// Визуализира елемента.
		/// </summary>
		/// <param name="grfx">Къде да бъде визуализиран елемента.</param>
		public virtual void DrawSelf(Graphics grfx)
		{
			// shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
		}
		public virtual object Clone()
		{
			// Базовая реализация, которая может быть переопределена в наследниках
			return this.MemberwiseClone();
		}

        /// <summary>
        /// Ray casting algorithm for point-in-polygon test.
        /// Returns true if the point is inside the polygon.
        /// </summary>
        /// <param name="polygon">Array of polygon vertices (in order).</param>
        /// <param name="testPoint">Point to test.</param>
        public static bool IsPointInPolygon(PointF[] polygon, PointF testPoint)
        {
            int n = polygon.Length;
            bool inside = false;

            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                if (((polygon[i].Y > testPoint.Y) != (polygon[j].Y > testPoint.Y)) &&
                    (testPoint.X < (polygon[j].X - polygon[i].X) * (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    inside = !inside;
                }
            }
            return inside;
        }

        public RectangleF GetTransformedBounds()
        {
            using (var path = GetPath())
            {
                var transformedPath = (GraphicsPath)path.Clone();
                transformedPath.Transform(TransformationMatrix);
                return transformedPath.GetBounds();
            }
        }



    }
}
