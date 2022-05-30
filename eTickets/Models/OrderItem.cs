using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        //_______________________________________________
        public int Amount { get; set; }
        public double Price { get; set; }

        //Reference to the movie itself
        public int MovieId { get; set; }
        [ForeignKey("MovieId")] 
        public Movie Movie { get; set; } //MVC will U it's the MovieId even if you don't write it here

        //_______________________________________________
        //relationship to an order
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; } //MVC will U it's the MovieId even if you don't write it here
    }
}
