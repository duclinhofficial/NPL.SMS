using NPL.SMS.R2S.Training.Entities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.DAO
{
    class CustomerDAO : ICustomerDAO
    {
        // Khai bao const string
        const string GET_ORDER_BY_CUSTOMERID = "SELECT * FROM Orders WHERE customer_id=@customerId";


        // Cau 2: Lay tat ca order theo customer id
        public List<Order> GetAllOrdersByCustomerId(int customerId)
        {
            // Khoi tao SqlConnection conn
            using SqlConnection conn = Connect.GetSqlConnection();

            //Mo ket noi
            conn.Open();

            //Tao Sqlcommand 
            using SqlCommand cmd = Connect.GetSqlCommand(GET_ORDER_BY_CUSTOMERID, conn);

            //Truyen tham so
            cmd.Parameters.Add(new SqlParameter("@customerId", customerId));

            //Dung phuong thuc ExecuteReader
            // tra ve SqlDataReader
            using SqlDataReader dataReader = cmd.ExecuteReader();

            //Them thong tin vao list
            List<Order> list = new List<Order>();
            while (dataReader.Read())
            {
                //Bat loi NULL
                try
                {
                    //tao mot order tam de luu
                    Order order = new Order();

                    //Doc du lieu cac cot vao bien order 
                    order.OrderId = (int)dataReader["order_id"];
                    order.OrderDate = (DateTime)dataReader["order_date"];
                    order.CustomerId = (int)dataReader["customer_id"];
                    order.EmployeeId = (int)dataReader["employee_id"];
                    order.Total = (double)dataReader["total"];

                    //them order vao list
                    list.Add(order);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return list;
        }


    }
}
