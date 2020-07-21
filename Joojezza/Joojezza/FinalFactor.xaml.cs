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
    /// Interaction logic for FinalFactor.xaml
    /// </summary>
    public partial class FinalFactor : Window
    {
        string imageLocation;
        string temp;
        public FinalFactor()
        {
            InitializeComponent();
            Initializing();
        }

        private void Initializing()
        {
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from Orders", SqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (MainWindow.position == "user")
                {
                    if (OrderShowing.FinalID.ToString() == sqlDataReader["id"].ToString())
                    {

                        nameTxt.Text = sqlDataReader["username"].ToString();
                        today.Text = "Today: " + sqlDataReader["today"].ToString();
                        date.Text = "Delivery date: " + sqlDataReader["date"].ToString();
                        nameTxt.Text = UserLogin.name;
                        pricesTxt.Text = "Final price: " + sqlDataReader["totalPrice"].ToString() + "$"; ;
                        imageLocation = sqlDataReader["imageFile"].ToString();
                        informationTxt.Text = sqlDataReader["information"].ToString();
                        if (sqlDataReader["payment"].ToString() == "online")
                        {
                            checkTxt.Text = "Online";
                        }
                        else if (sqlDataReader["payment"].ToString() == "Presence")
                        {
                            checkTxt.Text = "Presence";
                        }

                        image.Source = new BitmapImage(new Uri(imageLocation));
                    }
                }
                else if(MainWindow.position == "admin")
                {
                    if (OrderShowingAdmin.FinalID.ToString() == sqlDataReader["id"].ToString())
                    {

                        nameTxt.Text = sqlDataReader["username"].ToString();
                        today.Text = "Today: " + sqlDataReader["today"].ToString();
                        date.Text = "Delivery date: " + sqlDataReader["date"].ToString();
                        nameTxt.Text = UserLogin.name;
                        pricesTxt.Text = "Final price: " + sqlDataReader["totalPrice"].ToString() + "$";
                        imageLocation = sqlDataReader["imageFile"].ToString();
                        informationTxt.Text = sqlDataReader["information"].ToString();
                        if (sqlDataReader["payment"].ToString() == "online")
                        {
                            checkTxt.Text = "Online";
                        }
                        else if (sqlDataReader["payment"].ToString() == "presence")
                        {
                            checkTxt.Text = "Presence";
                        }

                        image.Source = new BitmapImage(new Uri(imageLocation));
                    }
                }

            }
            sqlDataReader.Close();
        }
    }
}
