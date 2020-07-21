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
using System.Globalization;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for OrderShowingAdmin.xaml
    /// </summary>
    public partial class OrderShowingAdmin : UserControl
    {
        public List<OrderData3> datas { set; get; }
        List<int> ids = new List<int>();
        public static int FinalID { set; get; }
        string name, number;
        List<string> names = new List<string>();
        List<string> numbers = new List<string>();
        List<int> intNumbers = new List<int>();
        int intNumber;
        int counter;
        string date, time1, time2, time3, time4;
        string[] today;
        string[] orderDate;
        List<int> firstNumbers = new List<int>();
        string payment;
        double price;
        double depositPrice;
        double cashPrice;
        double totalPrice;
        double gain;
        string username;
        public OrderShowingAdmin()
        {
            InitializeComponent();
            RecevingData();
        }

        private void RecevingData()
        {
            int counter = 1;
            datas = new List<OrderData3>();
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from Orders", sqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                
                OrderData3 data = new OrderData3();
                data.Number = counter;
                data.Price = sqlDataReader["totalPrice"].ToString() + "$";
                data.Today = sqlDataReader["today"].ToString();
                data.Date = sqlDataReader["date"].ToString();
                data.Gain = sqlDataReader["gain"].ToString();
                datas.Add(data);
                string temp2 = sqlDataReader["totalPrice"].ToString();
                price = double.Parse(temp2);
                this.listView.Items.Add(new OrderData3 { Number = counter, Today = sqlDataReader["today"].ToString(), Price = sqlDataReader["totalPrice"].ToString() + "$", Date = sqlDataReader["date"].ToString(), Gain = sqlDataReader["gain"].ToString() });
                ids.Add(int.Parse(sqlDataReader["id"].ToString()));
                counter++;
                
            }
            sqlDataReader.Close();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            bool correct = true;
            var item = (sender as FrameworkElement).DataContext;
            int index = listView.Items.IndexOf(item);

            SqlConnection sqlConnection2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection2.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from Orders", sqlConnection2);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (sqlDataReader["id"].ToString() == ids[index].ToString())
                {
                    FinalID = ids[index];
                    name = sqlDataReader["name"].ToString();
                    number = sqlDataReader["number"].ToString();
                    names.Add(name);
                    numbers.Add(number);
                    date = sqlDataReader["date"].ToString();
                    time1 = sqlDataReader["Time1"].ToString();
                    time2 = sqlDataReader["Time2"].ToString();
                    time3 = sqlDataReader["Time3"].ToString();
                    time4 = sqlDataReader["Time4"].ToString();
                    username = sqlDataReader["username"].ToString();
                    gain = double.Parse(sqlDataReader["gain"].ToString());
                    totalPrice = double.Parse(sqlDataReader["totalPrice"].ToString());
                    payment = sqlDataReader["payment"].ToString();
                    orderDate = sqlDataReader["date"].ToString().Split('/');
                    today = DateTime.Now.ToString("dd/MM/yyyy").Split('/');
                }
            }
            sqlDataReader.Close();

            if (int.Parse(orderDate[2]) < int.Parse(today[2]))
            {
                correct = false;
            }
            else if (int.Parse(orderDate[2]) == int.Parse(today[2]))
            {
                if (int.Parse(orderDate[1]) < int.Parse(today[1]))
                {
                    correct = false;
                }
                else if (int.Parse(orderDate[1]) == int.Parse(today[1]))
                {
                    if (int.Parse(orderDate[0]) <= int.Parse(today[0]))
                    {
                        correct = false;
                    }
                    else
                    {
                        correct = true;
                    }
                }
                else
                {
                    correct = true;
                }
            }
            else
            {
                correct = true;
            }

            if (correct)
            {
                SqlCommand sqlCommand2 = new SqlCommand("Delete from Orders where id = @id And totalPrice = @totalPrice", sqlConnection2);
                sqlCommand2.Parameters.Add("@id", FinalID);
                sqlCommand2.Parameters.Add("@totalPrice", totalPrice.ToString());
                sqlCommand2.ExecuteNonQuery();
                sqlConnection2.Close();

                SqlConnection sqlConnection3 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                sqlConnection3.Open();
                SqlCommand SqlCommand4 = new SqlCommand("select * from Food", sqlConnection3);
                SqlDataReader sqlDataReader4 = SqlCommand4.ExecuteReader();
                while (sqlDataReader4.Read())
                {
                    for (counter = 0; counter < names.Count; counter++)
                    {
                        if ((names[counter] == (sqlDataReader4["name"].ToString() + " ") || names[counter] == (sqlDataReader4["name"].ToString())) && date == sqlDataReader4["date"].ToString() && time1 == sqlDataReader4["Time1"].ToString() && time2 == sqlDataReader4["Time2"].ToString() && time3 == sqlDataReader4["Time3"].ToString() && time4 == sqlDataReader4["Time4"].ToString())
                        {
                            firstNumbers.Add(int.Parse(sqlDataReader4["number"].ToString()));
                        }
                    }
                }
                sqlDataReader4.Close();

                SqlCommand sqlCommand3 = new SqlCommand("update Food set number = @number where name = @name And date = @date And Time1 = @Time1 And Time2 = @Time2 And Time3 = @Time3 And Time4 = @Time4", sqlConnection3);
                for (counter = 0; counter < names.Count; counter++)
                {
                    sqlCommand3.Parameters.Add("@name", names[counter]);
                    sqlCommand3.Parameters.Add("@date", date);
                    sqlCommand3.Parameters.Add("@Time1", time1);
                    sqlCommand3.Parameters.Add("@Time2", time2);
                    sqlCommand3.Parameters.Add("@Time3", time3);
                    sqlCommand3.Parameters.Add("@Time4", time4);
                    sqlCommand3.Parameters.Add("@number", (number[counter] + firstNumbers[counter]));
                    sqlCommand3.ExecuteNonQuery();
                }

                double benefit = 0;
                double totalPrice2 = 0;
                SqlCommand sqlCommand = new SqlCommand("select * from Calculation", sqlConnection3);
                SqlDataReader sqlDataReader1 = sqlCommand.ExecuteReader();
                while (sqlDataReader1.Read())
                {
                    if (sqlDataReader1["date"].ToString() == date)
                    {
                        benefit = double.Parse(sqlDataReader1["benefit"].ToString());
                        totalPrice = double.Parse(sqlDataReader1["totalPrice"].ToString());
                    }
                }
                sqlDataReader1.Close();

                SqlCommand sqlCommand1 = new SqlCommand("update Calculation set benefit = @benefit, totalPrice = @totalPrice where date = @date", sqlConnection3);
                sqlCommand1.Parameters.Add("@date", date);
                sqlCommand1.Parameters.Add("@totalPrice", (totalPrice2 - price).ToString());
                sqlCommand1.Parameters.Add("@benefit", (benefit - gain).ToString());
                sqlCommand1.ExecuteNonQuery();

                depositPrice = price * 9 / 10;
                cashPrice = price - depositPrice;
                if (MainWindow.position == "user")
                {
                    if (payment == "online")
                    {
                        MessageBox.Show("We dwposit " + depositPrice + " to your account", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (payment == "presence")
                    {
                        MessageBox.Show("Please deposit " + cashPrice + " to your account", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    SqlCommand sqlCommand5 = new SqlCommand("insert into Message (name, message) values(@name, @message)", sqlConnection3);
                    sqlCommand5.Parameters.Add("@name", name);
                    sqlCommand5.Parameters.Add("@message", "Your order is being canceled by admin. We return " + totalPrice.ToString() + " to your account.");
                    sqlCommand5.ExecuteNonQuery();
                    sqlConnection3.Close();
                }
            }
            else
            {
                MessageBox.Show("You can`t cancel your order now!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void More_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = listView.Items.IndexOf(item);

            SqlConnection sqlConnection2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection2.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from Orders", sqlConnection2);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (sqlDataReader["id"].ToString() == ids[index].ToString())
                {
                    FinalID = ids[index];

                }
            }

            FinalFactor finalFactor = new FinalFactor();
            finalFactor.Show();
        }


    }

    public class OrderData3
    {

        public int Number { set; get; }
        public string Today { set; get; }
        public string Date { set; get; }
        public string Price { set; get; }
        public string Gain { set; get; }
    }
}
