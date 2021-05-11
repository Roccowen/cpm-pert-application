using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using QSBMODLibraryNF.Classes;
using QSBMODWinFormsNF.Classes;
using System.Drawing.Text;

namespace QSBMODWinFormsNF
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<int, List<int>> bindingDictionary = new Dictionary<int, List<int>>
        {
            { 3 , new List<int>{ 1 } },
            { 5 , new List<int>{ 4 } }
        };
        private readonly string[] titles = new string[]
            {"Номер работы","t","Tmin","Tmax","c","Cmin","Cmax","Нач.событие","Кон.событие"};
        private readonly bool[] isStringTb = new bool[]
            { true, false, false, false, false, false, false, true, true };
        private readonly bool[] isReadOnlyTB = new bool[]
            { true, true, false, false, true, false, false, false, false };
        private readonly int columnsCnt = 9,
            stringTextBoxWidth = 110, numericTextBoxWidth = 50, textBoxHeight = 25,
            marginHor = 5, marginWid = 7, x = 5, y = 30;

        private readonly Stack<TextBox[]> textBoxesRows = new Stack<TextBox[]>();

        private bool IsModelChanged = true;
        private EventGraphAnalyzer eventGraphAnalyzerValue;
        public EventGraphAnalyzer EventGraphAnalyzer
        {
            get
            {
                if (eventGraphAnalyzerValue is null || IsModelChanged)
                    eventGraphAnalyzerValue = new EventGraphAnalyzer(EventGraph);
                return eventGraphAnalyzerValue;
            }
            set
            {
                eventGraphAnalyzerValue = value;
            }
        }
        private EventGraph EventGraph
        {
            get
            {
                var eventGraph = new EventGraph();
                var reversedRows = textBoxesRows.Reverse();
                foreach (var row in reversedRows)
                {
                    try
                    {
                        var w = new Work(row[0].Text,
                                        float.Parse(row[1].Text),
                                        float.Parse(row[2].Text),
                                        float.Parse(row[3].Text),
                                        float.Parse(row[4].Text),
                                        float.Parse(row[5].Text),
                                        float.Parse(row[6].Text),
                                        row[7].Text,
                                        row[8].Text);
                        eventGraph.AddWork(w);
                    }
                    catch (FormatException)
                    {
                        throw new FormatException("Строка имела неверный формат");
                    }
                    catch (ArgumentException)
                    {
                        throw new ArgumentException("Номера работ должны иметь уникальные названия");
                    }
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
                    row[2].Text = Convert.ToString(Math.Round(w.DurationMin, 2));
                    row[3].Text = Convert.ToString(Math.Round(w.DurationMax, 2));
                    row[5].Text = Convert.ToString(Math.Round(w.ResourcesMin, 2));
                    row[6].Text = Convert.ToString(Math.Round(w.ResourcesMax, 2));
                    row[7].Text = w.FirstEventTitle;
                    row[8].Text = w.SecondEventTitle;
                }
                var tbr = textBoxesRows.Reverse().ToArray();
                int i = 0;
                foreach (var w in value.WorksByTitle.Values)
                {
                    tbr[i][1].Text = Convert.ToString(Math.Round(w.Duration, 2));
                    tbr[i][4].Text = Convert.ToString(Math.Round(w.Resources, 2));
                    i++;
                }
            }
        }
        private int rowsCount = 0;
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
        private PrivateFontCollection fontCollectionValue;
        private PrivateFontCollection FontCollection
        {
            get
            {
                if (fontCollectionValue is null)
                {
                    fontCollectionValue = new PrivateFontCollection();
                    var fontsNames = new string[] { "Montserrat-Regular.otf", "Montserrat-Bold.otf", "Montserrat-SemiBold.otf" };
                    foreach (var f in fontsNames)
                        FontCollection.AddFontFile(Path.Combine(Environment.CurrentDirectory, @"Resources\MontserratFontsOFT\", f));
                }
                return fontCollectionValue;
            }
        }
        public event EventHandler OnRowsCountChanged;
        public Form1()
        {
            Loger.Msg("public Form1()");
            InitializeComponent();
            errorStripLabel.Font = new Font(FontCollection.Families[0], 9f);
            this.MinimumSize = new Size(GetWidth(), 500);
            DrawTableHead();

            openToolStripButton.Click += ModelStateChanged;
            addWorkToolStripButton.Click += ModelStateChanged;
            delWorkToolStripButton.Click += ModelStateChanged;
            optimiseToolStripButton.Click += ModelStateSaved;
            fullOptimisationToolStripButton.Click += ModelStateSaved;
            OnRowsCountChanged += RowsCountChanged;
        }
        private int GetWidth()
        {
            Loger.Msg("private int GetWidth()");
            int width = 2 * x + 25;
            foreach (var tb in isStringTb)
                if (tb)
                    width += stringTextBoxWidth + marginWid;
                else
                    width += numericTextBoxWidth + marginWid;
            return width;
        }
        private void DrawTableHead()
        {
            Loger.Msg("private void DrawTableHead()");
            int cursor = x;
            for (int i = 0; i < titles.Length; i++)
            {
                Label lbl = new Label
                {
                    Font = new Font(FontCollection.Families[1], 10f),
                    Text = titles[i],
                    TextAlign = ContentAlignment.MiddleCenter,
                    Anchor = ((AnchorStyles)
                        ((AnchorStyles.Left | AnchorStyles.Top))),
                    Location = new Point(cursor, y)
                };
                if (isStringTb[i])
                {
                    lbl.Size = new Size(stringTextBoxWidth, textBoxHeight);
                    cursor += stringTextBoxWidth + marginHor;
                }
                else
                {
                    lbl.Size = Size = new Size(numericTextBoxWidth, textBoxHeight);
                    cursor += numericTextBoxWidth + marginHor;
                }
                Controls.Add(lbl);
            }
        }
        private TextBox[] AddWork()
        {
            Loger.Msg("private TextBox[] AddWork()");
            var tbRow = new TextBox[columnsCnt];
            int cursor = x;
            for (int i = 0; i < columnsCnt; i++)
            {
                TextBox tb = new TextBox
                {
                    Font = new Font(FontCollection.Families[0], 10f),
                    Anchor = ((AnchorStyles)
                        ((AnchorStyles.Left | AnchorStyles.Top))),
                    Name = $"textBox{textBoxesRows.Count + i}",
                    Location = new Point(cursor, y + (textBoxesRows.Count + 1) * (textBoxHeight + marginWid)),
                };
                tbRow[i] = tb;
                tb.TextChanged += ModelStateChanged;
                List<int> bindable;
                if (bindingDictionary.TryGetValue(i, out bindable))
                {
                    foreach (var tbid in bindable)
                    {
                        tb.TextChanged += delegate (object sender, EventArgs e)
                        {
                            tbRow[tbid].Text = tb.Text;
                            tbRow[tbid].TextChanged -= ModelStateChanged;
                        };
                    }
                }
                if (i == 8)
                {
                    tbRow[8].TextChanged += delegate (object sender, EventArgs e)
                    {
                        tbRow[0].Text = String.Format($"{tbRow[7].Text} - {tbRow[8].Text}");
                    };
                    tbRow[7].TextChanged += delegate (object sender, EventArgs e)
                    {
                        tbRow[0].Text = String.Format($"{tbRow[7].Text} - {tbRow[8].Text}");
                    };
                }
                if (isStringTb[i])
                {
                    tb.Size = new Size(stringTextBoxWidth, textBoxHeight);
                    cursor += stringTextBoxWidth + marginHor;
                }
                else
                {
                    tb.Size = new Size(numericTextBoxWidth, textBoxHeight);
                    cursor += numericTextBoxWidth + marginHor;
                }
                if (isReadOnlyTB[i])
                {
                    tb.ReadOnly = true;
                }
                if (i == 0)
                {
                    tb.Text = Convert.ToString(RowsCount + 1);
                }

                Controls.Add(tb);
            }
            foreach (var tb in tbRow)
                Controls.Add(tb);
            textBoxesRows.Push(tbRow);
            RowsCount++;

            return tbRow;
        }
        private TextBox[] DelLastWork()
        {
            Loger.Msg("private TextBox[] DelLastWork()");
            var delRow = textBoxesRows.Pop();
            foreach (var tb in delRow)
                Controls.Remove(tb);
            RowsCount--;
            IsModelChanged = true;
            errorStripLabel.Text = "";
            return delRow;
        }
        private List<TextBox[]> DelAllWorks()
        {
            Loger.Msg("private List<TextBox[]> DelAllWorks()");
            int count = textBoxesRows.Count;
            List<TextBox[]> textBoxes = new List<TextBox[]>(count);
            for (int i = 0; i < count; i++)
            {
                var tbrow = DelLastWork();
                textBoxes.Add(tbrow);
            }
            return textBoxes;
        }
        private void ReturnCoefs()
        {
            Loger.Msg("private void ReturnCoefs()");
            foreach (var row in textBoxesRows)
            {
                row[1].Text = row[3].Text;
                row[4].Text = row[5].Text;
            };
        }
        private void UpdateCoefs()
        {
            Loger.Msg("private void UpdateCoefs()");
            var tbr = textBoxesRows.Reverse().ToArray();
            int i = 0;
            foreach (var w in EventGraphAnalyzer.Works)
            {
                tbr[i][1].Text = Convert.ToString(Math.Round(w.Duration, 2));
                tbr[i][4].Text = Convert.ToString(Math.Round(w.Resources, 2));
                i++;
            };
        }
        private void ModelStateChanged(object sender, EventArgs e)
        {
            Loger.Msg("private void ModelStateChanged(object sender, EventArgs e)");
            IsModelChanged = true;
            errorStripLabel.Text = "";
            ReturnCoefs();
            EventGraphAnalyzer = null;
        }
        private void ModelStateSaved(object sender, EventArgs e)
        {
            Loger.Msg("private void ModelStateSaved(object sender, EventArgs e)");
            IsModelChanged = false;
        }
        private void RowsCountChanged(object sender, EventArgs e)
        {
            Loger.Msg("private void RowsCountChanged(object sender, EventArgs e)");
            if (RowsCount > 0)
            {
                delWorkToolStripButton.Enabled = true;
                saveToolStripButton.Enabled = true;
                optimiseToolStripButton.Enabled = true;
                fullOptimisationToolStripButton.Enabled = true;
                resultToolStripButton.Enabled = true;
            }
            else
            {
                delWorkToolStripButton.Enabled = false;
                saveToolStripButton.Enabled = false;
                optimiseToolStripButton.Enabled = false;
                fullOptimisationToolStripButton.Enabled = false;
                resultToolStripButton.Enabled = false;
            }
        }
        private void AddWorkToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Loger.Msg("private void AddWorkToolStripButton_Click(object sender, EventArgs e)");
                AddWork();
            }
            catch (Exception ex)
            {
                Loger.Msg(ex);
                errorStripLabel.Text = ex.Message;
            }
        }
        private void DelWorkToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Loger.Msg("private void DelWorkToolStripButton_Click(object sender, EventArgs e)");
                if (textBoxesRows.Count > 0)
                    DelLastWork();
            }
            catch (Exception ex)
            {
                Loger.Msg(ex);
                errorStripLabel.Text = ex.Message;
            }
        }
        private void OptimiseToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Loger.Msg("private void OptimiseToolStripButton_Click(object sender, EventArgs e)");
                EventGraphAnalyzer.OptimizeForOneDay();
                UpdateCoefs();
            }
            catch (Exception ex)
            {
                Loger.Msg(ex);
                errorStripLabel.Text = ex.Message;
            }
        }
        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Loger.Msg("private void OpenToolStripButton_Click(object sender, EventArgs e)");
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                    openFileDialog.Filter = "csv files (*.csv)|*.csv";
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        EventGraph = EventGraphReader.ReadFromCSV(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                Loger.Msg(ex);
                errorStripLabel.Text = ex.Message;
            }
        }
        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Loger.Msg("private void SaveToolStripButton_Click(object sender, EventArgs e)");
                string filename;
                var SFD = new SaveFileDialog
                {
                    InitialDirectory = Directory.GetCurrentDirectory(),
                    Filter = "csv files (*.csv)|*.csv"
                };
                if (SFD.ShowDialog() == DialogResult.OK)
                    filename = SFD.FileName;
                else
                    return;
                EventGraphReader.SaveToCSV(EventGraph, filename);
            }
            catch (Exception ex)
            {
                Loger.Msg(ex);
                errorStripLabel.Text = ex.Message;
            }
        }
        private void ResultToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Loger.Msg("private void ResultToolStripButton_Click(object sender, EventArgs e)");
                var rf = new ResultForm(this, EventGraphAnalyzer.callback);
                rf.Show();
            }
            catch (Exception ex)
            {
                Loger.Msg(ex);
                errorStripLabel.Text = ex.Message;
            }
        }
        private void FullOptimisationToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Loger.Msg("private void FullOptimisationToolStripButton_Click(object sender, EventArgs e)");
                EventGraphAnalyzer.FullOptimize();
                UpdateCoefs();
            }
            catch (Exception ex)
            {
                Loger.Msg(ex);
                errorStripLabel.Text = ex.Message;
            }
        }
    }
}
