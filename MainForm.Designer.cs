namespace CnCEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.treeItems = new System.Windows.Forms.TreeView();
            this.treeViewImages = new System.Windows.Forms.ImageList(this.components);
            this.textFileViewer = new ScintillaNET.Scintilla();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.memoryUsage = new System.Windows.Forms.ToolStripProgressBar();
            this.memoryUsageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.memoryUsageValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenuStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1017, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "MIX files|*.mix";
            // 
            // treeItems
            // 
            this.treeItems.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeItems.HideSelection = false;
            this.treeItems.ImageIndex = 0;
            this.treeItems.ImageList = this.treeViewImages;
            this.treeItems.Location = new System.Drawing.Point(0, 24);
            this.treeItems.Name = "treeItems";
            this.treeItems.SelectedImageIndex = 0;
            this.treeItems.Size = new System.Drawing.Size(385, 603);
            this.treeItems.TabIndex = 2;
            this.treeItems.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeItems_NodeMouseDoubleClick);
            // 
            // treeViewImages
            // 
            this.treeViewImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImages.ImageStream")));
            this.treeViewImages.TransparentColor = System.Drawing.Color.Transparent;
            this.treeViewImages.Images.SetKeyName(0, "box.png");
            this.treeViewImages.Images.SetKeyName(1, "document-text.png");
            // 
            // textFileViewer
            // 
            this.textFileViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textFileViewer.Lexer = ScintillaNET.Lexer.Properties;
            this.textFileViewer.Location = new System.Drawing.Point(385, 24);
            this.textFileViewer.Name = "textFileViewer";
            this.textFileViewer.Size = new System.Drawing.Size(632, 603);
            this.textFileViewer.TabIndex = 3;
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memoryUsageLabel,
            this.memoryUsage,
            this.memoryUsageValue});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 627);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(1017, 22);
            this.mainStatusStrip.TabIndex = 4;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // memoryUsage
            // 
            this.memoryUsage.Maximum = 2048;
            this.memoryUsage.Name = "memoryUsage";
            this.memoryUsage.Size = new System.Drawing.Size(250, 16);
            this.memoryUsage.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // memoryUsageLabel
            // 
            this.memoryUsageLabel.Name = "memoryUsageLabel";
            this.memoryUsageLabel.Size = new System.Drawing.Size(89, 17);
            this.memoryUsageLabel.Text = "Memory usage:";
            // 
            // memoryUsageValue
            // 
            this.memoryUsageValue.Name = "memoryUsageValue";
            this.memoryUsageValue.Size = new System.Drawing.Size(37, 17);
            this.memoryUsageValue.Text = "0 MiB";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 649);
            this.Controls.Add(this.textFileViewer);
            this.Controls.Add(this.treeItems);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.mainStatusStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "CNC Editor";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TreeView treeItems;
        private ScintillaNET.Scintilla textFileViewer;
        private System.Windows.Forms.ImageList treeViewImages;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripProgressBar memoryUsage;
        private System.Windows.Forms.ToolStripStatusLabel memoryUsageLabel;
        private System.Windows.Forms.ToolStripStatusLabel memoryUsageValue;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

