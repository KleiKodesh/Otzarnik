using OtzarnikLib.FsViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OtzarnikLib.FileViewer
{
    /// <summary>
    /// Interaction logic for FileView.xaml
    /// </summary>
    public partial class FileView : UserControl
    {
        public TreeItem TreeItem { get; private set; }

        public FileView()
        {
            InitializeComponent();
        }

        public string ScrollIndex()
        {
            return null;
        }
    }
}
