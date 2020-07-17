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
    /// Interaction logic for Date.xaml
    /// </summary>
    public partial class Date : UserControl
    {
        public static string choosenDate;
        public Date()
        {
            InitializeComponent();
            calender.DisplayDateStart = DateTime.Today;
            calender.BlackoutDates.Add(new CalendarDateRange(new DateTime(1990, 1, 1),
            DateTime.Now.AddDays(-1)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            choosenDate = calender.SelectedDate.Value.ToString("dd/MM/yyyy");
            AdminPanel.date = choosenDate.ToString();
            UserPanel.date = choosenDate.ToString();
            Clock clock = new Clock();
            clock.Show();
            
        }
    }
}
