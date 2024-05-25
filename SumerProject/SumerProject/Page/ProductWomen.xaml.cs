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

namespace SumerProject.Page
{
    /// <summary>
    /// Логика взаимодействия для ProductWomen.xaml
    /// </summary>
    public partial class ProductWomen : Window
    {
        private ProductsWomen _selectedProduct;
        private TestEntities5 _context;
        public ImageSource ProductImage { get; set; }
        public ProductWomen(ProductsWomen selectedProduct)
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
            ImageControl.Source = _selectedProduct.ImageRes; // Make sure you convert the byte[] to an ImageSource
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
            ProductsWomen selectedProduct = _selectedProduct;
            if (SizeSelector.SelectedItem != null)
                selectedProduct.SelectedSize = ((Sizes)SizeSelector.SelectedItem).Size;
            CartWomen cartWindow = Application.Current.Windows.OfType<CartWomen>().FirstOrDefault();

            if (cartWindow == null)
            {
                cartWindow = new CartWomen();
                cartWindow.Show();
            }
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
