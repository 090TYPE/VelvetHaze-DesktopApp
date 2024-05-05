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
    /// Логика взаимодействия для Registrations.xaml
    /// </summary>
    public partial class Registrations : Window
    {
        public Registrations()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            {
                string login = LoginTextBox.Text.Trim();
                string password = PasswordTextBox.Password.Trim();
                string confirmPassword = PasswordTextBox2.Password.Trim();
                string number = NumberTextBox.Text.Trim();
                string firstName = FirstNameTextBox.Text.Trim();
                string lastName = LastNameTextBox.Text.Trim();

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Логин и пароль не должны быть пустыми.");
                    return;
                }

                if (password != confirmPassword)
                {
                    MessageBox.Show("Введенные пароли не совпадают.");
                    return;
                }

                using (var context = new TestEntities5())
                {
                    if (context.users.Any(u => u.loginUser == login))
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.");
                        return;
                    }

                    var newUser = new users
                    {
                        loginUser = login,
                        PasswordUser = password,
                        Number = number,
                        FirstName = firstName, 
                        LastName = lastName
                    };

                    context.users.Add(newUser);
                    context.SaveChanges();

                    MessageBox.Show("Регистрация успешна!");
                    Close();
                    Authorization authorization = new Authorization();
                    authorization.Show();
                }
            }
        }
    }   
}
