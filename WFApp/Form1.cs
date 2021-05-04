using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QSBMODLibrary.Classes;

namespace WFApp
{
    public partial class Form1 : Form
    {
        private EventGraph eventGraph;
        private List<TextBox> textBoxes;
        public Form1()
        {
            InitializeComponent();
            optimiseToolStripButton.Click += OptimiseToolStripButton_Click;
            newToolStripButton.Click += NewToolStripButton_Click;
            openToolStripButton.Click += OpenToolStripButton_Click;
            saveToolStripButton.Click += SaveToolStripButton_Click;
            addNewWorkToolStripButton.Click += AddNewWorkToolStripButton_Click;
            backToolStripButton.Click += BackToolStripButton_Click;
            deleteToolStripButton.Click += DeleteToolStripButton_Click; 
        }

        private void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BackToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddNewWorkToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OptimiseToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
