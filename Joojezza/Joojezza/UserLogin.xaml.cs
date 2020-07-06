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
using System.Data.SqlClient;
using System.Data;
using System.Data;
using System.Data.SqlClient;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for UserLoginxaml.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
       
        public UserLogin()
        {
            InitializeComponent();
            
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserRegister userRegister = new UserRegister();
            userRegister.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from [UserInformation]", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                if (emailTxt.Text == sqlDataReader[0].ToString() && passwordTxt.Password == sqlDataReader[1].ToString())
                {
                    MessageBox.Show("Login successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Login failed", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            SqlConnection.Close();
        }
    }
}
