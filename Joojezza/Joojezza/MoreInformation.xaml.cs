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
    /// Interaction logic for MoreInformation.xaml
    /// </summary>
    public partial class MoreInformation : Window
    {
        string chief;
        int customers;
        int number;
        int counter = 0;
        string name;
        public MoreInformation()
        {
            InitializeComponent();

            if (UserPanel.cart == false)
            {
                AddToCart.Content = "Add to cart";
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from Food", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    if (FoodCard.id == int.Parse(sqlDataReader["id"].ToString()))
                    {
                        chiefTxt.Text = "Chief: " + sqlDataReader["chief"].ToString();
                        customersTxt.Text = "Customers point: " + sqlDataReader["customers"].ToString();
                        chief = sqlDataReader["chief"].ToString();
                        customers = int.Parse(sqlDataReader["customers"].ToString());
                        number = int.Parse(sqlDataReader["number"].ToString());
                    }
                }
                sqlDataReader.Close();

                SqlCommand sqlCommand2 = new SqlCommand("select * from Cart", sqlConnection);
                SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();

                while (sqlDataReader2.Read())
                {
                    counter++;
                }
            }
            else
            {
                AddToCart.Content = "Change number";
            }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (UserPanel.cart == false)
            {
                if (int.Parse(numberTxt.Text) > number)
                {
                    MessageBox.Show("We don`t have this food enough.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    double result, temp;
                    temp = double.Parse(FoodCard.price.ToString());
                    result = (temp * 124 / 100);
                    SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("insert into Cart ([name],[date],[Time1],[number],[type],[description],[price],[imageFile],[Time2],[Time3],[Time4],[userID],[chief],[customers],[Id],[username]) values(@name,@date,@Time1,@number,@type,@description,@price,@imageFile,@Time2,@Time3,@Time4,@userID,@chief,@customers,@Id,@username)", sqlConnection);
                    sqlCommand.Parameters.Add("@name", FoodCard.name);
                    sqlCommand.Parameters.Add("@date", UserPanel.date);
                    sqlCommand.Parameters.Add("@Time1", UserPanel.clock1);
                    sqlCommand.Parameters.Add("@Time2", UserPanel.clock2);
                    sqlCommand.Parameters.Add("@Time3", UserPanel.clock3);
                    sqlCommand.Parameters.Add("@Time4", UserPanel.clock4);
                    sqlCommand.Parameters.Add("@number", int.Parse(numberTxt.Text));
                    sqlCommand.Parameters.Add("@type", FoodCard.type);
                    sqlCommand.Parameters.Add("@description", FoodCard.description);
                    sqlCommand.Parameters.Add("@price", result.ToString());
                    sqlCommand.Parameters.Add("@imageFile", FoodCard.imageLocation);
                    sqlCommand.Parameters.Add("@userID", UserLogin.id);
                    sqlCommand.Parameters.Add("@username", UserLogin.name);
                    sqlCommand.Parameters.Add("@chief", chief);
                    sqlCommand.Parameters.Add("@customers", customers);
                    sqlCommand.Parameters.Add("@Id", counter + 1);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                    this.Close();
                }

            }
            else
            {

                SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from Food", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    
                    if (FoodCard.id == int.Parse(sqlDataReader["id"].ToString()))
                    {
                        chiefTxt.Text = "Chief: " + sqlDataReader["chief"].ToString();
                        customersTxt.Text = "Customers point: " + sqlDataReader["customers"].ToString();
                        chief = sqlDataReader["chief"].ToString();
                        customers = int.Parse(sqlDataReader["customers"].ToString());
                        number = int.Parse(sqlDataReader["number"].ToString());
                        name = sqlDataReader["name"].ToString();  
                    }
                    
                }

                sqlDataReader.Close();

                if (int.Parse(numberTxt.Text) > number)
                {
                    MessageBox.Show("We don`t have this food enough.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    SqlCommand sqlCommand2 = new SqlCommand("update Cart set number = @number where name = @name", sqlConnection);
                    sqlCommand2.Parameters.Add("@name", name);
                    sqlCommand2.Parameters.Add("@number", int.Parse(numberTxt.Text.ToString()));
                    sqlCommand2.ExecuteNonQuery();
                    sqlConnection.Close();

                    this.Close();
                }
            }
        }
    }
}
