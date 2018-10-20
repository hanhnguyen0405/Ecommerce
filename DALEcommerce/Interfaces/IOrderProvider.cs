using CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DALEcommerce.Interfaces
{
    public interface IOrderProvider
    {
        int CreateOrder(Order order);
        Order ReadOrder(int Id);
        void UpdateOrder(Order order);
        int DeleteOrder(int Id);
        int GetCurrentOrderId();
        void AddItemIntoOrder(OrderItem item);
    }
}
