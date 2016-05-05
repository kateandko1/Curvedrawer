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
        ChartArea chart_area;
        Legend legend;
        const int npoints = 1000;
        int nseries = 0;

        public void GetCurveParams(string CurveName, string CurveType, string[] ParamsNames, double[] ParameterValues)
        {
            System.Windows.Forms.MessageBox.Show("Parameters sucssesfully sended!");
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

        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            //ICurve curve = curve_factory.CreateCurve("TEST");
            //curves.Add(curve);
            //DrawCurve(curve);
            CurveSettingsWindow csw = new CurveSettingsWindow(this);
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
    }
}
