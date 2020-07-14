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
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
        }


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listView.SelectedIndex;
            MoveCursorMenu(index);
            
            switch(index)
            {
                case 0:
                    principal.Children.Clear();
                    principal.Children.Add(new Date());
                    break;
                case 1:
                    principal.Children.Clear();
                    principal.Children.Add(new FoodCard());
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    principal.Children.Clear();
                    principal.Children.Add(new InformationOfRestaurant());
                    break;
            }
        }

        private void MoveCursorMenu(int index)
        {
            transitioning.OnApplyTemplate();
            gridCursor.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }

        private void profileBtn_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Close();
        }
    }
}
