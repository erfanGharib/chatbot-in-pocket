using System.Windows;
using chatbot_in_pocket.Controls;
using chatbot_in_pocket.Utils;

namespace chatbot_in_pocket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    // TODO change combo box background

    public partial class MainWindow : Window
    {
        public bool isWindowPinned = false;
        public bool isHidden = true;
        public double loc = 0;

        public MainWindow()
        {
            SavedConfigUtil.InitConfig();

            var webView = new WebView();
            var closeButton = new CloseButton();
            var pinButton = new PinButton();
            var showOnAllDesktopsButton = new ShowOnAllDesktopsButton();
            var chatbotsComboBox = new ChatbotsComboBox();

            InitializeComponent();

            webView.InitializeWebView(this);
            closeButton.InitializeComponent(this);
            pinButton.InitializeComponent(this);
            showOnAllDesktopsButton.InitializeComponent(this);
            chatbotsComboBox.InitializeComponent(this);

            loc = Width;

            Topmost = true;
            ShowInTaskbar = false;
            Top = 68;

            HideWindow();
        }

        public void ShowWindow()
        {
            isHidden = false;
            loc = 0;

            while (loc <= Width)
            {
                loc += 4;
                Left = SystemParameters.WorkArea.Width - loc + 2;
            }
        }

        public void HideWindow()
        {
            // prevent window from hiding if window is pinned
            if (isWindowPinned) return;

            isHidden = true;
            loc = 0;

            while (loc <= Width)
            {
                Left = SystemParameters.WorkArea.Width - Width + loc - 3;
                loc += 4;
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            HideWindow();
        }
    }
}