using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using chatbot_in_pocket.Helpers;
using static chatbot_in_pocket.Utils.SavedConfigUtil;
using System.Windows.Interop;

namespace chatbot_in_pocket.Controls
{
    internal class ShowOnAllDesktopsButton
    {
        private MainWindow mainWindow;
        private SavedConfig savedConfig = new SavedConfig();

        public void InitializeComponent(MainWindow _mainWindow) {
            mainWindow = _mainWindow;

            StateChangeHandler(savedConfig);
            OnUpdateConfig += new UpdateHandler(StateChangeHandler);

            mainWindow.showOnAllDesktopsButton.Click += showOnAllDesktopsButton_Click;
            mainWindow.showOnAllDesktopsButton.MouseEnter += showOnAllDesktopsButton_MouseEnter;
            mainWindow.showOnAllDesktopsButton.MouseLeave += showOnAllDesktopsButton_MouseLeave;
        }
        private void StateChangeHandler(SavedConfig savedConfig)
        {
            var handle = new WindowInteropHelper(mainWindow).EnsureHandle();

            if (savedConfig.isShowedOnAllDesktops)
            {
                VirtualDesktopHelper.MakeWindowVisibleOnAllDesktops(handle);
                ChangeButtonBackground(false);
                return;
            }

            VirtualDesktopHelper.RemoveWindowFromAllDesktops(handle);
            ChangeButtonBackground(true);
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
            savedConfig.isShowedOnAllDesktops = !savedConfig.isShowedOnAllDesktops;
        }
        private void showOnAllDesktopsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeButtonBackground(false);
        }
        private void showOnAllDesktopsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!savedConfig.isShowedOnAllDesktops)
                ChangeButtonBackground(true);
        }
    }
}
