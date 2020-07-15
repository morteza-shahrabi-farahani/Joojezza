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
        public static string description;
        public static int number;
        public static int pirce;
        public FoodCard()
        {
            InitializeComponent();
            foodInformation1.Text = description;
            numberTxt.Text = number.ToString();
            priceTxt.Text = priceTxt.ToString();
            if(MainWindow.position == "user")
            {
                button.Content = "Add to cart";
            }
            else
            {
                button.Content = "Change number";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(MainWindow.position == "user")
            {
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("insert into Cart ([name],[date],[Time1],[number],[type],[description],[price],[imageFile],[Time2],[Time3],[Time4]) values(@name,@phone,@email,@id,@address,@password)", sqlConnection);
                sqlCommand.Parameters.Add("@name", nameTxt.Text);
                sqlCommand.Parameters.Add("@phone", phoneTxt.Text);
                sqlCommand.Parameters.Add("@email", emailTxt.Text);
                sqlCommand.Parameters.Add("@id", idTxt.Text);
                sqlCommand.Parameters.Add("@address", addressTxt.Text);
                sqlCommand.Parameters.Add("@password", passwordTxt.Password);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            else
            {
                
            }
        }
    }
}
