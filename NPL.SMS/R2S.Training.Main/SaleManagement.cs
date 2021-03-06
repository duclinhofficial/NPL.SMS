using System;
using System.Collections.Generic;
using NPL.SMS.R2S.Training.DAO;
using NPL.SMS.R2S.Training.Entities;
using System.Formats;

namespace NPL.SMS.R2S.Training.Main
{
    class SaleManagement
    {
        static void Main(string[] args)
        {
            //chuc nang
            const string GET_ALL_CUSTOMER = "1";
            const string GET_ALL_ORDERS_BY_CUSTOMER_ID = "2";
            const string GET_ALL_ITEMS_BY_ORDER_ID = "3";
            const string COMPUTE_ORDER_TOTAL = "4";
            const string ADD_CUSTOMER = "5";
            const string DELETE_CUSTOMER = "6";
            const string UPDATE_CUSTOMER = "7";
            const string ADD_ORDER = "8";
            const string ADD_LINE_ITEM = "9";
            const string UPDATE_ORDER_TOTAL = "10";
            const string EXIT = "0";

            //Khoi tao 
            LineItemDAO lineItem = new LineItemDAO();
            CustomerDAO CD = new CustomerDAO();
            OrderDAO OD = new OrderDAO();

            //Cau 1: Get all customer
            void Get_All_Customer()
            {
                List<Customer> list = CD.GetAllCustomers();

                if (list.Count == 0)
                {
                    Console.WriteLine("--> LIST IS EMPTY!");
                }
                else
                {
                    foreach (Customer customers in list)
                    {
                        Console.WriteLine(customers.ToString());
                    }
                }
            }

            //Cau 2: lay tat ca order theo customer id
            void Get_All_Orders_By_Customer_Id()
            {

                //Nhap customer id
                int customerid = 0;
                bool check = false;
                while (check == false)
                {
                    Console.Write("ENTER CUSTOMER ID: ");
                    check = int.TryParse(Console.ReadLine(), out customerid);
                    if (check == false)
                        Console.WriteLine("INVALID ID, RE-ENTER!");
                }

                //Goi phuong phuong thuc get all order cua CD truyen vao list
                List<Order> list = CD.GetAllOrdersByCustomerId(customerid);

                // Xuat thong tin cua list
                if (list.Count == 0)
                {
                    Console.WriteLine("--> LIST IS EMPTY!");
                }
                else
                {
                    Console.WriteLine("__________________LIST ORDER________________");
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].ExportInfor();
                    }
                }
            }

            //Cau 3: GET_ALL_ITEMS_BY_ORDER_ID
            void Get_All_Items_By_Order_Id()
            {

                Console.Write("Get All Line Item by Order ID: ");
                int findId = int.Parse(Console.ReadLine());
                //Khoi tao res de nhan cac LineItem co cung OrderId
                List<LineItem> res;
                //thuc hien chuc nang lai tat ca cac LineItem co cung OrderId
                res = lineItem.GetAllItemsByOrderId(findId);

                //In ket qua

                if (res != null && res.Count > 0)
                {
                    Console.WriteLine("{0}   {1}   {2}\t\t{3}", "OrderId", "ProductId", "Quantity", "Price");
                    for (byte i = 0; i < res.Count; i++)
                    {
                        Console.WriteLine("{0}\t  {1}\t\t{2}\t\t{3}", res[i].OrderId, res[i].ProductId, res[i].Quatity, res[i].Price);
                    }

                }
                else
                {
                    Console.WriteLine("Don't Find");
                }
            }

            // Cau 4: Tinh tong order
            void Compute_Order_Total()
            {
                //nhap order id
                int orderId = 0;
                bool check = false;
                while (check == false)
                {
                    Console.Write("ENTER ORDER ID: ");
                    check = int.TryParse(Console.ReadLine(), out orderId);
                    if (check == false)
                        Console.WriteLine("INVALID ID, RE-ENTER!");
                }

                //Goi phuong thuc tinh va xuat ra man hinh
                Console.WriteLine("TOTAL: " + OD.ComputeOrderTotal(orderId));
            }

