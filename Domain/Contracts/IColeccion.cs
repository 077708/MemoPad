using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IColeccion<T> where T : class
    {
        (string, string) Reader();

        void Delete(string path);

        string Save(string path, string note); 
    }
}
