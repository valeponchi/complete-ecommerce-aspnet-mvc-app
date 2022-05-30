using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        //_______________________________________________
        //order are going to be related to a User
        public string Email { get; set; }
        public string UserId { get; set; } //it'll be the loogedIn UserId

        //_______________________________________________
        //relation order - user
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }


        //_______________________________________________
        //For each order we can have multiple items
        //in shopping cart

        //Reference to the OrderItem
        public List<OrderItem> OrderItems { get; set; }


    }
}
