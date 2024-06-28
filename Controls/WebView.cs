using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System.IO;

namespace chatbot_in_pocket.Controls
{
    public class WebView
    {
        private MainWindow mainWindow;

        public void InitializeWebView(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;

            mainWindow.webView.CoreWebView2InitializationCompleted += WebView21_CoreWebView2InitializationCompleted;
            mainWindow.webView.WebMessageReceived += WebView21_WebMessageReceived;
        }

        private async void WebView21_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            try
            {
                string script = File.ReadAllText("index.js");
                await mainWindow.webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(script);
            } catch(FileNotFoundException)
            {
                Console.WriteLine("no index.js found!");
            }
        }

        public struct JsonObject
        {
            public string key;
        }

        private void WebView21_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            JsonObject jsonObject = JsonConvert.DeserializeObject<JsonObject>(e.WebMessageAsJson);
            if (jsonObject.key == "click" && mainWindow.isHidden)
                mainWindow.ShowWindow();
        }
    }
}
