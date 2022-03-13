using AppCore.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppCore.Processes
{
    public class View : IDirectory
    {
        public TreeNode PopulateTreeView(DirectoryInfo directoryInfo)
        {
            TreeNode root = new TreeNode(directoryInfo.Name);

            foreach (var item in directoryInfo.GetDirectories())
            {
                root.Nodes.Add(PopulateTreeView(item));
            }

            foreach (var item in directoryInfo.GetFiles())
            {
                var test = item.Extension;
                if (item.Extension == ".txt")
                    root.Nodes.Add(item.Name);
            }

            return root;
        }
    }
}
