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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace passgen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private PassGeneration gne = new PassGeneration();

        private string passholderTM = "";


        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            updatePasswordVisibility();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string pass = gne.Generate();
            passholderTM = pass;
            updatePasswordVisibility();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LengthBox.Text = LengthSlider.Value.ToString();

            gne.Length = (int)LengthSlider.Value;


            string pass = gne.Generate();
            passholderTM = pass;
            updatePasswordVisibility();
        }


        private void updatePasswordVisibility()
        {
            if ((bool)ToggleShowGenerated.IsChecked)
            {
                PassOutputBox.Text = passholderTM;
            }
            else
            {
                string tmpstr = "";
                for (int i = 0; i < passholderTM.Length; i++)
                {
                    tmpstr = tmpstr + "●";
                }
                PassOutputBox.Text = tmpstr;
            }
        }

        private void LengthBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(LengthBox.Text == "" || LengthSlider == null)
            {
                return;
            }

            if(1 > Int32.Parse(LengthBox.Text))
            {
                LengthBox.Text = "1";
            }

            if(64 < Int32.Parse(LengthBox.Text))
            {
                LengthBox.Text = "64";
            }

            LengthSlider.Value = Int32.Parse(LengthBox.Text);

        }

        private void LengthBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
