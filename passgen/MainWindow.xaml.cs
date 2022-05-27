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

            LowercaseLettersCheckBox.IsChecked = true;

            updateFlags(null,null);
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

            if(LengthSlider.Minimum > Int32.Parse(LengthBox.Text))
            {
                LengthBox.Text = LengthSlider.Minimum.ToString();
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

        private void MenuItem_CloseClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void PassOutputBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(passholderTM);
        }

        private void updateFlags(object sender, RoutedEventArgs e)
        {
            bool[] tmpflags = new bool[16];

            
            
            tmpflags[0] = (bool)LowercaseLettersCheckBox.IsChecked;
            tmpflags[1] = (bool)UppercaseLettersCheckBox.IsChecked;
            tmpflags[2] = (bool)NumbersCheckBox.IsChecked;
            tmpflags[3] = (bool)BasicSpecialCharsCheckBox.IsChecked;
            tmpflags[4] = (bool)ExtendedSpecialCharsCheckBox.IsChecked;
            tmpflags[5] = (bool)BasicSafetyCheckBox.IsChecked;
            tmpflags[6] = (bool)ExtendedSafetyCheckBox.IsChecked;
            tmpflags[7] = true;
            tmpflags[11] = (bool)LessSpecialCharsCheckBox.IsChecked;

            gne.Flags = tmpflags;

            string pass = gne.Generate();
            passholderTM = pass;

            updatePasswordVisibility();
        }

        private void BasicSafetyCheckBox_Click(object sender, RoutedEventArgs e)
        {
            
            if ((bool)BasicSafetyCheckBox.IsChecked)
            {
                if (LengthSlider.Value < 8)
                {
                    LengthSlider.Value = 8;
                    LengthBox.Text = "8";
                }
                LengthSlider.Minimum = 8;
                
                gne.Length = (int)LengthSlider.Value;
                ExtendedSafetyCheckBox.IsEnabled = false;

                updateCheckBoxesForSafety(true);


            }
            else
            {
                LengthSlider.Minimum = 1;
                ExtendedSafetyCheckBox.IsEnabled = true;

                updateCheckBoxesForSafety(false);
            }
            updateFlags(sender, e);
        }

        private void ExtendedSafetyCheckBox_Click(object sender, RoutedEventArgs e)
        {
            
            if ((bool)ExtendedSafetyCheckBox.IsChecked)
            {
                if (LengthSlider.Value < 16)
                {
                    LengthSlider.Value = 16;
                    LengthBox.Text = "16";
                }
                LengthSlider.Minimum = 16;

                gne.Length = (int)LengthSlider.Value;
                BasicSafetyCheckBox.IsEnabled = false;

                updateCheckBoxesForSafety(true);
            }
            else
            {
                LengthSlider.Minimum = 1;
                BasicSafetyCheckBox.IsEnabled = true;

                updateCheckBoxesForSafety(false);


            }
            updateFlags(sender, e);
        }

        private void updateCheckBoxesForSafety(bool onOrOff)
        {
            if(onOrOff)
            {
                LowercaseLettersCheckBox.IsChecked = true;
                LowercaseLettersCheckBox.IsEnabled = false;
                UppercaseLettersCheckBox.IsChecked = true;
                UppercaseLettersCheckBox.IsEnabled = false;
                NumbersCheckBox.IsChecked = true;
                NumbersCheckBox.IsEnabled = false;
                BasicSpecialCharsCheckBox.IsChecked = true;
                BasicSpecialCharsCheckBox.IsEnabled = false;
            }
            else
            {
                LowercaseLettersCheckBox.IsEnabled = true;
                UppercaseLettersCheckBox.IsEnabled = true;
                NumbersCheckBox.IsEnabled = true;
                BasicSpecialCharsCheckBox.IsEnabled = true;
            }
        }
    }
}
