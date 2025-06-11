using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtzarnikLib.AppData
{
    public static class SettingsViewModel
    {
        public static Settings Settings { get;} = new Settings();
        public static History History { get;} = new History();
        public static BookMarks BookMarks { get;} = new BookMarks();
    }
}
