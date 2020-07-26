using CnCEditor.FileFormats;
using ScintillaNET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CnCEditor.GUI;

namespace CnCEditor
{
    public partial class MainForm : Form
    {
        public Dictionary<string,MIXFile> openFiles = new Dictionary<string,MIXFile>();

        public MainForm()
        {
            InitializeComponent();

            UpdateMemoryUsage();

            textFileViewer.StyleResetDefault();
            textFileViewer.Styles[Style.Default].Font = "Consolas";
            textFileViewer.Styles[Style.Default].Size = 10;
            textFileViewer.StyleClearAll();

            textFileViewer.Styles[Style.Properties.Section].ForeColor = Color.Blue;
            textFileViewer.Styles[Style.Properties.Key].ForeColor = Color.FromArgb(163, 21, 21);
            textFileViewer.Styles[Style.Properties.Comment].ForeColor = Color.FromArgb(0, 128, 0);
            textFileViewer.Styles[Style.Properties.DefVal].ForeColor = Color.Gray;
        }

        private void SolveFiles( ref MIXFile mixFile, TreeNode rootNode )
        {
            foreach (var file in mixFile.Files)
            {
                FileInfo selectedFile = new FileInfo(file.Name);

                switch (selectedFile.Extension.ToLower())
                {
                    case ".mix":
                        MIXFile embeddedMixFile;

                        if (!openFiles.ContainsKey(file.Name))
                        {
                            SubStream embeddedFile = mixFile.GetFileAsStream(file);
//                            byte[] embeddedFile = mixFile.GetFile(file);
                            embeddedMixFile = new MIXFile(embeddedFile, file.Name);
                            openFiles.Add(file.Name, embeddedMixFile);
                        }
                        else
                        {
                            embeddedMixFile = openFiles[file.Name];
                        }

                        TreeNode subRootNode = new TreeNode(selectedFile.Name);
                        subRootNode.Tag = selectedFile.Name;
                        subRootNode.SelectedImageIndex = 0;
                        subRootNode.ImageIndex = 0;

                        SolveFiles(ref embeddedMixFile, subRootNode);

                        rootNode.Nodes.Add(subRootNode);

                        UpdateMemoryUsage();

                        break;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UpdateMemoryUsage()
        {
            using (Process proc = Process.GetCurrentProcess())
            {
                float memoryUsageInMiB = (float)(proc.PrivateMemorySize64 / 1024.0 / 1024.0);
                memoryUsageValue.Text = $"{memoryUsageInMiB:0.0} MiB";

                memoryUsage.Value = (int)memoryUsageInMiB;
                Application.DoEvents();
            }
        }

        private void treeItems_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node.Tag != null)
            {
                string mixFile = (string)e.Node.Tag;

                var fileInfo = new FileInfo(mixFile);

                switch (fileInfo.Extension)
                {
                    case ".mix":
                        listContainerContents.Items.Clear();
                        imageFilePreviews.Images.Clear();

                        listContainerContents.StartSuspend();

                        // show contents of container
                        foreach (MIXFile.SubFile subFile in openFiles[mixFile].Files)
                        {
                            var subFileInfo = new FileInfo(subFile.Name);
                            switch (subFileInfo.Extension)
                            {
                                case ".sno":
                                case ".tem":
                                case ".int":
                                    // read file
                                    var rawFile = new TilesetFile(openFiles[mixFile].GetFile(subFile.Name));

                                    if (rawFile.tileImage != null)
                                    {
                                        // use the preview image
                                        imageFilePreviews.Images.Add( FilePreviews.CenteredBitmap(rawFile.tileImage) );

                                        listContainerContents.Items.Add(new ListViewItem()
                                        {
                                            Text = subFileInfo.Name,
                                            ImageIndex = imageFilePreviews.Images.Count
                                        });

                                        break;
                                    }

                                    goto default;
                                default:
                                    listContainerContents.Items.Add(new ListViewItem()
                                    {
                                        Text = subFileInfo.Name,
                                        Tag = ( MixFile: mixFile, FileName: subFile.Name )
                                    });
                                    break;
                            }
                        }

                        listContainerContents.LargeImageList = imageFilePreviews;
                        listContainerContents.Sorting = SortOrder.Ascending;

                        listContainerContents.EndSuspend();

                        break;
                }
            }
        }

        private void CalculateFolding()
        {
            // Instruct the lexer to calculate folding
            textFileViewer.SetProperty("fold", "1");
            textFileViewer.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            textFileViewer.Margins[2].Type = MarginType.Symbol;
            textFileViewer.Margins[2].Mask = Marker.MaskFolders;
            textFileViewer.Margins[2].Sensitive = true;
            textFileViewer.Margins[2].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                textFileViewer.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                textFileViewer.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            textFileViewer.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            textFileViewer.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            textFileViewer.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            textFileViewer.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            textFileViewer.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            textFileViewer.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            textFileViewer.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            textFileViewer.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while(openFiles.Any() ) { 
                openFiles.Last().Value.Dispose();
                openFiles.Remove( openFiles.Last().Key );
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            treeItems.Nodes.Clear();
            textFileViewer.Text = "";

            UpdateMemoryUsage();
        }

        private void containerFileMIXMEGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenContainerFile();
        }

        private void listContainerContents_DoubleClick(object sender, EventArgs e)
        {
            if (listContainerContents.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = listContainerContents.SelectedItems[0];
                if (selectedItem.Tag != null)
                {
                    (string mixFile, string listFileName) = (ValueTuple<string, string>) (selectedItem.Tag);

                    var subFileInfo = new FileInfo(listFileName);
                    switch (subFileInfo.Extension)
                    {
                        case ".ini":
                            byte[] rawFile = openFiles[mixFile].GetFile(listFileName);

                            textFileViewer.Lexer = Lexer.Properties;
                            textFileViewer.Text = Encoding.UTF8.GetString(rawFile);

                            CalculateFolding();

                            break;
                    }
                }
            }
        }
    }
}
