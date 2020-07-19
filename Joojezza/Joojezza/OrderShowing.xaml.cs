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
    /// Interaction logic for OrderShowing.xaml
    /// </summary>
    public partial class OrderShowing : UserControl
    {
        public List<OrderData2> datas { set; get; }
        public OrderShowing()
        {
            InitializeComponent();
            RecevingData();
        }

        private void RecevingData()
        {
            int counter = 1;
            datas = new List<OrderData2>();
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from Orders", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
            
            while (sqlDataReader.Read())
            {
                OrderData2 data = new OrderData2();
                data.Number = counter;
                data.Price = sqlDataReader["totalPrice"].ToString() + "$";
                data.Today = sqlDataReader["today"].ToString();
                data.Date = sqlDataReader["date"].ToString();
                datas.Add(data);
                this.listView.Items.Add(new OrderData2 { Number = counter, Today = sqlDataReader["today"].ToString(), Price = sqlDataReader["totalPrice"].ToString() + "$", Date = sqlDataReader["date"].ToString()});
                counter++;
            }
            sqlDataReader.Close();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void More_Click(object sender, RoutedEventArgs e)
        {
            /*var curItem = ((ListBoxItem)listView.ContainerFromElement((Button)sender));
            listView.SelectedItem = (ListBoxItem)curItem;
            MessageBox.Show(listView.SelectedIndex.ToString());*/

            var item = (sender as FrameworkElement).DataContext;
            int index = listView.Items.IndexOf(item);
            
        }
    }

    public class OrderData2
    {
        public int Number { set; get; }
        public string Today { set; get; }
        public string Date { set; get; }
        public string Price { set; get; }
    }
}
