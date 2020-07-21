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
        double tax;
        double discount;
        double finalPrice;
        double gain;
        int counter4;
        bool same = false;
        int counter5;
        List<string> lastDates = new List<string>();
        Dictionary<string, int> food = new Dictionary<string, int>();
        Dictionary<string, int> foodNumber = new Dictionary<string, int>();
        Dictionary<string, string> foodDate = new Dictionary<string, string>();
        Dictionary<string, string> foodClock1 = new Dictionary<string, string>();
        Dictionary<string, string> foodClock2 = new Dictionary<string, string>();
        Dictionary<string, string> foodClock3 = new Dictionary<string, string>();
        Dictionary<string, string> foodClock4 = new Dictionary<string, string>();
        List<string> names = new List<string>();
        List<int> numbers = new List<int>();
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
                if (sqlDataReader["userID"].ToString() == UserLogin.id.ToString())
                {

                    cartText = cartText + "name: " + sqlDataReader["name"].ToString() + "\n";
                    cartText = cartText + "number: " + sqlDataReader["number"].ToString() + "\n";
                    cartText = cartText + "price: " + sqlDataReader["price"].ToString() + "$" + "\n";
                    cartText = cartText + "\n";

                    food.Add(sqlDataReader["name"].ToString(), int.Parse(sqlDataReader["number"].ToString()));
                    names.Add(sqlDataReader["name"].ToString());
                    numbers.Add(int.Parse(sqlDataReader["number"].ToString()));
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
                pricesTxt.Text = "Total price: " + totalPrice + "$" + "\n";
            }
            if (imageLocation != "" && imageLocation != null)
            {
                image.Source = new BitmapImage(new Uri(imageLocation));
            }
            else
            {
                imageLocation ="G:/works/university/AP/Joojezza/Joojezza/logo/signature1111111121.jpg";
            }
            tax = CalculateTax();
            discount = CalculateDiscount();
            finalPrice = CalculateFinalPrice(totalPrice, tax, discount);
            pricesTxt.Text = pricesTxt.Text + "tax: " + tax + "%" + "\n";
            pricesTxt.Text = pricesTxt.Text + "discount: " + discount + "%" + "\n";
            pricesTxt.Text = pricesTxt.Text + "finalPrice: " + finalPrice + "$" + "\n";
        }

        private double CalculateFinalPrice(double totalPrice, double tax, double discount)
        {
            double finalPrice;
            finalPrice = (totalPrice + (totalPrice * tax / 100) - (totalPrice * discount / 100));
            return finalPrice;
        }

        private double CalculateDiscount()
        {
            double discount = 0;
            int counter = 0;
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Orders", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (sqlDataReader["username"].ToString() == UserLogin.name)
                {
                    counter++;
                }
            }
            if (counter < 4)
            {
                if(counter == 1)
                {
                    discount = 5;
                }
                else if(counter == 2)
                {
                    discount = 10;
                }
                else if(counter == 3)
                {
                    discount = 0;
                }
                
            }
            else if (counter >= 4 && counter < 8)
            {
                discount = 5;
            }
            else if (counter >= 8 && counter < 12)
            {
                discount = 8;
            }
            else if (counter >= 12)
            {
                discount = 10;
            }
            sqlDataReader.Close();

            return discount;
        }

        private double CalculateTax()
        {
            double tax = 0;
            int counter = 0;
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Orders", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if(sqlDataReader["username"].ToString() == UserLogin.name)
                {
                    counter++;
                }
            }
            if(counter < 4)
            {
                tax = 9;
            }
            else if(counter >= 4 && counter < 8)
            {
                tax = 7;
            }
            else if(counter >= 8 && counter < 12)
            {
                tax = 5;
            }
            else if(counter >= 12)
            {
                tax = 0;
            }
            sqlDataReader.Close();

            return tax;
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("Delete from Cart where userID = @userID", SqlConnection);
            SqlCommand.Parameters.Add("@userID", UserLogin.id);
            SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();

            this.Close();
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
                    int counter3;
                    string finalFood = "";
                    string finalNumber = "";
                    for(counter3 = 0; counter3 < food.Count; counter3++)
                    {
                        finalFood = finalFood + names[counter3] + " ";
                    }
                    for (counter3 = 0; counter3 < food.Count; counter3++)
                    {
                        finalNumber = finalNumber + numbers[counter3].ToString() + " ";
                    }

                    double holdDouble = totalPrice - (totalPrice * 100 / 124);
                    if(discount > tax)
                    {
                        gain = holdDouble - ((discount - tax / 100) * totalPrice);
                    }
                    else
                    {
                        gain = holdDouble;
                    }

                    int counter8 = 0;
                    SqlCommand sqlCommand1 = new SqlCommand("select * from Cart", SqlConnection);
                    SqlDataReader sqlDataReader3 = sqlCommand1.ExecuteReader();
                    while (sqlDataReader3.Read())
                    {
                        counter8++;
                    }

                    string[,] dates = new string[counter8,3];
                    string min = "";
                    int hold, minimum;
                    int counter7 = 0;
                    
                    SqlCommand sqlCommand = new SqlCommand("select * from Cart", SqlConnection);
                    SqlDataReader sqlDataReader2 = sqlCommand.ExecuteReader();
                    while(sqlDataReader2.Read())
                    {
                        if(UserLogin.name == sqlDataReader2["username"].ToString())
                        {
                            dates[counter8, 0] = (sqlDataReader2["date"].ToString().Split('/')[0]);
                            dates[counter8, 1] = (sqlDataReader2["date"].ToString().Split('/')[1]);
                            dates[counter8, 2] = (sqlDataReader2["date"].ToString().Split('/')[2]);
                        }
                    }
                    for(counter7 = 0; counter7 < counter8 - 1; counter7++)
                    {
                        if (int.Parse(dates[counter7, 2]) < int.Parse(dates[counter7 + 1, 2]))
                        {
                            min = dates[counter7, 0].ToString() + "/" + dates[counter7, 1].ToString() + "/" + dates[counter7, 2].ToString();
                        }
                        else if (int.Parse(dates[counter7, 2]) == int.Parse(dates[counter7 + 1, 2] ))
                        {
                            if (int.Parse(dates[counter7, 1]) < int.Parse(dates[counter7 + 1, 1]))
                            {
                                 min = dates[counter7, 0].ToString() + "/" + dates[counter7, 1].ToString() + "/" + dates[counter7, 2].ToString();
                            }
                            else if (int.Parse(dates[counter7, 1]) == int.Parse(dates[counter7 + 1, 1]))
                            {
                                if (int.Parse(dates[counter7, 0]) <= int.Parse(dates[counter7 + 1, 0]))
                                {
                                    min = dates[counter7, 0].ToString() + "/" + dates[counter7, 1].ToString() + "/" + dates[counter7, 2].ToString();
                                }
                                else
                                {
                                    min = dates[counter7 + 1, 0].ToString() + "/" + dates[counter7 + 1, 1].ToString() + "/" + dates[counter7 + 1, 2].ToString();
                                }
                            }
                            else
                            {
                                 min = dates[counter7 + 1, 0].ToString() + "/" + dates[counter7 + 1, 1].ToString() + "/" + dates[counter7 + 1, 2].ToString();
                            }
                        }
                        else
                        {
                            min = dates[counter7 + 1, 0].ToString() + "/" + dates[counter7 + 1, 1].ToString() + "/" + dates[counter7 + 1, 2].ToString();
                        }
                    }
                    SqlCommand SqlCommand3 = new SqlCommand("insert into Orders (id, username, payment, date, totalPrice, Time1, Time2, Time3, Time4, information, numberOfFood, today, name, number, gain, imageFile) values(@id, @username, @payment, @date, @totalPrice, @Time1, @Time2, @Time3, @Time4, @information, @numberOfFood, @today, @name, @number, @gain, @imageFile)", SqlConnection);
                    SqlCommand3.Parameters.Add("@date", min);
                    SqlCommand3.Parameters.Add("@username", UserLogin.name);
                    SqlCommand3.Parameters.Add("@id", counter + 1);
                    SqlCommand3.Parameters.Add("@payment", payment);
                    SqlCommand3.Parameters.Add("@totalPrice", finalPrice.ToString());
                    SqlCommand3.Parameters.Add("@information", cartText);
                    SqlCommand3.Parameters.Add("@numberOfFood", counter2);
                    SqlCommand3.Parameters.Add("@today", DateTime.Now.ToString("dd/MM/yyyy"));
                    SqlCommand3.Parameters.Add("@name", finalFood);
                    SqlCommand3.Parameters.Add("@number", finalNumber);
                    SqlCommand3.Parameters.Add("@imageFile", imageLocation);
                    SqlCommand3.Parameters.Add("@gain", gain.ToString());
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

                    SqlCommand SqlCommand6 = new SqlCommand("Delete from Cart where userID = @userID", SqlConnection);
                    SqlCommand6.Parameters.Add("@userID", UserLogin.id);
                    SqlCommand6.ExecuteNonQuery();

                    SqlCommand sqlCommand7 = new SqlCommand("select * from Calculation", SqlConnection);
                    SqlDataReader sqlDataReader1 = sqlCommand7.ExecuteReader();
                    while(sqlDataReader1.Read())
                    {
                        counter4++;
                        lastDates.Add(sqlDataReader1["date"].ToString());
                    }
                    sqlDataReader1.Close();
                    
                    for(counter5 = 0; counter5 < lastDates.Count; counter5++)
                    {
                        if (min == lastDates[counter5])
                        {
                            same = true;
                        }
                    }
                    if (same == false)
                    {
                        SqlCommand sqlCommand8 = new SqlCommand("insert into Calculation (benefit, totalPrice, date) values(@benefit, @totalPrice, @date)", SqlConnection);
                        sqlCommand8.Parameters.Add("@benefit", gain.ToString());
                        sqlCommand8.Parameters.Add("@totalPrice", finalPrice.ToString());
                        sqlCommand8.Parameters.Add("@date", min);
                        sqlCommand8.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand sqlCommand9 = new SqlCommand("update Calculation set benefit = @benefit, totalPrice = @totalPrice where date = @date", SqlConnection);
                        sqlCommand9.Parameters.Add("@date", min);
                        sqlCommand9.Parameters.Add("@benefit", gain.ToString());
                        sqlCommand9.Parameters.Add("@totalPrice", finalPrice.ToString());
                        sqlCommand9.ExecuteNonQuery();
                    }

                    if (payment == "online")
                    {
                        MessageBoxResult result = MessageBox.Show("You successfully paid " + finalPrice + "$", "Congratulation", MessageBoxButton.OK, MessageBoxImage.Information);
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
                this.Close();
            }
       
        }
    }
}
