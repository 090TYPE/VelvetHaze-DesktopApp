using SumerProject.Assets;
using SumerProject.DataBase;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SumerProject.Page
{
    public partial class ProductMen : Window
    {
        private ProductsMen _selectedProduct;
        private TestEntities5 _context;
        public ImageSource ProductImage { get; set; }
        public ProductMen(ProductsMen selectedProduct)
        {
            InitializeComponent();
            _selectedProduct = selectedProduct;
            _context = new TestEntities5();
            LoadProductDetails();
            LoadSizes();
        }

        private void LoadProductDetails()
        {
            Name.Text = _selectedProduct.NameProduct;
            Name2.Text = _selectedProduct.Coast.ToString() + " ₽";
            Name3.Text = _selectedProduct.Color;
            Opis.Text = _selectedProduct.DescriptionProduct;
            ImageControl.Source = _selectedProduct.ImageRes;
        }

        private byte[] BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
        private byte[] ConvertImageToByteArray(BitmapImage image)
        {
            byte[] imageData;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                imageData = ms.ToArray();
            }
            return imageData;
        }

        private void AddCart_Click(object sender, RoutedEventArgs e)
        {
            ProductsMen selectedProduct = _selectedProduct;
            if (SizeSelector.SelectedItem != null)
                selectedProduct.SelectedSize = ((Sizes)SizeSelector.SelectedItem).Size;
            CartMen cartWindow = Application.Current.Windows.OfType<CartMen>().FirstOrDefault();

            if (cartWindow == null)
            {
                cartWindow = new CartMen();
                cartWindow.Show();
            }

            // Создание объекта CartProduct
            var cartProduct = new CartProduct
            {
                NameProduct = selectedProduct.NameProduct,
                Coast = (int)selectedProduct.Coast,
                SelectedSize = selectedProduct.SelectedSize,
                Image = ConvertImageToByteArray(_selectedProduct.ImageRes)
            };

            cartWindow.Items.Add(cartProduct);
        }

        private void LoadSizes()
        {
            var sizes = _context.Sizes
                .Where(s => s.ID_Product == _selectedProduct.ID_Product)
                .ToList();

            SizeSelector.ItemsSource = sizes;
            SizeSelector.DisplayMemberPath = "Size";
            SizeSelector.SelectedValuePath = "ID_Size";

            if (sizes.Any())
                SizeSelector.SelectedIndex = 0;
        }
    }
}
