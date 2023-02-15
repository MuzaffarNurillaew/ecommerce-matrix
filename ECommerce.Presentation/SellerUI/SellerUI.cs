
using ECommerce.Domain.Entities;

namespace ECommerce.Presentation.SellerUI
{
    public class SellerUI
    {
        public void Seller()
        {
            while (true)
            {
                Console.WriteLine("1.Create ");
                Console.WriteLine("2.Update ");
                Console.WriteLine("3.Get ");
                Console.WriteLine("4.GetAll ");
                Console.WriteLine("5.Delete ");
                Console.WriteLine();
                Console.WriteLine("Enter the number of your chosen department: ");
                int number = int.Parse(Console.ReadLine());

                if (number == 1)
                {
                    CreateProduct();
                }
                if (number == 2)
                {

                }
            }

        }

        public void CreateProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter product Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter product Discription: ");
            string discription = Console.ReadLine();
            Console.Write("Enter product categoryId: ");
            int categoryid = int.Parse(Console.ReadLine());
            Console.Write("Can we deliver this product? Yes or Not: ");
            string delivery = Console.ReadLine();
            bool candelivery;
            if (delivery.ToLower() == "yes")
            {
                candelivery = true;
            }
            else if(delivery.ToLower() == "not")  
            {
                candelivery = false;
            }
            var model = new Product
            {
                Name = name,
                Price = price,
                Description = discription,
                CategoryId = categoryid,
                CanDeliver = candelivery,
                CreatedAt = DateTime.Now,

            };
        }

        // Update function
        public void UpdateProduct()
        {
            Updatemenu:
            Console.WriteLine("     Update Menu ");
            Console.WriteLine("1.Name Update ");
            Console.WriteLine("2.Price Update ");
            Console.WriteLine("3.Discription Update ");
            Console.WriteLine("4.CategoryId Update ");
            Console.WriteLine("5.CanDeliver Update ");
            Console.WriteLine();
            Console.Write("Enter the part number you want to update: ");
            int num = int.Parse(Console.ReadLine());
            var model = new Product();
            while (true)
            {
                if (num == 1)
                {
                    Console.Write("Enter product new name: ");
                    model.Name = Console.ReadLine();
                    Console.Clear();
                    goto Updatemenu;
                }
                if (num == 2)
                {
                    Console.Write("Enter product new price: ");
                    model.Price = decimal.Parse(Console.ReadLine());
                    Console.Clear();
                    goto Updatemenu;
                }
                if(num == 3)
                {
                    Console.Write("Enter product new discription: ");
                }

            }
        }
    }
}
