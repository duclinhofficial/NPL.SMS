using NPL.SMS.R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.DAO
{
    interface ICustomerDAO
    {
        //Cau 2: Lay tat ca orders theo customer id
        List<Order> GetAllOrdersByCustomerId(int customerId);

        bool DeleteCustomer(int customerId);
        bool UpdateCustomer(Customer customer);
    }
}
