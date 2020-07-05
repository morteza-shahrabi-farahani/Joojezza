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

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        
        public AdminLogin()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlCommand SqlCommand = new SqlCommand();
            SqlConnection.Open();
            SqlCommand.Connection = SqlConnection;
            SqlCommand.CommandText = "select * from Table";
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
            if(sqlDataReader.Read())
            {
                if(idTxt.Text == sqlDataReader["id"] && passwordTxt.Password == sqlDataReader["password"])
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
