using SumerProject.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SumerProject.Page
{
    public partial class MenСlothing : Window, INotifyPropertyChanged
    {
        public MenСlothing()
        {
            InitializeComponent();
            DataContext = this;

            // Initialize Products collection
            Products = new ObservableCollection<ProductsMen>();
            // Call LoadData method to retrieve data from database
            LoadData();
            ProductList.SelectionChanged += ProductList_SelectionChanged;
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductsMen selectedProduct = ProductList.SelectedItem as ProductsMen;

            if (selectedProduct != null)
            {
                // Используйте конструктор, принимающий объект ProductsMen
                ProductMen productmen = new ProductMen(selectedProduct);
                productmen.DataContext = selectedProduct;
                productmen.Show();

            }
        }
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            ProductsMen selectedProduct = ProductList.SelectedItem as ProductsMen;

            if (selectedProduct != null)
            {
                // Используйте конструктор, принимающий объект ProductsMen
                ProductMen productmen = new ProductMen(selectedProduct);
                productmen.DataContext = selectedProduct;
                productmen.Show();

            }
        }

        //private void AddToCart_Click(object sender, RoutedEventArgs e)
        //{
        //    // Получите выбранный элемент (товар)
        //    ProductsMen selectedProduct = ((FrameworkElement)sender).DataContext as ProductsMen;

        //    // Создайте экземпляр класса Cart, если он еще не создан
        //    CartMen cartWindow = Application.Current.Windows.OfType<CartMen>().FirstOrDefault();
        //    if (cartWindow == null)
        //    {
        //        cartWindow = new CartMen();
        //        cartWindow.Show();
        //    }

        //    // Добавьте выбранный товар в корзину
        //    cartWindow.Items.Add(selectedProduct);
        //}

        private ObservableCollection<ProductsMen> _products;
        public ObservableCollection<ProductsMen> Products
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
                .Where(product => product.Gender == "Мужской" && (category == null || product.Category == category))
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

                    List<ProductsMen> productsMen = productsQuery.Select(p => new ProductsMen
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
                    foreach (var product in productsMen)
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
            CartMen cart = new CartMen();
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

        private void RubClick(object sender, RoutedEventArgs e)
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
    public class ProductsMen : INotifyPropertyChanged
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
