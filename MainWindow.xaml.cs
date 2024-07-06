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

            Topmost = true;
            ShowInTaskbar = false;
            Top = SystemParameters.WorkArea.Height - Height;
        }
    }
}