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

            mainWindow.webView.CoreWebView2InitializationCompleted += WebView21_CoreWebView2InitializationCompleted;
            mainWindow.webView.WebMessageReceived += WebView21_WebMessageReceived;
            mainWindow.Deactivated += Window_Deactivated;

            HideWindow();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            HideWindow();
        }

        private async void WebView21_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            try
            {
                string script = File.ReadAllText("index.js");
                await mainWindow.webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(script);
            } catch(FileNotFoundException)
            {
                Console.WriteLine("no index.js found");
            }
        }

        public struct JsonObject
        {
            public string key;
        }

        private void WebView21_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            JsonObject jsonObject = JsonConvert.DeserializeObject<JsonObject>(e.WebMessageAsJson);
            if (jsonObject.key == "click" && isHidden)
                ShowWindow();
        }

        public void ShowWindow()
        {
            isHidden = false;
            loc = 0;

            while (loc <= mainWindow.Width)
            {
                loc += 4;
                mainWindow.Left = SystemParameters.WorkArea.Width - loc + 2;
            }

            loc = 0;
        }

        public void HideWindow()
        {
            // prevent window from hiding if window is pinned
            if (mainWindow.isWindowPinned) return;
            // run this method when the window has been shown
            if (loc != 0) return;

            isHidden = true;
            loc = 0;

            while (loc <= mainWindow.Width)
            {
                mainWindow.Left = SystemParameters.WorkArea.Width - mainWindow.Width + loc - 6;
                loc += 4;
            }
        }
    }
}
