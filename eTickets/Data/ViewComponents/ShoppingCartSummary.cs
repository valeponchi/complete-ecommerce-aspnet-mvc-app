using eTickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Data.ViewComponents
{
    //[ViewComponent]
    public class ShoppingCartSummary : ViewComponent
    {
        //inject ShoppingCart
        private readonly ShoppingCart _shoppingCart;

        //CTOR
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        //to call the VC we need the invoke method
        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            return View(items.Count);
        }
    }
}
