using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for BankAccount.xaml
    /// </summary>
    public partial class BankAccount : UserControl
    {
        System.Windows.Point current;
        public BankAccount()
        {
            InitializeComponent();
            current = new System.Windows.Point();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                Line line = new Line();
                line.Stroke = System.Windows.SystemColors.WindowFrameBrush;
                line.X1 = current.X;
                line.Y1 = current.Y;
                line.X2 = e.GetPosition(this).X;
                line.Y2 = e.GetPosition(this).Y;

                current = e.GetPosition(this);
                canvas.Children.Add(line);
            }
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ButtonState == MouseButtonState.Pressed)
            {
                current = e.GetPosition(this);
            }
        }
    }
}
