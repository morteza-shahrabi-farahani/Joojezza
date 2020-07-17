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
    /// Interaction logic for EditFood.xaml
    /// </summary>
    public partial class EditFood : Window
    {
        string imageLocation;
        public EditFood()
        {
            InitializeComponent();
            string imageLocation;

            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Food", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                if(FoodCard.id == int.Parse(sqlDataReader["id"].ToString()))
                {
                    nameTxt.Text = sqlDataReader["name"].ToString();
                    descriptionTxt.Text = sqlDataReader["description"].ToString();
                    priceTxt.Text = sqlDataReader["price"].ToString();
                    numberTxt.Text = sqlDataReader["number"].ToString();
                    imageLocation = sqlDataReader["imageFile"].ToString();
                    foodImage1.Source = new BitmapImage(new Uri(imageLocation));
                    ChiefTxt.Text = sqlDataReader["chief"].ToString();
                }
            }
            sqlDataReader.Close();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            int foodCounter = 0;
            int counter = 0;
            bool same = false;
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Food", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                if (nameTxt.Text == sqlDataReader["name"].ToString())
                {
                    same = true;
                }

                if (AdminPanel.date == sqlDataReader["date"].ToString())
                {
                    foodCounter++;
                }

                counter++;
            }


            sqlDataReader.Close();

                if (nameTxt.Text == "" || numberTxt.Text == "" || descriptionTxt.Text == "" || priceTxt.Text == "")
                {
                    MessageBox.Show("invalid inputs", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                   

                        string date = AdminPanel.date;
                        string time1, time2, time3, time4;
                        time1 = AdminPanel.clock1.ToString();
                        time2 = AdminPanel.clock2.ToString();
                        time3 = AdminPanel.clock3.ToString();
                        time4 = AdminPanel.clock4.ToString();
                        ComboBoxItem unknown = (ComboBoxItem)typeComboBox.SelectedItem;
                        string type = unknown.Content.ToString();

                        
                        SqlCommand sqlCommand2 = new SqlCommand("update Food set date = @date, name = @name, Time1 = @Time1, Time2 = @Time2, Time3 = @Time3, Time4 = @Time4, number = @number, type = @type, description = @description, price = @price, imageFile = @imageFile, chief = @chief where id = @id", sqlConnection);
                        sqlCommand2.Parameters.Add("@id", FoodCard.id);
                        sqlCommand2.Parameters.Add("@name", nameTxt.Text);
                        sqlCommand2.Parameters.Add("@date", date);
                        sqlCommand2.Parameters.Add("@Time1", time1);
                        sqlCommand2.Parameters.Add("@Time2", time2);
                        sqlCommand2.Parameters.Add("@Time3", time3);
                        sqlCommand2.Parameters.Add("@Time4", time4);
                        sqlCommand2.Parameters.Add("@number", int.Parse(numberTxt.Text));
                        sqlCommand2.Parameters.Add("@type", type);
                        sqlCommand2.Parameters.Add("@description", descriptionTxt.Text);
                        sqlCommand2.Parameters.Add("@price", int.Parse(priceTxt.Text));
                        if (imageLocation != "")
                        {
                            sqlCommand2.Parameters.Add("@imageFile", imageLocation);
                        }
                        else
                        {
                            sqlCommand2.Parameters.Add("@imageFile", "G:/works/university/AP/Joojezza/Joojezza/logo/logo.png");
                        }
                        sqlCommand2.Parameters.Add("@chief", ChiefTxt.Text);
                        

                        sqlCommand2.ExecuteNonQuery();
                        sqlConnection.Close();

                        this.Close();
                    
                    
                }
            
        }

        private void picture_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BitmapImage temp = new BitmapImage();
                    temp.UriSource = new Uri(openFileDialog.FileName);
                    foodImage1.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    imageLocation = openFileDialog.FileName;
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from Food", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                if (FoodCard.id == int.Parse(sqlDataReader["id"].ToString()))
                {
                    nameTxt.Text = sqlDataReader["name"].ToString();
                    descriptionTxt.Text = sqlDataReader["description"].ToString();
                    priceTxt.Text = sqlDataReader["price"].ToString();
                    numberTxt.Text = sqlDataReader["number"].ToString();
                    imageLocation = sqlDataReader["imageFile"].ToString();
                    foodImage1.Source = new BitmapImage(new Uri(imageLocation));
                    ChiefTxt.Text = sqlDataReader["chief"].ToString();
                }
            }
            sqlDataReader.Close();
            sqlConnection.Close();
        }
    }
}
