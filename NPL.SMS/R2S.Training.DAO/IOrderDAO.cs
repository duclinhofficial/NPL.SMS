using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.DAO
{
    interface IOrderDAO
    {
        //Cau 4: Tinh tong order 
        Double ComputeOrderTotal(int orderId);
    }
}
