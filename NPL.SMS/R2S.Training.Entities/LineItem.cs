using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities
{
    class LineItem
    {
        private int orderId;
        private int productId;
        private int quantity;
        private double price;

        public LineItem() { }

        public LineItem(int orderId, int productId, int quantity, double price)
        {
            this.OrderId = orderId;
            this.ProductId = productId;
            this.Quatity = quantity;
            this.Price = price;
        }

        public int OrderId
        {
            get => orderId;
            set
            {
                if (value > 0)
                {
                    orderId = value;
                }
                else
                {
                    do
                    {
                        Console.Write("Nhap lai ID > 0 : ");
                        orderId = int.Parse(Console.ReadLine());
                    } while (orderId <= 0);
                }
            }
        }

        public int ProductId
        {
            get => productId;
            set
            {
                if (value > 0)
                {
                    productId = value;
                }
                else
                {
                    do
                    {
                        Console.Write("Nhap lai ID > 0 : ");
                        productId = int.Parse(Console.ReadLine());
                    } while (productId <= 0);
                }
            }
        }

        public int Quatity
        {
            get => quantity;
            set
            {
                if (value >= 0)
                {
                    quantity = value;
                }
                else
                {
                    do
                    {
                        Console.Write("Nhap lai ID > 0 : ");
                        quantity = int.Parse(Console.ReadLine());
                    } while (quantity < 0);
                }
            }
        }

        public double Price
        {
            get => price;

            set
            {
                if (value >= 0)
                {
                    price = value;
                }
                else
                {
                    do
                    {
                        Console.Write("Nhap lai ID > 0 : ");
                        price = int.Parse(Console.ReadLine());
                    } while (price < 0);
                }
            }
        }

    }
}
