using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}


		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		public void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.currentStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.numericLineWidth = new System.Windows.Forms.NumericUpDown();
            this.trackOpacity = new System.Windows.Forms.TrackBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.rTextBox = new System.Windows.Forms.TextBox();
            this.aTextBox = new System.Windows.Forms.TextBox();
            this.gTextBox = new System.Windows.Forms.TextBox();
            this.bTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.colorPreview = new System.Windows.Forms.Panel();
            this.drawRectangleSpeedButton = new System.Windows.Forms.ToolStripButton();
            this.drawEllipseButton = new System.Windows.Forms.ToolStripButton();
            this.DrawStarSplitButton = new System.Windows.Forms.ToolStripButton();
            this.AddLineShape = new System.Windows.Forms.ToolStripButton();
            this.SnowflakeStripButton = new System.Windows.Forms.ToolStripButton();
            this.pickUpSpeedButton = new System.Windows.Forms.ToolStripButton();
            this.AddTriangleShapeButton = new System.Windows.Forms.ToolStripButton();
            this.AddPointShape = new System.Windows.Forms.ToolStripButton();
            this.GroupObjectsButton = new System.Windows.Forms.ToolStripButton();
            this.RotateButton = new System.Windows.Forms.ToolStripButton();
            this.ScaleButton = new System.Windows.Forms.ToolStripButton();
            this.TranslateButtton = new System.Windows.Forms.ToolStripButton();
            this.fillColorButton = new System.Windows.Forms.ToolStripButton();
            this.UngroupShape = new System.Windows.Forms.ToolStripButton();
            this.applyNamedColorButton = new System.Windows.Forms.ToolStripButton();
            this.applyRGBAButton = new System.Windows.Forms.ToolStripButton();
            this.nameChanging = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.speedMenu = new System.Windows.Forms.ToolStrip();
            this.startColorButton = new System.Windows.Forms.ToolStripButton();
            this.endColorButton = new System.Windows.Forms.ToolStripButton();
            this.borderColorButton = new System.Windows.Forms.ToolStripButton();
            this.applyLastColorButton = new System.Windows.Forms.ToolStripButton();
            this.NewImageMenuItem = new System.Windows.Forms.ToolStripButton();
            this.useGradientCheckBox = new System.Windows.Forms.CheckBox();
            this.linearGradientRadioButton = new System.Windows.Forms.RadioButton();
            this.radialGradientRadioButton = new System.Windows.Forms.RadioButton();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPort = new Draw.DoubleBufferedPanel();
            this.mainMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLineWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackOpacity)).BeginInit();
            this.speedMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1902, 28);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newColorsToolStripMenuItem,
            this.toolStripSeparator1});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // newColorsToolStripMenuItem
            // 
            this.newColorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.greenToolStripMenuItem,
            this.yellowToolStripMenuItem,
            this.blackToolStripMenuItem,
            this.redToolStripMenuItem});
            this.newColorsToolStripMenuItem.Name = "newColorsToolStripMenuItem";
            this.newColorsToolStripMenuItem.Size = new System.Drawing.Size(168, 26);
            this.newColorsToolStripMenuItem.Text = "New Colors";
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.greenToolStripMenuItem.Text = "Green";
            // 
            // yellowToolStripMenuItem
            // 
            this.yellowToolStripMenuItem.Name = "yellowToolStripMenuItem";
            this.yellowToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.yellowToolStripMenuItem.Text = "Yellow";
            // 
            // blackToolStripMenuItem
            // 
            this.blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            this.blackToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.blackToolStripMenuItem.Text = "Black";
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.redToolStripMenuItem.Text = "Red";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentStatusLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 1033);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusBar.Size = new System.Drawing.Size(1902, 22);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusStrip1";
            // 
            // currentStatusLabel
            // 
            this.currentStatusLabel.Name = "currentStatusLabel";
            this.currentStatusLabel.Size = new System.Drawing.Size(0, 16);
            // 
            // numericLineWidth
            // 
            this.numericLineWidth.Location = new System.Drawing.Point(800, 800);
            this.numericLineWidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericLineWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericLineWidth.Name = "numericLineWidth";
            this.numericLineWidth.Size = new System.Drawing.Size(200, 22);
            this.numericLineWidth.TabIndex = 0;
            this.numericLineWidth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericLineWidth.ValueChanged += new System.EventHandler(this.NumericLineWidth_ValueChanged);
            // 
            // trackOpacity
            // 
            this.trackOpacity.Location = new System.Drawing.Point(1752, 417);
            this.trackOpacity.Maximum = 255;
            this.trackOpacity.Name = "trackOpacity";
            this.trackOpacity.Size = new System.Drawing.Size(150, 56);
            this.trackOpacity.TabIndex = 1;
            this.trackOpacity.TickFrequency = 5;
            this.trackOpacity.Value = 255;
            this.trackOpacity.Scroll += new System.EventHandler(this.TrackOpacity_Scroll);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // rTextBox
            // 
            this.rTextBox.Location = new System.Drawing.Point(235, 367);
            this.rTextBox.Name = "rTextBox";
            this.rTextBox.Size = new System.Drawing.Size(100, 22);
            this.rTextBox.TabIndex = 7;
            // 
            // aTextBox
            // 
            this.aTextBox.Location = new System.Drawing.Point(235, 451);
            this.aTextBox.Name = "aTextBox";
            this.aTextBox.Size = new System.Drawing.Size(100, 22);
            this.aTextBox.TabIndex = 8;
            // 
            // gTextBox
            // 
            this.gTextBox.Location = new System.Drawing.Point(235, 395);
            this.gTextBox.Name = "gTextBox";
            this.gTextBox.Size = new System.Drawing.Size(100, 22);
            this.gTextBox.TabIndex = 9;
            // 
            // bTextBox
            // 
            this.bTextBox.Location = new System.Drawing.Point(235, 423);
            this.bTextBox.Name = "bTextBox";
            this.bTextBox.Size = new System.Drawing.Size(100, 22);
            this.bTextBox.TabIndex = 10;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(75, 33);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 22);
            this.nameTextBox.TabIndex = 12;
            // 
            // colorPreview
            // 
            this.colorPreview.Location = new System.Drawing.Point(52, 31);
            this.colorPreview.Name = "colorPreview";
            this.colorPreview.Size = new System.Drawing.Size(17, 24);
            this.colorPreview.TabIndex = 13;
            // 
            // drawRectangleSpeedButton
            // 
            this.drawRectangleSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawRectangleSpeedButton.Image = ((System.Drawing.Image)(resources.GetObject("drawRectangleSpeedButton.Image")));
            this.drawRectangleSpeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawRectangleSpeedButton.Name = "drawRectangleSpeedButton";
            this.drawRectangleSpeedButton.Size = new System.Drawing.Size(37, 24);
            this.drawRectangleSpeedButton.Text = "DrawRectangleButton";
            this.drawRectangleSpeedButton.Click += new System.EventHandler(this.DrawRectangleSpeedButtonClick);
            // 
            // drawEllipseButton
            // 
            this.drawEllipseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawEllipseButton.Image = ((System.Drawing.Image)(resources.GetObject("drawEllipseButton.Image")));
            this.drawEllipseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawEllipseButton.Name = "drawEllipseButton";
            this.drawEllipseButton.Size = new System.Drawing.Size(37, 24);
            this.drawEllipseButton.Text = "drawEllipseButton";
            this.drawEllipseButton.ToolTipText = "drawEllipseButton";
            this.drawEllipseButton.Click += new System.EventHandler(this.drawEllipseSpeedButton_Click);
            // 
            // DrawStarSplitButton
            // 
            this.DrawStarSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DrawStarSplitButton.Image = ((System.Drawing.Image)(resources.GetObject("DrawStarSplitButton.Image")));
            this.DrawStarSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DrawStarSplitButton.Name = "DrawStarSplitButton";
            this.DrawStarSplitButton.Size = new System.Drawing.Size(37, 24);
            this.DrawStarSplitButton.Text = "DrawStarSplitButton";
            this.DrawStarSplitButton.Click += new System.EventHandler(this.DrawStarSplitButton_Click);
            // 
            // AddLineShape
            // 
            this.AddLineShape.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddLineShape.Image = ((System.Drawing.Image)(resources.GetObject("AddLineShape.Image")));
            this.AddLineShape.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddLineShape.Name = "AddLineShape";
            this.AddLineShape.Size = new System.Drawing.Size(37, 24);
            this.AddLineShape.Text = "AddLineShapeButton";
            this.AddLineShape.Click += new System.EventHandler(this.AddLineShape_Click);
            // 
            // SnowflakeStripButton
            // 
            this.SnowflakeStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SnowflakeStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SnowflakeStripButton.Image")));
            this.SnowflakeStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SnowflakeStripButton.Name = "SnowflakeStripButton";
            this.SnowflakeStripButton.Size = new System.Drawing.Size(37, 24);
            this.SnowflakeStripButton.Text = "SnowflakeStripButton";
            this.SnowflakeStripButton.Click += new System.EventHandler(this.SnowflakeStripButton_Click);
            // 
            // pickUpSpeedButton
            // 
            this.pickUpSpeedButton.CheckOnClick = true;
            this.pickUpSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pickUpSpeedButton.Image = ((System.Drawing.Image)(resources.GetObject("pickUpSpeedButton.Image")));
            this.pickUpSpeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pickUpSpeedButton.Name = "pickUpSpeedButton";
            this.pickUpSpeedButton.Size = new System.Drawing.Size(37, 24);
            this.pickUpSpeedButton.Text = "toolStripButton1";
            // 
            // AddTriangleShapeButton
            // 
            this.AddTriangleShapeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddTriangleShapeButton.Image = ((System.Drawing.Image)(resources.GetObject("AddTriangleShapeButton.Image")));
            this.AddTriangleShapeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddTriangleShapeButton.Name = "AddTriangleShapeButton";
            this.AddTriangleShapeButton.Size = new System.Drawing.Size(37, 24);
            this.AddTriangleShapeButton.Text = "AddTriangleShape";
            this.AddTriangleShapeButton.Click += new System.EventHandler(this.AddTriangleShapeButton_Click);
            // 
            // AddPointShape
            // 
            this.AddPointShape.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddPointShape.Image = ((System.Drawing.Image)(resources.GetObject("AddPointShape.Image")));
            this.AddPointShape.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddPointShape.Name = "AddPointShape";
            this.AddPointShape.Size = new System.Drawing.Size(37, 24);
            this.AddPointShape.Text = "AddPointShape";
            this.AddPointShape.Click += new System.EventHandler(this.AddPointShape_Click);
            // 
            // GroupObjectsButton
            // 
            this.GroupObjectsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GroupObjectsButton.Image = ((System.Drawing.Image)(resources.GetObject("GroupObjectsButton.Image")));
            this.GroupObjectsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GroupObjectsButton.Name = "GroupObjectsButton";
            this.GroupObjectsButton.Size = new System.Drawing.Size(37, 24);
            this.GroupObjectsButton.Text = "GroupObjects";
            this.GroupObjectsButton.Click += new System.EventHandler(this.GroupObjectsButton_Click);
            // 
            // RotateButton
            // 
            this.RotateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RotateButton.Image = ((System.Drawing.Image)(resources.GetObject("RotateButton.Image")));
            this.RotateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RotateButton.Name = "RotateButton";
            this.RotateButton.Size = new System.Drawing.Size(37, 24);
            this.RotateButton.Text = "RotateButton";
            this.RotateButton.Click += new System.EventHandler(this.RotateButton_Click_1);
            // 
            // ScaleButton
            // 
            this.ScaleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ScaleButton.Image = ((System.Drawing.Image)(resources.GetObject("ScaleButton.Image")));
            this.ScaleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScaleButton.Name = "ScaleButton";
            this.ScaleButton.Size = new System.Drawing.Size(37, 24);
            this.ScaleButton.Text = "ScaleButton";
            this.ScaleButton.Click += new System.EventHandler(this.TranslateButton_Click);
            // 
            // TranslateButtton
            // 
            this.TranslateButtton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TranslateButtton.Image = ((System.Drawing.Image)(resources.GetObject("TranslateButtton.Image")));
            this.TranslateButtton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TranslateButtton.Name = "TranslateButtton";
            this.TranslateButtton.Size = new System.Drawing.Size(37, 24);
            this.TranslateButtton.Text = "TranslateButtton";
            this.TranslateButtton.Click += new System.EventHandler(this.ScaleButton_Click);
            // 
            // fillColorButton
            // 
            this.fillColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fillColorButton.Image = ((System.Drawing.Image)(resources.GetObject("fillColorButton.Image")));
            this.fillColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fillColorButton.Name = "fillColorButton";
            this.fillColorButton.Size = new System.Drawing.Size(37, 24);
            this.fillColorButton.Text = "fillColorButton";
            this.fillColorButton.Click += new System.EventHandler(this.FillColorButton_Click);
            // 
            // UngroupShape
            // 
            this.UngroupShape.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UngroupShape.Image = ((System.Drawing.Image)(resources.GetObject("UngroupShape.Image")));
            this.UngroupShape.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UngroupShape.Name = "UngroupShape";
            this.UngroupShape.Size = new System.Drawing.Size(37, 24);
            this.UngroupShape.Text = "UngroupShape";
            this.UngroupShape.Click += new System.EventHandler(this.UngroupShape_Click);
            // 
            // applyNamedColorButton
            // 
            this.applyNamedColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.applyNamedColorButton.Image = ((System.Drawing.Image)(resources.GetObject("applyNamedColorButton.Image")));
            this.applyNamedColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.applyNamedColorButton.Name = "applyNamedColorButton";
            this.applyNamedColorButton.Size = new System.Drawing.Size(37, 24);
            this.applyNamedColorButton.Text = "8";
            this.applyNamedColorButton.Click += new System.EventHandler(this.applyNamedColorButton_Click);
            // 
            // applyRGBAButton
            // 
            this.applyRGBAButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.applyRGBAButton.Image = ((System.Drawing.Image)(resources.GetObject("applyRGBAButton.Image")));
            this.applyRGBAButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.applyRGBAButton.Name = "applyRGBAButton";
            this.applyRGBAButton.Size = new System.Drawing.Size(37, 24);
            this.applyRGBAButton.Text = "applyRGBAButton";
            this.applyRGBAButton.Click += new System.EventHandler(this.applyRGBAButton_Click);
            // 
            // nameChanging
            // 
            this.nameChanging.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nameChanging.Image = ((System.Drawing.Image)(resources.GetObject("nameChanging.Image")));
            this.nameChanging.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nameChanging.Name = "nameChanging";
            this.nameChanging.Size = new System.Drawing.Size(37, 24);
            this.nameChanging.Text = "nameTextBox";
            this.nameChanging.Click += new System.EventHandler(this.nameChanging_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(37, 24);
            this.toolStripButton2.Text = "eyedropperButton ";
            this.toolStripButton2.Click += new System.EventHandler(this.eyedropperButton_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(37, 4);
            // 
            // speedMenu
            // 
            this.speedMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.speedMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.speedMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawRectangleSpeedButton,
            this.drawEllipseButton,
            this.DrawStarSplitButton,
            this.AddPointShape,
            this.AddLineShape,
            this.AddTriangleShapeButton,
            this.SnowflakeStripButton,
            this.pickUpSpeedButton,
            this.GroupObjectsButton,
            this.RotateButton,
            this.ScaleButton,
            this.TranslateButtton,
            this.fillColorButton,
            this.UngroupShape,
            this.applyNamedColorButton,
            this.applyRGBAButton,
            this.nameChanging,
            this.toolStripButton2,
            this.toolStripButton3,
            this.startColorButton,
            this.endColorButton,
            this.borderColorButton,
            this.applyLastColorButton,
            this.NewImageMenuItem});
            this.speedMenu.Location = new System.Drawing.Point(0, 28);
            this.speedMenu.Name = "speedMenu";
            this.speedMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.speedMenu.Size = new System.Drawing.Size(40, 1005);
            this.speedMenu.TabIndex = 3;
            this.speedMenu.Text = "toolStrip1";
            this.speedMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.speedMenu_ItemClicked);
            // 
            // startColorButton
            // 
            this.startColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startColorButton.Image = ((System.Drawing.Image)(resources.GetObject("startColorButton.Image")));
            this.startColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startColorButton.Name = "startColorButton";
            this.startColorButton.Size = new System.Drawing.Size(37, 24);
            this.startColorButton.Text = "startColorButton";
            this.startColorButton.Click += new System.EventHandler(this.startColorButton_Click);
            // 
            // endColorButton
            // 
            this.endColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.endColorButton.Image = ((System.Drawing.Image)(resources.GetObject("endColorButton.Image")));
            this.endColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.endColorButton.Name = "endColorButton";
            this.endColorButton.Size = new System.Drawing.Size(37, 24);
            this.endColorButton.Text = "endColorButton";
            this.endColorButton.ToolTipText = "endColorButton";
            this.endColorButton.Click += new System.EventHandler(this.endColorButton_Click);
            // 
            // borderColorButton
            // 
            this.borderColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.borderColorButton.Image = ((System.Drawing.Image)(resources.GetObject("borderColorButton.Image")));
            this.borderColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.borderColorButton.Name = "borderColorButton";
            this.borderColorButton.Size = new System.Drawing.Size(37, 24);
            this.borderColorButton.Text = "borderColorButton";
            this.borderColorButton.Click += new System.EventHandler(this.borderColorButton_Click);
            // 
            // applyLastColorButton
            // 
            this.applyLastColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.applyLastColorButton.Image = ((System.Drawing.Image)(resources.GetObject("applyLastColorButton.Image")));
            this.applyLastColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.applyLastColorButton.Name = "applyLastColorButton";
            this.applyLastColorButton.Size = new System.Drawing.Size(37, 24);
            this.applyLastColorButton.Text = "applyLastColorButton";
            this.applyLastColorButton.Click += new System.EventHandler(this.applyLastColorButton_Click);
            // 
            // NewImageMenuItem
            // 
            this.NewImageMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewImageMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NewImageMenuItem.Image")));
            this.NewImageMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewImageMenuItem.Name = "NewImageMenuItem";
            this.NewImageMenuItem.Size = new System.Drawing.Size(37, 24);
            this.NewImageMenuItem.Text = "NewImageMenuItem";
            this.NewImageMenuItem.Click += new System.EventHandler(this.NewImageMenuItem_Click);
            // 
            // useGradientCheckBox
            // 
            this.useGradientCheckBox.AutoSize = true;
            this.useGradientCheckBox.Location = new System.Drawing.Point(735, 203);
            this.useGradientCheckBox.Name = "useGradientCheckBox";
            this.useGradientCheckBox.Size = new System.Drawing.Size(163, 20);
            this.useGradientCheckBox.TabIndex = 14;
            this.useGradientCheckBox.Text = "useGradientCheckBox";
            this.useGradientCheckBox.UseVisualStyleBackColor = true;
            this.useGradientCheckBox.CheckedChanged += new System.EventHandler(this.useGradientCheckBox_CheckedChanged);
            // 
            // linearGradientRadioButton
            // 
            this.linearGradientRadioButton.AutoSize = true;
            this.linearGradientRadioButton.Location = new System.Drawing.Point(735, 229);
            this.linearGradientRadioButton.Name = "linearGradientRadioButton";
            this.linearGradientRadioButton.Size = new System.Drawing.Size(186, 20);
            this.linearGradientRadioButton.TabIndex = 15;
            this.linearGradientRadioButton.TabStop = true;
            this.linearGradientRadioButton.Text = "linearGradientRadioButton";
            this.linearGradientRadioButton.UseVisualStyleBackColor = true;
            this.linearGradientRadioButton.CheckedChanged += new System.EventHandler(this.linearGradientRadioButton_CheckedChanged);
            // 
            // radialGradientRadioButton
            // 
            this.radialGradientRadioButton.AutoSize = true;
            this.radialGradientRadioButton.Location = new System.Drawing.Point(735, 255);
            this.radialGradientRadioButton.Name = "radialGradientRadioButton";
            this.radialGradientRadioButton.Size = new System.Drawing.Size(187, 20);
            this.radialGradientRadioButton.TabIndex = 16;
            this.radialGradientRadioButton.TabStop = true;
            this.radialGradientRadioButton.Text = "radialGradientRadioButton";
            this.radialGradientRadioButton.UseVisualStyleBackColor = true;
            this.radialGradientRadioButton.CheckedChanged += new System.EventHandler(this.radialGradientRadioButton_CheckedChanged);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // cutToolStripMenuItem1
            // 
            this.cutToolStripMenuItem1.Name = "cutToolStripMenuItem1";
            this.cutToolStripMenuItem1.Size = new System.Drawing.Size(112, 24);
            this.cutToolStripMenuItem1.Text = "Cut";
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(112, 24);
            this.copyToolStripMenuItem1.Text = "Copy";
            // 
            // pasteToolStripMenuItem1
            // 
            this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
            this.pasteToolStripMenuItem1.Size = new System.Drawing.Size(112, 24);
            this.pasteToolStripMenuItem1.Text = "Paste";
            // 
            // viewPort
            // 
            this.viewPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPort.Location = new System.Drawing.Point(40, 28);
            this.viewPort.Margin = new System.Windows.Forms.Padding(5);
            this.viewPort.Name = "viewPort";
            this.viewPort.Size = new System.Drawing.Size(1862, 1005);
            this.viewPort.TabIndex = 4;
            this.viewPort.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewPortPaint);
            this.viewPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewPort_KeyDown);
            this.viewPort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseDown);
            this.viewPort.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseMove);
            this.viewPort.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1055);
            this.Controls.Add(this.numericLineWidth);
            this.Controls.Add(this.radialGradientRadioButton);
            this.Controls.Add(this.linearGradientRadioButton);
            this.Controls.Add(this.useGradientCheckBox);
            this.Controls.Add(this.colorPreview);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.bTextBox);
            this.Controls.Add(this.gTextBox);
            this.Controls.Add(this.aTextBox);
            this.Controls.Add(this.rTextBox);
            this.Controls.Add(this.trackOpacity);
            this.Controls.Add(this.viewPort);
            this.Controls.Add(this.speedMenu);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Draw";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLineWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackOpacity)).EndInit();
            this.speedMenu.ResumeLayout(false);
            this.speedMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ToolStripStatusLabel currentStatusLabel;
		private Draw.DoubleBufferedPanel viewPort;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem newColorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yellowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.NumericUpDown numericLineWidth;
        //private System.Windows.Forms.TrackBar TrackOpacity;
        private System.Windows.Forms.TrackBar trackOpacity;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
        private ToolStripMenuItem openToolStripMenuItem;
        private TextBox rTextBox;
        private TextBox aTextBox;
        private TextBox gTextBox;
        private TextBox bTextBox;
        private TextBox nameTextBox;
        private Panel colorPreview;
        private ToolStripButton drawRectangleSpeedButton;
        private ToolStripButton drawEllipseButton;
        private ToolStripButton DrawStarSplitButton;
        private ToolStripButton AddLineShape;
        private ToolStripButton SnowflakeStripButton;
        private ToolStripButton pickUpSpeedButton;
        private ToolStripButton AddTriangleShapeButton;
        private ToolStripButton AddPointShape;
        private ToolStripButton GroupObjectsButton;
        private ToolStripButton RotateButton;
        private ToolStripButton ScaleButton;
        private ToolStripButton TranslateButtton;
        private ToolStripButton fillColorButton;
        private ToolStripButton UngroupShape;
        private ToolStripButton applyNamedColorButton;
        private ToolStripButton applyRGBAButton;
        private ToolStripButton nameChanging;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStrip speedMenu;
        private ToolStripButton startColorButton;
        private ToolStripButton endColorButton;
        private ToolStripSeparator toolStripSeparator1;
        private CheckBox useGradientCheckBox;
        private RadioButton linearGradientRadioButton;
        private RadioButton radialGradientRadioButton;
        private ToolStripButton borderColorButton;
        private ToolStripButton applyLastColorButton;
        private ToolStripButton NewImageMenuItem;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem cutToolStripMenuItem1;
        private ToolStripMenuItem copyToolStripMenuItem1;
        private ToolStripMenuItem pasteToolStripMenuItem1;
    }
}
