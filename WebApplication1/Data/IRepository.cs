using System.Collections.Generic;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data
{
    public interface IRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        Order GetOrderById(int id);
        Order GetOrderById(string username, int id);

        void AddOrder(Order newOrder);
        void AddEntity(object model);

        bool SaveAll();
    }
}