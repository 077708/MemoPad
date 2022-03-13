using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Contracts
{
    public interface IServices <T> where T : class
    {
        void Add(T service);

        (string, string) Reader();
    }
}
