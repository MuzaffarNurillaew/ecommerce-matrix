
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Service.Interfaces;
using ECommerce.Service.Services;
using System.Transactions;

namespace ECommerce.Presentation.SellerUI
{
    public class SellerUI
    {
        private IProductService productService = new ProductService();
        public async Task Seller()
        {
            while (true)
            {
                Console.WriteLine("1.Create ");
                Console.WriteLine("2.Update ");
                Console.WriteLine("3.Get ");
                Console.WriteLine("4.GetAll ");
                Console.WriteLine("5.Delete ");
                Console.WriteLine();
                Console.Write("Enter the number of your chosen department: ");
                int number = int.Parse(Console.ReadLine());

                if (number == 1)
                {
                    CreateProduct();
                }
                else if (number == 2)
                {
                    UpdateProduct();
                }
                else if (number == 3)
                {

                }
            }

        }

        public async Task CreateProduct()
        {
            getname:
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            // bir xil ismliligiga teekshiradi
            var response1 = await productService.GetByNameAsync(name);

            if (response1.StatusCode == 404)
            {
                Console.WriteLine(response1.Message);
                goto getname;
            }

            Console.Write("Enter product Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter product Discription: ");
            string discription = Console.ReadLine();

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

            Console.Write("Can we deliver this product? {Yes, y, YES for Yes} or {Any other for Not}: ");
            string delivery = Console.ReadLine();
            bool candelivery = false;
            
            if (delivery.ToLower() == "yes" || delivery.ToLower() == "y")
            {
                candelivery = true;
            }
            else  
            {
                candelivery = false;
            }

            var model = new Product
            {
                Name = name,
                Price = price,
                Description = discription,
                Category = (ProductCategory)(choice * 10),
                CanDeliver = candelivery,
                CreatedAt = DateTime.Now,
            };

            await productService.AddAsync(model);
        }

        // Update function
        public async Task UpdateProduct()
        {
            UpdatePro:
            Console.Write("Enter the id of the product you want to update: ");
            int numid = int.Parse(Console.ReadLine());
            var oldmodel = await productService.GetByIdAsync(numid);
            if(oldmodel.StatusCode== 404) 
            {
                Console.WriteLine(oldmodel.Message);
                Console.Clear();
                goto UpdatePro;
            }
            Console.WriteLine($"Name: {oldmodel.Result.Name} Price: {oldmodel.Result.Price} Description: {oldmodel.Result.Description}");
            Console.WriteLine($"Category: {oldmodel.Result.Category} QRCode: {oldmodel.Result.QRCode} {oldmodel.Result.CanDeliver}");

            Updatemenu:
            Console.WriteLine();
            Console.WriteLine("1.Name Update ");
            Console.WriteLine("2.Price Update ");
            Console.WriteLine("3.Description Update ");
            Console.WriteLine("4.Category Update ");
            Console.WriteLine("5.CanDeliver Update ");
            Console.WriteLine("6.QRCode Update ");
            Console.WriteLine("7.Return Main menu");
            Console.WriteLine();
            Console.Write("Enter the part number you want to update: ");

            int num = int.Parse(Console.ReadLine());
            
            
            
            while (true)
            {
                //Name Update 
                if (num == 1)
                {
                    Console.Write("Enter product new name: ");
                    oldmodel.Result.Name = Console.ReadLine();
                    Console.Clear();
                    goto Updatemenu;
                }

                // Price update
                if (num == 2)
                {
                    Console.Write("Enter product new price: ");
                    oldmodel.Result.Price = decimal.Parse(Console.ReadLine());
                    Console.Clear();
                    goto Updatemenu;
                }

                //discription update
                if(num == 3)
                {
                    Console.Write("Enter product new discription: ");
                    oldmodel.Result.Description = Console.ReadLine();
                    Console.Clear();
                    goto Updatemenu;
                }

                //Catecgoryid update
                if(num == 4)
                {
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
                    oldmodel.Result.Category = (ProductCategory) (choice * 10);
                    Console.Clear();
                    goto Updatemenu;
                }

                // Deliver update
                if(num == 5)
                {
                    Console.Write("Can we deliver this product? Yes or Not: ");
                    string delivery = Console.ReadLine();
                    
                    if (delivery.ToLower() == "yes")
                    {
                        oldmodel.Result.CanDeliver = true;
                    }
                    else if (delivery.ToLower() == "not")
                    {
                        oldmodel.Result.CanDeliver = false;
                    }
                    Console.Clear() ;
                    goto Updatemenu;
                }

                if(num == 6)
                {
                    Console.WriteLine("Enter product new QRCode: ");
                    oldmodel.Result.QRCode = Console.ReadLine();
                    Console.Clear();
                    goto Updatemenu;
                }
                //return Main menu
                if(num==7)
                {
                    Console.Clear();
                    break;
                }

            }
            
            
        }
         
        public async Task Getproduct()
        {
            Console.WriteLine($"1.Search by id\n" +
                $"2.Search by name\n" );
            Console.Write("Enter the part number you want to search: ");
            int number = int.Parse(Console.ReadLine());
            while(true)
            {
                if (number == 1)
                {
                    Console.WriteLine(" ");
                }
            }
            
        }
    }
}
