using System.IO;
using System.Windows.Forms;
using CnCEditor.FileFormats;

namespace CnCEditor
{
    public partial class MainForm
    {
        public void OpenContainerFile()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo selectedFile = new FileInfo(openFileDialog.FileName);

                switch (selectedFile.Extension.ToLower())
                {
                    case ".mix":
                        MIXFile mixFile;

                        if (!openFiles.ContainsKey(openFileDialog.FileName))
                        {
                            mixFile = new MIXFile(openFileDialog.FileName);
                            openFiles.Add(openFileDialog.FileName, mixFile);
                        }
                        else
                        {
                            mixFile = openFiles[openFileDialog.FileName];
                        }

                        if (!mixFile.IsValid)
                        {
                            MessageBox.Show("Invalid MIX file!", "Error loading file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        UpdateMemoryUsage();

                        TreeNode rootNode = new TreeNode(openFileDialog.FileName);
                        rootNode.ImageIndex = 0;
                        SolveFiles(ref mixFile, rootNode);
                        treeItems.Nodes.Add(rootNode);

                        treeItems.Sort();

                        break;
                }
            }
        }
    }
}
