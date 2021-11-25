using System;
using System.Collections.Generic;
using System.Text;
using NPL.SMS.R2S.Training.Entities;

namespace NPL.SMS
{
    interface ILineItem
    {
        List<LineItem> GetAllItemsByOrderId(int orderId);

        bool AddLineItem(LineItem item);
    }
}
