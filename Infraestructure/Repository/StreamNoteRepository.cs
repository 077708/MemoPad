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
        private BinaryReader binaryReader;
        private BinaryWriter binaryWriter;

        public void Delete(string path)
        {
            if (path == null)
            {
                throw new Exception("El archivo no existe");
            }

            File.Delete(path);
        }

        public (string,string) Reader()
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    string Reader = "";
                    openFileDialog.Filter = "Text files (.txt)|*.txt";
                    openFileDialog.Title = "Open file";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            binaryReader = new BinaryReader(fileStream);
                            long length = binaryReader.BaseStream.Length;

                            binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
                            while (binaryReader.BaseStream.Position < length)
                            {
                                Reader = binaryReader.ReadString();
                            }

                            return (openFileDialog.FileName, Reader);
                        }
                    }
                    else
                    {
                        throw new IOException("The file did not open");
                    }
                }

            }
            catch (IOException)
            {
                throw;
            }
        }

        public (string, string) Reader(string path)
        {
            string Read = "";
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                binaryReader = new BinaryReader(fileStream);
                long length = binaryReader.BaseStream.Length;

                binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
                while (binaryReader.BaseStream.Position < length)
                {
                    Read = binaryReader.ReadString();
                }

                return (path, Read);
            }
        }

        public string Save(string path, string note)
        {
            try
            {
                if (path.Equals("File Name"))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text files (.txt)|*.txt";
                    saveFileDialog.Title = "Open file";
                    saveFileDialog.ShowDialog();
                    path = saveFileDialog.FileName;
                    if (path == null)
                    {
                        throw new IOException("Por favor seleccione la ruta");
                    }
                    else
                    {
                        using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write))
                        {
                            binaryWriter = new BinaryWriter(fileStream);
                            binaryWriter.Write(note);
                            binaryWriter.Close();
                        }
                    }
                    return path;
                }
                else
                {
                    using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Write))
                    {
                        binaryWriter = new BinaryWriter(file);
                        binaryWriter.BaseStream.Seek(0, SeekOrigin.End);
                        binaryWriter.Write(note);
                    }
                    return path;
                }

            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}
