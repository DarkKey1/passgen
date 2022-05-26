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

namespace passgen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private PassGeneration gne = new PassGeneration();




        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if((bool)ToggleShowGenerated.IsChecked)
            {
                PassOutput.Visibility = Visibility.Hidden;
                PassOutputBox.Visibility = Visibility.Visible;
            }
            else
            {
                PassOutput.Visibility = Visibility.Visible;
                PassOutputBox.Visibility = Visibility.Hidden;


                gne.Length = 39;
                
            }
        }
    }
}
