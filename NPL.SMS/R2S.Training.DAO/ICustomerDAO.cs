using NPL.SMS.R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.DAO
{
    interface ICustomerDAO
    {
        //Cau 1: List tat ca customers o order tale
        List<Customer> GetAllCustomers();

        //Cau 2: Lay tat ca orders theo customer id
        List<Order> GetAllOrdersByCustomerId(int customerId);

        //Cau 5: Them mot khach hang moi
        bool AddCustomer(Customer customer);
    }
}
