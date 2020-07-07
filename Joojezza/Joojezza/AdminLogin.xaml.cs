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
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from [Table]", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
            if(sqlDataReader.Read())
            {
                if(idTxt.Text == sqlDataReader[0].ToString() && passwordTxt.Password == sqlDataReader[1].ToString())
                {
                    UserPanel userPanel = new UserPanel();
                    userPanel.Show();
                    this.Close();
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
