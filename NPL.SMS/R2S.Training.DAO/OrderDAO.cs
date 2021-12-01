using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPL.SMS.R2S.Training.Entities;

namespace NPL.SMS.R2S.Training.DAO
{
    class OrderDAO : IOrderDAO
    {
        //Khai bao chuoi lenh
        const string COMPUTE_ORDER_TOTAL = "SELECT dbo.FN_Compute_OrderTotal(@Order_id)";
        private const string UPDATE = "Update Orders set total  = @total where order_id = @order_id";
        private const string ADD_ORDER = @"INSERT INTO [dbo].[Orders](order_date, customer_id, employee_id,total) VALUES(@order_date,@customer_id,@employee_id,@total)";
        private const string SELECT_ALL_ORDER = "Select* from Orders";

        // Cau 4: Tinh tong order
        public double ComputeOrderTotal(int orderId)
        {
            // Khoi tao ket noi
            using SqlConnection conn = Connect.GetSqlConnection();

            //Mo ket noi
            conn.Open();

            // Tao Sql Command
            using SqlCommand cmd = Connect.GetSqlCommand(COMPUTE_ORDER_TOTAL, conn);

            // Truyen tham so
            cmd.Parameters.Add(new SqlParameter("@Order_id", orderId));

            try //Goi ExecuteScalar (vi QUERY tra ve gia tri duy nhat )
            {
                return (double)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public bool checkOrder(int orderId)
        {
            using SqlConnection conn = Connect.GetSqlConnection();
            conn.Open();
            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ALL_ORDER, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (orderId == (int)reader["order_id"])
                    return true;
            }
            return false;
        }
        // Cau 10 Cập nhậtt tổng order vào cơ sở dữ liệu 
        public bool UpdateOrderTotal(int orderId)
        {
            using SqlConnection sqlcon = Connect.GetSqlConnection();
            sqlcon.Open();
            using SqlCommand sqlcmd = Connect.GetSqlCommand(UPDATE, sqlcon);
            // Kiểm tra có Order trong bảng hay không?
            if (checkOrder(orderId) == false)
            {
                Console.WriteLine("Order khong ton tai");
                return false;
            }
            try
            {
                sqlcmd.Parameters.AddRange(new[]
                {
                    // Tính total truyền vào Parameter
                    new SqlParameter("@total",ComputeOrderTotal(orderId)),
                    new SqlParameter("@order_id",orderId)
                });
                sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        // Cau 8: Add Order
        public bool AddOrder(Order order)
        {
            using SqlConnection conn = Connect.GetSqlConnection();

            using SqlCommand cmd = Connect.GetSqlCommand(ADD_ORDER, conn);

            // Thêm parameter
            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@order_date",order.OrderDate),
                new SqlParameter("@customer_id",order.CustomerId),
                new SqlParameter("@employee_id",order.EmployeeId),
                new SqlParameter("@total",order.Total),
            });
            try
            {
                // Mở kết nối 
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
