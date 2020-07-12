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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        public static bool changing = false;
        public Edit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(MainWindow.position == "user")
            {
                if (isValidMobileNumber(phoneTxt.Text.ToString()) && isValidEmail(emailTxt.Text.ToString()) && passwordTxt.Password.ToString() == confirmTxt.Password.ToString() && isValidNameUser(nameTxt.Text) && isValidPasswordUser(passwordTxt.Password.ToString()))
                {
                    SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                    sqlConnection.Open();
                    if (MainWindow.position == "user")
                    {
                        SqlCommand sqlCommand = new SqlCommand("update UserInformation set name = @name, phone = @phone, email = @email, password = @password where id = @id", sqlConnection);
                        sqlCommand.Parameters.Add("@id", UserLogin.id);
                        sqlCommand.Parameters.Add("@name", nameTxt.Text);
                        sqlCommand.Parameters.Add("@phone", phoneTxt.Text);
                        sqlCommand.Parameters.Add("@email", emailTxt.Text);
                        sqlCommand.Parameters.Add("@password", passwordTxt.Password);

                        sqlCommand.ExecuteNonQuery();

                        sqlConnection.Close();

                        UserLogin userLogin = new UserLogin();
                        userLogin.Show();
                        this.Close();
                    }
                    else
                    {
                        SqlCommand sqlCommand = new SqlCommand("update [Table] set name = @name, phone = @phone, email = @email, password = @password where id = @id", sqlConnection);
                        sqlCommand.Parameters.Add("@id", AdminLogin.id);
                        sqlCommand.Parameters.Add("@name", nameTxt.Text);
                        sqlCommand.Parameters.Add("@phone", phoneTxt.Text);
                        sqlCommand.Parameters.Add("@email", emailTxt.Text);
                        sqlCommand.Parameters.Add("@password", passwordTxt.Password);

                        sqlCommand.ExecuteNonQuery();

                        sqlConnection.Close();

                        AdminLogin adminLogin = new AdminLogin();
                        adminLogin.Show();
                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("invalid inputs", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (isValidMobileNumber(phoneTxt.Text.ToString()) && isValidEmail(emailTxt.Text.ToString()) && passwordTxt.Password.ToString() == confirmTxt.Password.ToString() && isValidNameAdmin(nameTxt.Text) && isValidPasswordUser(passwordTxt.Password.ToString()))
                {
                    SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
                    sqlConnection.Open();
                    if (MainWindow.position == "user")
                    {
                        SqlCommand sqlCommand = new SqlCommand("update UserInformation set name = @name, phone = @phone, email = @email, password = @password where id = @id", sqlConnection);
                        sqlCommand.Parameters.Add("@id", UserLogin.id);
                        sqlCommand.Parameters.Add("@name", nameTxt.Text);
                        sqlCommand.Parameters.Add("@phone", phoneTxt.Text);
                        sqlCommand.Parameters.Add("@email", emailTxt.Text);
                        sqlCommand.Parameters.Add("@password", passwordTxt.Password);

                        sqlCommand.ExecuteNonQuery();

                        sqlConnection.Close();

                        UserLogin userLogin = new UserLogin();
                        userLogin.Show();
                        this.Close();
                    }
                    else
                    {
                        SqlCommand sqlCommand = new SqlCommand("update [Table] set name = @name, phone = @phone, email = @email, password = @password where id = @id", sqlConnection);
                        sqlCommand.Parameters.Add("@id", AdminLogin.id);
                        sqlCommand.Parameters.Add("@name", nameTxt.Text);
                        sqlCommand.Parameters.Add("@phone", phoneTxt.Text);
                        sqlCommand.Parameters.Add("@email", emailTxt.Text);
                        sqlCommand.Parameters.Add("@password", passwordTxt.Password);

                        sqlCommand.ExecuteNonQuery();
                        changing = true;
                        sqlConnection.Close();

                        AdminLogin adminLogin = new AdminLogin();
                        adminLogin.Show();
                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("invalid inputs", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        public static bool isValidMobileNumber(string inputMobileNumber)
        {
            string strRegex = @"(^09[0-9]{9}$)|(^9[0-9]{9}$)|(^\+9809[0-9]{9}$)|(^00989[0-9]{9}$)";

            // Class Regex Repesents an 
            // immutable regular expression. 
            //   Format                Pattern 
            // xxxxxxxxxx           ^[0 - 9]{ 10}$ 
            // +xx xx xxxxxxxx     ^\+[0 - 9]{ 2}\s +[0 - 9]{ 2}\s +[0 - 9]{ 8}$ 
            // xxx - xxxx - xxxx   ^[0 - 9]{ 3} -[0 - 9]{ 4}-[0 - 9]{ 4}$ 
            Regex re = new Regex(strRegex);

            // The IsMatch method is used to validate 
            // a string or to ensure that a string 
            // conforms to a particular pattern. 
            if (re.IsMatch(inputMobileNumber))
                return (true);
            else
                return (false);
        }

        //checking validity of email
        public static bool isValidEmail(string inputEmail)
        {

            // This Pattern is use to verify the email 
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);

            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public static bool isValidNameUser(string name)
        {
            string strRegex = @"^[a-zA-Z]+$";
            Regex regex = new Regex(strRegex);
            Match match = regex.Match(name);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isValidNameAdmin(string name)
        {
            if (name.Contains("admin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isValidPasswordUser(string password)
        {
            bool valid1 = password.All(c => char.IsLetterOrDigit(c));
            bool valid2 = password.Any(c => char.IsDigit(c));
            bool valid3 = password.Any(c => char.IsLetter(c));
            if (valid2 && valid3 && valid1 == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isValidPasswordAdmin(string password)
        {
            bool valid1 = password.All(c => char.IsLetterOrDigit(c));
            bool valid2 = password.Any(c => char.IsDigit(c));
            bool valid3 = password.Any(c => char.IsLetter(c));
            if (valid2 && valid3 && valid1 == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isValidID(string id)
        {
            bool result = false;
            bool sameChars = false;
            char[] ID = id.ToCharArray();
            int a, c;
            int b = 0;
            int[] digitID = new int[ID.Length];
            int counter, counter2 = 0;
            //converting char to int
            for (counter = 0; counter < ID.Length; counter++)
            {
                digitID[counter] = (int)Char.GetNumericValue(ID[counter]);
                if (counter != 9)
                {
                    b = b + digitID[counter] * (10 - counter);
                }

            }
            a = digitID[ID.Length - 1];
            c = b % 11;
            if (digitID[0] == digitID[1] && digitID[1] == digitID[2] && digitID[2] == digitID[3] && digitID[3] == digitID[4] && digitID[4] == digitID[5] &&
                digitID[5] == digitID[6] && digitID[6] == digitID[7] && digitID[7] == digitID[8] && digitID[8] == digitID[9])
            {
                sameChars = true;
                result = false;
            }
            else if (c == 0 && a == 0)
            {
                result = true;
            }
            else if (c == 1 && a == 1)
            {
                result = true;
            }
            else if (c >= 1 && a == Math.Abs(c - 11))
            {
                result = true;
            }

            return result;

        }
    }
}
