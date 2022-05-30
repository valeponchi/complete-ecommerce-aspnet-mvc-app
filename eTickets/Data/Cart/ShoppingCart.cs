using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Cart
{
    //IT's in DataFolder bc we are not going to store data in DB as usual

    //used to add/remove data to/from ShoppingCart
    public class ShoppingCart
    {
        //inject AppDbContext
        public AppDbContext _context { get; set; }

        //placeholder for the ShoppingCartItems in the ShoppingCart
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        //inject the DbTranslator here (CTOR)
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }


        //POST an ITEM (to Cart)
        public void AddItemToCart(Movie movie)
        {
            //do we have the movie in the cart? 
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            //N -> create a new item in Cart
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            //Y -> amount +1
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        //READ - GET Shopping Cart
        //static bc we are going to use it in the startup file
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            //if we have cartId session
            // ?? if this is null -> generate a new cartId
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        //READ - GET ALL ShoppingCartItems
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            // ?? means "if null"
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
        }

        //READ - GET ShoppingCart £Total
        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
            return total;
        }

        //DELETE Item from Cart
        public void RemoveItemFromCart(Movie movie)
        {
            //do we have the movie in the cart? 
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            //Y -> 
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }


    }
}
