using SumerProject.Assets;
using SumerProject.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SumerProject.Page
{
    public partial class OrderRegistration : Window
    {
        public static OrderRegistration CurrentOrderWindow { get; private set; }
        public ObservableCollection<CartProduct> Products { get; set; }

        public OrderRegistration(List<CartProduct> cartProducts)
        {
            InitializeComponent();
            Products = new ObservableCollection<CartProduct>(cartProducts);
            DataContext = this;
            CurrentOrderWindow = this;
        }

        public void AddProducts(List<CartProduct> cartProducts)
        {
            foreach (var product in cartProducts)
            {
                Products.Add(product);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            CurrentOrderWindow = null;
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateOrderDetails())
                {
                    using (var context = new TestEntities5())
                    {
                        var order = new Orders
                        {
                            UserID = 1, // Пример пользователя
                            OrderDate = DateTime.Now,
                            TotalAmount = Products.Sum(p => p.Coast), // Пример подсчета общей суммы
                            ShippingAddress = this.ShippingAddress,
                            City = this.City,
                            PhoneNumber = this.PhoneNumber,
                            PostalCode = this.PostalCode,
                            CardNumber = this.CardNumber,
                            CardHolderName = this.CardHolderName,
                            CardExpiry = this.CardExpiry,
                            CardCVV = this.CardCVV
                        };

                        context.Orders.Add(order);
                        context.SaveChanges();

                        foreach (var product in Products)
                        {
                            var orderItem = new OrderItems
                            {
                                OrderID = order.OrderID,
                                ProductID = product.ID_Product, // Здесь изменено на правильное имя свойства
                                Quantity = 1, // Пример количества
                                Size = product.SelectedSize,
                                Price = product.Coast
                            };

                            context.OrderItems.Add(orderItem);
                        }

                        context.SaveChanges();
                    }

                    MessageBox.Show("Заказ успешно оформлен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оформлении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateOrderDetails()
        {
            // Проверка правильности данных
            return !string.IsNullOrEmpty(this.FullName) && !string.IsNullOrEmpty(this.ShippingAddress) &&
                   !string.IsNullOrEmpty(this.City) && !string.IsNullOrEmpty(this.PhoneNumber) &&
                   !string.IsNullOrEmpty(this.PostalCode) && !string.IsNullOrEmpty(this.CardNumber) &&
                   !string.IsNullOrEmpty(this.CardHolderName) && !string.IsNullOrEmpty(this.CardExpiry) &&
                   !string.IsNullOrEmpty(this.CardCVV);
        }

        // Свойства для биндинга данных
        public string FullName { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpiry { get; set; }
        public string CardCVV { get; set; }
    }
}