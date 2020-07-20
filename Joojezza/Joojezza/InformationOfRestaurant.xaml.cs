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
    /// Interaction logic for InformationOfRestaurant.xaml
    /// </summary>
    public partial class InformationOfRestaurant : UserControl
    {
        public static int restaurantID { get; set; }
        public InformationOfRestaurant()
        {
            InitializeComponent();
            if(MainWindow.position == "user")
            {
                edit.Visibility = Visibility.Hidden;
                changePicture.Visibility = Visibility.Hidden;
            }

            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from [RestaurantInformation]", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                managerTxt.Text = "manager name: " + sqlDataReader["manager"].ToString();
                addressTxt.Text = "address of Joojezza: " + sqlDataReader["address"].ToString();
                neighbourhoodTxt.Text = "neighbourhood of Joojezza: " + sqlDataReader["neighbourhood"].ToString();
                typeTxt.Text = "What type of foods do we have? " + sqlDataReader["type"].ToString();
                restaurantID = int.Parse(sqlDataReader["id"].ToString());
                image.Source = new BitmapImage(new Uri(sqlDataReader["imageFile"].ToString()));
            }

            SqlConnection.Close();
        }

        private void changePicture_Click(object sender, RoutedEventArgs e)
        {
            string imageLocation;
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BitmapImage temp = new BitmapImage();
                    temp.UriSource = new Uri(openFileDialog.FileName);
                    image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    imageLocation = openFileDialog.FileName;

                    SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update RestaurantInformation set imageFile = @imageFile where id = @id", sqlConnection);
                    
                    sqlCommand.Parameters.Add("@id", restaurantID);
                    
                    sqlCommand.Parameters.Add("@imageFile", imageLocation);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            EditRestaurantInformation editRestaurantInformation = new EditRestaurantInformation();
            editRestaurantInformation.Show();
            
        }
    }
}
