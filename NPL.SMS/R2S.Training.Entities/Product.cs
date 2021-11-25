using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS.R2S.Training.Entities
{
    class Product
    {
        private int productId;
        private string productName;
        private double productPrice;
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
                        Console.Write("RE-ENTER ID > 0 : ");
                        productId = int.Parse(Console.ReadLine());
                    } while (productId <= 0);
                }
            }
        }
        public string ProductName
        {
            get => productName;
            set => productName = value;
        }
        public double ProductPrice
        {
            get => productPrice;
            set
            {
                if (value > 0)
                {
                    productPrice = value;
                }
                else
                {
                    do
                    {
                        Console.Write("RE-ENTER Price > 0 : ");
                        productPrice = int.Parse(Console.ReadLine());
                    } while (productPrice <= 0);
                }
            }
        }


    }
}
