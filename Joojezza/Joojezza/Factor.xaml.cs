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
using System.Data;
using System.Data.SqlClient;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for Factor.xaml
    /// </summary>
    public partial class Factor : UserControl
    {
        double totalPrice = 0;
        double price;
        List<double> prices = new List<double>();
        string cartText = "";
        string imageLocation;
        public Factor()
        {
            InitializeComponent();
            FillingXaml();
            
        }

        private void FillingXaml()
        {
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from Cart", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (sqlDataReader["userID"].ToString() == UserLogin.id.ToString() && sqlDataReader["date"].ToString() == UserPanel.date.ToString() && int.Parse(sqlDataReader["Time1"].ToString()) == UserPanel.clock1 && int.Parse(sqlDataReader["Time2"].ToString()) == UserPanel.clock2 && int.Parse(sqlDataReader["Time3"].ToString()) == UserPanel.clock3 && int.Parse(sqlDataReader["Time4"].ToString()) == UserPanel.clock4)
                {

                    cartText = cartText + "name: " + sqlDataReader["name"].ToString() + "\n";
                    cartText = cartText + "number: " + sqlDataReader["number"].ToString() + "\n";
                    cartText = cartText + "price: " + sqlDataReader["price"].ToString() + "\n";
                    cartText = cartText + "\n";

                    nameTxt.Text = sqlDataReader["username"].ToString();
                    today.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    date.Text = sqlDataReader["date"].ToString();

                    price = double.Parse(sqlDataReader["price"].ToString()) * int.Parse(sqlDataReader["number"].ToString());
                    prices.Add(price);
                    imageLocation = sqlDataReader["imageFile"].ToString();
                }

                
                
            }

            foreach(double d in prices)
            {
                totalPrice = totalPrice + d;
                pricesTxt.Text = "Total price: " + "\n" + totalPrice;
            }

            image.Source = new BitmapImage(new Uri(sqlDataReader["imageFile"].ToString()));
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("Delete from Cart where date = @data, username = @username, Time1 = @Time1, Time2 = @Time2, Time3 = @Time3, Time4 = @Time4", SqlConnection);
            SqlCommand.Parameters.Add("@date", UserPanel.date);
            SqlCommand.Parameters.Add("@username", UserLogin.name);
            SqlCommand.Parameters.Add("@Time1", UserPanel.clock1);
            SqlCommand.Parameters.Add("@Time2", UserPanel.clock2);
            SqlCommand.Parameters.Add("@Time3", UserPanel.clock3);
            SqlCommand.Parameters.Add("@Time4", UserPanel.clock4);
            SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string payment = "";
            bool check = false;
            int counter = 0;
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from Orders", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
            while(sqlDataReader.Read())
            {
                counter++;
            }
            sqlDataReader.Close();

            if(online.IsChecked == true)
            {
                payment = "online";
                check = true;
            }
            else if(presence.IsChecked == true)
            {
                payment = "presence";
                check = true;
            }
            else
            {
                MessageBox.Show("you should choose online or presence", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (check)
            {
                SqlCommand SqlCommand3 = new SqlCommand("insert into Orders (id, username, payment, date, totalPrice, Time1, Time2, Time3, Time4) values(@id, @username, @payment, @date, @totalPrice, @Time1, @Time2, @Time3, @Time4)", SqlConnection);
                SqlCommand3.Parameters.Add("@date", UserPanel.date);
                SqlCommand3.Parameters.Add("@username", UserLogin.name);
                SqlCommand3.Parameters.Add("@id", counter + 1);
                SqlCommand3.Parameters.Add("@payment", payment);
                SqlCommand3.Parameters.Add("@totalPrice", totalPrice);
                SqlCommand3.Parameters.Add("@Time1", UserPanel.clock1);
                SqlCommand3.Parameters.Add("@Time2", UserPanel.clock2);
                SqlCommand3.Parameters.Add("@Time3", UserPanel.clock3);
                SqlCommand3.Parameters.Add("@Time4", UserPanel.clock4);
                SqlCommand3.ExecuteNonQuery();
                SqlConnection.Close();
            }
        }
    }
}
