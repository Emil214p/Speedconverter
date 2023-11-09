using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Speedconverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindSpeeds();
            ClearControls();
        }

        readonly Regex regex = MyRegex();

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        { // makes sure that only numbers are inserted into the input boxes

            var text = ((TextBox)sender).Text + e.Text;

            if (!regex.IsMatch(text))
            {
                // if not match deny new char
                e.Handled = true;
            }
        }

        public double Speed = 0;

        public DataTable dtSpeed = new();
        public void BindSpeeds()
        {
            dtSpeed.Columns.Add("Text");
            dtSpeed.Columns.Add("Value");
            dtSpeed.Rows.Add("Select", "select");
            dtSpeed.Rows.Add("m/s to km/h", 18 / 5 * Speed);
            dtSpeed.Rows.Add("km/h to m/s", 5 / 18 * Speed);

            Speed_Type.ItemsSource = dtSpeed.DefaultView;
            Speed_Type.DisplayMemberPath = "Text";
            Speed_Type.SelectedValuePath = "Value";
            Speed_Type.SelectedIndex = 0;
        }
        public string Speedtext = "";
        public void Convert_Click(object sender, RoutedEventArgs e)
        {
            if (Speed_Input.Text == null || Speed_Input.Text.Trim() == "")
            {
                MessageBox.Show("You need to put a number");

            }

            else if (Speed_Type.SelectedValue == null || Speed_Type.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select something");
            }

            else if (Speed_Type.SelectedIndex == 1)
            {
                Speedtext = Speed_Input.Text;
               Double Speed2 = Double.Parse(Speedtext);
                dtSpeed.Rows[1]["Value"] = (Double)(18 * Speed2) / 5;
                Speed_Output.Content = dtSpeed.Rows[1]["Value"].ToString();
            }

            else if (Speed_Type.SelectedIndex == 2)
            {
                Speedtext = Speed_Input.Text;
                Double Speed2 = Double.Parse(Speedtext);
                dtSpeed.Rows[2]["Value"] = (Double)(5 * Speed2) / 18;
                Speed_Output.Content = dtSpeed.Rows[2]["Value"].ToString();
            }

            else
            {
                MessageBox.Show("Invalid");
            }
        }

        public void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();    
        }
        private void ClearControls()
        { // will clear selected and converted speeds
            Speed_Input.Text = string.Empty;
            if (Speed_Type.Items.Count > 0)
                Speed_Type.SelectedIndex = 0;
            Speed_Output.Content = ""; //Text should probably be changed to Context
            Speed_Input.Focus();
        }

        [GeneratedRegex("^\\d+[,|\\.]{0,1}\\d{0,}$")]
        private static partial Regex MyRegex();
    }
}
