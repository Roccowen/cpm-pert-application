using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace QSBMODWinForms
{
    public partial class ResultForm : Form
    {
        private readonly Form1 ParentForm;
        private PrivateFontCollection fontCollectionValue;
        private readonly float LabelSize = 20f;
        private readonly FontSpec PlotLabelsFont = new FontSpec("Leelawadee UI", 20f, Color.Black, false, false, false);
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
        public ResultForm(Form1 parentForm, List<(float t, float c)> callback)
        {
            ParentForm = parentForm;
            InitializeComponent();
            
            resoltTextBox.ReadOnly = true;
            resoltTextBox.Font = new Font("Consolas", 10f);
            resoltTextBox.Text = GetTextResult();
            
            DrawDependencyPlot(callback);
            DrawPropabilytyPlot();
        }
        private string GetTextResult()
        {
            static string isKrit(float x, float y) => x == y ? "Крит." : x.ToString();
            List<string> result = new List<string>();
            result.Add($"  КП продолжительность : {ParentForm.EventGraphAnalyzer.Duration}");
            result.Add($"\r\n  КП стоимость : {ParentForm.EventGraphAnalyzer.Cost}");
            result.Add("\r\n  КП : ");
            foreach (var w in ParentForm.EventGraphAnalyzer.CriticalWorks)
                result.Add($"{w.Title} ");
            result.Add(String.Format("\r\n\r\n  {0, -6} {1, -6} {2, -6} {3, -6} {4, -6} {5, -6} {6, -6} {7, -6} {8, -6} {9, -8} {10, -8} {11, -6}\r\n\r\n"
                                                , "№", "t", "c", "Tmin", "Cmax", "РН", "ПН", "РО", "ПО", "Резерв", "Напряж.", "tgA"));
            foreach (var w in ParentForm.EventGraphAnalyzer.Works)
                result.Add(String.Format("  {0, -6} {1, -6} {2, -6} {3, -6} {4, -6} {5, -6} {6, -6} {7, -6} {8, -6} {9, -8} {10, -8} {11, -6}\r\n",
                                               w.Title, Math.Round(w.Duration, 2), Math.Round(w.Resources, 2), Math.Round(w.DurationMin, 2), 
                                               Math.Round(w.ResourcesMax, 2), w.ES, w.LS, w.EE, w.LE, isKrit(w.FR, 0), isKrit((float)Math.Round(w.K, 2), 1), 
                                               Math.Round(w.tgA, 2)));
            result.Add($"\r\n\r\n   Количество памяти текущего процесса : {Process.GetCurrentProcess().PrivateMemorySize64 / 1024} kB");
            return string.Join("", result);
        }
        private void DrawDependencyPlot(List<(float t, float c)> callback)
        {
            GraphPane mainPane = dependencyPlot.GraphPane;
            mainPane.Title.Text = "";
            mainPane.CurveList.Clear();
            mainPane.XAxis.IsVisible = true;
            mainPane.YAxis.IsVisible = true;
            mainPane.XAxis.Title.FontSpec.Size = LabelSize;
            mainPane.YAxis.Title.FontSpec.Size = LabelSize;
            mainPane.XAxis.Title.Text = "c";
            mainPane.YAxis.Title.Text = "t";

            mainPane.XAxis.Scale.FontSpec.Size = LabelSize;
            mainPane.XAxis.MajorGrid.Color = Color.Black;
            mainPane.XAxis.MajorGrid.IsVisible = true;
            mainPane.YAxis.Scale.FontSpec.Size = LabelSize;
            mainPane.YAxis.MajorGrid.Color = Color.Black;
            mainPane.YAxis.MajorGrid.IsVisible = true;

            PointPairList pointList = new PointPairList();
            foreach (var c in callback)
                pointList.Add(c.c, c.t);
            LineItem myCurve = mainPane.AddCurve("Зависимость t от c", pointList, Color.Blue, SymbolType.None);
            myCurve.Label.FontSpec = PlotLabelsFont;
            dependencyPlot.AxisChange();
            dependencyPlot.Invalidate();
        }
        private void DrawPropabilytyPlot()
        {
            GraphPane mainPane = propabilityPlot.GraphPane;
            mainPane.Title.Text = "";
            mainPane.CurveList.Clear();
            mainPane.XAxis.IsVisible = true;
            mainPane.YAxis.IsVisible = true;
            mainPane.XAxis.Title.FontSpec.Size = LabelSize;
            mainPane.YAxis.Title.FontSpec.Size = LabelSize;
            mainPane.XAxis.Title.Text = "t";
            mainPane.YAxis.Title.Text = "p";

            mainPane.XAxis.Scale.FontSpec.Size = LabelSize;
            mainPane.XAxis.MajorGrid.Color = Color.Black;
            mainPane.XAxis.MajorGrid.IsVisible = true;
            mainPane.YAxis.Scale.FontSpec.Size = LabelSize;
            mainPane.YAxis.MajorGrid.Color = Color.Black;
            mainPane.YAxis.MajorGrid.IsVisible = true;

            PointPairList pointList = new PointPairList();
            //foreach (var c in callback)
            //    pointList.Add(c.c, c.t);
            LineItem myCurve = mainPane.AddCurve("Вероятность выполнения за t", pointList, Color.Red, SymbolType.None);
            myCurve.Label.FontSpec = PlotLabelsFont;
            dependencyPlot.AxisChange();
            dependencyPlot.Invalidate();
        }
        private void ResultForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ParentForm.Enabled = true;
        }
        private void ResultForm_Load(object sender, EventArgs e)
        {
            ParentForm.Enabled = false;
        }
    }
}
