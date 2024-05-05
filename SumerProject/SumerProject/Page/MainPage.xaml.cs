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

namespace SumerProject.Page
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
            cab.MouseDown += cab_MouseDown;
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void Label_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            MenСlothing menСlothing = new MenСlothing();
            menСlothing.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void Label_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {
            WomenClothing womenClothing = new WomenClothing();
            womenClothing.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void cab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
