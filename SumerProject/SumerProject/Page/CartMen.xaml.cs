using SumerProject.Assets;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SumerProject.Page
{
    public partial class CartMen : Window, INotifyPropertyChanged
    {
        private ObservableCollection<CartProduct> _items = new ObservableCollection<CartProduct>();
        public ObservableCollection<CartProduct> Items
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
            DataContext = this;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Items.Clear();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            if (OrderRegistration.CurrentOrderWindow != null)
            {
                OrderRegistration.CurrentOrderWindow.AddProducts(Items.ToList());
            }
            else
            {
                OrderRegistration orderWindow = new OrderRegistration(Items.ToList());
                orderWindow.Show();
            }
            this.Close();
        }
    }
}
