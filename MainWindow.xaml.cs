using System.Windows;
using chatbot_in_pocket.Controls;

namespace chatbot_in_pocket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    // TODO change combo box background
    // TODO choose default chatbot

    public partial class MainWindow : Window
    {
        public bool isWindowPinned = false;
        public bool isWindowClicked = false;
        public bool isShowedOnAllDesktops = false;

        public MainWindow()
        {
            var webView = new WebView();
            var closeButton = new CloseButton();
            var pinButton = new PinButton();
            var showOnAllDesktopsButton = new ShowOnAllDesktopsButton();
            var chatbotsComboBox = new ChatbotsComboBox();

            InitializeComponent();

            Topmost = true; 
            ShowInTaskbar = false;
            Top = 68;

            webView.InitializeWebView(this);
            closeButton.InitializeComponent(this);
            pinButton.InitializeComponent(this);
            showOnAllDesktopsButton.InitializeComponent(this);
            chatbotsComboBox.InitializeComponent(this);
        }

        private void Window_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isWindowClicked = true;
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            isWindowClicked = false;
        }
    }
}