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
        const string SELECT_ALLCUSTOMERS = "SELECT * FROM Customer WHERE EXISTS(SELECT Orders.customer_id FROM Orders WHERE Orders.customer_id = Customer.customer_id)";
        const string ADD_CUSTOMER = "sp_add_customer";

        // Cau 1: List tat ca customer o order table

        public List<Customer> GetAllCustomers()
        {
            //Creat connection 
            using SqlConnection conn = Connect.GetSqlConnection();

            //Open connection
            conn.Open();

            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ALLCUSTOMERS, conn); //Creat a sql command 
            using SqlDataReader dataReader = cmd.ExecuteReader(); // Execute command 

            List<Customer> list = new List<Customer>();
            while (dataReader.Read())
            {
                Customer customer = new Customer
                {
                    CustomerId = dataReader.GetInt32(0),
                    CustomerName = dataReader.GetString(1)
                };

                list.Add(customer);
            }

            return list;

        }

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

        //Cau 5: Them mot khach hang moi su dung Stored Procedure
        public bool AddCustomer(Customer customer)
        {
            //Creat connection
            using SqlConnection conn = Connect.GetSqlConnection();

            //Open connection
            conn.Open();

            //Creat parameter 
            SqlParameter param = new SqlParameter
            {
                ParameterName = "@customerName",
                Value = customer.CustomerName
            };

            //Creat sql command 
            using SqlCommand cmd = Connect.GetSqlCommand(ADD_CUSTOMER, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Add parameter to sql command 
            cmd.Parameters.Add(param);

            //Execute sql delete command and return logic status
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else
                return false;
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
