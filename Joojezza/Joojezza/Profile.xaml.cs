using Microsoft.Win32;
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
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        static string unknown;
        static bool first = true;
        public Profile()
        {
            InitializeComponent();
            if(first == false)
            {
                imageBrush.ImageSource = new BitmapImage(new Uri(unknown));
            }
            else
            {
                first = false;
            }

            if (MainWindow.position == "user")
            {
                UserLogin userLogin = new UserLogin();
                nameTxt.Text = UserLogin.name;
                addressTxt.Text = UserLogin.address;
                phoneTxt.Text = UserLogin.phone;
                emailTxt.Text = UserLogin.email;
            }

            else if(MainWindow.position == "admin")
            {
                AdminLogin adminLogin = new AdminLogin();
                nameTxt.Text = AdminLogin.name;
                addressTxt.Text = AdminLogin.address;
                phoneTxt.Text = AdminLogin.phone;
                emailTxt.Text = AdminLogin.email;
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            UserPanel userPanel = new UserPanel();
            userPanel.Show();
            this.Close();
        }

        private void changePicture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +"Portable Network Graphic (*.png)|*.png";
                if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BitmapImage temp = new BitmapImage();
                    temp.UriSource = new Uri(openFileDialog.FileName);
                    imageBrush.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                    unknown = openFileDialog.FileName;
                }
            }
            catch(Exception)
            {
                System.Windows.Forms.MessageBox.Show("Eror", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
