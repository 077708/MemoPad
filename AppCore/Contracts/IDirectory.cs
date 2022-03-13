using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppCore.Contracts
{
    public interface IDirectory
    {
        TreeNode PopulateTreeView(DirectoryInfo directoryInfo);
    }
}
