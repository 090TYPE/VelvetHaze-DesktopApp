using SumerProject.Assets;
using SumerProject.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
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
using Windows.System;

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
        private byte[] ImageData;


        public PersonalAccount(string firstName, string lastName, string phoneNumber, int ID_User, byte[] ImageRes)
        {
            InitializeComponent();
            LoadOrders(ID_User);
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.ID_User = ID_User;
            this.ImageData = ImageRes;
            FirstNameTextBlock.Text = this.firstName;
            LastNameTextBlock.Text = this.lastName;
            PhoneNumberTextBlock.Text = this.phoneNumber;
            if (ImageData == null || ImageData.Length == 0)
            {
                BitmapImage defaultImage = new BitmapImage(new Uri("/Resources/UnkownUser.png", UriKind.Relative));
                UserImage.Source = defaultImage;
            }
            else
            {
                BitmapImage imageSource = ByteArrayToBitmapImage(ImageData);

                UserImage.Source = imageSource;
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainPage mainPage = new MainPage();
            mainPage.Show();
        }

        private void LoadOrders(int UserId)
        {
            try
            {
                using (var context = new TestEntities5())
                {
                    int userId = UserId;

                    var orders = context.Orders
                        .Where(o => o.UserID == userId)
                        .Select(o => new
                        {
                            o.OrderID,
                            o.OrderDate,
                            o.TotalAmount,
                            o.ShippingAddress,
                            o.City,
                            o.PhoneNumber,
                            o.PostalCode
                        }).ToList();

                    OrdersListView.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке заказов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private BitmapImage ByteArrayToBitmapImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;

            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze(); // Оптимально для многопоточных приложений
            return image;
        }
    }
}
