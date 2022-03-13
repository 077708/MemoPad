using AppCore.Contracts;
using AppCore.Processes;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeView.Forms
{
    public partial class FrmNotes : Form
    {
        private INoteServices noteServices;
        private string dir = "";
        private DirectoryInfo directoryInfo;
        public FrmNotes(INoteServices noteServices)
        {
            InitializeComponent();
            this.noteServices = noteServices;
            lblNameFile.Text = "File Name";
            lblAppName.Text = "MemoPad";
        }

        #region PanelContenedor

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelContenedor_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion

        #region EventsButtonsFiles

        private void btnFile_MouseEnter(object sender, EventArgs e)
        {
            panelButton.Visible = true;
        }

        private void guna2Panel1_MouseLeave(object sender, EventArgs e)
        {
            panelButton.Visible = false;
        }

        private void panelButtonsUpdate_MouseLeave(object sender, EventArgs e)
        {
            panelButtonsUpdate.Visible = false;
        }

        private void guna2Button1_MouseEnter(object sender, EventArgs e)
        {
            panelButtonsUpdate.Visible = true;
        }

        private void btnFont_MouseEnter(object sender, EventArgs e)
        {
            panelFont.Visible = true;
        }

        private void panelFont_MouseLeave(object sender, EventArgs e)
        {
            panelFont.Visible = false;
        }

        #endregion

        #region Files

        private void btnSelectDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                string path = "";

                if (folderBrowserDialog.ShowDialog().Equals(DialogResult.OK))
                {
                    path = folderBrowserDialog.SelectedPath;
                }
                if (path == null)
                {
                    return;
                }
                dir = path;

                treeView1.Nodes.Clear();
                IDirectory directory = new AppCore.Processes.View();
                directoryInfo = new DirectoryInfo(dir);
                treeView1.Nodes.Add(directory.PopulateTreeView(directoryInfo));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewFile_Click(object sender, EventArgs e)
        {
            rtxNotes.Text = string.Empty;
            lblNameFile.Text = "File Name";
        }

        private void btnOpen_Click(object sender, EventArgs e) 
        {
            try
            {
                (lblNameFile.Text, rtxNotes.Text) = noteServices.Reader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                lblNameFile.Text = "File Name";

                Note note = new Note()
                {
                    BlocNote = rtxNotes.Text,
                };

                lblNameFile.Text = noteServices.Save(lblNameFile.Text, note.BlocNote);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            rtxNotes.Text = string.Empty;
            lblNameFile.Text = "File Name";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblNameFile.Text.Equals("File Name"))
                {
                    MessageBox.Show("The file don't exist");
                    lblNameFile.Text = "File Name";
                }
                else
                {
                    noteServices.Delete(lblNameFile.Text);
                    rtxNotes.Text = string.Empty;
                    lblNameFile.Text = "File Name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblNameFile.Text = noteServices.Save(lblNameFile.Text, rtxNotes.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void FrmNotes_Load(object sender, EventArgs e)
        {

        }

        private void Charge()
        {

        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string carpeta = treeView1.SelectedNode.FullPath;
            DirectoryInfo directoryInfo = new DirectoryInfo(dir);

            //Cadena para abrir el archivo
            string path = directoryInfo.FullName + carpeta.Remove(0,directoryInfo.Name.Length);


            if (carpeta.Contains(".txt"))
            {
                MessageBox.Show(path);
                (lblNameFile.Text, rtxNotes.Text) = noteServices.Reader(path);
            }
        }
    }
}
