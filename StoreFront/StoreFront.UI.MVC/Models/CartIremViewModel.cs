using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Models
{
    public class CartIremViewModel
    {
        public int Qty { get; set; }
        public Pokemon CartProd { get; set; }

        public CartIremViewModel() { }

        public CartIremViewModel (int qty, Pokemon cartProd)
        {
            Qty = qty;
            CartProd = cartProd;
        }
    }
}
