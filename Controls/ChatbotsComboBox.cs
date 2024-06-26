using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace chatbot_in_pocket.Controls
{
    internal class ChatbotsComboBox
    {
        public class Chatbot
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public CheckBox checkBox { get; set; }
        }

        public List<Chatbot> chatbots;
        private MainWindow mainWindow;

        public void InitializeComponent(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;

            mainWindow.chatbotsComboBox.SelectionChanged += ComboBox_SelectionChanged;
            chatbots = new List<Chatbot> {
                new Chatbot { Name = "ChatGPT", Value = "https://chatgpt.com" },
                new Chatbot { Name = "Poe", Value = "https://poe.com" },
                new Chatbot { Name = "Gemini", Value = "https://gemini.google.com/app?hl=en" },
                new Chatbot { Name = "Copilot", Value = "https://copilot.microsoft.com" }
            };

            mainWindow.chatbotsComboBox.ItemsSource = new ObservableCollection<ComboBoxItem>(chatbots.Select(val => {
                var checkBox = new CheckBox 
                {
                    ToolTip = "Default Chatbot",
                    Margin = new Thickness {
                        Top = 5
                    }
                };
                var label = new Label 
                { 
                    Content = val.Name,
                    Margin = new Thickness
                    {
                        Top = 1
                    }
                };
                var stackPanel = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Orientation = Orientation.Horizontal,
                    Children =
                    {
                        checkBox,
                        label,
                    }
                };
                var comboBoxItem = new ComboBoxItem 
                { 
                    Content = stackPanel
                };

                return comboBoxItem;
            }));
            mainWindow.chatbotsComboBox.SelectedIndex = 0;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)mainWindow.chatbotsComboBox.SelectedItem;

            if (selectedItem == null) return;

            var selectedItemTextValue = ((Label)((StackPanel)selectedItem.Content).Children[1]).Content.ToString();

            List<Chatbot> filteredChatbots = chatbots.Where(val => val.Name == selectedItemTextValue).ToList();
            Uri uri = new Uri(filteredChatbots[0].Value);

            mainWindow.webView.Source = uri;
        }
    }
}
