using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.Entities
{
    class Order
    {
        // Khai bao private
        private int orderId;
        private DateTime orderDate;
        private int customerId;
        private int employeeId;
        private double total;

        //Order ID
        public int OrderId
        {
            get => orderId;
            set => orderId = value;
        }

        //OrderDate
        public DateTime OrderDate
        {
            get => orderDate;
            set => orderDate = value;
        }

        //CustomerID > 0
        public int CustomerId
        {
            get => customerId;
            set
            {
                if (value > 0)
                {
                    customerId = value;
                }
                else
                {
                    do
                    {
                        Console.Write("RE-ENTER ID > 0 : ");
                        customerId = int.Parse(Console.ReadLine());
                    } while (customerId <= 0);
                }
            }
        }

        //EmployeeID >0
        public int EmployeeId
        {
            get => employeeId;
            set
            {
                if (value > 0)
                {
                    employeeId = value;
                }
                else
                {
                    do
                    {
                        Console.Write("RE-ENTER ID > 0 :");
                        employeeId = int.Parse(Console.ReadLine());
                    } while (employeeId <= 0);
                }
            }
        }

        // Total
        public double Total
        {
            get => total;
            set => total = value;
        }

        // Phuong thuc khoi tao 0 tham so
        public Order()
        {
        }

        // Phuong thuc khoi tao 5 tham so
        public Order(int orderId, DateTime orderDate, int customerId,
            int employeeId, double total)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            CustomerId = customerId;
            EmployeeId = employeeId;
            Total = total;
        }

        //Xuat thong tin 
        public void ExportInfor()
        {
            Console.WriteLine($"Order id: {OrderId}, Order date: {OrderDate}, " +
                $"Customer id: { CustomerId}, Employee id: {EmployeeId}, Total: {Total}");
        }
    }
}
