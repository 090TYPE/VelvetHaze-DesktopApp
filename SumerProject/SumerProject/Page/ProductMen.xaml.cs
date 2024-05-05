using SumerProject.DataBase;
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
    public partial class ProductMen : Window
    {
        private ProductsMen _selectedProduct;
        private TestEntities5 _context;

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
            ImageControl.Source = _selectedProduct.ImageRes; // Make sure you convert the byte[] to an ImageSource
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
            cartWindow.Items.Add(selectedProduct);
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
