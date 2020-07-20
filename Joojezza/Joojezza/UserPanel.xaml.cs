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
using System.Security.Cryptography;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        public static string date = "";
        public static int clock1 = 0;
        public static int clock2 = 0;
        public static int clock3 = 0;
        public static int clock4 = 0;
        public static int price { set; get; }
        public static bool cart { set; get; }
        bool same = false;
        int number = 0;
        int set = 0;
        int clicking = 0;
        public static List<string> names = new List<string>();
        public UserPanel()
        {
            InitializeComponent();
            cart = false;
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

            switch (index)
            {
                case 0:
                    principal.Children.Clear();
                    foodCard1.Visibility = Visibility.Hidden;
                    foodCard2.Visibility = Visibility.Hidden;
                    foodCard3.Visibility = Visibility.Hidden;
                    next.Visibility = Visibility.Hidden;
                    previous.Visibility = Visibility.Hidden;
                    orderShowing.Visibility = Visibility.Hidden;
                    payment.Visibility = Visibility.Hidden;
                    nameSearch.Visibility = Visibility.Hidden;
                    informationSearch.Visibility = Visibility.Hidden;
                    priceSearch.Visibility = Visibility.Hidden;
                    principal.Children.Add(new Date());
                    break;
                case 1:
                    cart = false;
                    principal.Children.Clear();
                    foodCard1.Visibility = Visibility.Hidden;
                    foodCard2.Visibility = Visibility.Hidden;
                    foodCard3.Visibility = Visibility.Hidden;
                    next.Visibility = Visibility.Hidden;
                    previous.Visibility = Visibility.Hidden;
                    orderShowing.Visibility = Visibility.Hidden;
                    payment.Visibility = Visibility.Hidden;
                    nameSearch.Visibility = Visibility.Hidden;
                    informationSearch.Visibility = Visibility.Hidden;
                    priceSearch.Visibility = Visibility.Hidden;
                    if (date == "" || (clock1 == 0 && clock2 == 0 && clock3 == 0 && clock4 == 0 ))
                    {
                        principal.Children.Clear();
                        MessageBox.Show("First you have to choose date and time", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        principal.Children.Clear();
                        FoodShowing();
                    }
                    break;
                case 2:
                    cart = true;
                    principal.Children.Clear();
                    foodCard1.Visibility = Visibility.Hidden;
                    foodCard2.Visibility = Visibility.Hidden;
                    foodCard3.Visibility = Visibility.Hidden;
                    next.Visibility = Visibility.Hidden;
                    previous.Visibility = Visibility.Hidden;
                    orderShowing.Visibility = Visibility.Hidden;
                    payment.Visibility = Visibility.Visible;
                    nameSearch.Visibility = Visibility.Hidden;
                    informationSearch.Visibility = Visibility.Hidden;
                    priceSearch.Visibility = Visibility.Hidden;
                    if (date == "" || (clock1 == 0 && clock2 == 0 && clock3 == 0 && clock4 == 0))
                    {
                        principal.Children.Clear();
                        MessageBox.Show("First you have to choose date and time", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        cart = true;
                        CartShowing();
                    }
                    break;
                case 3:
                    principal.Children.Clear();
                    foodCard1.Visibility = Visibility.Hidden;
                    foodCard2.Visibility = Visibility.Hidden;
                    foodCard3.Visibility = Visibility.Hidden;
                    next.Visibility = Visibility.Hidden;
                    previous.Visibility = Visibility.Hidden;
                    orderShowing.Visibility = Visibility.Hidden;
                    payment.Visibility = Visibility.Hidden;
                    nameSearch.Visibility = Visibility.Hidden;
                    informationSearch.Visibility = Visibility.Hidden;
                    priceSearch.Visibility = Visibility.Hidden;
                    principal.Children.Add(new OrderShowing());
                    break;
                case 4:
                    principal.Children.Clear();
                    foodCard1.Visibility = Visibility.Hidden;
                    foodCard2.Visibility = Visibility.Hidden;
                    foodCard3.Visibility = Visibility.Hidden;
                    next.Visibility = Visibility.Hidden;
                    previous.Visibility = Visibility.Hidden;
                    orderShowing.Visibility = Visibility.Hidden;
                    payment.Visibility = Visibility.Hidden;
                    nameSearch.Visibility = Visibility.Visible;
                    informationSearch.Visibility = Visibility.Visible;
                    priceSearch.Visibility = Visibility.Visible;
                    break;
                case 5:
                    principal.Children.Clear();
                    foodCard1.Visibility = Visibility.Hidden;
                    foodCard2.Visibility = Visibility.Hidden;
                    foodCard3.Visibility = Visibility.Hidden;
                    next.Visibility = Visibility.Hidden;
                    previous.Visibility = Visibility.Hidden;
                    orderShowing.Visibility = Visibility.Hidden;
                    nameSearch.Visibility = Visibility.Hidden;
                    priceSearch.Visibility = Visibility.Hidden;
                    informationSearch.Visibility = Visibility.Hidden;
                    principal.Children.Add(new InformationOfRestaurant());
                    break;
            }

            }

        private void Signature()
        {
            
        }

        private void CartShowing()
        {
            set = 0;
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Cart", SqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (clicking != 0)
                {
                    previous.IsEnabled = true;
                }
                else
                {
                    previous.IsEnabled = false;
                }
                if (clicking < set)
                {
                    next.IsEnabled = true;
                }
                if (sqlDataReader["date"].ToString() == Date.choosenDate && (sqlDataReader[1].ToString() == clock1.ToString() || sqlDataReader["Time2"].ToString() == clock2.ToString() || sqlDataReader["Time3"].ToString() == clock3.ToString() || sqlDataReader["Time4"].ToString() == clock4.ToString()) && number == 0)
                {
                    same = true;
                    foodCard1.numberTxt.Text = sqlDataReader["number"].ToString();
                    foodCard1.priceTxt.Text = sqlDataReader["price"].ToString() + "$";
                    foodCard1.foodInformation1.Text = sqlDataReader["description"].ToString();
                    foodCard1.nameTxt.Text = sqlDataReader["name"].ToString();
                    foodCard1.typeTxt.Content = sqlDataReader["type"].ToString();
                    foodCard1.foodImage1.Source = new BitmapImage(new Uri(sqlDataReader["imageFile"].ToString()));
                    foodCard1.Visibility = Visibility.Visible;
                    number++;
                    
                    continue;
                }

                if (sqlDataReader["date"].ToString() == Date.choosenDate && (sqlDataReader[1].ToString() == clock1.ToString() || sqlDataReader["Time2"].ToString() == clock2.ToString() || sqlDataReader["Time3"].ToString() == clock3.ToString() || sqlDataReader["Time4"].ToString() == clock4.ToString()) && number == 1)
                {
                    same = true;
                    foodCard2.numberTxt.Text = sqlDataReader["number"].ToString();
                    foodCard2.priceTxt.Text = sqlDataReader["price"].ToString() + "$";
                    foodCard2.foodInformation1.Text = sqlDataReader["description"].ToString();
                    foodCard2.nameTxt.Text = sqlDataReader["name"].ToString();
                    foodCard2.typeTxt.Content = sqlDataReader["type"].ToString();
                    foodCard2.foodImage1.Source = new BitmapImage(new Uri(sqlDataReader["imageFile"].ToString()));
                    foodCard2.Visibility = Visibility.Visible;
                    number++;

                    continue;

                }

                if (sqlDataReader["date"].ToString() == Date.choosenDate && (sqlDataReader[1].ToString() == clock1.ToString() || sqlDataReader["Time2"].ToString() == clock2.ToString() || sqlDataReader["Time3"].ToString() == clock3.ToString() || sqlDataReader["Time4"].ToString() == clock4.ToString()) && number == 2)
                {
                    same = true;
                    foodCard3.numberTxt.Text = sqlDataReader["number"].ToString();
                    foodCard3.priceTxt.Text = sqlDataReader["price"].ToString() + "$";
                    foodCard3.foodInformation1.Text = sqlDataReader["description"].ToString();
                    foodCard3.nameTxt.Text = sqlDataReader["name"].ToString();
                    foodCard3.typeTxt.Content = sqlDataReader["type"].ToString();
                    foodCard3.typeTxt.Content = sqlDataReader["type"].ToString();
                    foodCard3.foodImage1.Source = new BitmapImage(new Uri(sqlDataReader["imageFile"].ToString()));
                    foodCard3.Visibility = Visibility.Visible;
                    
                    number = 0;
                    set++;
                    next.Visibility = Visibility.Visible;
                    next.IsEnabled = true;
                    if (set > 1)
                    {
                        previous.Visibility = Visibility.Visible;
                    }
                    if (set == clicking + 1)
                    {
                        break;
                    }
                    if (clicking >= set)
                    {
                        next.IsEnabled = false;
                    }
                    if (clicking == 0)
                    {
                        previous.IsEnabled = false;
                    }
                }
                number = 0;
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

        private void FoodShowing()
        {
            set = 0;
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Food", SqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (clicking != 0)
                {
                    previous.IsEnabled = true;
                }
                else
                {
                    previous.IsEnabled = false;
                }
                if (clicking < set)
                {
                    next.IsEnabled = true;
                }
                if (sqlDataReader["date"].ToString() == Date.choosenDate && (sqlDataReader[1].ToString() == clock1.ToString() || sqlDataReader["Time2"].ToString() == clock2.ToString() || sqlDataReader["Time3"].ToString() == clock3.ToString() || sqlDataReader["Time4"].ToString() == clock4.ToString()) && number == 0)
                {
                    same = true;
                    foodCard1.numberTxt.Text = sqlDataReader["number"].ToString();
                    foodCard1.priceTxt.Text = sqlDataReader["price"].ToString() + "$";
                    foodCard1.foodInformation1.Text = sqlDataReader["description"].ToString();
                    foodCard1.nameTxt.Text = sqlDataReader["name"].ToString();
                    foodCard1.typeTxt.Content = sqlDataReader["type"].ToString();
                    foodCard1.foodImage1.Source = new BitmapImage(new Uri(sqlDataReader["imageFile"].ToString()));
                    foodCard1.Visibility = Visibility.Visible;
                    number++;
                    continue;
                }

                if (sqlDataReader["date"].ToString() == Date.choosenDate && (sqlDataReader[1].ToString() == clock1.ToString() || sqlDataReader["Time2"].ToString() == clock2.ToString() || sqlDataReader["Time3"].ToString() == clock3.ToString() || sqlDataReader["Time4"].ToString() == clock4.ToString()) && number == 1)
                {
                    same = true;
                    foodCard2.numberTxt.Text = sqlDataReader["number"].ToString();
                    foodCard2.priceTxt.Text = sqlDataReader["price"].ToString() + "$";
                    foodCard2.foodInformation1.Text = sqlDataReader["description"].ToString();
                    foodCard2.nameTxt.Text = sqlDataReader["name"].ToString();
                    foodCard2.typeTxt.Content = sqlDataReader["type"].ToString();
                    foodCard2.foodImage1.Source = new BitmapImage(new Uri(sqlDataReader["imageFile"].ToString()));
                    foodCard2.Visibility = Visibility.Visible;
                    number++;
                    continue;

                }

                if (sqlDataReader["date"].ToString() == Date.choosenDate && (sqlDataReader[1].ToString() == clock1.ToString() || sqlDataReader["Time2"].ToString() == clock2.ToString() || sqlDataReader["Time3"].ToString() == clock3.ToString() || sqlDataReader["Time4"].ToString() == clock4.ToString()) && number == 2)
                {
                    same = true;
                    foodCard3.numberTxt.Text = sqlDataReader["number"].ToString();
                    foodCard3.priceTxt.Text = sqlDataReader["price"].ToString() + "$";
                    foodCard3.foodInformation1.Text = sqlDataReader["description"].ToString();
                    foodCard3.nameTxt.Text = sqlDataReader["name"].ToString();
                    foodCard3.typeTxt.Content = sqlDataReader["type"].ToString();
                    foodCard3.typeTxt.Content = sqlDataReader["type"].ToString();
                    foodCard3.foodImage1.Source = new BitmapImage(new Uri(sqlDataReader["imageFile"].ToString()));
                    foodCard3.Visibility = Visibility.Visible;
                    number = 0;
                    set++;
                    next.Visibility = Visibility.Visible;
                    next.IsEnabled = true;
                    if (set > 1)
                    {
                        previous.Visibility = Visibility.Visible;
                    }
                    if (set == clicking + 1)
                    {
                        break;
                    }
                    if (clicking >= set)
                    {
                        next.IsEnabled = false;
                    }
                    if (clicking == 0)
                    {
                        previous.IsEnabled = false;
                    }
                }


            }
            number = 0;
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            clicking++;
            previous.Visibility = Visibility.Visible;
            if (cart == false)
            {
                FoodShowing();
            }
            else
            {
                CartShowing();
            }
            
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            clicking--;
            number = 0;
            next.IsEnabled = true;
            if (cart == false)
            {
                FoodShowing();
            }
            else
            {
                CartShowing();
            }
        }

        private void payment_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Cart", SqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                counter++;
            }
            if(counter != 0)
            {
                RealSignature realSignature = new RealSignature();
                realSignature.Show();
            }
            else
            {
                MessageBox.Show("You must add items to your cart first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            principal.Children.Add(new OrderShowing());
        }

        private void nameSearch_Click(object sender, RoutedEventArgs e)
        {
            NameSearch nameSearch = new NameSearch();
            nameSearch.Show();
        }

        private void informationSearch_Click(object sender, RoutedEventArgs e)
        {
            InformationSearch informationSearch = new InformationSearch();
            informationSearch.Show();
        }

        private void priceSearch_Click(object sender, RoutedEventArgs e)
        {
            PriceSearch priceSearch = new PriceSearch();
            priceSearch.Show();
        }
    }
}
