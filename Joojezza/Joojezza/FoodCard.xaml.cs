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
                button.Content = "Edit";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(MainWindow.position == "user")
            {
               
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("insert into Cart ([name],[date],[Time1],[number],[type],[description],[price],[imageFile],[Time2],[Time3],[Time4],[userID]) values(@name,@date,@Time1,@number,@type,@description,@price,@imageFile,@Time2,@Time3,@userID)", sqlConnection);
                sqlCommand.Parameters.Add("@name", nameTxt.Text);
                sqlCommand.Parameters.Add("@date", UserPanel.date);
                sqlCommand.Parameters.Add("@Time1", UserPanel.clock1);
                sqlCommand.Parameters.Add("@Time2", UserPanel.clock2);
                sqlCommand.Parameters.Add("@Time3", UserPanel.clock3);
                sqlCommand.Parameters.Add("@Time4", UserPanel.clock4);
                sqlCommand.Parameters.Add("@number", numberTxt.Text);
                sqlCommand.Parameters.Add("@type", typeTxt.Content);
                sqlCommand.Parameters.Add("@description", foodInformation1.Text);
                sqlCommand.Parameters.Add("@price", priceTxt.Text);
                sqlCommand.Parameters.Add("@imageFile", foodImage1.Source);
                sqlCommand.Parameters.Add("@userID", UserLogin.id);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            else
            {
                
            }
        }
    }
}
