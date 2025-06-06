using Draw.src.Model;
using Draw.src.Model.KrutieFiguri;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor
		
		public DialogProcessor()
		{
		}

        #endregion

        #region Properties

        /// <summary>
        /// Избран елемент.
        /// </summary>


        public Color LastSelectedColor { get; set; } = Color.Black;


        private List<Shape> selection = new List<Shape>();
        public List<Shape> Selection
        {
            get { return selection; }
            set { selection = value; }
        }

        /// <summary>
        /// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
        /// </summary>
        private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		
		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}
		
		#endregion
		
		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		/// 

		public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);
			
			RectangleShape rect = new RectangleShape(new Rectangle(x,y,100,200));
			rect.FillColor = Color.White;

			ShapeList.Add(rect);
		}

        public void AddRandomEllipse()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 100, 100));
            ellipse.FillColor = Color.White;
            ellipse.StrokeColor = Color.Green;

            ShapeList.Add(ellipse);
        }

        public void AddRandomPoint()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            PointShape point = new PointShape(new PointF(x, y));
            point.FillColor = Color.Black; // Черная точка

            ShapeList.Add(point);
        }

        public void AddRandomTriangle()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            TriangleShape rect = new TriangleShape(new Rectangle(x, y, 100, 200));
            rect.FillColor = Color.White;
            rect.StrokeColor = Color.Black;
            ShapeList.Add(rect);
        }

        public void AddRandomLine()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			LineShape line = new LineShape(new Rectangle(x, y, 100, 200));
			line.FillColor = Color.White;
			line.StrokeColor = Color.Black;
			ShapeList.Add(line);
		}


		public void AddRandomSnowflake()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            SnowflakeShape line = new SnowflakeShape(new Rectangle(x, y, 100, 200));
            line.FillColor = Color.White;
            line.StrokeColor = Color.Black;
            ShapeList.Add(line);
        }
		public void AddRandomStar()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			StarShape rect = new StarShape(new Rectangle(x, y, 200, 200));
			rect.FillColor = Color.White;
			rect.StrokeColor = Color.Black;
			ShapeList.Add(rect);
		}

        public float CalculateAngle(PointF start, PointF end)
        {
            float dx = end.X - start.X;
            float dy = end.Y - start.Y;
            return (float)(Math.Atan2(dy, dx) * (180 / Math.PI)); 
        }

        public bool isEyedropperActive = false;

        /// <summary>
        /// Проверява дали дадена точка е в елемента.
        /// Обхожда в ред обратен на визуализацията с цел намиране на
        /// "най-горния" елемент т.е. този който виждаме под мишката.
        /// </summary>
        /// <param name="point">Указана точка</param>
        /// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
        public Shape ContainsPoint(PointF point)
        {
            for (int i = ShapeList.Count - 1; i >= 0; i--)
            {
                if (ShapeList[i].Contains(point))
                {
                    return ShapeList[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
        /// </summary>
        /// <param name="p">Вектор на транслация.</param>
        public void TranslateTo(PointF newLocation)
        {
            float dx = newLocation.X - LastLocation.X;
            float dy = newLocation.Y - LastLocation.Y;

            foreach (var shape in Selection)
            {
                shape.Translate(dx, dy);
            }

            LastLocation = newLocation;
        }

        public void RotateTo(float angle)
        {
            if (selection.Count > 0)
            {
                foreach (Shape shape in selection)
                {
                    var m = shape.TransformationMatrix.Clone();
                    m.Invert();
                    PointF[] pointsToConvert = new PointF[] { shape.Location };
                    m.TransformPoints(pointsToConvert);
                    PointF convertedPoint = pointsToConvert[0];
                    shape.Rotate(angle, convertedPoint);
                }
            }
        }

		public void ScaleTo(float scaleX, float scaleY)
		{
			if (selection.Count > 0)
			{
				foreach (Shape shape in selection)
				{
					var m = shape.TransformationMatrix.Clone();
					m.Invert();
					PointF[] pointsToConvert = new PointF[] { shape.Location };
					m.TransformPoints(pointsToConvert);
					PointF convertedPoint = pointsToConvert[0];
					shape.Scale(scaleX, scaleY, convertedPoint);
				}
			}
		}

        public void SetFillColor(string colorNameOrHex)
        {
            // Try by name
            Color color = Color.FromName(colorNameOrHex);
            if (!color.IsKnownColor && !color.IsNamedColor && !color.IsSystemColor)
            {
                // Try as hex
                try
                {
                    color = ColorTranslator.FromHtml(colorNameOrHex);
                }
                catch
                {
                    throw new ArgumentException("Invalid color name or hex value.");
                }
            }
            foreach (Shape shape in ShapeList)
            {
                shape.FillColor = color;
            }
        }

        public void SetFillColor(int r, int g, int b, int a)
        {
            foreach (Shape shape in ShapeList)
            {
                shape.FillColor = Color.FromArgb(a, r, g, b);
            }
        }

        public void SetStrokeColor(string colorNameOrHex)
        {
            Color color = Color.FromName(colorNameOrHex);
            if (!color.IsKnownColor && !color.IsNamedColor && !color.IsSystemColor)
            {
                try
                {
                    color = ColorTranslator.FromHtml(colorNameOrHex);
                }
                catch
                {
                    throw new ArgumentException("Invalid color name or hex value.");
                }
            }
            foreach (Shape shape in ShapeList)
            {
                shape.StrokeColor = color;
            }
        }

        public void SetStrokeColor(int r, int g, int b, int a = 255)
        {
            foreach (Shape shape in ShapeList)
            {
                shape.StrokeColor = Color.FromArgb(a, r, g, b);
            }
        }



        public override void Draw(Graphics grfx)
		{
			base.Draw(grfx);

            if (IsSelecting)
            {
                using (Pen dashedPen = new Pen(Color.Black))
                {
                    dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    grfx.DrawRectangle(dashedPen, SelectionRectangle);
                }
            }
        }

        public void GroupSelectedShapes()
        {
            if (Selection.Count <= 1)
                return;

            // Определим прямоугольник, охватывающий все выделенные фигуры
            float left = Selection.Min(s => s.Location.X);
            float top = Selection.Min(s => s.Location.Y);
            float right = Selection.Max(s => s.Location.X + s.Width);
            float bottom = Selection.Max(s => s.Location.Y + s.Height);

            RectangleF groupRect = new RectangleF(left, top, right - left, bottom - top);
            GroupShape group = new GroupShape(groupRect);

            foreach (var shape in Selection)
            {
                group.SubShapes.Add(shape);
                Selection.Remove(shape); // убираем из списка, чтобы не дублировались
            }

            Selection.Add(group);

            Selection.Clear();
            Selection.Add(group);
        }


        public Shape SelectShapeAt(Point location, bool ctrl)
        {
            foreach (var shape in ShapeList.Reverse<Shape>())
            {
                if (shape.Contains(location))
                {
                    if (ctrl)
                    {
                        if (!Selection.Contains(shape))
                            Selection.Add(shape);
                    }

                    else
                    {
                        Selection.Clear();
                        Selection.Add(shape);
                    }

                    return shape;
                }
            }

            return null;
        }



        public void UngroupSelected(GroupShape groupShape )
        {
            List<Shape> shapesToAdd = new List<Shape>();
            List<Shape> shapesToRemove = new List<Shape>();

            foreach (var shape in Selection.OfType<GroupShape>())
            {
                shapesToAdd.AddRange(shape.SubShapes);
                shapesToRemove.Add(shape);
            }

            foreach (var shape in shapesToRemove)
            {
                ShapeList.Remove(shape);
                Selection.Remove(shape);
            }

            ShapeList.AddRange(shapesToAdd);
            Selection.AddRange(shapesToAdd);

        }

        public bool IsSelecting = false;
        public Point SelectionStart;
        public Point SelectionEnd;

        public Rectangle SelectionRectangle
        {
            get
            {
                return new Rectangle(
                    Math.Min(SelectionStart.X, SelectionEnd.X),
                    Math.Min(SelectionStart.Y, SelectionEnd.Y),
                    Math.Abs(SelectionEnd.X - SelectionStart.X),
                    Math.Abs(SelectionEnd.Y - SelectionStart.Y));
            }
        }

        public void SelectShapesInRectangle(Rectangle selectionRectangle)
        {
            Selection.Clear(); // или оставить существующее, если Ctrl удержан — по желанию

            foreach (var shape in ShapeList)
            {
                if (selectionRectangle.IntersectsWith(Rectangle.Truncate(shape.Rectangle)))
                {
                    Selection.Add(shape);
                }

            }
        }


        public void UngroupShape(GroupShape group)
        {
            if (group == null) return;

            foreach (var shape in group.SubShapes)
            {
                Selection.Add(shape); // возвращаем вложенные
            }

            Selection.Remove(group);
            Selection.Clear();
        }

        public void SelectAll()
        {
            Selection.Clear();

            foreach (var shape in ShapeList)
            {
                shape.IsSelected = true;
                Selection.Add(shape);
            }
        }


        public void DeleteSelectedShapes()
        {
            foreach (var shape in Selection)
                ShapeList.Remove(shape);
            Selection.Clear();
        }
        public List<Shape> ClipboardShapes = new List<Shape>();

        public void CopySelected()
        {
            ClipboardShapes = Selection.Select(s => (Shape)s.Clone()).ToList();
        }

        public void CutSelected()
        {
            CopySelected();
            DeleteSelectedShapes();
        }

        public void PasteClipboard()
        {
            var pasted = ClipboardShapes.Select(s => (Shape)s.Clone()).ToList();

            // Немного сместить при вставке, чтобы было видно
            foreach (var shape in pasted)
            {
                shape.Location = new PointF(shape.Location.X + 10, shape.Location.Y + 10);
                ShapeList.Add(shape);
            }

            Selection = pasted;
        }
        public void ClearSelection()
        {
            foreach (var shape in Selection)
            {
                shape.IsSelected = false;
            }

            Selection.Clear();
        }
        public void InvertSelection()
        {
            List<Shape> newSelection = new List<Shape>();

            foreach (var shape in ShapeList)
            {
                if (Selection.Contains(shape))
                {
                    shape.IsSelected = false;
                }
                else
                {
                    shape.IsSelected = true;
                    newSelection.Add(shape);
                }
            }

            Selection = newSelection;
        }

        public enum ResizeHandle
        {
            None,
            TopLeft, Top, TopRight,
            Right,
            BottomRight, Bottom, BottomLeft,
            Left
        }

        public ResizeHandle CurrentResizeHandle { get; set; } = ResizeHandle.None;
        public RectangleF CurrentResizeBounds { get; set; }
        public PointF ResizeStartPoint { get; set; }

        public ResizeHandle GetHandleAt(PointF mouse, RectangleF bounds)
        {
            float x = mouse.X, y = mouse.Y;
            float left = bounds.Left, right = bounds.Right, top = bounds.Top, bottom = bounds.Bottom;
            float cx = left + bounds.Width / 2, cy = top + bounds.Height / 2;
            float hs = HandleSize;

            if (IsInRect(x, y, left, top, hs)) return ResizeHandle.TopLeft;
            if (IsInRect(x, y, cx, top, hs)) return ResizeHandle.Top;
            if (IsInRect(x, y, right, top, hs)) return ResizeHandle.TopRight;
            if (IsInRect(x, y, right, cy, hs)) return ResizeHandle.Right;
            if (IsInRect(x, y, right, bottom, hs)) return ResizeHandle.BottomRight;
            if (IsInRect(x, y, cx, bottom, hs)) return ResizeHandle.Bottom;
            if (IsInRect(x, y, left, bottom, hs)) return ResizeHandle.BottomLeft;
            if (IsInRect(x, y, left, cy, hs)) return ResizeHandle.Left;
            return ResizeHandle.None;
        }

        private bool IsInRect(float x, float y, float cx, float cy, float size)
        {
            return Math.Abs(x - cx) <= size / 2 && Math.Abs(y - cy) <= size / 2;
        }
        public void ResizeGroup(List<Shape> shapes, RectangleF oldBounds, RectangleF newBounds)
        {
            foreach (var shape in shapes)
            {
                // Пропорционально изменяем размеры и позицию каждой фигуры в группе
                float scaleX = newBounds.Width / oldBounds.Width;
                float scaleY = newBounds.Height / oldBounds.Height;

                float dx = shape.Rectangle.X - oldBounds.X;
                float dy = shape.Rectangle.Y - oldBounds.Y;

                shape.Rectangle = new RectangleF(
                    newBounds.X + dx * scaleX,
                    newBounds.Y + dy * scaleY,
                    shape.Rectangle.Width * scaleX,
                    shape.Rectangle.Height * scaleY
                );
            }
        }



        private const int HandleSize = 8;

        public void DrawSelection(Graphics g)
        {
            if (Selection.Count == 0)
                return;

            RectangleF bounds;
            if (Selection.Count == 1)
            {
                bounds = Selection[0].GetTransformedBounds();
            }
            else
            {
                var allBounds = Selection.Select(s => s.GetTransformedBounds()).ToList();
                float left = allBounds.Min(r => r.Left);
                float top = allBounds.Min(r => r.Top);
                float right = allBounds.Max(r => r.Right);
                float bottom = allBounds.Max(r => r.Bottom);
                bounds = new RectangleF(left, top, right - left, bottom - top);
            }

            // Рисуем рамку
            using (Pen pen = new Pen(Color.Blue, 1) { DashStyle = DashStyle.Dash })
                g.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width, bounds.Height);

            // Рисуем ручки
            DrawHandle(g, bounds.Left, bounds.Top); // Top-Left
            DrawHandle(g, bounds.Right, bounds.Top); // Top-Right
            DrawHandle(g, bounds.Left, bounds.Bottom); // Bottom-Left
            DrawHandle(g, bounds.Right, bounds.Bottom); // Bottom-Right
            DrawHandle(g, bounds.Left + bounds.Width / 2, bounds.Top); // Top-Center
            DrawHandle(g, bounds.Left + bounds.Width / 2, bounds.Bottom); // Bottom-Center
            DrawHandle(g, bounds.Left, bounds.Top + bounds.Height / 2); // Middle-Left
            DrawHandle(g, bounds.Right, bounds.Top + bounds.Height / 2); // Middle-Right
        }


        private void DrawHandle(Graphics g, float x, float y)
        {
            g.FillRectangle(Brushes.White, x - HandleSize / 2, y - HandleSize / 2, HandleSize, HandleSize);
            g.DrawRectangle(Pens.Black, x - HandleSize / 2, y - HandleSize / 2, HandleSize, HandleSize);
        }



        List<Shape> shapes = new List<Shape>();
        public void SaveToCustomFormat(string path, List<Shape> shapes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, shapes);
            }
        }

        // Загружаем
        public List<Shape> LoadFromCustomFormat(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return (List<Shape>)formatter.Deserialize(stream);
            }
        }
        public void SaveAsImage(string path, ImageFormat format, Control drawingSurface)
        {
            Bitmap bmp = new Bitmap(drawingSurface.Width, drawingSurface.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White); // Белый фон
                drawingSurface.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            }
            bmp.Save(path, format);
        }

        public void SaveToTransparentPng(string path, ImageFormat format, Control drawingSurface)
        {
            int width = drawingSurface.Width;
            int height = drawingSurface.Height;

            // Создаем прозрачный Bitmap
            using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Transparent); // делаем фон прозрачным

                    foreach (var shape in ShapeList)
                    {
                        shape.DrawSelf(g);
                    }

                    bmp.Save(path, ImageFormat.Png);
                }
            }
        }

        public void SaveAsJson(string path, List<Shape> shapes)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            File.WriteAllText(path, JsonConvert.SerializeObject(shapes, settings));
        }


        public List<Shape> LoadFromJson(string path)
        {
            string json = File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            options.Converters.Add(new ShapeJsonConverter());
            options.Converters.Add(new ColorJsonConverter());
            options.Converters.Add(new SizeFJsonConverter());

            return System.Text.Json.JsonSerializer.Deserialize<List<Shape>>(json, options);
        }

        public void SaveAsTatti(string filePath)
        {
            IFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fs, ShapeList);
            }
        }

        public List<Shape> LoadFromTatti(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<Shape>)formatter.Deserialize(fs);
            }
        }

        public Type[] GetKnownShapeTypes()
        {
            return new Type[]
            {
        typeof(LineShape),
        typeof(EllipseShape),
        typeof(CircleWithChordsShape),
        typeof(GroupShape),
        typeof(RectangleShape),
        typeof(PointShape),
        typeof(TriangleShape),
        typeof(SnowflakeShape),
        typeof(StarShape),
        typeof(CrossRectangleShape),
        typeof(Shape)
            };
        }

    }
}
