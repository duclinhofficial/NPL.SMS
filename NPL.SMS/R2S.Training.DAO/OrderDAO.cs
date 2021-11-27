using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.DAO
{
    class OrderDAO : IOrderDAO
    {
        //Khai bao chuoi lenh
        const string COMPUTE_ORDER_TOTAL = "SELECT dbo.FN_Compute_OrderTotal(@Order_id)";

        //Tinh tong order
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
    }
}
