using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Draw.DialogProcessor;
using System.Drawing.Imaging;
using Draw.src.GUI;

namespace Draw
{
    /// <summary>
    /// Върху главната форма е поставен потребителски контрол,
    /// в който се осъществява визуализацията
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
        /// </summary>
        private DialogProcessor dialogProcessor = new DialogProcessor();

        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            this.IsMdiContainer = true;

            copyToolStripMenuItem1.Click += (s, e) => dialogProcessor.CopySelected();
            pasteToolStripMenuItem1.Click += (s, e) => { dialogProcessor.PasteClipboard(); viewPort.Invalidate(); };
            cutToolStripMenuItem1.Click += (s, e) => {
                dialogProcessor.DeleteSelectedShapes(); viewPort.Invalidate();
            };
            // Може да отворите първоначален изглед:
            //var imageView = new ImageViewForm();
            //imageView.MdiParent = this;
            //imageView.Show();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        /// <summary>
        /// Изход от програмата. Затваря главната форма, а с това и програмата.
        /// </summary>
        void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
        /// </summary>
        private void ViewPortPaint(object sender, PaintEventArgs e)
        {
            dialogProcessor.ReDraw(sender, e);
            dialogProcessor.DrawSelection(e.Graphics);

            // Рисуем рамку выделения
            if (dialogProcessor.IsSelecting)
            {
                using (Pen pen = new Pen(Color.Blue))
                {
                    pen.DashStyle = DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, dialogProcessor.SelectionRectangle);
                }
            }
        }

        private PointF rotationStart;
        /// <summary>
        /// Бутон, който поставя на произволно място правоъгълник със зададените размери.
        /// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
        /// </summary>
        void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomRectangle();

            statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

            viewPort.Invalidate();
        }

        /// <summary>
        /// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
        /// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
        /// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
        /// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
        /// </summary>
        void ViewPortMouseDown(object sender, MouseEventArgs e)
        {
            viewPort.Focus();

            if (pickUpSpeedButton.Checked)
            {
                bool ctrl = (ModifierKeys & Keys.Control) == Keys.Control;
                var shape = dialogProcessor.SelectShapeAt(e.Location, ctrl);

                if (shape != null)
                {
                    dialogProcessor.IsDragging = true;
                    dialogProcessor.LastLocation = e.Location;
                    statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
                }
                else
                {
                    if (!ctrl)
                        dialogProcessor.Selection.Clear();

                    dialogProcessor.IsSelecting = true;
                    dialogProcessor.SelectionStart = e.Location;
                    dialogProcessor.SelectionEnd = e.Location;
                    statusBar.Items[0].Text = "Последно действие: Изчистване на селекция";
                }

                viewPort.Invalidate();
            }

            // ⛔ НЕ НУЖНО сразу выходить
            if (dialogProcessor.Selection.Count == 0)
                return;

            RectangleF bounds;
            if (dialogProcessor.Selection.Count == 1)
                bounds = dialogProcessor.Selection[0].Rectangle;
            else
            {
                float left = dialogProcessor.Selection.Min(s => s.Rectangle.Left);
                float top = dialogProcessor.Selection.Min(s => s.Rectangle.Top);
                float right = dialogProcessor.Selection.Max(s => s.Rectangle.Right);
                float bottom = dialogProcessor.Selection.Max(s => s.Rectangle.Bottom);
                bounds = new RectangleF(left, top, right - left, bottom - top);
            }

            var handle = dialogProcessor.GetHandleAt(e.Location, bounds);
            if (handle != ResizeHandle.None)
            {
                dialogProcessor.CurrentResizeHandle = handle;
                dialogProcessor.CurrentResizeBounds = bounds;
                dialogProcessor.ResizeStartPoint = e.Location;
                return;
            }
            if (e.Button == MouseButtons.Right && dialogProcessor.Selection.Count > 0)
            {
                rotationStart = e.Location;
            }

            if (dialogProcessor.isEyedropperActive)
            {
                using (Bitmap bmp = new Bitmap(viewPort.Width, viewPort.Height))
                {
                    viewPort.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                    if (e.X >= 0 && e.Y >= 0 && e.X < bmp.Width && e.Y < bmp.Height)
                    {
                        Color pickedColor = bmp.GetPixel(e.X, e.Y);
                        dialogProcessor.LastSelectedColor = pickedColor;

                        // Assign color to selected shapes  
                        foreach (var shape in dialogProcessor.Selection)
                            shape.FillColor = pickedColor;

                        // Update interface  
                        viewPort.Invalidate();
                        Cursor = Cursors.Default;
                        dialogProcessor.isEyedropperActive = false;
                        MessageBox.Show($"Выбран цвет: {pickedColor}");
                    }
                }
            }
            //if (dialogProcessor.Selection.Count == 0)
            //    return;
            //foreach (var shape in dialogProcessor.Selection)
            //{
            //    var mode = dGetResizeMode(e.Location, shape.Rectangle);
            //    if (mode != ResizeMode.None)
            //    {
            //        dialogProcessor.CurrentResizeMode = mode;
            //        dialogProcessor.ResizingShape = shape;
            //        dialogProcessor.ResizeStartPoint = e.Location;
            //        break;
            //    }
            //}

        }


        /// <summary>
        /// Прихващане на преместването на мишката.
        /// Ако сме в режм на "влачене", то избрания елемент се транслира.
        /// </summary>
        void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dialogProcessor.CurrentResizeHandle != ResizeHandle.None)
            {
                var start = dialogProcessor.ResizeStartPoint;
                var bounds = dialogProcessor.CurrentResizeBounds;
                float dx = e.X - start.X;
                float dy = e.Y - start.Y;
                RectangleF newBounds = bounds;

                switch (dialogProcessor.CurrentResizeHandle)
                {
                    case DialogProcessor.ResizeHandle.Right:
                    case DialogProcessor.ResizeHandle.TopRight:
                    case DialogProcessor.ResizeHandle.BottomRight:
                        newBounds.Width = Math.Max(1, bounds.Width + dx);
                        break;
                    case DialogProcessor.ResizeHandle.Left:
                    case DialogProcessor.ResizeHandle.TopLeft:
                    case DialogProcessor.ResizeHandle.BottomLeft:
                        newBounds.X = bounds.X + dx;
                        newBounds.Width = Math.Max(1, bounds.Width - dx);
                        break;
                }
                switch (dialogProcessor.CurrentResizeHandle)
                {
                    case DialogProcessor.ResizeHandle.Bottom:
                    case DialogProcessor.ResizeHandle.BottomLeft:
                    case DialogProcessor.ResizeHandle.BottomRight:
                        newBounds.Height = Math.Max(1, bounds.Height + dy);
                        break;
                    case DialogProcessor.ResizeHandle.Top:
                    case DialogProcessor.ResizeHandle.TopLeft:
                    case DialogProcessor.ResizeHandle.TopRight:
                        newBounds.Y = bounds.Y + dy;
                        newBounds.Height = Math.Max(1, bounds.Height - dy);
                        break;
                }

                // Применяем новые размеры  
                if (dialogProcessor.Selection.Count == 1)
                {
                    dialogProcessor.Selection[0].Rectangle = newBounds;
                }
                else
                {
                    // Для группы: пересчитать размеры и позиции всех фигур пропорционально изменению рамки  
                    dialogProcessor.ResizeGroup(dialogProcessor.Selection, bounds, newBounds);
                    dialogProcessor.ResizeStartPoint = e.Location;
                }

                 // Обновляем начальную точку для предотвращения "прыжков"  
                viewPort.Invalidate();
                return; // Не продолжаем обработку других режимов  
            }

            if (dialogProcessor.IsDragging)
            {
                if (dialogProcessor.Selection.Count > 0)
                {
                    dialogProcessor.TranslateTo(e.Location);
                    viewPort.Invalidate();
                }
                if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
            }
            if (dialogProcessor.IsSelecting)
            {
                dialogProcessor.SelectionEnd = e.Location;
                viewPort.Invalidate();
            }
            if (e.Button == MouseButtons.Right && dialogProcessor.Selection.Count > 0)
            {
                PointF rotationEnd = e.Location;
                float angle = dialogProcessor.CalculateAngle(rotationStart, rotationEnd);

                dialogProcessor.RotateTo(angle);

                rotationStart = rotationEnd;
                viewPort.Invalidate();
            }


        }

        /// <summary>
        /// Прихващане на отпускането на бутона на мишката.
        /// Излизаме от режим "в4лачене".
        /// </summary>
        void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dialogProcessor.IsDragging)
                dialogProcessor.IsDragging = false;
            
            if (dialogProcessor.IsSelecting)
            {
                dialogProcessor.IsSelecting = false;
                dialogProcessor.SelectShapesInRectangle(dialogProcessor.SelectionRectangle);
                statusBar.Items[0].Text = "Выделены объекты в рамке.";

                viewPort.Invalidate();
            }dialogProcessor.CurrentResizeHandle = ResizeHandle.None;

            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(viewPort, e.Location);
            }

        }

        private void drawEllipseSpeedButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllipse();

            statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

            viewPort.Invalidate();

        }

        private void drawTriangleSpeedButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomTriangle();

            statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";

            viewPort.Invalidate();
        }


        private void drawPointSpeedButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomPoint();
            statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

            viewPort.Invalidate();

        }

        private void SnowflakeStripButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomSnowflake();

            statusBar.Items[0].Text = "Последно действие: Рисуване на снежинка";

            viewPort.Invalidate();
        }

        private void DrawStarSplitButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomStar();

            statusBar.Items[0].Text = "Последно действие: Рисуване на звезда";

            viewPort.Invalidate();
        }



        private void AddLineShape_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomLine();

            statusBar.Items[0].Text = "Последно действие: Рисуване на отсечка";

            viewPort.Invalidate();
        }

        private void AddTriangleShapeButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomTriangle();
            statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";
            viewPort.Invalidate();
        }

        private void AddPointShape_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomPoint();
            statusBar.Items[0].Text = "Последно действие: Рисуване на точка";
            viewPort.Invalidate();
        }

        private void GroupObjectsButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.GroupSelectedShapes();
            statusBar.Items[0].Text = "Выбранные объекты сгруппированы.";
            viewPort.Invalidate();
        }





        private void speedMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private const float RotationAngleStep = 90f; // Define the constant for rotation angle step

        private void RotateButton_Click_1(object sender, EventArgs e)
        {
            dialogProcessor.RotateTo(RotationAngleStep); // Use the defined constant  
            viewPort.Invalidate();
            if (float.TryParse(nameTextBox.Text, out float angle)) // Fixed the condition to pass a string instead of a boolean  
            {
                dialogProcessor.RotateTo(angle);

                viewPort.Invalidate();
            }
            else
            {
                MessageBox.Show("Введите корректный угол.");
            }
        }


        private void TranslateButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.ScaleTo(1.2f, 1.2f);
            viewPort.Invalidate();
        }

        private void ScaleButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.TranslateTo(new PointF(10, 10)); // Примерно преместване
            viewPort.Invalidate();
        }

        private void FillColorButton_Click(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.Count > 0)
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        Color chosenColor = colorDialog.Color;
                        dialogProcessor.LastSelectedColor = chosenColor; // Save the last selected color  

                        foreach (var shape in dialogProcessor.Selection)
                            shape.FillColor = chosenColor;

                        colorPreview.BackColor = chosenColor; // Update the color preview indicator  
                        viewPort.Invalidate();
                    }
                }
            }
            else
            {
                MessageBox.Show("No shapes selected. Please select a shape to apply the fill color.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void UngroupShape_Click(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.FirstOrDefault() is GroupShape group)
            {
                dialogProcessor.UngroupShape(group);
                viewPort.Invalidate();
            }
        }

      
        private void NumericLineWidth_ValueChanged(object sender, EventArgs e)
        {
            float newWidth = (float)numericLineWidth.Value;

            foreach (var shape in dialogProcessor.Selection)
            {
                shape.BorderWidth = newWidth;
                statusBar.Items[0].Text = $"Установлена граница: {newWidth} для фигуры {shape.Name}";
            }

            viewPort.Invalidate();
        }


        private void TrackOpacity_Scroll(object sender, EventArgs e)
        {

            foreach (var shape in dialogProcessor.Selection)
            {
                shape.Opacity = trackOpacity.Value;
            }
                viewPort.Invalidate();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            float newWidth = (float)numericLineWidth.Value;


            foreach (var shape in dialogProcessor.Selection)
            {
                shape.BorderWidth = newWidth;
                statusBar.Items[0].Text = $"Установлена граница: {newWidth} для фигуры {shape.Name}";
            }

            viewPort.Invalidate();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save As";
                sfd.Filter =
                    "PNG Image|*.png|" +
                    "JPEG Image|*.jpg|" +
                    "TIFF Image|*.tiff|" +
                    "Custom Binary File|*.bin|" +
                    "JSON File|*.json|" +
                    "Tatti Vector File (*.tatti)|*.tatti";
                sfd.DefaultExt = "png";
                sfd.AddExtension = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string path = sfd.FileName;
                    string ext = Path.GetExtension(path).ToLower();

                    switch (ext)
                    {
                        case ".png":
                            dialogProcessor.SaveToTransparentPng(path, System.Drawing.Imaging.ImageFormat.Png, viewPort); break;
                        case ".jpg":
                            dialogProcessor.SaveAsImage(path, System.Drawing.Imaging.ImageFormat.Jpeg, viewPort); break;
                        case ".tiff":
                            dialogProcessor.SaveAsImage(path, System.Drawing.Imaging.ImageFormat.Tiff, viewPort); break;
                        case ".bin":
                            dialogProcessor.SaveToCustomFormat(path, dialogProcessor.ShapeList); break;
                        case ".json":
                            dialogProcessor.SaveAsJson(path, dialogProcessor.ShapeList); break;
                        case ".tatti":
                            dialogProcessor.SaveAsTatti(path); break;
                        default:
                            MessageBox.Show("Неподдерживаемый формат.");
                            break;
                    }
                }
            }
        }

        private void canvasPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (var shape in dialogProcessor.ShapeList)
            {
                shape.DrawSelf(g);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Open File";
                ofd.Filter =
                    "All Supported Files (*.tatti;*.bin;*.json)|*.tatti;*.bin;*.json|" +
                    "Tatti Vector File (*.tatti)|*.tatti|" +
                    "Custom Binary File (*.bin)|*.bin|" +
                    "JSON File (*.json)|*.json|" +
                    "All Files (*.*)|*.*";
                ofd.DefaultExt = "tatti";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path = ofd.FileName;
                    string ext = Path.GetExtension(path).ToLower();

                    try
                    {
                        switch (ext)
                        {
                            case ".bin":
                                dialogProcessor.ShapeList = dialogProcessor.LoadFromCustomFormat(path);
                                break;
                            case ".json":
                                dialogProcessor.ShapeList = dialogProcessor.LoadFromJson(path);
                                break;
                            case ".tatti":
                                dialogProcessor.ShapeList = dialogProcessor.LoadFromTatti(path);
                                break;
                            default:
                                MessageBox.Show("Неподдерживаемый формат.");
                                return;
                        }

                        viewPort.Invalidate(); // Перерисовать окно
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке файла:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ViewPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) dialogProcessor.DeleteSelectedShapes();
            else if (e.KeyCode == Keys.Escape) dialogProcessor.ClearSelection();
            else if (e.Control && e.KeyCode == Keys.C) dialogProcessor.CopySelected();
            else if (e.Control && e.KeyCode == Keys.X) dialogProcessor.CutSelected();
            else if (e.Control && e.KeyCode == Keys.V) dialogProcessor.PasteClipboard();
            else if (e.Control && e.KeyCode == Keys.A)
            {
                dialogProcessor.SelectAll();
                statusBar.Items[0].Text = "Выделены все фигуры.";
            }
            else if (e.Control && e.KeyCode == Keys.I)
            {
                dialogProcessor.InvertSelection();
            }
            viewPort.Invalidate();
        }

        private void applyNamedColorButton_Click(object sender, EventArgs e)
        {
            string userColor = nameTextBox.Text.Trim(); // "Red" или "#FF00FF"
            try
            {
                dialogProcessor.SetFillColor(userColor);
                viewPort.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка цвета: " + ex.Message);
            }
        }

        private void applyRGBAButton_Click(object sender, EventArgs e)
        {
            int r = int.Parse(rTextBox.Text);
            int g = int.Parse(gTextBox.Text);
            int b = int.Parse(bTextBox.Text);
            int a = int.Parse(aTextBox.Text); // можно по умолчанию 255

            dialogProcessor.SetFillColor(r, g, b, a);
            viewPort.Invalidate();
        }

        private void colorTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void rTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void nameChanging_Click(object sender, EventArgs e)
        {
            string newName = nameTextBox.Text.Trim();

            if (dialogProcessor.Selection.Count > 0)
            {
                foreach (var shape in dialogProcessor.Selection)
                {
                    shape.Name = newName;
                }

                statusBar.Items[0].Text = $"Имя \"{newName}\" присвоено выбранным объектам.";
            }
            else
            {
                MessageBox.Show("Сначала выберите фигуру.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void eyedropperButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.isEyedropperActive = true;
            Cursor = Cursors.Cross;

            statusBar.Items[0].Text = "Режим пипетки активирован. Щелкните на холсте, чтобы выбрать цвет.";

            viewPort.MouseClick += (s, ev) =>
            {
                if (dialogProcessor.isEyedropperActive)
                {
                    Bitmap bmp = new Bitmap(viewPort.Width, viewPort.Height);
                    viewPort.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                    if (ev.X >= 0 && ev.Y >= 0 && ev.X < bmp.Width && ev.Y < bmp.Height)
                    {
                        Color pickedColor = bmp.GetPixel(ev.X, ev.Y);
                        dialogProcessor.LastSelectedColor = pickedColor;

                        foreach (var shape in dialogProcessor.Selection)
                        {
                            shape.FillColor = pickedColor;
                        }

                        viewPort.Invalidate();
                        Cursor = Cursors.Default;
                        dialogProcessor.isEyedropperActive = false;

                        MessageBox.Show($"Выбран цвет: {pickedColor}", "Пипетка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            };
        }

      

        private void endColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (var shape in dialogProcessor.Selection)
                        shape.GradientEndColor = dlg.Color;
                    viewPort.Invalidate();
                }
            }
        }

        private void startColorButton_Click(object sender, EventArgs e)
        {

            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (var shape in dialogProcessor.Selection)
                        shape.GradientStartColor = dlg.Color;
                    viewPort.Invalidate();
                }
            }
        }

        private void linearGradientRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var shape in dialogProcessor.Selection)
                shape.UseLinearGradient = linearGradientRadioButton.Checked;
            viewPort.Invalidate();
        }

        private void useGradientCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var shape in dialogProcessor.Selection)
                shape.UseGradient = useGradientCheckBox.Checked;
            viewPort.Invalidate();
        }

        private void radialGradientRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var shape in dialogProcessor.Selection)
                shape.UseRadialGradientRadioButton = radialGradientRadioButton.Checked;
            viewPort.Invalidate();
        }

        private void borderColorButton_Click(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.Count > 0)
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (var shape in dialogProcessor.Selection)
                        {
                            shape.StrokeColor = colorDialog.Color;
                        }
                        viewPort.Invalidate();
                    }
                }
            }
        }

        private void applyLastColorButton_Click(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.Count > 0)
            {
                foreach (var shape in dialogProcessor.Selection)
                    shape.FillColor = dialogProcessor.LastSelectedColor;

                colorPreview.BackColor = dialogProcessor.LastSelectedColor;
                viewPort.Invalidate();
            }
            else
            {
                MessageBox.Show("Сначала выберите фигуру.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Declare the sharedProcessor field in the MainForm class to resolve the error.  
        private DialogProcessor sharedProcessor = new DialogProcessor();

        private void NewViewMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Кнопка нажата");
            var imageView = new ImageViewForm();
            imageView.MdiParent = this;
            imageView.Processor = sharedProcessor; // Assign the sharedProcessor instance  
            imageView.Text = "Общий вид";
            imageView.Show();
        }

        // Новый процессор — отдельное изображение
        private void NewImageMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Кнопка нажата");
            var imageView = new ImageViewForm();
            imageView.MdiParent = this;
            imageView.Processor = new DialogProcessor(); // новое изображение
            imageView.Text = "Новое изображение";
            imageView.Show();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }


























    //private void ColorButton_Click(object sender, EventArgs e)
    //{
    //    if (colorDialog1.ShowDialog() == DialogResult.OK)
    //    {
    //        if (dialogProcessor.Selection != null)
    //        {
    //            dialogProcessor.Selection.FillColor1 = colorDialog1.Color;
    //            dialogProcessor.Selection.FillColor2 = colorDialog1.Color;
    //            viewPort.Invalidate();
    //        }
    //    }
    //}
    //private void GradientButton_Click(object sender, EventArgs e)
    //{
    //    if (colorDialog1.ShowDialog() == DialogResult.OK)
    //    {
    //        if (dialogProcessor.Selection != null)
    //        {
    //            dialogProcessor.Selection.FillColor2 = colorDialog1.Color;
    //            viewPort.Invalidate();
    //        }
    //    }
    //}
    //      private void trackBar1_Scroll(object sender, EventArgs e)
    //      {
    //          // dialogProcessor.Selection.Opacity = trackBar1.Value;


    //          if (dialogProcessor.Selection != null)
    //          {
    //              dialogProcessor.Selection.Opacity = trackBarOpacity.Value;
    //              viewPort.Invalidate();
    //          }

    //      }

    //      private void trackBar1_Scroll_1(object sender, EventArgs e)
    //      {
    //          if (dialogProcessor.Selection != null)
    //          {
    //              dialogProcessor.Selection.Width = trackBarWidth.Value;
    //              viewPort.Invalidate();
    //          }
    //      }


    //      private void greenToolSpritMenuItem_Click(object sender, EventArgs e)
    //{

    //}

    //private void textBox1_TextChanged(object sender, EventArgs e)
    //      {
    //          foreach(Shape item in dialogProcessor.Selection) item.Name = textBox1.Text;
    //      }

    //      //private void textBox
    //      private void toolStripButton3_Click(object sender, EventArgs e)
    //{
    //          foreach (Shape shape in selection)
    //	{
    //		if (colorDialog1.ShowDialog() == DialogResult.OK)
    //		{
    //			dialogProcessor.Selection.FillColor = colorDialog1.Color;
    //			viewPort.Invalidate();
    //		}
    //	}

    //}






}

