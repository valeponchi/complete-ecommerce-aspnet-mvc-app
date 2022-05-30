using eTickets.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IOrdersService
    {
        //ADD ORDERS TO DB
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);

        //GET ALL ORDERS FROM DB
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);

    }
}
