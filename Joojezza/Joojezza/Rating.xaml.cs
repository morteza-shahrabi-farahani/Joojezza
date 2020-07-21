using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for Rating.xaml
    /// </summary>
    public partial class Rating : Window
    {
        public Rating()
        {
            InitializeComponent();
            FillCombo();
        }

        private void FillCombo()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Food", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                foodCombo.Items.Add(sqlDataReader["name"].ToString());
            }
            sqlDataReader.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            double hold = 0;
            double rate = 0;
            double result;
            if (foodCombo.SelectedIndex > -1 && ratingCombo.SelectedIndex > -1)
            {
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from Food", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if(foodCombo.SelectedItem.ToString() == sqlDataReader["name"].ToString())
                    {
                        hold = double.Parse(sqlDataReader["counter"].ToString());
                        rate = double.Parse(sqlDataReader["customers"].ToString());
                        break;
                    }
                }
                sqlDataReader.Close();

                result = (rate * hold) + double.Parse(ratingCombo.SelectedItem.ToString());
                result = result / (hold + 1);
                SqlCommand sqlCommand2 = new SqlCommand("update Food set customers = @customers, counter = @counter where name = @name", sqlConnection);
                sqlCommand2.Parameters.Add("@name", foodCombo.SelectedItem.ToString());
                sqlCommand2.Parameters.Add("@customers", result);
                sqlCommand2.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}
