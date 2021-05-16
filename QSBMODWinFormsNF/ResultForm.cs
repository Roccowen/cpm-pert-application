using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using QSBMODLibraryNF.Classes;
using System.Windows.Forms;
using ZedGraph;
using System.Drawing.Text;
using System.IO;
using System.Diagnostics;

namespace QSBMODWinFormsNF
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
        public ResultForm(Form1 parentForm)
        {
            ParentForm = parentForm;
            InitializeComponent();

            resoltTextBox.ReadOnly = true;
            resoltTextBox.Font = new Font("Consolas", 10f);
            resoltTextBox.Text = GetTextResult();

            DrawDependencyPlot(parentForm.EventGraphAnalyzer.CallbackTC);
            DrawPropabilytyPlot(parentForm.EventGraphAnalyzer.CallbackTP);
        }
        private string GetTextResult()
        {
            List<string> result = new List<string>();

            result.Add($"\r\n  КП минимальная продолжительность : {Math.Round(ParentForm.EventGraphAnalyzer.DurationMin, 2)}");
            result.Add($"\r\n  КП номинальная продолжительность : {Math.Round(ParentForm.EventGraphAnalyzer.DurationNom, 2)}");
            result.Add($"\r\n  КП продолжительность после оптимизации : {Math.Round(ParentForm.EventGraphAnalyzer.Duration, 2)}");
            result.Add($"\r\n  КП минимальная стоимость : {Math.Round(ParentForm.EventGraphAnalyzer.CostMin, 2)}");
            result.Add($"\r\n  КП стоимость после оптимизации : {Math.Round(ParentForm.EventGraphAnalyzer.Cost, 2)}");            
            result.Add("\r\n  КП : ");
            foreach (var w in ParentForm.EventGraphAnalyzer.CriticalWorks)
                result.Add($"{w.Title} ");
            result.Add("\r\n\r\n  Работы:");
            result.Add(String.Format($"\r\n  {"Код", -8} {"t", -6} {"Tmin", -6} {"c", -6} {"Cmax", -6} {"РН", -6} {"ПН", -6} " +
                    $"{"РО", -6} {"ПО", -6} {"ПР", -6} {"ЧР", -6} {"СР", -6} {"НР", -8} {"Напряж.", -8} {"tgA", -6} {"МО", -6} " +
                    $"{ "Дисп.", -6}\r\n\r\n"));
            foreach (var w in ParentForm.EventGraphAnalyzer.Works)
                result.Add($"  {w.Title, -8} {Math.Round(w.Duration, 2), -6} {Math.Round(w.DurationMin, 2), -6} " +
                        $"{Math.Round(w.Resources, 2), -6} {Math.Round(w.ResourcesMax, 2), -6} {Math.Round(w.ES, 2), -6} " +
                        $"{Math.Round(w.LS, 2), -6} {Math.Round(w.EE, 2), -6} {Math.Round(w.LE, 2), -6} {Math.Round(w.FR, 2), -6} " +
                        $"{Math.Round(w.PR, 2), -6} {Math.Round(w.FreeR, 2), -6} {Math.Round(w.IR, 2), -8} {Math.Round(w.K, 2), -8} " +
                        $"{Math.Round(w.tgA, 2), -6} {Math.Round(w.exp, 2), -6} {Math.Round(w.dis, 2), -6}\r\n");
            result.Add("\r\n\r\n  События:");
            result.Add(String.Format("\r\n  {0, -8} {1, -8} {2, -8} {3, -8}\r\n\r\n", "Номер", "Код", "РН", "ПН"));
            foreach (var ev in ParentForm.EventGraphAnalyzer.ProjectEvents.OrderBy(e => e.Id))
                result.Add(String.Format("  {0, -8} {1, -8} {2, -8} {3, -8}\r\n", ev.Id, ev.Title, Math.Round(ev.ES, 2), Math.Round(ev.LS, 2)));

            result.Add($"\r\n\r\n  РН - Раннее начало.");
            result.Add($"\r\n  ПН - Позднее начало.");
            result.Add($"\r\n  РО - Раннее окончание.");
            result.Add($"\r\n  ПО - Позднее окончание.");
            result.Add($"\r\n  ПР - Полный резерв");
            result.Add($"\r\n  ЧР - Частный резерв первого порядка.");
            result.Add($"\r\n  СР - Свободный резерв.");
            result.Add($"\r\n  НР - Независимый резерв.");
            result.Add($"\r\n  МО - Математическое ожидание.");
            result.Add($"\r\n\r\n  Количество памяти текущего процесса : {Process.GetCurrentProcess().PrivateMemorySize64 / 1024} kB");
            return string.Join("", result);
        }
        private void DrawDependencyPlot(List<(float t, float c)> callbackTC)
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
            foreach (var c in callbackTC)
                pointList.Add(c.c, c.t);
            LineItem myCurve = mainPane.AddCurve("Зависимость t от c", pointList, Color.Blue, SymbolType.None);
            myCurve.Label.FontSpec = PlotLabelsFont;
            dependencyPlot.AxisChange();
            dependencyPlot.Invalidate();
        }
        private void DrawPropabilytyPlot(List<(float t, float p)>  callbackTP)
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
            foreach (var c in callbackTP)
                pointList.Add(c.t, c.p);
            LineItem myCurve = mainPane.AddCurve("Вероятность выполнения за t", pointList, Color.Red, SymbolType.None);
            myCurve.Label.FontSpec = PlotLabelsFont;
            propabilityPlot.AxisChange();
            propabilityPlot.Invalidate();
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
