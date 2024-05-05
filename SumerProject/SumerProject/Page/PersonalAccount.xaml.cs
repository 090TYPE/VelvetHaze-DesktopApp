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
    /// Логика взаимодействия для PersonalAccount.xaml
    /// </summary>
    public partial class PersonalAccount : Window
    {
        private string firstName;
        private string lastName;
        private string phoneNumber;
        private int ID_User;
        public PersonalAccount(string firstName, string lastName, string phoneNumber, int ID_User)
        {
            InitializeComponent();
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.ID_User = ID_User;

            FirstNameTextBlock.Text = this.firstName;
            LastNameTextBlock.Text = this.lastName;
            PhoneNumberTextBlock.Text = this.phoneNumber;
        }
    }
}
