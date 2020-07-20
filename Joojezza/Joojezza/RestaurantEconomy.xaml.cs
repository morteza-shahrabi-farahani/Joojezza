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
    /// Interaction logic for RestaurantEconomy.xaml
    /// </summary>
    public partial class RestaurantEconomy : UserControl
    {
        public RestaurantEconomy()
        {
            InitializeComponent();
            FillTable();
        }

        private void FillTable()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Food", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Economy economy = new Economy();
                economy.Benefit = sqlDataReader["benefit"].ToString() + "$";
                economy.Price = sqlDataReader["totalPrice"].ToString() + "$";
                economy.date = sqlDataReader["date"].ToString();
                this.listView.Items.Add(economy);
            }
        }
    }

    class Economy
    {
        public string Benefit { set; get; }
        public string Price { set; get; }
        public string date { set; get; }
    }
}
