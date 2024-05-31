using SumerProject.Assets;
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
        }

        private void Lich_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalUser.ID_User != 0)
            {
                PersonalAccount personalAccount = new PersonalAccount(GlobalUser.FirstName, GlobalUser.LastName, GlobalUser.Number, GlobalUser.ID_User, GlobalUser.ImageRes);
                personalAccount.Show();
            }
            else
            {
                Authorization authorization = new Authorization();
                authorization.Show();
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void Muzh_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalUser.ID_User != 0)
            {
                MenСlothing menСlothing = new MenСlothing();
                menСlothing.Show();
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Перед входом на эту форму рекомендуется авторизироваться");
            }
        }

        private void Zhen_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalUser.ID_User != 0)
            {
                WomenClothing womenClothing = new WomenClothing();
                womenClothing.Show();
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Перед входом на эту форму рекомендуется авторизироваться");
            }
        }
    }
}
