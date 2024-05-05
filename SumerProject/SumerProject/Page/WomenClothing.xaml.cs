using SumerProject.Assets;
using SumerProject.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Wpf.Ui.Input;

namespace SumerProject.Page
{
    public partial class WomenClothing : Window, INotifyPropertyChanged
    {
        public WomenClothing()
        {
            InitializeComponent();
            DataContext = this;

            // Initialize Products collection
            Products = new ObservableCollection<ProductsWomen>();
            // Call LoadData method to retrieve data from database
            LoadData();
            ProductList.SelectionChanged += ProductList_SelectionChanged;

        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductsWomen selectedProduct = ProductList.SelectedItem as ProductsWomen;

            if (selectedProduct != null)
            {
                // Используйте конструктор, принимающий объект ProductsMen
                ProductWomen productwomen = new ProductWomen(selectedProduct);
                productwomen.DataContext = selectedProduct;
                productwomen.Show();

            }
        }
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            // Получите выбранный элемент (товар)
            ProductsWomen selectedProduct = ProductList.SelectedItem as ProductsWomen;

            if (selectedProduct != null)
            {
                // Используйте конструктор, принимающий объект ProductsMen
                ProductWomen product = new ProductWomen(selectedProduct);
                product.DataContext = selectedProduct;
                product.Show();

            }
        }
        private ObservableCollection<ProductsWomen> _products;
        public ObservableCollection<ProductsWomen> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }
        private void LoadData(string category = null)
        {
            try
            {
                using (TestEntities5 context = new TestEntities5()) // Replace with your context name
                {
                    // Retrieve products from the database
                    var productsQuery = context.product
                .Where(product => product.Gender == "Женский" && (category == null || product.Category == category))
                .Select(product => new
                {
                    ID_Product = product.ID_Product,
                    NameProduct = product.NameProduct,
                    Coast = product.Coast,
                    ImageData = product.ImageRes,
                    Color = product.Color,
                    DescriptionProduct = product.DescriptionProduct
                })
                .ToList();

                    List<ProductsWomen> productsWomen = productsQuery.Select(p => new ProductsWomen
                    {
                        ID_Product = p.ID_Product,
                        NameProduct = p.NameProduct,
                        Coast = p.Coast,
                        ImageRes = ByteArrayToBitmapImage(p.ImageData),
                        Color = p.Color,
                        DescriptionProduct = p.DescriptionProduct
                    }).ToList();

                    // Clear existing products before adding new ones (optional)
                    Products.Clear();

                    // Add retrieved products to the ObservableCollection
                    foreach (var product in productsWomen)
                    {
                        Products.Add(product);
                    }

                    // Handle empty data sets (optional)
                    if (Products.Count == 0)
                    {
                        MessageBox.Show("No products found in the database.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting data: " + ex.ToString());
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

        // Implement INotifyPropertyChanged interface for data binding
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OpenCart_Click(object sender, RoutedEventArgs e)
        {
            CartWomen cart = new CartWomen();
            cart.Show();
        }

        private void VseClick(object sender, RoutedEventArgs e)
        {
            LoadData(); // Загрузка всех товаров
        }

        private void FutClick(object sender, RoutedEventArgs e)
        {
            LoadData("Футболка");
        }

        private void PlatClick(object sender, RoutedEventArgs e)
        {
            LoadData("Рубашка");
        }

        private void DzhinsClick(object sender, RoutedEventArgs e)
        {
            LoadData("Джинсы");
        }

        private void KurClick(object sender, RoutedEventArgs e)
        {
            LoadData("Куртка");
        }

        private void ObuvClick(object sender, RoutedEventArgs e)
        {
            LoadData("Обувь");
        }
    }

    // View model class to hold product display properties
    public class ProductsWomen : INotifyPropertyChanged
    {
        public int ID_Product { get; set; }
        public string NameProduct { get; set; }
        public decimal Coast { get; set; }
        public BitmapImage ImageRes { get; set; }
        public string Color { get; set; }
        public string DescriptionProduct { get; set; }
        public string SelectedSize { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
