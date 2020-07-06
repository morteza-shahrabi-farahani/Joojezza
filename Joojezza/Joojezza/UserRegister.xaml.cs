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
    /// Interaction logic for UserRegister.xaml
    /// </summary>
    public partial class UserRegister : Window
    {
        public UserRegister()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("insert into UserInformation ([name],[phone],[email],[id],[password]) values(@name,@phone,@email,@id,@password)", sqlConnection);
            sqlCommand.Parameters.Add("@name", nameTxt.Text);
            sqlCommand.Parameters.Add("@phone", phoneTxt.Text);
            sqlCommand.Parameters.Add("@email", emailTxt.Text);
            sqlCommand.Parameters.Add("@id", idTxt.Text);
            sqlCommand.Parameters.Add("@password", passwordTxt.Password);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
