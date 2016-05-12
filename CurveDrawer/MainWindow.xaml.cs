using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace CurveDrawer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CurveFactory curve_factory;
        List<ICurve> curves;
        List<string> curves_names;
        ChartArea chart_area;
        Legend legend;
        const int npoints = 1000;
        int nseries = 0;

        public void CreateCurveFromParams(string CurveName, string CurveType, string[] ParamsNames, double[] ParameterValues, int NumberOfParams)
        {
            ICurve curve = curve_factory.CreateCurve(CurveType);
            curve.Name = CurveName;
            curve.nparams = NumberOfParams;
            curve.Params = ParameterValues;
            curve.ParamsNames = ParamsNames;
            curves.Add(curve);
            curves_names.Add(CurveName);
            curveNameLabel.Content = CurveType + ": " + CurveName;
            DrawCurve(curve);
            ComboBox.Items.Add(CurveName);
            //curve.
            //System.Windows.Forms.MessageBox.Show("Parameters sucssesfully created!");
        }
        
        public void DrawAll()
        {
            for (int i = 0; i < curves.Count; i++)
            {
                DrawCurve(curves[i]);
            }
        }

        public void SetCurveParams(string CurveName, string CurveType, string[] ParamsNames, double[] ParameterValues, int ParamsCount, int index)
        {
            ICurve curve = curve_factory.CreateCurve(CurveType);
            curve.Name = CurveName;
            curve.nparams = ParamsCount;
            curve.Params = ParameterValues;
            curve.ParamsNames = ParamsNames;
            curves[index] = curve;
            DrawAll();
            //System.Windows.Forms.MessageBox.Show("Parameters sucssesfully edited!");
        }  

        private void  DrawCurve(ICurve curve)
        {
            Series series = new Series
            {
                Name = "Series" + nseries,
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line
            };

            nseries++;
            chart.Series.Add(series);

            for (int i = 0; i < npoints; i++)
            {
                series.Points.AddXY(i, curve.FuncVal(i));
            }
            chart.Invalidate();
        }

        public MainWindow()
        {
            InitializeComponent();
            curve_factory = new CurveFactory();
            chart_area = new ChartArea();
            legend = new Legend();
            chart_area.Name = "ChartArea";
            chart.ChartAreas.Add(chart_area);
            chart.Legends.Add(legend);
            curves = new List<ICurve>();
            curves_names = new List<string>();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //ICurve curve = curve_factory.CreateCurve("TEST");
            //curves.Add(curve);
            //DrawCurve(curve);
            //test data
            //end
            ICurve curve = null;
            string name;
            try
            {
                name = ComboBox.SelectedItem.ToString();
            }
            catch
            {
                return;
            }
            bool f = false;
            int index = 0;
            for (int i = 0; i < curves.Count() && !f; i++)
            {
                if (curves[i].Name == name)
                {
                    curve = curves[i];
                    f = true;
                    index = i;
                }
            }
            if (!f)
                return;
            EditCurveParameters csw = new EditCurveParameters(this, curve.ParamsNames,
                curve.Params, name, curve.Type, index);
            csw.Show();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter myStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "CurveDrawer files (.cdf)|*.cdf|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            saveFileDialog.ShowDialog();
            if ((myStream = new StreamWriter(saveFileDialog.OpenFile())) != null)
            {     
                for (int i = 0; i < curves.Count(); i++)
                {
                    myStream.WriteLine(curves[i].Serialize());
                }
                myStream.Close();
            }
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            StreamReader myStream;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CurveDrawer files (.cdf)|*.cdf|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            openFileDialog.ShowDialog();
            if ((myStream = new StreamReader(openFileDialog.OpenFile())) != null)
            {
                curves.Clear();
                chart.Series.Clear();
                while (!myStream.EndOfStream)
                {
                    ICurve curve = curve_factory.DeserializeFromString(myStream.ReadLine());
                    curves.Add(curve);
                    DrawCurve(curve);
                }
                myStream.Close();
            }
        }
        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            //ICurve curve = curve_factory.CreateCurve("TEST");
            //curves.Add(curve);
            //DrawCurve(curve);
            CurveSettingsWindow csw = new CurveSettingsWindow(this);
            csw.Show();
        }
    }
}
