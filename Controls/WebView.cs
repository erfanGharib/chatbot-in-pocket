using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace chatbot_in_pocket.Controls
{
    public class WebView
    {
        private static bool isHidden;
        private MainWindow mainWindow;
        public double loc = 0;

        public void InitializeWebView(MainWindow _mainWindow)
        {
            isHidden = true;
            mainWindow = _mainWindow;

            loc = mainWindow.Width;

            mainWindow.webView.CoreWebView2InitializationCompleted += WebView21_CoreWebView2InitializationCompleted;
            mainWindow.webView.WebMessageReceived += WebView21_WebMessageReceived;
            mainWindow.Deactivated += Window_Deactivated;

            HideForm();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            HideForm();
        }

        private async void WebView21_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            string script = File.ReadAllText("index.js");
            await mainWindow.webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(script);
        }

        public struct JsonObject
        {
            public string key;
        }

        private void WebView21_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            JsonObject jsonObject = JsonConvert.DeserializeObject<JsonObject>(e.WebMessageAsJson);
            if (jsonObject.key == "click" && isHidden)
                ShowForm();
        }

        public void ShowForm()
        {
            isHidden = false;
            loc = 0;

            while (loc <= mainWindow.Width)
            {
                loc += 4;
                mainWindow.Left = SystemParameters.WorkArea.Width - loc + 2;
            }
        }

        public void HideForm()
        {
            // prevent window from hiding if part of application clicked
            if (mainWindow.isWindowClicked) return;
            // prevent window from hiding if window is pinned
            if (mainWindow.isWindowPinned) return;

            isHidden = true;
            loc = 0;

            while (loc <= mainWindow.Width)
            {
                mainWindow.Left = SystemParameters.WorkArea.Width - mainWindow.Width + loc - 3;
                loc += 4;
            }
        }
    }
}
