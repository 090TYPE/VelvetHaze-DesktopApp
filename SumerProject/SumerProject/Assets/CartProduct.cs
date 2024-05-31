using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SumerProject.Assets
{
    public class CartProduct
    {
        public int ID_Product { get; set; }
        public string NameProduct { get; set; }
        public int Coast { get; set; }
        public string SelectedSize { get; set; }
        public byte[] Image { get; set; }
    }
}
