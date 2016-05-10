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

namespace CurveDrawer
{
    /// <summary>
    /// Логика взаимодействия для EditCurveParameters.xaml
    /// </summary>
    /// 




    public partial class EditCurveParameters : Window
    {
        const string Polynom = "Polynomic";
        const string Garmonic = "Garmonic";
        string retType;
        string[] ParameterNames;
        double[] ParameterValue;
        string CurveName;
        string CurveType;
        //private ObservableCollection<CurveParameter> params_list = new ObservableCollection<CurveParameter>();
        MainWindow mwnd;
        List<CurveParameter> params_list = new List<CurveParameter>();

        public EditCurveParameters(MainWindow wnd,
            string[] parameterNames, double[] parameterValue,
            string curveName, string curveType
            )
        {
            InitializeComponent();
            mwnd = wnd;
            DataGridView.CanUserAddRows = false;
            DataGridView.CanUserSortColumns = false;
            retType = Polynom;
            
            ParameterNames = parameterNames;
            ParameterValue = parameterValue;
            CurveName = curveName;
            CurveType = curveType;
            TextBox3.IsReadOnly = true;
            TextBox3.Text = CurveType;
            TextBox2.IsReadOnly = true;
            TextBox2.Text = (ParameterValue.Length - 1).ToString();
            TextBox1.Text = CurveName;
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



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddItemsInTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] ParameterNames;
            double[] ParameterValue;
            int count = Convert.ToInt32(TextBox2.Text.ToString()) + 1;
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
            mwnd.SetCurveParams(TextBox1.Text.ToString(), retType,
                ParameterNames, ParameterValue);
            Close();
        }
    }


}

