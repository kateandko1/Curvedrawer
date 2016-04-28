using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.ObjectModel;
//using System.Windows.Forms;

namespace CurveDrawer
{
    /// <summary>
    /// Логика взаимодействия для CurveSettingsWindow.xaml
    /// 
    /// </summary>
    ///
    public class CurveParameter
    {
        public string name { get; set; }
        public double val  { get; set; }
    }

    public partial class CurveSettingsWindow : Window
    {
        string curve_type;

        //private ObservableCollection<CurveParameter> params_list = new ObservableCollection<CurveParameter>();
        MainWindow mwnd;
        List<CurveParameter> params_list = new List<CurveParameter>();
        public CurveSettingsWindow(MainWindow wnd)
        {
            InitializeComponent();
            mwnd = wnd;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //curve_type = ComboBox1.SelectedItemProperty.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                listView.Items.Clear();
                for (int i = 0; i < Convert.ToInt32(TextBox2.Text.ToString()); i++)
                {
                    params_list.Add(new CurveParameter() { name = "a" + i, val = 0.0 });
                    //listView.Items.Add(new CurveParameter() { name = "a" + i, val = 0.0});
                }
                listView.ItemsSource = params_list;
            }
            catch {

            }
            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mwnd.CreateAndDraw("f");
            Close();
        }
    }
}
