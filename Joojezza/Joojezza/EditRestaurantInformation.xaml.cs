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
    /// Interaction logic for EditRestaurantInformation.xaml
    /// </summary>
    public partial class EditRestaurantInformation : Window
    {
        public EditRestaurantInformation()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("update RestaurantInformation set manager = @manager, address = @address, neighbourhood = @neighbourhood, type = @type where id = @id", sqlConnection);
                sqlCommand.Parameters.Add("@id", InformationOfRestaurant.restaurantID);
                sqlCommand.Parameters.Add("@manager", managerTxt.Text);
                sqlCommand.Parameters.Add("@address", addressTxt.Text);
                sqlCommand.Parameters.Add("@neighbourhood", neighbourhoodTxt.Text);
                sqlCommand.Parameters.Add("@type", typeTxt.Text);

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                this.Close();
            
            
        }
    }
}
