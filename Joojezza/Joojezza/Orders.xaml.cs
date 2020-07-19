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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        public List<OrderData> datas { set; get; }
        public Orders()
        {
            InitializeComponent();
            RecevingData();
        }

        private void RecevingData()
        {
            int counter = 1;
            datas = new List<OrderData>();
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from Orders", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                OrderData data = new OrderData();
                data.number = counter;
                data.price = sqlDataReader["totalPrice"].ToString() + "$";
                data.today = sqlDataReader["today"].ToString();
                data.date = sqlDataReader["date"].ToString();
                datas.Add(data);
                counter++;
            }
            sqlDataReader.Close();

            DataContext = this;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void More_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class OrderData
    {
        public int number { set; get; }
        public string today { set; get; }
        public string date { set; get; }
        public string price { set; get; }
    }

}
