using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
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
                Console.Write("enter your id:    ");
                long ownerId = long.Parse(Console.ReadLine());
                Console.WriteLine($"1.Food\n" +
                       $"2.Electronics\n" +
                       $"3.Clothes\n" +
                       $"4.Accesories\n" +
                       $"5.Furnitures\n" +
                       $"6.Perfumes\n" +
                       $"7.Souviners\n" +
                       $"8.Toys\n" +
                       $"9.Books\n" +
                       $"10.Others\n");
                Console.Write("Enter the number of the new category: ");
                int choice = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter name of product:   ");
                string nameProduct = Console.ReadLine();

                Console.WriteLine("Enter price of product:   ");
                decimal price = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter {yes || no } about you need deliver product:  ");
                string answer = Console.ReadLine();
                bool delivery;
                if (answer.ToLower() == "yes" || answer.ToLower()[0] == 'y') delivery = true;
                else  delivery = false;

                Console.WriteLine("Enter QR code to this product:   ");
                string qrCode = Console.ReadLine();

                Console.WriteLine("Enter Description to your product:  ");
                string desc = Console.ReadLine();

                var model = new Product()
                {
                    OwnerId = ownerId,
                    Category = (ProductCategory)(choice * 10),
                    Name = nameProduct,
                    Price = price,
                    CanDeliver = delivery,
                    QRCode = qrCode,
                    Description = desc
                };

            }
        }
    }
}