            //Cau 5: ADD_CUSTOMER
            void Add_Customer()
            {
                Customer customer = new Customer();

                Console.Write("Enter name: ");
                customer.CustomerName = Console.ReadLine();

                try
                {
                    if (CD.AddCustomer(customer))
                    {
                        Console.WriteLine("Add successful! ");
                    }
                    else
                    {
                        Console.WriteLine("Failed! ");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            // Cau 6: DELETE_CUSTOMER()
            void Delete_Customer()
            {
                Customer info = new Customer();
                // Id to delete
                Console.Write("Enter id: ");
                info.CustomerId = Convert.ToInt32(Console.ReadLine());
                CustomerDAO test = new CustomerDAO();
                if (test.DeleteCustomer(info.CustomerId) == true)
                {
                    Console.WriteLine("Successfully");
                }
                else
                {
                    Console.WriteLine("Failed");
                }


            }

            //Cau 7: UPDATE_CUSTOMER
            void Update_Customer()
            {
                Customer info = new Customer();
                CustomerDAO test = new CustomerDAO();
                Console.Write("Enter id: ");
                info.CustomerId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter new name: ");
                info.CustomerName = Console.ReadLine();
                try
                {
                    if (test.UpdateCustomer(info) == true)
                    {
                        Console.WriteLine("Successfully");
                    }
                    else
                    {
                        Console.WriteLine("Failed");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }

            //Cau 8: ADD_ORDER
            void Add_Order()
            {
                Order order = new Order();
                OrderDAO orderDAO = new OrderDAO();
                Console.WriteLine("Create Order into data base");

                Console.Write("Enter order Date: ");
                order.OrderDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter CustomerId: ");
                order.CustomerId = int.Parse(Console.ReadLine());
                Console.Write("Enter EmployeeId: ");
                order.EmployeeId = int.Parse(Console.ReadLine());
                Console.Write("Enter Total: ");
                order.Total = double.Parse(Console.ReadLine());

                // Thực hiện chức năng thêm một order vào data base
                if (orderDAO.AddOrder(order) == true)
                    Console.WriteLine("Successfully");
                else
                    Console.WriteLine("Failed");
            }

            //Cau 9: ADD_LINE_ITEM
            void Add_Line_Item()
            {
                //câu 3 khoi tao t để chứa LineItem can them vao
                LineItem item = new LineItem();

                Console.WriteLine("Create Line Item into database...");

                Console.Write("Enter Order ID: ");
                item.OrderId = int.Parse(Console.ReadLine());
                Console.Write("Enter Product ID: ");
                item.ProductId = int.Parse(Console.ReadLine());
                Console.Write("Enter Quatity: ");
                item.Quatity = int.Parse(Console.ReadLine());
                Console.Write("Enter Price: ");
                item.Price = double.Parse(Console.ReadLine());

                //thuc hien chuc nang them mot LineItem vao data base
                if (lineItem.AddLineItem(item) == true)
                    Console.WriteLine("Successfully!");
                else
                    Console.WriteLine("Failed!");
            }

            //Cau 10: UPDATE_ORDER_TOTAL
            void Update_Order_Total()
            {
                OrderDAO orderDAO = new OrderDAO();
                Console.WriteLine("Enter order ID");
                    int orderId;
                    orderId = Convert.ToInt32(Console.ReadLine());
                    if (orderDAO.UpdateOrderTotal(orderId))
                        Console.WriteLine("Succesfully");
                    else
                        Console.WriteLine("Failed");
            }

            string OPTION;
            do
            {
                //Menu chuc nang
                Console.WriteLine("____________________MENU____________________");
                Console.WriteLine("\t1. GET ALL CUSTOMER.");
                Console.WriteLine("\t2. GET ALL ORDER BY CUSTOMER ID.");
                Console.WriteLine("\t3. GET ALL ITEM BY ORDER ID.");
                Console.WriteLine("\t4. COMPUTE ORDER TOTAL.");
                Console.WriteLine("\t5. ADD CUSTOMER.");
                Console.WriteLine("\t6. DELETE CUSTOMER.");
                Console.WriteLine("\t7. UPDATE CUSTOMER.");
                Console.WriteLine("\t8. ADD ORDER.");
                Console.WriteLine("\t9. ADD LINE ITEM.");
                Console.WriteLine("\t10. UPDATE ORDER TOTAL.");
                Console.WriteLine("\t0. EXIT");

                //input lua chon
                Console.Write("SELECT OPTION: ");
                OPTION = Console.ReadLine();

                switch (OPTION)
                {
                    case GET_ALL_CUSTOMER: //cau 1
                        Get_All_Customer();
                        break;
                    case GET_ALL_ORDERS_BY_CUSTOMER_ID: //cau 2
                        Get_All_Orders_By_Customer_Id();
                        break;
                    case GET_ALL_ITEMS_BY_ORDER_ID: //cau 3
                        Get_All_Items_By_Order_Id();
                        break;
                    case COMPUTE_ORDER_TOTAL: //cau 4
                        Compute_Order_Total();
                        break;
                    case ADD_CUSTOMER: //cau 5
                        Add_Customer();
                        break;
                    case DELETE_CUSTOMER: //cau 6
                        Delete_Customer();
                        break;
                    case UPDATE_CUSTOMER: //cau 7
                        Update_Customer();
                        break;
                    case ADD_ORDER: //cau 8
                        Add_Order();
                        break;
                    case ADD_LINE_ITEM: //cau 9
                        Add_Line_Item();
                        break;
                    case UPDATE_ORDER_TOTAL: //cau 10
                        Update_Order_Total();
                        break;
                    case EXIT:
                        Console.WriteLine("---> SUCCESSFUL EXIT!");
                        break;
                    default:
                        Console.WriteLine("!!! OPTION DOES NOT EXIT !!!");
                        break;
                }
            } while (OPTION != EXIT);
        }
    }
}
