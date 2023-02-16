using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Service.Interfaces;
using ECommerce.Service.Services;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace ECommerce.Presentation.CustomerUI
{
    public class CustomerUI
    {
        private readonly IOrderService orderService = new OrderService();
        private readonly IProductService productService = new ProductService();
        private readonly IUserService userService = new UserService();
        List<OrderItem> products = new List<OrderItem>();
        public async Task CustomerU()
        {
        ProductCategory:
            Console.WriteLine($"1.Food\n" +
                       $"   2.Electronics\n" +
                       $"   3.Clothes\n" +
                       $"   4.Accesories\n" +
                       $"   5.Furnitures\n" +
                       $"   6.Perfumes\n" +
                       $"   7.Souviners\n" +
                       $"   8.Toys\n" +
                       $"   9.Books\n" +
                       $"   10.Others\n");
            Console.WriteLine("11.My Profile");
            Console.WriteLine("12.Savat ");

            Console.Write("Enter the number of the new category: ");
            int number = int.Parse(Console.ReadLine());

            string[] cate = "Food Electronics Assecories Furnitures Perfumes Souviners Toys Books Others".Split();
            var models = await productService.GetAllAsync();
            List<long> list = new List<long>();
            int i = 1;
            if (number < 11)
            {
            ProductMenu:
                foreach (var model in models.Result)
                {
                    if (cate[number - 1] == Convert.ToString(model.Category))
                    {
                        Console.WriteLine("\n===================================================================================");
                        Console.Write($"{i}. Name: {model.Name} Description: {model.Description} \n" +
                                $"Price: {model.Price} Category: {model.Category} Can we deliver: {model.CanDeliver}\n" +
                                $"CreateAtTime: {model.CreatedAt}");
                        list.Add(model.Id);
                        i++;
                    }
                }

                Console.Write("Enter the number of the product you want to buy: ");
                var item = await productService.GetByIdAsync(list[int.Parse(Console.ReadLine()) - 1]);

                Console.WriteLine($" Name: {item.Result.Name} Description: {item.Result.Description} \n" +
                                   $"Price: {item.Result.Price} Category: {item.Result.Category} Can we deliver: {item.Result.CanDeliver}\n" +
                                   $"CreateAtTime: {item.Result.CreatedAt}\n");
                Console.WriteLine("1.Buy \n 2.Return Main Menu\n 3.Return products");
                Console.Write("Enter the number of the product you want to department: ");
                int num = int.Parse(Console.ReadLine());
                if (num == 1)
                {
                    //bu yeri qoldi ertaga davom ettiraman
                    Console.Write("How many do you want to buy?: ");
                    int buy = int.Parse(Console.ReadLine());
                }
                else if (num == 2)
                {
                    Console.Clear();
                    goto ProductCategory;
                }
                else if (num == 3)
                {
                    Console.Clear();
                    goto ProductMenu;
                }
            }
            else if (number > 10)
            {
                if (number == 11)
                {
                    //MyProfile();

                }
            }

        }
        public async Task MyProfile(User userperson)
        {
        myprofil:
            Console.Clear();

            Console.WriteLine("     My Profile ");
            Console.WriteLine($"    1.FirstName: {userperson.FirstName}\n" +
                $"  2.LastName: {userperson.LastName}\n" +
                $"  3.UserName: {userperson.Username} \n" +
                $"  4.Email: {userperson.Email} \n" +
                $"  5.PhoneNumber: {userperson.PhoneNumber} \n");
            Console.WriteLine("6.Return Main Menu");
            Console.WriteLine("");
            Console.WriteLine("If you want to change your information, enter the number of the information you want to change: ");
            int number = int.Parse(Console.ReadLine());
            if (number == 1)
            {
                Console.Write("Enter new name: ");
                userperson.FirstName = Console.ReadLine();
                goto myprofil;
            }
        }
    }
}
