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
using System.ComponentModel;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for NameSearch.xaml
    /// </summary>
    public partial class NameSearch : Window
    {
        List<string> names = new List<string>();
        List<FoodList> foodlists = new List<FoodList>();
        private CollectionViewSource foodCollection;
        public event PropertyChangedEventHandler PropertyChanged;
        
        public NameSearch()
        {
            InitializeComponent();
            
            FillList();
        }

        private void FillList()
        {
            
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\works\university\AP\Joojezza\Joojezza\Joojezza\Joojezza\users.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand SqlCommand = new SqlCommand("select * from Food", sqlConnection);
            SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
            while(sqlDataReader.Read())
            {
                FoodList foodList = new FoodList();
                foodList.Name = sqlDataReader["name"].ToString();
                names.Add(foodList.Name);
                foodList.Price = int.Parse(sqlDataReader["price"].ToString());
                foodList.Number = int.Parse(sqlDataReader["number"].ToString());
                foodList.Information = sqlDataReader["description"].ToString();
                foodList.Date = sqlDataReader["date"].ToString();
                foodList.Type = sqlDataReader["type"].ToString();
                if(sqlDataReader["Time1"].ToString() == "1")
                {
                    foodList.Time = "12-14";
                }
                else if (sqlDataReader["Time2"].ToString() == "2")
                {
                    foodList.Time = "14-16";
                }
                else if (sqlDataReader["Time3"].ToString() == "3")
                {
                    foodList.Time = "20-22";
                }
                else if (sqlDataReader["Time4"].ToString() == "4")
                {
                    foodList.Time = "22-24";
                }
                foodlists.Add(foodList);
                this.foods.Items.Add(new FoodList { Number = foodList.Number, Name = foodList.Name, Price = foodList.Price, Date = foodList.Date, Information = foodList.Information, Time = foodList.Time, Type = foodList.Type });
                
            }

        }


        private void FoodFilter(object item)
        {
            /*if(String.IsNullOrEmpty(txtFilter.Text))
            {
                return true;
            }
            else
            {
                return ((item as FoodList).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }*/

            
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            int counter;
            foods.Items.Clear();
            for(counter = 0; counter < foodlists.Count; counter++)
            {
                if(foodlists[counter].Name.ToLower().Contains(txtFilter.Text.ToLower()))
                {
                    this.foods.Items.Add(new FoodList { Number = foodlists[counter].Number, Name = foodlists[counter].Name, Price = foodlists[counter].Price, Date = foodlists[counter].Date, Information = foodlists[counter].Information, Time = foodlists[counter].Time, Type = foodlists[counter].Type });
                }
            }
        }
    }
}
