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

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for Clock.xaml
    /// </summary>
    public partial class Clock : Window
    {
        public static int result { set; get; }
        public Clock()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if(button1.IsChecked == true)
            {
                
                AdminPanel.clock1 = 1;
                this.Close();
            }
            else if(button2.IsChecked == true)
            {
                
                AdminPanel.clock2 = 1;
                this.Close();
            }
            else if(button3.IsChecked == true)
            {
                
                AdminPanel.clock3 = 1;
                this.Close();
            }
            else if(button4.IsChecked == true)
            {
                
                AdminPanel.clock4 = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("You should choose one of the radio buttons.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
