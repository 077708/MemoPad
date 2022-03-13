using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppCore.Processes
{
    public class FolderBrowser
    {
        public static string FolderBw()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            string path = "";
            if (folderBrowserDialog.ShowDialog().Equals(DialogResult.OK))
            {
                 path = folderBrowserDialog.SelectedPath;
            }

            return path;
        }
    }
}
