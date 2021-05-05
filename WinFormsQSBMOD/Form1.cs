using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using QSBMODLibrary.Classes;

namespace WinFormsQSBMOD
{
    public partial class Form1 : Form
    {
        public EventGraph EventGraph
        {
            get
            {
                var eventGraph = new EventGraph();
                foreach (var row in textBoxesRows)
                {
                    eventGraph.AddWork(new Work(row[0].Text, 
                                    float.Parse(row[1].Text), 
                                    float.Parse(row[2].Text), 
                                    float.Parse(row[3].Text), 
                                    float.Parse(row[4].Text), 
                                    float.Parse(row[5].Text), 
                                    float.Parse(row[6].Text), 
                                    row[0].Text,
                                    row[0].Text));
                }
                return eventGraph;
            }
            set
            {
                DelAllWorks();
                foreach (var w in value.WorksByTitle.Values)
                {
                    var row = AddWork();
                    row[0].Text = w.Title;
                    row[1].Text = Convert.ToString(w.Duration);
                    row[2].Text = Convert.ToString(w.DurationMin);
                    row[3].Text = Convert.ToString(w.DurationMax);
                    row[4].Text = Convert.ToString(w.Resources);
                    row[5].Text = Convert.ToString(w.ResourcesMin);
                    row[6].Text = Convert.ToString(w.ResourcesMax);
                    row[7].Text = w.FirstEventTitle;
                    row[8].Text = w.SecondEventTitle;
                }
            }
        }

        private readonly Stack<TextBox[]> textBoxesRows = new Stack<TextBox[]>();
        private readonly string[] titles = new string[]
            {"Название работы","t_ij","T_min","T_max","c","C_min","C_max","Нач. событие","Конеч. событие"};
        private readonly bool[] stringTb = new bool[] { true, false, false, false, false, false, false, true, true };
        private readonly int columnsCnt = 9, 
            stringTextBoxWidth = 100, numericTextBoxWidth = 60, textBoxHeight = 25, 
            marginHor = 5, marginWid = 5, x = 5, y = 30;

        public Form1()
        {
            InitializeComponent();
            this.Width = GetWidth();
            DrawTableHead();
            saveToolStripButton.Click += SaveToolStripButton_Click;
            openToolStripButton.Click += OpenToolStripButton_Click;
            addWorkToolStripButton.Click += AddWorkToolStripButton_Click;
            delWorkToolStripButton.Click += DelWorkToolStripButton_Click;
            optimiseToolStripButton.Click += OptimiseToolStripButton_Click;
            toBackToolStripButton.Click += ToBackToolStripButton_Click;
        }
        private int GetWidth()
        {
            int width = 4 * x + 25;
            foreach (var tb in stringTb)
                if (tb)
                    width += stringTextBoxWidth + marginWid;
                else
                    width += numericTextBoxWidth + marginWid;
            return width;
        }
        private void DrawTableHead()
        {           
            int cursor = x;
            for (int i = 0; i < titles.Length; i++)
            {
                var lbl = new Label();
                if (stringTb[i])
                {
                    lbl = new Label
                    {
                        Text = titles[i],
                        TextAlign = ContentAlignment.MiddleCenter,
                        Anchor = ((AnchorStyles)
                        ((AnchorStyles.Left | AnchorStyles.Top))),
                        Location = new Point(cursor, y),
                        Size = new Size(stringTextBoxWidth, textBoxHeight),
                    };
                    cursor += stringTextBoxWidth + marginHor;
                }
                else
                {
                    lbl = new Label
                    {
                        Text = titles[i],
                        TextAlign = ContentAlignment.MiddleCenter,
                        Anchor = ((AnchorStyles)
                        ((AnchorStyles.Left | AnchorStyles.Top))),
                        Location = new Point(cursor, y),
                        Size = new Size(numericTextBoxWidth, textBoxHeight),
                    };
                    cursor += numericTextBoxWidth + marginHor;
                }
                Controls.Add(lbl);
            }
        }        
        private void DelAllWorks()
        {
            int count = textBoxesRows.Count;
            for (int i = 0; i < count; i++)
                DelLastWork();
        }
        private void DelLastWork()
        {
            var delRow = textBoxesRows.Pop();
            foreach (var tb in delRow)
                Controls.Remove(tb);
        }
        private TextBox[] AddWork()
        {
            var tbRow = new TextBox[columnsCnt];
            int cursor = x;
            for (int i = 0; i < columnsCnt; i++)
            {
                var tb = new TextBox();
                if (stringTb[i])
                {
                    tb = new TextBox
                    {
                        Anchor = ((AnchorStyles)
                        ((AnchorStyles.Left | AnchorStyles.Top))),
                        Location = new Point(cursor, y + (textBoxesRows.Count + 1) * (textBoxHeight + marginWid)),
                        Name = $"textBox{textBoxesRows.Count + i}",
                        Size = new Size(stringTextBoxWidth, textBoxHeight),
                    };
                    cursor += stringTextBoxWidth + marginHor;
                }
                else
                {
                    tb = new TextBox
                    {
                        Anchor = ((AnchorStyles)
                        ((AnchorStyles.Left | AnchorStyles.Top))),
                        Location = new Point(cursor, y + (textBoxesRows.Count + 1) * (textBoxHeight + marginWid)),
                        Name = $"textBox{textBoxesRows.Count + i}",
                        Size = new Size(numericTextBoxWidth, textBoxHeight),
                    };
                    cursor += numericTextBoxWidth + marginHor;
                }
                tbRow[i] = tb;
                Controls.Add(tb);
            }
            foreach (var tb in tbRow)
                Controls.Add(tb);
            textBoxesRows.Push(tbRow);
            return tbRow;
        }
        private void AddWorkToolStripButton_Click(object sender, EventArgs e)
        {
            AddWork();
        }
        private void DelWorkToolStripButton_Click(object sender, EventArgs e)
        {
            if (textBoxesRows.Count > 0)
                DelLastWork();
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
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    EventGraph = EventGraphReader.ReadFromCSV(filePath);
                }
            }
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            string folder = "";
            var folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                folder = folderBrowserDialog1.SelectedPath + "\\data.csv";
            }
            EventGraphReader.SaveToCSV(EventGraph, folder + "\\data.csv");
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
