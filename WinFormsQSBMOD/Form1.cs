using System;
using System.Collections.Generic;
using System.Collections;
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
        private EventGraphAnalyzer EventGraphAnalyzer;
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
                                    row[7].Text,
                                    row[8].Text));
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
        //private readonly Stack<Stack<TextBox[]>> textBoxesHistory = new Stack<Stack<TextBox[]>>(10);
        private int rowsCount = 0;
        public event System.EventHandler OnRowsCountChanged;
        private int RowsCount
        {
            get
            {
                return rowsCount;
            }
            set
            {
                if (value >= 0 && value == textBoxesRows.Count)
                {
                    rowsCount = value;
                    OnRowsCountChanged(this, EventArgs.Empty);
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
            fullOptimisationToolStripButton.Click += FullOptimisationToolStripButton_Click;
            resultToolStripButton.Click += ResultToolStripButton_Click;
            this.OnRowsCountChanged += EnableControl;
        }
        private void EnableControl(object sender, EventArgs e)
        {
            if (RowsCount > 0)
            {
                delWorkToolStripButton.Enabled = true;
                saveToolStripButton.Enabled = true;
                optimiseToolStripButton.Enabled = true;
                fullOptimisationToolStripButton.Enabled = true;
            }
            else
            {
                delWorkToolStripButton.Enabled = false;
                saveToolStripButton.Enabled = false;
                optimiseToolStripButton.Enabled = false;
                fullOptimisationToolStripButton.Enabled = false;
            }
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
                Label lbl;
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
        private List<TextBox[]> DelAllWorks()
        {           
            int count = textBoxesRows.Count;
            List<TextBox[]> textBoxes = new List<TextBox[]>(count);
            for (int i = 0; i < count; i++)
            {
                var tbrow = DelLastWork();
                textBoxes.Add(tbrow);
            }
            return textBoxes;
        }
        private TextBox[] DelLastWork()
        {
            var delRow = textBoxesRows.Pop();
            foreach (var tb in delRow)
                Controls.Remove(tb);
            RowsCount--;
            return delRow;
        }
        private TextBox[] AddWork()
        {
            var tbRow = new TextBox[columnsCnt];
            int cursor = x;
            for (int i = 0; i < columnsCnt; i++)
            {
                TextBox tb;
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
            RowsCount++;
            return tbRow;
        }
        private void mainFormToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //var cp = (Stack)textBoxesRows.Clone()
            //textBoxesHistory.Push(cp);
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
            if (EventGraphAnalyzer is null)
                EventGraphAnalyzer = new EventGraphAnalyzer(EventGraph);
            EventGraphAnalyzer.OptimizeForOneDay();
        }
        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "csv files (*.csv)|*.csv";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                EventGraph = EventGraphReader.ReadFromCSV(filePath);
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
        private void ResultToolStripButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void FullOptimisationToolStripButton_Click(object sender, EventArgs e)
        {
            if (EventGraphAnalyzer is null)
                EventGraphAnalyzer = new EventGraphAnalyzer(EventGraph);
            EventGraphAnalyzer.FullOptimize();
        }
    }
}
