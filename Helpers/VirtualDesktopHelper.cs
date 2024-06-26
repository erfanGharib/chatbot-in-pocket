using System.Runtime.InteropServices;

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

        public static void MakeWindowVisibleOnAllDesktops(IntPtr hwnd)
        {
            int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);

            exStyle &= ~WS_EX_APPWINDOW;
            exStyle |= WS_EX_TOOLWINDOW;

            SetWindowLong(hwnd, GWL_EXSTYLE, exStyle);
        }

        public static void RemoveWindowFromAllDesktops(IntPtr hwnd)
        {
            int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);

            exStyle &= ~WS_EX_TOOLWINDOW;
            exStyle |= WS_EX_APPWINDOW;

            SetWindowLong(hwnd, GWL_EXSTYLE, exStyle);
        }
    }
}
