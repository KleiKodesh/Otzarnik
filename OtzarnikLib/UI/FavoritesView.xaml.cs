using OtzarnikLib.AppData;
using System.Windows.Controls;
using WpfLib.Helpers;

namespace OtzarnikLib.UI
{
    /// <summary>
    /// Interaction logic for FavoritesView.xaml
    /// </summary>
    public partial class FavoritesView : UserControl
    {
        public FavoritesView()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null || listBox.SelectedIndex < 0) return;
            LoadSelectedItem(listBox.SelectedItem);
            listBox.SelectedIndex = -1;
        }

        void LoadSelectedItem(object selectedItem)
        {
            var mainControl = DependencyHelper.FindParent<MainView>(this);
            if (mainControl == null) return;

            if (selectedItem is HistoryItem historyItem)
                mainControl.LoadItem(historyItem.Path);
            else if (selectedItem is BookMarkItem bookMarkItem)
                mainControl.LoadItem(bookMarkItem.Path , bookMarkItem.ScrollIndex);
            else if (selectedItem is BookMarkGroup bookMarkGroup)
                foreach (var item in bookMarkGroup.Items)
                    mainControl.LoadItem(item.Path, item.ScrollIndex);
        }
    }
}
