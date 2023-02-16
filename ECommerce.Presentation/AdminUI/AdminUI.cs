
using ECommerce.Domain.Entities;
using ECommerce.Service.Interfaces;
using ECommerce.Service.Services;
using System.Reflection;

namespace ECommerce.Presentation.AdminUI
{

    public class AdminUI
    {
        private IUserService userService = new UserService();
        public async Task Admin()
        {
            AdminMenu:
            Console.WriteLine("         Main Menu   ");
            Console.WriteLine("1.Search User");
            Console.WriteLine("2.Get All Users");
            Console.WriteLine("3.Delete User");
            Console.Write("Enter the number of your chosen department: ");
            int number = int.Parse(Console.ReadLine());
            while (true)
            {
                if (number == 1)
                {
                    SearchAsync();
                    goto AdminMenu;
                }
                else if (number == 2)
                {
                    GetAsync();
                    goto AdminMenu;
                }
                else if(number == 3)
                {
                    DeleteUserAsync();
                    goto AdminMenu;
                }
            }
        }


        public async Task SearchAsync()
        {
        Get:
            Console.WriteLine($"1.Search by id\n" +
                $"2.Search by name\n");
            Console.Write("Enter the part number you want to search: ");
            int number = int.Parse(Console.ReadLine());
            while (true)
            {
                if (number == 1)
                {
                    Console.Write("Enter the product id you want to search for: ");
                    int num = int.Parse(Console.ReadLine());
                    var model = await userService.GetByIdAsync(num);
                    if (model.StatusCode == 404)
                    {
                        Console.WriteLine(model.Message);
                    }
                    Console.WriteLine($"Id: {model.Result.Id} Name: {model.Result.FirstName} LastName: {model.Result.LastName} UserName: {model.Result.Username}");
                    Console.WriteLine($"Email: {model.Result.Email} Phone Number: {model.Result.PhoneNumber} Money: {model.Result.AvailableMoney}");

                    Console.ReadKey();
                    goto Get;
                }
                else if (number == 2)
                {
                    Console.Write("Enter the product name you want to search for: ");
                    string namesearch = Console.ReadLine();
                    var model = await userService.GetByNameAsync(namesearch);
                    if (model.StatusCode == 404)
                    {
                        Console.WriteLine(model.Message);
                    }
                    Console.WriteLine($"Id: {model.Result.Id} Name: {model.Result.FirstName} LastName: {model.Result.LastName} UserName: {model.Result.Username}");
                    Console.WriteLine($"Email: {model.Result.Email} Phone Number: {model.Result.PhoneNumber} Money: {model.Result.AvailableMoney}");

                    Console.ReadKey();
                    goto Get;
                }
            }
        }
        public async Task GetAsync()
        {
            var model = await userService.GetAllAsync();
            foreach (var item in model.Result)
            {
                Console.WriteLine("====================================================================================");
                Console.WriteLine($"Id: {item.Id} Name: {item.FirstName} LastName: {item.LastName} UserName: {item.Username}");
                Console.WriteLine($"Email: {item.Email} Phone Number: {item.PhoneNumber} Money: {item.AvailableMoney}");
            }
            Console.ReadKey();

        }
        public async Task DeleteUserAsync()
        {
            Console.Write("Enter the id of the User you want to delete: ");
            int deleteid = int.Parse(Console.ReadLine());
            var model = userService.DeleteAsync(deleteid);
            if (model.Result.Result == true)
            {
                Console.WriteLine(model.Result.Message);
            }
            else
            {
                Console.WriteLine("User is not found");
            }

            Console.Write("Press any key to return main menu.");
            Console.ReadKey(true);
        }
    }
}