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
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for RealSignature.xaml
    /// </summary>
    public partial class RealSignature : Window
    {
        System.Windows.Point current;
        public RealSignature()
        {
            
            InitializeComponent();
            current = new System.Windows.Point();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Line line = new Line();
                line.Stroke = System.Windows.SystemColors.WindowFrameBrush;
                line.X1 = current.X;
                line.Y1 = current.Y;
                line.X2 = e.GetPosition(canvas).X;
                line.Y2 = e.GetPosition(canvas).Y;

                current = e.GetPosition(canvas);

            }
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                current = e.GetPosition(canvas);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            SqlConnection sqlConnection3 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection3.Open();
            SqlCommand SqlCommand4 = new SqlCommand("select * from Other", sqlConnection3);
            SqlDataReader sqlDataReader4 = SqlCommand4.ExecuteReader();
            while (sqlDataReader4.Read())
            {
                id = (int.Parse(sqlDataReader4["id"].ToString()) + 1);
            }
            var signaturePath = "G:/works/university/AP/Joojezza/Joojezza/logo/signature" + UserLogin.id + id.ToString() + ".jpg";
            FileStream fileStream = new FileStream(signaturePath, FileMode.Create);
            RenderTargetBitmap temp = new RenderTargetBitmap((int)canvas.Width, (int)canvas.Height, 96, 96, PixelFormats.Default);
            temp.Render(canvas);
            JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
            jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(temp));
            jpegBitmapEncoder.Save(fileStream);
            fileStream.Close();

            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("update Cart set signature = @signature, cardNumber = @cardNumber where userID = @userID", sqlConnection);
            sqlCommand.Parameters.Add("@userId", UserLogin.id.ToString());
            sqlCommand.Parameters.Add("@cardNumber", cardTxt.Text.ToString());
            sqlCommand.Parameters.Add("@signature", signaturePath);
            sqlCommand.ExecuteNonQuery();

            SqlCommand sqlCommand2 = new SqlCommand("update Other set id = @id", sqlConnection);
            sqlCommand2.Parameters.Add("@id", id + 1);
            sqlCommand2.ExecuteNonQuery();
            sqlConnection.Close();

            SaveFactor saveFactor = new SaveFactor();
            saveFactor.Show();
            this.Close();
        }
    }
}
