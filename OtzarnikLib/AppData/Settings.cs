using CommunityToolkit.Mvvm.Input;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;
using System.Text.Json;
using WpfLib;

namespace OtzarnikLib.AppData
{
    public class Settings : ViewModelBase
    {
        private static readonly string filePath = Path.Combine(Globals.AppDataFolder, "Settings.json");

        private string _otzariaFolder = "C:\\אוצריא\\אוצריא";
        private string _defaultFont = "Arial";
        private int _defaultFontSize = 16;
        private bool _adjustShemHashem = true;
        private bool _doNotChangeDocumentColors = false;

        public string OtzariaFolder
        {
            get { return _otzariaFolder; }
            set { SetProperty(ref _otzariaFolder, value, nameof(OtzariaFolder)); }
        }

        public string DefaultFont
        {
            get { return _defaultFont; }
            set { SetProperty(ref _defaultFont, value, nameof(DefaultFont)); }
        }

        public int DefaultFontSize
        {
            get { return _defaultFontSize; }
            set { SetProperty(ref _defaultFontSize, value, nameof(DefaultFontSize)); }
        }

        public bool AdjustShemHashem
        {
            get { return _adjustShemHashem; }
            set { SetProperty(ref _adjustShemHashem, value, nameof(AdjustShemHashem)); }
        }

        public bool DoNotChangeDocumentColors
        {
            get { return _doNotChangeDocumentColors; }
            set { SetProperty(ref _doNotChangeDocumentColors, value, nameof(DoNotChangeDocumentColors)); }
        }

        public RelayCommand SetFolderCommand { get; private set; }

        public Settings()
        {
            Load();
            PropertyChanged += (s, e) => Save();
            SetFolderCommand = new RelayCommand(SetFolder);
        }

        private void Load()
        {
            if (!File.Exists(filePath))
                return;

            try
            {
                string json = File.ReadAllText(filePath);
                var loaded = JsonSerializer.Deserialize<Settings>(json);
                if (loaded != null)
                {
                    OtzariaFolder = loaded.OtzariaFolder;
                    DefaultFont = loaded.DefaultFont;
                    DefaultFontSize = loaded.DefaultFontSize;
                    AdjustShemHashem = loaded.AdjustShemHashem;
                    DoNotChangeDocumentColors = loaded.DoNotChangeDocumentColors;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load settings: {ex.Message}");
            }
        }

        void SetFolder()
        {
            var dialog = new VistaFolderBrowserDialog { Multiselect = false};
            dialog.Description = "בחר תיקייה";
            dialog.UseDescriptionForTitle = true;
            var result = dialog.ShowDialog();

            if (result == true)
                OtzariaFolder = dialog.SelectedPath;
        }

        private void Save()
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
