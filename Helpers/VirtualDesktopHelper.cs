using System.Runtime.InteropServices;
using System.Windows;

namespace chatbot_in_pocket.Helpers
{
    public static class VirtualDesktopHelper
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_APPWINDOW = 0x00040000;
        private static int firstWindowStyle = 0;

        public static void MakeWindowVisibleOnAllDesktops(IntPtr hwnd)
        {
            if (firstWindowStyle == 0)
                firstWindowStyle = GetWindowLong(hwnd, GWL_EXSTYLE);

            SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_TOOLWINDOW);
        }

        public static void RemoveWindowFromAllDesktops(IntPtr hwnd)
        {
            SetWindowLong(hwnd, GWL_EXSTYLE, firstWindowStyle);
        }
    }
}
