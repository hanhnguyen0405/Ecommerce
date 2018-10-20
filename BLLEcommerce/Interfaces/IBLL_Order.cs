using System;
using System.Collections.Generic;
using System.Text;
using CommonModels;

namespace BLLEcommerce.Interfaces
{
    interface IBLL_Order
    {
        void AddItemsToCart(OrderItem item);

    }
}
