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
            this.containerFileMIXMEGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewImages = new System.Windows.Forms.ImageList(this.components);
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.memoryUsageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.memoryUsage = new System.Windows.Forms.ToolStripProgressBar();
            this.memoryUsageValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageFilePreviews = new System.Windows.Forms.ImageList(this.components);
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.treeItems = new System.Windows.Forms.TreeView();
            this.splitRightVertical = new System.Windows.Forms.SplitContainer();
            this.textFileViewer = new ScintillaNET.Scintilla();
            this.listContainerContents = new System.Windows.Forms.ListView();
            this.mainMenuStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRightVertical)).BeginInit();
            this.splitRightVertical.Panel1.SuspendLayout();
            this.splitRightVertical.Panel2.SuspendLayout();
            this.splitRightVertical.SuspendLayout();
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
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.containerFileMIXMEGToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // containerFileMIXMEGToolStripMenuItem
            // 
            this.containerFileMIXMEGToolStripMenuItem.Name = "containerFileMIXMEGToolStripMenuItem";
            this.containerFileMIXMEGToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.containerFileMIXMEGToolStripMenuItem.Text = "Container File (MIX, MEG)";
            this.containerFileMIXMEGToolStripMenuItem.Click += new System.EventHandler(this.containerFileMIXMEGToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.closeToolStripMenuItem.Text = "&Close all";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(115, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // treeViewImages
            // 
            this.treeViewImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImages.ImageStream")));
            this.treeViewImages.TransparentColor = System.Drawing.Color.Transparent;
            this.treeViewImages.Images.SetKeyName(0, "box.png");
            this.treeViewImages.Images.SetKeyName(1, "document-text.png");
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
            // memoryUsageLabel
            // 
            this.memoryUsageLabel.Name = "memoryUsageLabel";
            this.memoryUsageLabel.Size = new System.Drawing.Size(89, 17);
            this.memoryUsageLabel.Text = "Memory usage:";
            // 
            // memoryUsage
            // 
            this.memoryUsage.Maximum = 2048;
            this.memoryUsage.Name = "memoryUsage";
            this.memoryUsage.Size = new System.Drawing.Size(250, 16);
            this.memoryUsage.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // memoryUsageValue
            // 
            this.memoryUsageValue.Name = "memoryUsageValue";
            this.memoryUsageValue.Size = new System.Drawing.Size(37, 17);
            this.memoryUsageValue.Text = "0 MiB";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "MIX files (*.mix)|*.mix";
            // 
            // imageFilePreviews
            // 
            this.imageFilePreviews.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageFilePreviews.ImageSize = new System.Drawing.Size(64, 64);
            this.imageFilePreviews.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.treeItems);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.splitRightVertical);
            this.mainSplitContainer.Size = new System.Drawing.Size(1017, 603);
            this.mainSplitContainer.SplitterDistance = 339;
            this.mainSplitContainer.TabIndex = 6;
            // 
            // treeItems
            // 
            this.treeItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeItems.HideSelection = false;
            this.treeItems.ImageIndex = 0;
            this.treeItems.ImageList = this.treeViewImages;
            this.treeItems.Location = new System.Drawing.Point(0, 0);
            this.treeItems.Name = "treeItems";
            this.treeItems.SelectedImageIndex = 0;
            this.treeItems.Size = new System.Drawing.Size(339, 603);
            this.treeItems.TabIndex = 3;
            this.treeItems.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeItems_NodeMouseDoubleClick);
            // 
            // splitRightVertical
            // 
            this.splitRightVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRightVertical.Location = new System.Drawing.Point(0, 0);
            this.splitRightVertical.Name = "splitRightVertical";
            this.splitRightVertical.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitRightVertical.Panel1
            // 
            this.splitRightVertical.Panel1.Controls.Add(this.textFileViewer);
            // 
            // splitRightVertical.Panel2
            // 
            this.splitRightVertical.Panel2.Controls.Add(this.listContainerContents);
            this.splitRightVertical.Size = new System.Drawing.Size(674, 603);
            this.splitRightVertical.SplitterDistance = 432;
            this.splitRightVertical.TabIndex = 0;
            // 
            // textFileViewer
            // 
            this.textFileViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textFileViewer.Lexer = ScintillaNET.Lexer.Properties;
            this.textFileViewer.Location = new System.Drawing.Point(0, 0);
            this.textFileViewer.Name = "textFileViewer";
            this.textFileViewer.Size = new System.Drawing.Size(674, 432);
            this.textFileViewer.TabIndex = 4;
            // 
            // listContainerContents
            // 
            this.listContainerContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listContainerContents.HideSelection = false;
            this.listContainerContents.Location = new System.Drawing.Point(0, 0);
            this.listContainerContents.Name = "listContainerContents";
            this.listContainerContents.Size = new System.Drawing.Size(674, 167);
            this.listContainerContents.TabIndex = 6;
            this.listContainerContents.UseCompatibleStateImageBehavior = false;
            this.listContainerContents.DoubleClick += new System.EventHandler(this.listContainerContents_DoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 649);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.mainStatusStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "CNC Editor";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.splitRightVertical.Panel1.ResumeLayout(false);
            this.splitRightVertical.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRightVertical)).EndInit();
            this.splitRightVertical.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ImageList treeViewImages;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripProgressBar memoryUsage;
        private System.Windows.Forms.ToolStripStatusLabel memoryUsageLabel;
        private System.Windows.Forms.ToolStripStatusLabel memoryUsageValue;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem containerFileMIXMEGToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ImageList imageFilePreviews;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.TreeView treeItems;
        private System.Windows.Forms.SplitContainer splitRightVertical;
        private ScintillaNET.Scintilla textFileViewer;
        private System.Windows.Forms.ListView listContainerContents;
    }
}

