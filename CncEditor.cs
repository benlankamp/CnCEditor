using CnCEditor.FileFormats;
using System;
using System.Windows.Forms;

namespace CnCEditor
{
    static class CncEditor
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MIXDatabase.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
