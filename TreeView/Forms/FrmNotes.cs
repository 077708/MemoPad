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

        public FrmNotes(INoteServices noteServices)
        {
            InitializeComponent();
            this.noteServices = noteServices;
            lblNameFile.Text = "File Name";
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
            MessageBox.Show($"{FolderBrowser.FolderBw()}");
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
        #endregion

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            if (lblNameFile.Text == string.Empty)
            {
                MessageBox.Show("The file don't exist");
                lblNameFile.Text = "File name";
            }
            else
            {
                noteServices.Delete(lblNameFile.Text);
                rtxNotes.Text = string.Empty;
                lblNameFile.Text = string.Empty;    
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
    }
}
