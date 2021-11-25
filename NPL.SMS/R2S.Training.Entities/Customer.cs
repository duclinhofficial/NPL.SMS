using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS
{
    class Customer
    {
        private int customerId;
        private string customerName;

        public Customer () { }

        public Customer(int customerId, string customerName)
        {
            CustomerId = customerId;
            CustomerName = customerName;
        }

        public int CustomerId
        {
            get => customerId;
            set
            {
                if (value > 0)
                    customerId = value;
                else
                {
                    do
                    {
                        Console.Write("Nhap lai ID > 0 : ");
                        customerId = int.Parse(Console.ReadLine());
                    } while (customerId <= 0);
                }
            }
        }

        public string CustomerName
        {
            get => customerName;
            set => customerName = value;
        }
    }
}
