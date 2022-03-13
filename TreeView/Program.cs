using AppCore.Contracts;
using AppCore.Services;
using Autofac;
using Domain.Contracts;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreeView.Forms;

namespace TreeView
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<StreamNoteRepository>().As<INote>();
            builder.RegisterType<NoteServices>().As<INoteServices>();

            var container = builder.Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmNotes(container.Resolve<INoteServices>()));
        }
    }
}
