using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для CartMen.xaml
    /// </summary>
    public partial class CartMen : Window
    {
        private ObservableCollection<ProductsMen> _items = new ObservableCollection<ProductsMen>();
        public ObservableCollection<ProductsMen> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CartMen()
        {
            InitializeComponent();
            DataContext = this; // Установите DataContext в конструкторе
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Items.Clear(); // Очистить корзину
        }
    }
}
