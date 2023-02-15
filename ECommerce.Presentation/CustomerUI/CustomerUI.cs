using ECommerce.Service.Interfaces;
using ECommerce.Service.Services;
using System.Security.Cryptography.X509Certificates;

namespace ECommerce.Presentation.CustomerUI
{
    public class CustomerUI
    {
        private readonly IOrderService orderService = new OrderService();
        private readonly IProductService productService = new ProductService();

        public void Customer()
        {
            while (true)
            {
                Console.WriteLine("\t\t--CUSTOMER SERVICES: --");

                Console.WriteLine("\t1. --- Order new product ---");
                // for updating 
                Console.WriteLine("\t2. --- Change order count ---");
                Console.WriteLine("\t3. --- Get all active ordered products ---");
                Console.WriteLine("\t4. --- Cancel the order ---");
                Console.WriteLine("\t5. --- Get all history of orders ---");
                Console.WriteLine();
                Console.Write("\tEnter number => you choosen department :    ");

                int num = int.Parse(Console.ReadLine());

                if (num == 1)
                {

                }
                else if (num == 2)
                {

                }
                else if (num == 3)
                {

                }
                else if (num == 4)
                {

                }
                else if (num == 5)
                {

                }
                else
                {

                }
            };
            //      LOGIKA
            public async Task OrderNewProduct()
            {
                Console.WriteLine("enter new product");

            }

        }
    }
}
