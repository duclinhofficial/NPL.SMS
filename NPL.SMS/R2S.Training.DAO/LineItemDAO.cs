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


        public bool AddLineItem(LineItem item)
        {
            //Tao ket noi SqlConnection
            using SqlConnection connString = Connect.GetSqlConnection();
            //Tao chuoi truy van
            string read = "INSERT INTO [SMS].[dbo].[LineItem](order_id, product_id, quantity, price) VALUES(@order_id, @product_id, @quantity, @price)";
            //tao sqlcmd de chi dinh tuong tac them item xuong db
            SqlCommand cmd = new SqlCommand(read, connString);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public List<LineItem> GetAllItemsByOrderId(int orderId)
        {   // tao list items de chua cac lineitem theo orderid
            List<LineItem> items = new List<LineItem>();
            //Tao ket noi SqlConnection
            using SqlConnection connString = Connect.GetSqlConnection();//cau truy can
            string getList = "SELECT * FROM [SMS].[dbo].[LineItem] WHERE [SMS].[dbo].[LineItem].[order_id] = @order";
            //chi dinh truy van voi query va conn
            SqlCommand cmd = new SqlCommand(getList, connString);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return items;
        }
    }
}
