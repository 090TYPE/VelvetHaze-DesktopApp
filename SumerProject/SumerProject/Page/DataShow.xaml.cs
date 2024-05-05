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
    /// Логика взаимодействия для DataShow.xaml
    /// </summary>
    public partial class DataShow : Window
    {
        private TestEntities5 db;
        public DataShow()
        {
            InitializeComponent();
            db = new TestEntities5();
            db.users.Load();
            db.product.Load();
            db.Orders.Load();
            db.Sizes.Load();
            DataGrid1.ItemsSource = db.users.Local;
            DataGrid2.ItemsSource = db.product.Local;
            DataGrid3.ItemsSource = db.Sizes.Local;
            DataGrid4.ItemsSource = db.Orders.Local;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.SaveChanges(); // Сохранение изменений для основного контекста
                MessageBox.Show("Изменения сохранены успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
