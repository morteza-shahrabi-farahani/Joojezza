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
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        public static string name { get; set; }
        public static string email { get; set; }
        public static string passsword { get; set; }
        public static string address { get; set; }
        public static string id { get; set; }
        public static string phone { get; set; }
        public static string imageFile { get; set; }
        public static int counter;
        public int i = 0;
        public int vowels = 0;
        public AdminLogin()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool valid = false;
            SqlConnection SqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            SqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from [Table]", SqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while(sqlDataReader.Read())
            {
                if(idTxt.Text == sqlDataReader[2].ToString())
                {
                    if (sqlDataReader["edit"].ToString() == "true")
                    {
                        if (passwordTxt.Password == sqlDataReader[1].ToString())
                        {
                            name = sqlDataReader["name"].ToString();
                            passsword = sqlDataReader["password"].ToString();
                        }

                        address = sqlDataReader["address"].ToString();
                        id = sqlDataReader["id"].ToString();
                        email = sqlDataReader["email"].ToString();
                        phone = sqlDataReader["phone"].ToString();
                        imageFile = sqlDataReader["imageFile"].ToString();
                        valid = true;

                        AdminPanel adminPanel = new AdminPanel();
                        adminPanel.Show();
                        this.Close();
                    }
                    else
                    {
                        if (passwordTxt.Password == sqlDataReader[1].ToString())
                        {
                            name = sqlDataReader["name"].ToString();
                            counter = int.Parse((sqlDataReader["counter"].ToString())) + 1;
                            
                            for (i = 0; i < counter; i++)
                            {
                                passsword = passsword + "1";
                            }
                            for (i = 0; i < name.Length; i++)
                            {
                                char[] temp = name.ToCharArray();
                                if (temp[i] == 'a' || temp[i] == 'o' || temp[i] == 'e' || temp[i] == 'i' || temp[i] == 'u' || temp[i] == 'A' || temp[i] == 'O' || temp[i] == 'E' || temp[i] == 'I' || temp[i] == 'U')
                                {
                                    vowels++;
                                }
                            }
                            for (i = 0; i < vowels; i++)
                            {
                                passsword = passsword + "0";
                            }

                            address = sqlDataReader["address"].ToString();
                            id = sqlDataReader["id"].ToString();
                            email = sqlDataReader["email"].ToString();
                            phone = sqlDataReader["phone"].ToString();
                            imageFile = sqlDataReader["imageFile"].ToString();
                            valid = true;
                            

                            SqlCommand sqlCommand2 = new SqlCommand("update [Table] set password = @password, counter = @counter where id = @id", SqlConnection);
                            sqlCommand2.Parameters.Add("@id", sqlDataReader[0].ToString());
                            sqlDataReader.Close();
                            sqlCommand2.Parameters.Add("@password", passsword);
                            sqlCommand2.Parameters.Add("@counter", counter);
                            sqlCommand2.ExecuteNonQuery();

                            AdminPanel adminPanel = new AdminPanel();
                            adminPanel.Show();
                            this.Close();

                            break;
                        }
                    }
                    
                    

                    
                }
                
            }

            if (valid == false)
            {
                MessageBox.Show("Login failed", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SqlConnection.Close();
        }
    }
}
