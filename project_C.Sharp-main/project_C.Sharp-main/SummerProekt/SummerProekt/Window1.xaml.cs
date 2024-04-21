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

namespace SummerProekt
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private void Vhod_Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Reg_Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.Show();
            this.Close();
        }
        private void Exit_Button_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
