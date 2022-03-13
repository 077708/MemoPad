using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppCore.Contracts
{
    public interface IDirectory
    {
        void PopulateTreeView(TreeView treeView1, string path);
    }
}
