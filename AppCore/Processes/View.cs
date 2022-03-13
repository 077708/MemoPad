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
    public class View : Directory, IDirectory
    {
        public void PopulateTreeView(TreeView treeView1, string path)
        {
            TreeNode rootNode;
            //Obtengo lo que tiene ese directorio ya sean carpetas u subcarpetas!
            DirectoryInfo info = new DirectoryInfo($@"{path}");
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }
    }
}
