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
    /// Interaction logic for RestaurantInformation.xaml
    /// </summary>
    public partial class RestaurantInformation : Window
    {
        public RestaurantInformation()
        {
            InitializeComponent();

            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from [RestaurantInformation]", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();

            while(sqlDataReader.Read())
            {

            }
        }
    }
}
