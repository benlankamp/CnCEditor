using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CnCEditor.GUI
{
    public static class NativeMethods
    {
        #region Suspend
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;
        public static IDisposable BeginSuspendlock(this Control ctrl)
        {
            return new suspender(ctrl);
        }

        public static void StartSuspend(this Control ctrl)
        {
            SendMessage(ctrl.Handle, WM_SETREDRAW, false, 0);
        }

        public static void EndSuspend(this Control ctrl)
        {
            SendMessage(ctrl.Handle, WM_SETREDRAW, true, 0);
            ctrl.Refresh();
        }

        private class suspender : IDisposable
        {
            private readonly Control _ctrl;
            public suspender(Control ctrl)
            {
                _ctrl = ctrl;
                SendMessage(_ctrl.Handle, WM_SETREDRAW, false, 0);
            }
            public void Dispose()
            {
                SendMessage(_ctrl.Handle, WM_SETREDRAW, true, 0);
                _ctrl.Refresh();
            }
        }
        #endregion
    }
}
