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
using System.Data;
using System.Data.SqlClient;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public static string date = "";
        public static int clock1 = 0;
        public static int clock2 = 0;
        public static int clock3 = 0;
        public static int clock4 = 0;
        bool same = false;
        int number = 0;
        int set = 0;
        int clicking = 0;
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
                    if(date == "" || (clock1 == 0 && clock2 == 0 && clock3 == 0 && clock4 == 0))
                    {
                        MessageBox.Show("First you have to choose date and time", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        principal.Children.Clear();
                        FoodShowing();
                    }
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

        private void FoodShowing()
        {
            set = 0;
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Food", SqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while(sqlDataReader.Read())
            {
                if(sqlDataReader["date"].ToString() == Date.choosenDate && (sqlDataReader[2].ToString() == clock1.ToString() || sqlDataReader[2].ToString() == clock2.ToString() || sqlDataReader[2].ToString() == clock3.ToString() || sqlDataReader[2].ToString() == clock4.ToString()) && number == 0)
                {
                    same = true;
                    foodCard1.numberTxt.Text = sqlDataReader["number"].ToString();
                    foodCard1.priceTxt.Text = sqlDataReader["price"].ToString();
                    foodCard1.foodInformation1.Text = sqlDataReader["description"].ToString();
                    foodCard1.nameTxt.Text = sqlDataReader["name"].ToString();
                    foodCard1.Visibility = Visibility.Visible;
                    number++;
                }

                if (sqlDataReader["date"].ToString() == Date.choosenDate && (sqlDataReader[2].ToString() == clock1.ToString() || sqlDataReader[2].ToString() == clock2.ToString() || sqlDataReader[2].ToString() == clock3.ToString() || sqlDataReader[2].ToString() == clock4.ToString()) && number == 1)
                {
                    same = true;
                    foodCard2.numberTxt.Text = sqlDataReader["number"].ToString();
                    foodCard2.priceTxt.Text = sqlDataReader["price"].ToString();
                    foodCard2.foodInformation1.Text = sqlDataReader["description"].ToString();
                    foodCard2.nameTxt.Text = sqlDataReader["name"].ToString();
                    foodCard2.Visibility = Visibility.Visible;
                    number++;
                }

                if (sqlDataReader["date"].ToString() == Date.choosenDate && (sqlDataReader[2].ToString() == clock1.ToString() || sqlDataReader[2].ToString() == clock2.ToString() || sqlDataReader[2].ToString() == clock3.ToString() || sqlDataReader[2].ToString() == clock4.ToString()) && number == 2)
                {
                    same = true;
                    foodCard3.numberTxt.Text = sqlDataReader["number"].ToString();
                    foodCard3.priceTxt.Text = sqlDataReader["price"].ToString();
                    foodCard3.foodInformation1.Text = sqlDataReader["description"].ToString();
                    foodCard3.nameTxt.Text = sqlDataReader["name"].ToString();
                    foodCard3.Visibility = Visibility.Visible;
                    number = 0;
                    set++;
                    next.Visibility = Visibility.Visible;
                    if(set == clicking + 1)
                    {
                        break;
                    }
                }
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

        private void next_Click(object sender, RoutedEventArgs e)
        {
            clicking++;
            FoodShowing();
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            clicking++;
            FoodShowing();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
