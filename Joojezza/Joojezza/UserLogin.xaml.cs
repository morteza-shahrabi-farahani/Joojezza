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
    /// Interaction logic for UserLoginxaml.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        SqlConnection SqlConnection = new SqlConnection();
        SqlCommand SqlCommand = new SqlCommand();
        public UserLogin()
        {
            InitializeComponent();
            SqlConnection.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =| DataDirectory |\people.mdf; Integrated Security = True; Connect Timeout = 30";
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserRegister userRegister = new UserRegister();
            userRegister.Show();
            this.Close();
        }
    }
}
