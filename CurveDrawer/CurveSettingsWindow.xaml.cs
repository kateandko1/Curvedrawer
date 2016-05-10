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
        public string Parameter_Name { get; set; }
        public double Parameter_Value  { get; set; }
    }
    
    public partial class CurveSettingsWindow : Window
    {
        const string Polynom = "Polynomic";
        const string Garmonic = "Garmonic";
        string retType;

        //private ObservableCollection<CurveParameter> params_list = new ObservableCollection<CurveParameter>();
        MainWindow mwnd;
        List < CurveParameter > params_list = new List < CurveParameter >();
        public CurveSettingsWindow(MainWindow wnd)
        {
            InitializeComponent();
            mwnd = wnd;
            DataGridView.CanUserAddRows = false;
            DataGridView.CanUserSortColumns = false;
            retType = Polynom;
            AddItemsInTable();
            
        }
        private void AddItemsInTable()
        {
            try
            {
                DataGridView.ItemsSource = null;
                params_list.Clear();
                int count = Convert.ToInt32(TextBox2.Text.ToString());
                for (int i = 0; i <= count; i++)
                {
                    params_list.Add(new CurveParameter() { Parameter_Name = "a" + i, Parameter_Value = 0.0 });
                    //listView.Items.Add(new CurveParameter() { name = "a" + i, val = 0.0});
                }
                DataGridView.ItemsSource = params_list;
            }
            catch
            {

            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //curve_type = ComboBox1.SelectedItemProperty.ToString();
            try
            {
                string st = ComboBox1.SelectedIndex.ToString();
                if (ComboBox1.SelectedIndex.ToString().Equals("0"))
                {
                    TextBox2.IsReadOnly = false;
                    TextBox2.Text = "0";
                    retType = Polynom;
                }
                if (ComboBox1.SelectedIndex.ToString().Equals("1"))
                {
                    TextBox2.IsReadOnly = true;
                    TextBox2.Text = "3";
                    retType = Garmonic;
                }
            }
            catch
            {

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            AddItemsInTable();
         //   }       
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] ParameterNames;
            double[] ParameterValue;
            int count = Convert.ToInt32(TextBox2.Text.ToString())+1;
            ParameterNames = new string[count];
            ParameterValue = new double[count];
            DataGridView.UnselectAll();
            DataGridView.SelectAll();
            IList list = DataGridView.SelectedItems;
            CurveParameter param;
            for (int i = 0; i < list.Count; i++)
            {
                param = (CurveParameter)list[i];
                ParameterNames[i] = param.Parameter_Name;
                ParameterValue[i] = param.Parameter_Value;
            }
            string st = TextBox1.Text.ToString();
            mwnd.CreateCurveFromParams(TextBox1.Text.ToString(), retType, ParameterNames, ParameterValue, list.Count);
            Close();
        }
    }
}
