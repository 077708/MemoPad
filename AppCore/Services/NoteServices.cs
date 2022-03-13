using AppCore.Contracts;
using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class NoteServices : BaseServices<Note>, INoteServices
    {
        private INote note;
        public NoteServices(INote coleccion) : base(coleccion)
        {
            note = coleccion;
        }
    }
}
