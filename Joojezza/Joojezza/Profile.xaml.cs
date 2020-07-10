using Microsoft.Win32;
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
using System.Windows.Forms;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        string imageLocation;
        string filename;
        public Profile()
        {
            InitializeComponent();

            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from [Table]", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                if(MainWindow.position == "user")
                {
                    if(UserLogin.id == sqlDataReader["id"].ToString())
                    {
                        imageLocation = UserLogin.imageFile;
                        if(imageLocation == "")
                        {
                            imageLocation = "G:/works/university/AP/Joojezza/Joojezza/logo/logo.png";
                        }
                    }
                }
                else
                {
                    if (AdminLogin.id == sqlDataReader["id"].ToString())
                    {
                        imageLocation = AdminLogin.imageFile;
                        if (imageLocation == "")
                        {
                            imageLocation = "G:/works/university/AP/Joojezza/Joojezza/logo/logo.png";
                        }
                    }
                }
            }

            imageBrush.ImageSource = new BitmapImage(new Uri(imageLocation));
 
            if (MainWindow.position == "user")
            {
                UserLogin userLogin = new UserLogin();
                nameTxt.Text = UserLogin.name;
                addressTxt.Text = UserLogin.address;
                phoneTxt.Text = UserLogin.phone;
                emailTxt.Text = UserLogin.email;
            }

            else if(MainWindow.position == "admin")
            {
                AdminLogin adminLogin = new AdminLogin();
                nameTxt.Text = AdminLogin.name;
                addressTxt.Text = AdminLogin.address;
                phoneTxt.Text = AdminLogin.phone;
                emailTxt.Text = AdminLogin.email;
            }

            SqlConnection.Close();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            Edit edit = new Edit();
            edit.Show();
            this.Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            UserPanel userPanel = new UserPanel();
            userPanel.Show();
            this.Close();
        }

        private void changePicture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BitmapImage temp = new BitmapImage();
                    temp.UriSource = new Uri(openFileDialog.FileName);
                    imageBrush.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                    imageLocation = openFileDialog.FileName;

                    SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                    sqlConnection.Open();
                    SqlCommand sqlCommand;
                    if (MainWindow.position == "user")
                    {
                        sqlCommand = new SqlCommand("update UserInformation set imageFile = @imageFile where id = @id", sqlConnection);
                    }
                    else
                    {
                        sqlCommand = new SqlCommand("update [Table] set imageFile = @imageFile where id = @id", sqlConnection);
                    }

                    if (MainWindow.position == "user")
                    {
                        sqlCommand.Parameters.Add("@id", UserLogin.id);
                        UserLogin.imageFile = imageLocation;
                    }
                    else
                    {
                        sqlCommand.Parameters.Add("@id", AdminLogin.id);
                        AdminLogin.imageFile = imageLocation;
                    }
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
    }
}
