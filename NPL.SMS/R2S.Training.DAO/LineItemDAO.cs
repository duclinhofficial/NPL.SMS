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
            using SqlConnection connString = Connect.GetSqlConnection();
            
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

            using SqlConnection connString = Connect.GetSqlConnection();

            SqlCommand cmd = new SqlCommand(GET_ALL_LINE_ITEM, connString);

            SqlParameter param = new SqlParameter("@order", orderId);
            //them params vao cmd
            cmd.Parameters.Add(param);

            try
            {   //tao ket noi
                connString.Open();

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
    }
}
