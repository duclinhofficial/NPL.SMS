using System;
using System.Collections.Generic;
using System.Text;
using NPL.SMS.R2S.Training.Entities;
using System.Data.SqlClient;

namespace NPL.SMS.R2S.Training.DAO
{
    class LineItemDAO : ILineItem
    {
        private const byte ORDER_ID = 0;
        private const byte PRODUCT_ID = 1;
        private const byte QUANTITY = 2;
        private const byte PRICE = 3;

        private const string ADD_LINE_ITEM = "INSERT INTO [dbo].[LineItem](order_id, product_id, quantity, price) VALUES(@order_id, @product_id, @quantity, @price)";
        private const string GET_ALL_LINE_ITEM = "SELECT * FROM dbo.LineItem WHERE dbo.LineItem.order_id = @order";

        public bool AddLineItem(LineItem item)
        {
            if (CheckOrderId(item.OrderId) == false || CheckProductId(item.ProductId) == false)
            {
                return false;
            }
            //Tao ket noi SqlConnection
            using SqlConnection connString = Connect.GetSqlConnection();

            //tao sqlcmd de chi dinh tuong tac them item xuong db
            using SqlCommand cmd = Connect.GetSqlCommand(ADD_LINE_ITEM, connString);

            //them cac param 
            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@order_id", item.OrderId),
                new SqlParameter("@product_id", item.ProductId),
                new SqlParameter("@quantity", item.Quatity),
                new SqlParameter("@price", item.Price)
            }
            );

            try
            {   //mo ket noi
                connString.Open();
                //gui den cmd
                cmd.ExecuteNonQuery();

            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<LineItem> GetAllItemsByOrderId(int orderId)
        {
            // tao list items de chua cac lineitem theo orderid
            List<LineItem> items = new List<LineItem>();
            //Tao ket noi SqlConnection
            using SqlConnection connString = Connect.GetSqlConnection();//cau truy can
            //chi dinh truy van voi query va conn
            SqlCommand cmd = new SqlCommand(GET_ALL_LINE_ITEM, connString);
            //tao params
            SqlParameter param = new SqlParameter("@order", orderId);
            //them params vao cmd
            cmd.Parameters.Add(param);

            try
            {   //tao ket noi
                connString.Open();
                //gioi ExecuteReader de thuc thi va nhan ket qua
                SqlDataReader listItems = cmd.ExecuteReader();

                while (listItems.Read())
                {   //lay ket qua cua moi cot
                    LineItem item = new LineItem
                    {
                        OrderId = (int)listItems.GetInt32(ORDER_ID),
                        ProductId = (int)listItems.GetInt32(PRODUCT_ID),
                        Quatity = (int)listItems.GetInt32(QUANTITY),
                        Price = (double)listItems.GetDouble(PRICE),
                    };
                    //them ket qua vao list item
                    items.Add(item);
                }
            }
            catch
            {
                return null;
            }
            return items;
        }


        const string CHECK_ORDERID = "SELECT COUNT(*) FROM Orders WHERE order_id = @orderId";

        //Check order id in table Order
        public bool CheckOrderId(int orderId)
        {
            SqlConnection conn = Connect.GetSqlConnection();

            SqlCommand cmd = Connect.GetSqlCommand(CHECK_ORDERID, conn);

            SqlParameter param = new SqlParameter("@orderId", orderId);
            //them params vao cmd
            cmd.Parameters.Add(param);

            try
            {
                conn.Open();

                int listOrderID = (int)cmd.ExecuteScalar();

                if (listOrderID > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        const string CHECK_PRODUCTID = "SELECT COUNT(*) FROM Product WHERE product_id = @product_id";

        //Check order id in table Order
        public bool CheckProductId(int productID)
        {
            SqlConnection conn = Connect.GetSqlConnection();

            SqlCommand cmd = Connect.GetSqlCommand(CHECK_PRODUCTID, conn);

            SqlParameter param = new SqlParameter("@product_id", productID);
            //them params vao cmd
            cmd.Parameters.Add(param);

            try
            {
                conn.Open();

                int countProducID = (int)cmd.ExecuteScalar();

                if (countProducID > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
