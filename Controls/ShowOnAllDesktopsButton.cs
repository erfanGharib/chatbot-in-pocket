using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using chatbot_in_pocket.Helpers;

namespace chatbot_in_pocket.Controls
{
    internal class ShowOnAllDesktopsButton
    {
        private MainWindow mainWindow;
        public void InitializeComponent(MainWindow _mainWindow) {
            mainWindow = _mainWindow;
            mainWindow.showOnAllDesktopsButton.Click += showOnAllDesktopsButton_Click;
            mainWindow.showOnAllDesktopsButton.MouseEnter += showOnAllDesktopsButton_MouseEnter;
            mainWindow.showOnAllDesktopsButton.MouseLeave += showOnAllDesktopsButton_MouseLeave;
        }
        private void ChangeButtonBackground(bool removeBg)
        {
            if(removeBg) {
                mainWindow.showOnAllDesktopsButton.Background = null; 
                return;
            }

            BrushConverter bc = new BrushConverter();
            mainWindow.showOnAllDesktopsButton.Background = (Brush)bc.ConvertFrom("#444");
        }
        private void showOnAllDesktopsButton_Click(object sender, RoutedEventArgs e)
        {
            IntPtr handle = new System.Windows.Interop.WindowInteropHelper(mainWindow).Handle;
            mainWindow.isShowedOnAllDesktops = !mainWindow.isShowedOnAllDesktops;

            if (mainWindow.isShowedOnAllDesktops)
            {
                VirtualDesktopHelper.MakeWindowVisibleOnAllDesktops(handle);
                ChangeButtonBackground(false);
                return;
            }

            VirtualDesktopHelper.RemoveWindowFromAllDesktops(handle);
            ChangeButtonBackground(true);
        }
        private void showOnAllDesktopsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeButtonBackground(false);
        }
        private void showOnAllDesktopsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!mainWindow.isShowedOnAllDesktops)
                ChangeButtonBackground(true);
        }
    }
}
