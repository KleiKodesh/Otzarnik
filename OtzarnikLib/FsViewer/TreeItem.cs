using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLib.ViewModels;

namespace OtzarnikLib.FsViewer
{
    public class TreeItem : CheckedTreeItemBase<TreeItem>
    {
        public string Path { get; set; }
        public string Extension { get; set; }
    }
}
