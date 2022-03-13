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
        }

        private void btnOpen_Click(object sender, EventArgs e) 
        { 
            (lblNameFile.Text, rtxNotes.Text) = noteServices.Reader();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            Note note = new Note()
            {
                BlocNote = rtxNotes.Text,
            };

            noteServices.Add(note);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
