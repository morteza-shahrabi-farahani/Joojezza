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
    /// Interaction logic for SaveFactor.xaml
    /// </summary>
    public partial class SaveFactor : Window
    {
        double totalPrice = 0;
        double price;
        List<double> prices = new List<double>();
        string cartText = "";
        string imageLocation;
        bool correct = true;
        Dictionary<string, int> food = new Dictionary<string, int>();
        Dictionary<string, int> foodNumber = new Dictionary<string, int>();
        Dictionary<string, string> foodDate = new Dictionary<string, string>();
        Dictionary<string, string> foodClock1 = new Dictionary<string, string>();
        Dictionary<string, string> foodClock2 = new Dictionary<string, string>();
        Dictionary<string, string> foodClock3 = new Dictionary<string, string>();
        Dictionary<string, string> foodClock4 = new Dictionary<string, string>();

        public SaveFactor()
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
                    cartText = cartText + "price: " + sqlDataReader["price"].ToString() + "$" + "\n";
                    cartText = cartText + "\n";

                    food.Add(sqlDataReader["name"].ToString(), int.Parse(sqlDataReader["number"].ToString()));
                    foodDate.Add(sqlDataReader["name"].ToString(), sqlDataReader["date"].ToString());
                    foodClock1.Add(sqlDataReader["name"].ToString(), sqlDataReader["Time1"].ToString());
                    foodClock2.Add(sqlDataReader["name"].ToString(), sqlDataReader["Time2"].ToString());
                    foodClock3.Add(sqlDataReader["name"].ToString(), sqlDataReader["Time3"].ToString());
                    foodClock4.Add(sqlDataReader["name"].ToString(), sqlDataReader["Time4"].ToString());

                    nameTxt.Text = sqlDataReader["username"].ToString();
                    today.Text = "Today: " + DateTime.Now.ToString("dd/MM/yyyy");
                    date.Text = "Delivery date: " + sqlDataReader["date"].ToString();
                    nameTxt.Text = UserLogin.name;

                    price = double.Parse(sqlDataReader["price"].ToString()) * int.Parse(sqlDataReader["number"].ToString());
                    prices.Add(price);
                    imageLocation = sqlDataReader["signature"].ToString();
                }

            }
            informationTxt.Text = cartText;
            sqlDataReader.Close();


            foreach (double d in prices)
            {
                totalPrice = totalPrice + d;
                pricesTxt.Text = "Total price: " + "\n" + totalPrice + "$";
            }
            if (imageLocation != "" && imageLocation != null)
            {
                image.Source = new BitmapImage(new Uri(imageLocation));
            }
            else
            {
                imageLocation = "G:/works/university/AP/Joojezza/Joojezza/logo/signature1111111121.jpg";
            }


        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("Delete from Cart where date = @date AND userID = @userID AND Time1 = @Time1 AND Time2 = @Time2 AND Time3 = @Time3 AND Time4 = @Time4", SqlConnection);
            SqlCommand.Parameters.Add("@date", UserPanel.date);
            SqlCommand.Parameters.Add("@userID", UserLogin.id);
            SqlCommand.Parameters.Add("@Time1", UserPanel.clock1);
            SqlCommand.Parameters.Add("@Time2", UserPanel.clock2);
            SqlCommand.Parameters.Add("@Time3", UserPanel.clock3);
            SqlCommand.Parameters.Add("@Time4", UserPanel.clock4);
            SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            int counter2 = 0;
            string payment = "";
            bool check = false;
            int counter = 0;
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from Orders", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                counter++;
            }
            sqlDataReader.Close();

            if (online.IsChecked == true)
            {
                payment = "online";
                check = true;
            }
            else if (presence.IsChecked == true)
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
                SqlCommand sqlCommand4 = new SqlCommand("select * from Food", SqlConnection);
                
                for (counter2 = 0; counter2 < food.Count; counter2++)
                {
                    SqlDataReader sqlDataReader1 = sqlCommand4.ExecuteReader();
                    while (sqlDataReader1.Read())
                    {
                        if (sqlDataReader1[4].ToString() == food.ElementAt(counter2).Key)
                        {

                            if (int.Parse(sqlDataReader1["number"].ToString()) < food.ElementAt(counter2).Value)
                            {
                                correct = false;
                                MessageBox.Show("We dont have enough " + food.ElementAt(counter2).Key, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                break;
                            }
                            else
                            {
                                foodNumber.Add(sqlDataReader1["name"].ToString(), int.Parse(sqlDataReader1["number"].ToString()));
                            }
                        }
                    }
                    sqlDataReader1.Close();
                }
                

                if (correct)
                {
                    SqlCommand SqlCommand3 = new SqlCommand("insert into Orders (id, username, payment, date, totalPrice, Time1, Time2, Time3, Time4, information, numberOfFood, today) values(@id, @username, @payment, @date, @totalPrice, @Time1, @Time2, @Time3, @Time4, @information, @numberOfFood, @today)", SqlConnection);
                    SqlCommand3.Parameters.Add("@date", UserPanel.date);
                    SqlCommand3.Parameters.Add("@username", UserLogin.name);
                    SqlCommand3.Parameters.Add("@id", counter + 1);
                    SqlCommand3.Parameters.Add("@payment", payment);
                    SqlCommand3.Parameters.Add("@totalPrice", totalPrice);
                    SqlCommand3.Parameters.Add("@Time1", UserPanel.clock1);
                    SqlCommand3.Parameters.Add("@Time2", UserPanel.clock2);
                    SqlCommand3.Parameters.Add("@Time3", UserPanel.clock3);
                    SqlCommand3.Parameters.Add("@Time4", UserPanel.clock4);
                    SqlCommand3.Parameters.Add("@information", cartText);
                    SqlCommand3.Parameters.Add("@numberOfFood", counter2);
                    SqlCommand3.Parameters.Add("@today", DateTime.Now.ToString("dd/MM/yyyy"));
                    SqlCommand3.ExecuteNonQuery();

                    
                    for (counter2 = 0; counter2 < food.Count; counter2++)
                    {
                        SqlCommand sqlCommand5 = new SqlCommand("update Food set number = @number1 where name = @name1 AND date = @date1 AND Time1 = @Time11 AND Time2 = @TIme21 AND Time3 = @Time31 AND Time4 = @TIme41", SqlConnection);
                        sqlCommand5.Parameters.Add("@name1", food.ElementAt(counter2).Key);
                        sqlCommand5.Parameters.Add("@date1", foodDate.ElementAt(counter2).Value);
                        sqlCommand5.Parameters.Add("@Time11", foodClock1.ElementAt(counter2).Value);
                        sqlCommand5.Parameters.Add("@Time21", foodClock2.ElementAt(counter2).Value);
                        sqlCommand5.Parameters.Add("@Time31", foodClock3.ElementAt(counter2).Value);
                        sqlCommand5.Parameters.Add("@Time41", foodClock4.ElementAt(counter2).Value);
                        sqlCommand5.Parameters.Add("@number1", foodNumber[food.ElementAt(counter2).Key] - food[food.ElementAt(counter2).Key]);
                        sqlCommand5.ExecuteNonQuery();
                    }

                    SqlCommand SqlCommand6 = new SqlCommand("Delete from Cart where date = @date AND userID = @userID AND Time1 = @Time1 AND Time2 = @Time2 AND Time3 = @Time3 AND Time4 = @Time4", SqlConnection);
                    SqlCommand6.Parameters.Add("@date", UserPanel.date);
                    SqlCommand6.Parameters.Add("@userID", UserLogin.id);
                    SqlCommand6.Parameters.Add("@Time1", UserPanel.clock1);
                    SqlCommand6.Parameters.Add("@Time2", UserPanel.clock2);
                    SqlCommand6.Parameters.Add("@Time3", UserPanel.clock3);
                    SqlCommand6.Parameters.Add("@Time4", UserPanel.clock4);
                    SqlCommand6.ExecuteNonQuery();

                    if (payment == "online")
                    {
                        MessageBoxResult result = MessageBox.Show("You successfully paid " + totalPrice + "$", "Congratulation", MessageBoxButton.OK, MessageBoxImage.Information);
                        if(result == MessageBoxResult.OK)
                        {
                            MessageBox.Show("thanks", "", MessageBoxButton.OK);
                        }
                    }
                    else if(payment == "presence")
                    {
                        MessageBox.Show("thanks", "", MessageBoxButton.OK);
                    }
                }
            }
        }
    }
}
