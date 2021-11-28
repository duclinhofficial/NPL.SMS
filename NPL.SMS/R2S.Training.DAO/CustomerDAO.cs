using NPL.SMS.R2S.Training.Entities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NPL.SMS.R2S.Training.DAO
{
    class CustomerDAO : ICustomerDAO
    {
        // Khai bao const string
        const string GET_ORDER_BY_CUSTOMERID = "SELECT * FROM Orders WHERE customer_id=@customerId";
        const string UPDATE_CUSTOMER = "SMS.dbo.SP_updateCustomer";
        const string DELETE_CUSTOMER = "SMS.dbo.SP_deleteCustomer";


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

        //Cau 7
        public bool UpdateCustomer(Customer customer)
        {
            //Khoi tao SqlConnection conn
            using SqlConnection conn = Connect.GetSqlConnection();
            conn.Open();
            //Tao Sqlcommand 
            using SqlCommand cmd = Connect.GetSqlCommand(UPDATE_CUSTOMER, conn);

            //  Add multiple parameters to SQL command in one statement
            cmd.CommandType = CommandType.StoredProcedure;

            //Tao nhieu parmeter 
            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@customerID", customer.CustomerId),
                new SqlParameter("@customerName", customer.CustomerName)
            });
            cmd.ExecuteNonQuery();
            return true;
        }
        //cau 6
        public bool DeleteCustomer(int customerId)
        {
            //Mở kết nối
            using SqlConnection conn = Connect.GetSqlConnection();

            // Create parameter
            SqlParameter param = new SqlParameter
            {
                ParameterName = "@customerId",
                Value = customerId
            };

            // Create a sql command
            using SqlCommand cmd = Connect.GetSqlCommand(DELETE_CUSTOMER, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param);
            try
            {
                // Open a connection
                conn.Open();

                // Execute sql delete command
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }

    }
}
