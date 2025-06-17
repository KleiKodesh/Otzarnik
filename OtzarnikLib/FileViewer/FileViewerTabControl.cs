using OtzarnikLib.FsViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OtzarnikLib.FileViewer
{
    public class FileViewerTabControl : TabControl
    {
        public void AddItem(TreeItem treeItem)
        {
            if (treeItem == null) return;

            if (treeItem.Extension == ".pdf")
            {
                this.Items.Add(new TabItem { Header = treeItem.Name });
            }
            else if (treeItem.Extension == ".txt" || treeItem.Extension.Contains("htm"))
            {
                this.Items.Add(new TabItem { Header = treeItem.Name });
            }
        }
    }
}
