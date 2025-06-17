using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Threading;
using WpfLib;
using WpfLib.Helpers;

namespace OtzarnikLib.AppData
{
    public class HistoryItem
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string HebrewDate { get; set; }
    }

    public class History : ViewModelBase
    {
        readonly string filePath = Path.Combine(Globals.AppDataFolder, "History.json");

        ObservableCollection<HistoryItem> _items = new ObservableCollection<HistoryItem>();
        public ObservableCollection<HistoryItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public RelayCommand<HistoryItem> RemoveHistoryItemCommand { get; }

        public History()
        {
            Load();
            RemoveHistoryItemCommand = new RelayCommand<HistoryItem>(Remove);
        }

        void Load()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Items = JsonSerializer.Deserialize<ObservableCollection<HistoryItem>>(json);
            }
        }

        public void Add(string path, string name)
        {
            if (string.IsNullOrEmpty(path)) return;

            var existing = Items.FirstOrDefault(i => i.Path == path);
            if (existing != null)
                Items.Remove(existing);

            var item = new HistoryItem
            {
                Path = path,
                Name = name,
                Date = DateTime.Now,
                HebrewDate = HebrewDateHelper.GetHebrewDateTime(DateTime.Now),
            };

            Items.Add(item);

            Items = new ObservableCollection<HistoryItem>(
                Items.OrderByDescending(i => i.Date).Take(50)
            );

            OnPropertyChanged(nameof(Items));

            Commit();
        }

        public void Remove(HistoryItem item)
        {
            if (item == null) return;
            Items.Remove(item);
            Commit();
        }

        void Commit()
        {
            string json = JsonSerializer.Serialize(Items);
            File.WriteAllText(filePath, json);
        }
    }
}
