using System.Media;
using System.Windows;

namespace Joojizza
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer soundPlayer = new SoundPlayer(@"G:\works\university\AP\Joojizza\Joojizza\Joojizza\media.io_zapsplat_multimedia_button_click_fast_plastic_49161.wav");
            soundPlayer.Play();
        }
    }  
}
