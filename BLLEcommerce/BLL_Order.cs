using System;
using System.Collections.Generic;
using System.Text;
using DALEcommerce;
using CommonModels;
using DALEcommerce.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BLLEcommerce
{
    public class BLL_Order : Interfaces.IBLL_Order
    {
        private IOrderProvider _orderProvider;

        public BLL_Order(IConfiguration iconfiguration)
        {
            _orderProvider = new OrderProvider(iconfiguration);
        }
        public void AddItemsToCart(OrderItem item)
        {
            // Get status = "current" Order Id -  If not found, create new Order and set status to "current"
            int orderId = _orderProvider.GetCurrentOrderId();

            // Put cart Id into OrderItem
            item.OrderId = orderId;

            // Add entry orderitem into db Orderitem
            _orderProvider.AddItemIntoOrder(item);
        }
    }
}
