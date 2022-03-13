using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infraestructure.Repository
{
    public class StreamNoteRepository : INote
    {
        private StreamWriter streamWriter;
        private StreamReader streamReader;

        public void Add(Note t)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (.txt)|*.txt";
            saveFileDialog.Title = "Open file";
            saveFileDialog.ShowDialog();
            string path = saveFileDialog.FileName;

            streamWriter = File.AppendText(path);
            streamWriter.Write(t.BlocNote);
            streamWriter.Flush();
        }

        public (string,string) Reader()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (.txt)|*.txt";
            openFileDialog.Title = "Open file";
            openFileDialog.ShowDialog();
            openFileDialog.OpenFile();

            string ruta = openFileDialog.FileName;
            streamReader = File.OpenText(ruta);
            string Rtx = streamReader.ReadToEnd();

            return (ruta, Rtx);
        }
    }
}
