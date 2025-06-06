using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw.src.GUI
{
    public partial class ImageViewForm: Form
    {
        public DialogProcessor Processor { get; set; } = new DialogProcessor();
        private Panel viewPort;

        public ImageViewForm()
        {
            //MessageBox.Show("Открываю окно");
            InitializeComponent();

            viewPort = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            viewPort.Paint += ViewPort_Paint;
            viewPort.MouseDown += ViewPort_MouseDown;
            viewPort.MouseMove += ViewPort_MouseMove;
            viewPort.MouseUp += ViewPort_MouseUp;

            Controls.Add(viewPort);
        }

       

        private void ViewPort_Paint(object sender, PaintEventArgs e)
        {
            Processor.Draw(e.Graphics);
           
            Processor.DrawSelection(e.Graphics);

            // Рисуем рамку выделения
            if (Processor.IsSelecting)
            {
                using (Pen pen = new Pen(Color.Blue))
                {
                    pen.DashStyle = DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, Processor.SelectionRectangle);
                }
            }
        }
        private void ViewPort_MouseDown(object sender, MouseEventArgs e)
        {
            var shape = Processor.SelectShapeAt(e.Location, (ModifierKeys & Keys.Control) == Keys.Control);
            if (shape != null)
            {
                Processor.IsDragging = true;
                Processor.LastLocation = e.Location;
            }
            viewPort.Invalidate();
        }

        private void ViewPort_MouseMove(object sender, MouseEventArgs e)
        {
            if (Processor.IsDragging)
            {
                Processor.TranslateTo(e.Location);
                viewPort.Invalidate();
            }
        }

        private void ViewPort_MouseUp(object sender, MouseEventArgs e)
        {
            Processor.IsDragging = false;
        }
    }
}