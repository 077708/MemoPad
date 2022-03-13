using AppCore.Contracts;
using Domain.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class BaseServices<T> : IServices<T> where T : class
    {
        private IColeccion<T> coleccion;
        public BaseServices(IColeccion<T> coleccion)
        {
            this.coleccion = coleccion;
        }

        public void Delete(string path)
        {
            coleccion.Delete(path);
        }

        public (string, string) Reader()
        {
            return coleccion.Reader();
        }

        public string Save(string path, string note)
        {
            return coleccion.Save(path, note);
        }
    }
}
