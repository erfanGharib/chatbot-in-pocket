using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace chatbot_in_pocket.Controls
{
    internal class PinButton
    {
        private MainWindow mainWindow;
        public void InitializeComponent(MainWindow _mainWindow) {
            mainWindow = _mainWindow;
            mainWindow.pinButton.Click += PinButton_Click;
            mainWindow.pinButton.MouseEnter += PinButton_MouseEnter;
            mainWindow.pinButton.MouseLeave += PinButton_MouseLeave;
        }
        private void ChangeButtonBackground(bool removeBg)
        {
            if(removeBg) {
                mainWindow.pinButton.Background = null; 
                return;
            }

            BrushConverter bc = new BrushConverter();
            mainWindow.pinButton.Background = (Brush)bc.ConvertFrom("#444");
        }
        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.isWindowPinned = !mainWindow.isWindowPinned;

            if (mainWindow.isWindowPinned)
            {
                ChangeButtonBackground(false);
                return;
            }
            ChangeButtonBackground(true);
        }
        private void PinButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeButtonBackground(false);
        }
        private void PinButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!mainWindow.isWindowPinned)
                ChangeButtonBackground(true);
        }
    }
}
