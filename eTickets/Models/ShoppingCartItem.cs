using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        //Movie Item in the Cart
        public Movie Movie { get; set; }
        public int Amount { get; set; }

        //so we can clean up the DB after the Order is completed
        public string ShoppingCartId { get; set; } 

    }
}
