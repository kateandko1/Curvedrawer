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
        CurveFactory m_curve_factory ;
        List<ICurve> m_curves        ;
        ChartArea    m_chart_area    ;
        Legend       m_legend        ;
        const int    m_npoints = 1000;
        int          m_nseries = 0   ;

        public MainWindow()
        {
            InitializeComponent();
            m_curve_factory = new CurveFactory();
            m_chart_area    = new ChartArea   ();
            m_legend        = new Legend      ();
            m_chart_area.Name = "ChartArea";
            chart.ChartAreas.Add(m_chart_area);
            chart.Legends   .Add(m_legend    );
            m_curves = new List<ICurve>();
        }

        private void  DrawCurve(ICurve curve)
        {
            Series series = new Series
            {
                Name = "Series" + m_nseries       ,
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false         ,
                IsXValueIndexed   = true          ,
                ChartType = SeriesChartType.Line  ,
            };

            m_nseries++;
            chart.Series.Add(series);

            for (int i = 0; i < m_npoints; ++i)
            {
                series.Points.AddXY(i, curve.FuncVal(i));
            }
            chart.Invalidate();
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            ICurve curve = m_curve_factory.CreateCurve("TEST");
            m_curves.Add(curve);
            DrawCurve(curve);
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
                for (int i = 0; i < m_curves.Count(); ++i)
                {
                    myStream.WriteLine(m_curves[i].Serialize());
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
                m_curves.Clear();
                chart.Series.Clear();
                while (!myStream.EndOfStream)
                {
                    ICurve curve = m_curve_factory.DeserializeFromString(myStream.ReadLine());
                    m_curves.Add(curve);
                    DrawCurve(curve);
                }
                myStream.Close();
            }
        }
    }
}
