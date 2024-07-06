using System.IO;
using System.Text.Json;

namespace chatbot_in_pocket.Utils
{
    internal class SavedConfigUtil
    {
        public delegate void UpdateHandler(SavedConfig savedConfig);

        private static string filePath = "";
        private static int _defaultChatbot = 0;
        private static bool _isShowedOnAllDesktops = false;
        public static event UpdateHandler OnUpdateConfig;

        public class SavedConfig
        {
            public int defaultChatbot
            {
                get => _defaultChatbot;
                set
                {
                    if (_defaultChatbot != value)
                    {
                        _defaultChatbot = value;
                        WriteConfig();

                        if (OnUpdateConfig != null)
                            OnUpdateConfig(this);
                    }
                }
            }
            public bool isShowedOnAllDesktops
            {
                get => _isShowedOnAllDesktops;
                set
                {
                    if (_isShowedOnAllDesktops != value)
                    {
                        _isShowedOnAllDesktops = value;
                        WriteConfig();

                        if(OnUpdateConfig != null)
                            OnUpdateConfig(this);
                    }
                }
            }
        }

        public static void WriteConfig()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(new SavedConfig()));
        }
        private static string ReadConfig() {
            try
            {
                return File.ReadAllText(filePath);

            } catch(Exception)
            {
                return "{}";
            }
        }
        private static SavedConfig ParseJson(string json)
        {
            return JsonSerializer.Deserialize<SavedConfig>(json);
        }

        public static void InitConfig()
        {
            filePath = Path.Combine(MainWindow.configsDirPath, "saved_configs.json");

            var savedConfig  = new SavedConfig();
            var config       = ReadConfig();
            var parsedConfig = ParseJson(config);

            if(!savedConfig.Equals(parsedConfig))
            {
                WriteConfig();
                parsedConfig = ParseJson(config);
            }

            savedConfig.defaultChatbot = parsedConfig.defaultChatbot;
            savedConfig.isShowedOnAllDesktops = parsedConfig.isShowedOnAllDesktops;
        }
    }
}