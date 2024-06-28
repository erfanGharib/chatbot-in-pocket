using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using static chatbot_in_pocket.Utils.SavedConfigUtil;

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

        private SavedConfig savedConfig = new SavedConfig();
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

                checkBox.Checked += CheckBox_Checked;

                return comboBoxItem;
            }));

            mainWindow.chatbotsComboBox.SelectedIndex = savedConfig.defaultChatbot;
            var checkBox = GetComboBoxItemByIndex(savedConfig.defaultChatbot).checkBox;
            checkBox.IsChecked = true;
        }

        private class ComboboxItemContent {
            public CheckBox checkBox;
            public Label label;
        } 
        private ComboboxItemContent GetComboBoxItemByIndex(int index)
        {
            var comboBoxItems = mainWindow.chatbotsComboBox.Items;
            var stackPanelContent = (StackPanel)((ComboBoxItem)comboBoxItems[index]).Content;
            var currentCheckBox = (CheckBox)(stackPanelContent).Children[0];
            var currentLabel = (Label)(stackPanelContent).Children[1];

            return new ComboboxItemContent()
            {
                checkBox = currentCheckBox,
                label = currentLabel
            };
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var comboBoxItems = mainWindow.chatbotsComboBox.Items;
            var senderParent = (sender as CheckBox).Parent as StackPanel;

            for (int index = 0; index < comboBoxItems.Count; index++)
            {
                var currentComboBoxItem = GetComboBoxItemByIndex(index);
                if (currentComboBoxItem.label.Content != ((Label)senderParent.Children[1]).Content)
                {
                    currentComboBoxItem.checkBox.IsChecked = false;
                    continue;
                }

                savedConfig.defaultChatbot = index;
            }
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
