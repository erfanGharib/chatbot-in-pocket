using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace chatbot_in_pocket.Controls
{
    internal class CloseButton
    {
        private MainWindow mainWindow;
        public void InitializeComponent(MainWindow _mainWindow) {
            mainWindow = _mainWindow;

            mainWindow.closeButton.Click += CloseButton_Click;
            mainWindow.closeButton.MouseEnter += CloseButton_MouseEnter;
            mainWindow.closeButton.MouseLeave += CloseButton_MouseLeave;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            mainWindow.closeButton.Background = (Brush)bc.ConvertFrom("#444");
        }
        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            mainWindow.closeButton.Background = null;
        }
    }
}
