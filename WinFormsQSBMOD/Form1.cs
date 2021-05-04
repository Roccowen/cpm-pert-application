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

namespace WinFormsQSBMOD
{
    public partial class Form1 : Form
    {
        private readonly List<TableLayoutPanel> rows = new List<TableLayoutPanel>();
        private readonly List<TextBox> textBoxes = new List<TextBox>();
        private readonly EventGraph eventGraph = null;


        public Form1()
        {
            InitializeComponent();
            this.AutoScroll = false;
            this.HorizontalScroll.Enabled = false;
            this.HorizontalScroll.Visible = false;
            this.HorizontalScroll.Maximum = 0;
            this.AutoScroll = true;
            int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            dataTableLayout.Padding = new Padding(0, 0, vertScrollWidth, 0);
            saveToolStripButton.Click += SaveToolStripButton_Click;
            openToolStripButton.Click += OpenToolStripButton_Click;
            addWorkToolStripButton.Click += AddWorkToolStripButton_Click;
            delWorkToolStripButton.Click += DelWorkToolStripButton_Click;
            optimiseToolStripButton.Click += OptimiseToolStripButton_Click;
            toBackToolStripButton.Click += ToBackToolStripButton_Click;
        }
        private void AddWorkToolStripButton_Click(object sender, EventArgs e)
        {
            if (true || dataTableLayout.Size.Height < this.Size.Height - 131)
            {
                this.ClientSize = new Size(ClientRectangle.Height + 31, ClientRectangle.Width);
                for (int i = 0; i < 9; i++)
                {
                    var tb = new TextBox
                    {
                        Anchor = ((AnchorStyles)
                        ((AnchorStyles.Left | AnchorStyles.Right))),
                        Location = new Point(0, 0),
                        Name = $"textBox{textBoxes.Count + i}",
                        Size = new Size(80, 23),
                        TabIndex = 8
                    };
                    textBoxes.Add(tb);
                }
                for (int i = 0; i < 9; i++)
                {
                    dataTableLayout.Controls.Add(textBoxes[textBoxes.Count - 9 + i], i, dataTableLayout.RowCount - 1);
                }
                dataTableLayout.RowCount += 1;
            }

        }
        private void DelWorkToolStripButton_Click(object sender, EventArgs e)
        {
            if (dataTableLayout.RowCount > 0)
            {
                dataTableLayout.RowCount--;
                for (int i = 0; i < 9; i++)
                {
                    dataTableLayout.Controls.Remove(textBoxes[textBoxes.Count - 1 - i]);
                }               
            }           
        }
        private void ToBackToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OptimiseToolStripButton_Click(object sender, EventArgs e)
        {
            
            throw new NotImplementedException();
        }
        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            
        }

        private void dataTableLayout_ControlAdded(object sender, ControlEventArgs e)
        {
            
        }

        private void dataTableLayout_ControlAdded_1(object sender, ControlEventArgs e)
        {
            
        }
    }
}
