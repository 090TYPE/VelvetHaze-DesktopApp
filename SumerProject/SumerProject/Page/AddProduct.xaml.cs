using System;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using SumerProject.DataBase;
using System.Windows.Controls;

namespace SumerProject.Page
{
    public partial class AddProduct : Window
    {
        private TestEntities5 db;
        private byte[] imageData;


        public AddProduct()
        {
            InitializeComponent();
            db = new TestEntities5();
        }


        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imageData = File.ReadAllBytes(openFileDialog.FileName);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(imageData);
                bitmap.EndInit();

                if (!string.IsNullOrEmpty(txtColor.Text) &&
                    !string.IsNullOrEmpty(txtName.Text) &&
                    !string.IsNullOrEmpty(txtDescriptionProduct.Text) &&
                    !string.IsNullOrEmpty(txtPrice.Text) &&
                    cbGender.SelectedItem != null &&
                    cbCategory.SelectedItem != null)
                {
                    if (int.TryParse(txtPrice.Text, out int price))
                    {
                        var newProduct = new SumerProject.DataBase.product
                        {
                            NameProduct = txtName.Text,
                            Coast = price,
                            Color = txtColor.Text,
                            DescriptionProduct = txtDescriptionProduct.Text,
                            ImageRes = imageData,
                            Gender = ((ComboBoxItem)cbGender.SelectedItem).Content.ToString(),  
                            Category = ((ComboBoxItem)cbCategory.SelectedItem).Content.ToString()
                        };

                        db.product.Add(newProduct);
                        db.SaveChanges();
                        MessageBox.Show("Товар добавлен");
                    }
                    else
                    {
                        MessageBox.Show("Цена должна быть числом");
                    }
                }
                else
                {
                    MessageBox.Show("Все поля обязательны к заполнению, включая гендер и категорию");
                }
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
