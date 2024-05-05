using SumerProject.Assets;
using SumerProject.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            Registrations registrations = new Registrations();
            registrations.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void VhodButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordTextBox.Password.Trim();

                using (var context = new TestEntities5())
                {
                    var user = context.users.FirstOrDefault(u => u.loginUser == login && u.PasswordUser == password);
                    if (user != null)
                    {
                        if (login == "admin" && password =="admin")
                        {
                            AdminPage adminPage= new AdminPage();
                            adminPage.Show();
                            this.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                        GlobalUser.SetUser(user.FirstName, user.LastName, user.Number, user.ID_User,user.ImageRes);
                        PersonalAccount personalAccount = new PersonalAccount(GlobalUser.FirstName, GlobalUser.LastName, GlobalUser.Number,GlobalUser.ID_User, GlobalUser.ImageRes);
                        personalAccount.Show();
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильный логин или пароль.");
                    }
                }
            
        }
    }
}
