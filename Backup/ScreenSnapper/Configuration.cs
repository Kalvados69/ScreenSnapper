using System;
using System.IO;

namespace ScreenSnapper
{
    class Configuration
    {
        private string confFilePath;

        public Configuration()
        {
            Init();
        }

        public void Init()
        {
            confFilePath = Path.Combine(Environment.SpecialFolder.MyPictures.ToString(), "ScreenSnapper");
        }

        public void LoadFromFile()
        {
            if (File.Exists(confFilePath))
            {
            }
        }

        public void SaveToFile()
        {
            if (!Directory.Exists(confFilePath))
            {
                Directory.CreateDirectory(confFilePath);
            }
        }
    }
}
