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
    /// Interaction logic for FoodCard.xaml
    /// </summary>
    public partial class FoodCard : UserControl
    {
        public static string description { set; get; }
        public static int number { set; get; }
        public static double price { set; get; }
        public static int id { set; get; }
        public static string name { set; get; }
        public static string type { set; get; }
        public static string imageLocation { set; get; }

        public FoodCard()
        {
            InitializeComponent();
            foodInformation1.Text = description;
            numberTxt.Text = number.ToString();
            priceTxt.Text = price.ToString();
            if(MainWindow.position == "user")
            {
                if (UserPanel.cart = false)
                {
                    button.Content = "Add to cart";
                } 
                else
                {
                    button.Content = "number";
                }
            }
            else
            {
                button.Content = "Edit";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(MainWindow.position == "user")
            {
                if (UserPanel.cart == false)
                {
                    description = foodInformation1.Text;
                    number = int.Parse(numberTxt.Text.ToString());
                    price = int.Parse(priceTxt.Text.Split('$')[0]);
                    type = typeTxt.Content.ToString();
                    name = nameTxt.Text.ToString();
                    imageLocation = foodImage1.Source.ToString();

                    SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("select * from Food", sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        if (sqlDataReader["name"].ToString() == nameTxt.Text && sqlDataReader["number"].ToString() == numberTxt.Text && sqlDataReader["description"].ToString() == foodInformation1.Text)
                        {
                            id = int.Parse(sqlDataReader["id"].ToString());
                        }
                    }

                    MoreInformation moreInformation = new MoreInformation();
                    moreInformation.Show();
                }
                else
                {
                    description = foodInformation1.Text;
                    number = int.Parse(numberTxt.Text.ToString());
                    price = double.Parse(priceTxt.Text.Split('$')[0]);
                    type = typeTxt.Content.ToString();
                    name = nameTxt.Text.ToString();
                    imageLocation = foodImage1.Source.ToString();

                    SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("select * from Food", sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        if (sqlDataReader["name"].ToString() == nameTxt.Text)
                        {
                            id = int.Parse(sqlDataReader["id"].ToString());
                        }
                    }

                    MoreInformation moreInformation = new MoreInformation();
                    moreInformation.Show();
                }
            }
            else
            {
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from Cart", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    if(sqlDataReader["name"].ToString() == nameTxt.Text && sqlDataReader["number"].ToString() == numberTxt.Text && sqlDataReader["description"].ToString() == foodInformation1.Text)
                    {
                        id = int.Parse(sqlDataReader["id"].ToString());
                    }
                }

                EditFood editFood = new EditFood();
                editFood.Show();
            }
        }
    }
}
