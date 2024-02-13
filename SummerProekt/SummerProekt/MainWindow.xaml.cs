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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SummerProekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Lich_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
        }
        private void Muzh_Click(object sender,RoutedEventArgs e)
        {
            Muzh muzh = new Muzh();
            muzh.Show();
        }
        private void Zhen_Click(object sender, RoutedEventArgs e)
        {
            Zhen zhen = new Zhen();
            zhen.Show();
        }
        private void Aks_Click(object sender, RoutedEventArgs e)
        {
            Aks aks = new Aks();
            aks.Show();
        }
        private void Onas_Click(object sender, RoutedEventArgs e)
        {
            Onas onas = new Onas();
            onas.Show();
        }
    }
}
