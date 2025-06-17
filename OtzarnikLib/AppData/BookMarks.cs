using CommunityToolkit.Mvvm.Input;
using OtzarnikLib.FileViewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;
using WpfLib;
using WpfLib.Controls;
using WpfLib.Helpers;

namespace OtzarnikLib.AppData
{
    public class BookMarkItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string ScrollIndex { get; set; }
    }

    public class BookMarkGroup
    {
        public string Name { get; set; }
        public List<BookMarkItem> Items { get; } = new List<BookMarkItem>();
    }

    public class BookMarks : ViewModelBase
    {
        readonly string bookMarksFilePath = Path.Combine(Globals.AppDataFolder, "BookMarks.json");
        readonly string groupsFilePath = Path.Combine(Globals.AppDataFolder, "BookMarkGroups.json");

        ObservableCollection<BookMarkItem> _items;
        ObservableCollection<BookMarkGroup> _groups;

        public ObservableCollection<BookMarkItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ObservableCollection<BookMarkGroup> Groups
        {
            get => _groups;
            set => SetProperty(ref _groups, value);
        }

        public RelayCommand<FileView> AddBookMarkCommand { get; }
        public RelayCommand<BookMarkItem> RemoveBookMarkCommand { get; }
        public RelayCommand<TabControl> AddGroupCommand { get; }
        public RelayCommand<BookMarkGroup> RemoveGroupCommand { get; }


        public BookMarks()
        {
            LoadCollection();
            LoadGroups();
            AddBookMarkCommand = new RelayCommand<FileView>(AddBookMark);
            RemoveBookMarkCommand = new RelayCommand<BookMarkItem>(RemoveBookMark);
            AddGroupCommand = new RelayCommand<TabControl>(AddTabGroup);
            RemoveGroupCommand = new RelayCommand<BookMarkGroup>(RemoveGroup);
        }

        void LoadCollection()
        {
            if (File.Exists(bookMarksFilePath))
            {
                string json = File.ReadAllText(bookMarksFilePath);
                Items = JsonSerializer.Deserialize<ObservableCollection<BookMarkItem>>(json)
                    ?? new ObservableCollection<BookMarkItem>();
                return;
            }
            Items = new ObservableCollection<BookMarkItem>();
        }

        void LoadGroups()
        {
            if (File.Exists(groupsFilePath))
            {
                string json = File.ReadAllText(groupsFilePath);
                Groups = JsonSerializer.Deserialize<ObservableCollection<BookMarkGroup>>(json)
                    ?? new ObservableCollection<BookMarkGroup>();
                return;
            }
            Groups = new ObservableCollection<BookMarkGroup>();
        }

        public void AddBookMark(FileView fileView)
        {
            if (fileView == null) return;

            var inputBox = new HebrewInputBox("צור סימניה", "", fileView.TreeItem?.Name);
            inputBox.ShowDialog();
            if (inputBox.DialogResult == true)
            {
                var bookmark = new BookMarkItem
                {
                    Name = inputBox.Answer,
                    Path = fileView.TreeItem?.Path,
                    ScrollIndex = fileView.ScrollIndex() ?? "0"
                };

                Items.Add(bookmark);
                Commit();
            }
        }

        public void RemoveBookMark(BookMarkItem bookMark)
        {
            if (bookMark == null) return;

            Items.Remove(bookMark);
            Commit();
        }

        void Commit()
        {
            string json = JsonSerializer.Serialize(Items);
            File.WriteAllText(bookMarksFilePath, json);
        }

        void AddTabGroup(TabControl tabControl)
        {
            var group = new List<BookMarkItem>();
            foreach (TabItem item in tabControl.Items)
            {
                if (item.Content is FileView fileView)
                    group.Add(new BookMarkItem
                    {
                        Path = fileView.TreeItem?.Path,
                        ScrollIndex = fileView.ScrollIndex() ?? "0"
                    });
            }
            AddGroup(group);
        }


        public void AddGroup(List<BookMarkItem> group)
        {
            if (group == null || group.Count == 0) return;
            string date = HebrewDateHelper.GetHebrewDateTime(DateTime.Now);
            var inputBox = new HebrewInputBox("שמור סביבת עבודה", "", date);
            inputBox.ShowDialog();
            if (inputBox.DialogResult == true)
            {
                var newGroup = new BookMarkGroup
                {
                    Name = inputBox.Answer
                };
                newGroup.Items.AddRange(group);

                Groups.Add(newGroup);
                CommitGroups();
            }
        }

        public void RemoveGroup(BookMarkGroup group)
        {
            if (group == null) return;

            Groups.Remove(group);
            CommitGroups();
        }

        void CommitGroups()
        {
            string json = JsonSerializer.Serialize(Groups);
            File.WriteAllText(groupsFilePath, json);
        }

    }
}