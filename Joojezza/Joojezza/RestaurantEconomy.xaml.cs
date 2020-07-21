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
        List<string> dates = new List<string>();
        int counter;
        bool same = false;
        int counter2 = 0;
        public RestaurantEconomy()
        {
            InitializeComponent();
            FillTable();
        }

        private void FillTable()
        {
            
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Calculation", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                for (counter = 0; counter < dates.Count; counter++)
                {
                    if (sqlDataReader["date"].ToString() == dates[counter])
                    {
                        same = true;
                    }
                }
                if (same == false)
                {
                    dates.Add(sqlDataReader["date"].ToString());
                }

                /*Economy economy = new Economy();
                economy.Benefit = sqlDataReader["benefit"].ToString() + "$";
                economy.Price = sqlDataReader["totalPrice"].ToString() + "$";
                economy.Date = sqlDataReader["date"].ToString();
                this.listView.Items.Add(economy);*/
            }
            sqlDataReader.Close();

            for (counter = 0; counter < dates.Count; counter++)
            {
                List<double> benefit = new List<double>();
                List<double> price = new List<double>();
                double lastBenefit = 0;
                double lastPrice = 0;
                SqlCommand sqlCommand2 = new SqlCommand("select * from Calculation", sqlConnection);
                SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
                while (sqlDataReader2.Read())
                {
                    if(sqlDataReader2["date"].ToString() == dates[counter])
                    {
                        benefit.Add(double.Parse(sqlDataReader2["benefit"].ToString()));
                        price.Add(double.Parse(sqlDataReader2["totalPrice"].ToString()));
                    }

                }
                sqlDataReader2.Close();

                for(counter2 = 0; counter2 < benefit.Count; counter2++)
                {
                    lastBenefit = lastBenefit + benefit[counter2];
                    lastPrice = lastPrice + price[counter2];
                }
                Economy economy = new Economy();
                economy.Benefit = lastBenefit.ToString() + "$";
                economy.Price = lastPrice.ToString() + "$";
                economy.Date = dates[counter];
                this.listView.Items.Add(economy);
            }
        }
    }

    class Economy
    {
        public string Benefit { set; get; }
        public string Price { set; get; }
        public string Date { set; get; }
    }
}
