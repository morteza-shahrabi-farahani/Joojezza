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
using System.Security.Cryptography.X509Certificates;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for UserLoginxaml.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public static string name { get; set; }
        public static string email { get; set; }
        public static string passsword { get; set; }
        public static string address { get; set; }
        public static string id { get; set; }
        public static string phone { get; set; }
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
            bool valid = false;
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from [UserInformation]", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                if (emailTxt.Text == sqlDataReader["email"].ToString() && passwordTxt.Password == sqlDataReader["password"].ToString())
                {
                    name = sqlDataReader["name"].ToString();
                    passsword = sqlDataReader["password"].ToString();
                    address = sqlDataReader["address"].ToString();
                    id = sqlDataReader["id"].ToString();
                    email = sqlDataReader["email"].ToString();
                    phone = sqlDataReader["phone"].ToString();
                    valid = true; 

                    UserPanel userPanel = new UserPanel();
                    userPanel.Show();
                    this.Close();
                }
            }

            if(valid == false)
            {
                MessageBox.Show("Login failed", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SqlConnection.Close();
        }
    }
}
