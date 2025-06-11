using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtzarnikLib.AppData
{
    public static class Globals
    {
        public static string AppName => "Otzarnik";
        public static string AppDataFolder => GetAppDataFolder();

        static string GetAppDataFolder()
        {
            string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName);
            if (!Directory.Exists(appDataFolder)) 
                Directory.CreateDirectory(appDataFolder);
            return appDataFolder;
        }
    }
}
