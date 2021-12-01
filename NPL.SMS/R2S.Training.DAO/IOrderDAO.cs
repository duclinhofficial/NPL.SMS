using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPL.SMS.R2S.Training.Entities;

namespace NPL.SMS.R2S.Training.DAO
{
    interface IOrderDAO
    {
        //Cau 4: Tinh tong order 
        double ComputeOrderTotal(int orderId);
        //Cau 8: Add Order
        bool AddOrder(Order order);
        //Cau 10: Update Order Total
        bool UpdateOrderTotal(int orderId);
    }
}
