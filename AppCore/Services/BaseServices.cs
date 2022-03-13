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
        public void Add(T service)
        {
            coleccion.Add(service);
        }

        public (string, string) Reader()
        {
            return coleccion.Reader();
        }
    }
}
