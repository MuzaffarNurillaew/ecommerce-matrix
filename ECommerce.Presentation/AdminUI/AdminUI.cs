using ECommerce.Domain.Entities;
using ECommerce.Service.Interfaces;
using ECommerce.Service.Services;

namespace ECommerce.Presentation.AdminUI
{

    public class AdminUI
    {
        private User adminAccount;
        public AdminUI(User adminA)
        {
            adminAccount = adminA;
        }
        private IUserService userService = new UserService();
        private IChatService chatService = new ChatService();
        public async Task Admin()
        {
            while (true)
            {
                Console.WriteLine("         Main Menu   ");
                Console.WriteLine("1.Search User");
                Console.WriteLine("2.Get All Users");
                Console.WriteLine("3.Delete User\n" +
                    "4. Chats\n" +
                    "5. Edit a user's details\n" +
                    "6. Log out from account");
                Console.Write("Enter the number of your chosen department: ");
                int number = int.Parse(Console.ReadLine());

                if (number == 1)
                {
                    await SearchAsync();
                }
                else if (number == 2)
                {
                    await GetAsync();
                }
                else if (number == 3)
                {
                    await DeleteUserAsync();
                }
                else if (number == 4)
                {
                    await ChatAsync();
                }
                else if (number == 5)
                {
                    await Edit();
                }
                else if (number == 6)
                {
                    return;
                }
            }
        }

        private Task Edit()
        {
            throw new NotImplementedException();
        }

        private async Task ChatAsync()
        {
            while (true)
            {
                Console.Clear();

                Console.Write("1. Xabarlarni ko'rish.\n" +
                    "2. Xabar yozish.\n" +
                    "~ Exit.\n\n" +
                    "Your choice: ");
                string choice = Console.ReadLine();
                if (choice == "2")
                {
                getusername:
                    Console.Write("Enter the username of user to send message: ");
                    string username = Console.ReadLine();

                    var response = await userService.GetAsync(x => x.Username == username);

                    if (response.StatusCode == 404)
                    {
                        Console.WriteLine("Username not found");
                        goto getusername;
                    }

                    Console.Write("Type a message: ");
                    string message = Console.ReadLine();

                    await chatService.SendMessageAsync(new ChatInfo()
                    {
                        SenderId = adminAccount.Id,
                        RespondentId = response.Result.Id,
                        Message = message
                    });
                    Console.WriteLine("Successfully sent.");
                }
                else if (choice == "1")
                {
                    var response = await chatService.GetAll(x => x.SenderId == adminAccount.Id || x.RespondentId == adminAccount.Id);
                    var allMessages = response.Result;

                    foreach (var message in allMessages)
                    {
                        if (adminAccount.Id == message.SenderId)
                        {
                            var r = await userService.GetByIdAsync(message.RespondentId);
                            string fromUsername;
                            if (r.StatusCode == 404)
                            {
                                fromUsername = "DELETED";
                            }
                            else
                            {
                                fromUsername = r.Result.Username;
                            }
                            Console.WriteLine($"From: YOU\n" +
                                $"To: {fromUsername}\n" +
                                $"Message: {message.Message} \n" +
                                $"Date: {message.CreatedAt} \n");
                        }
                        else
                        {
                            var r = await userService.GetByIdAsync(message.SenderId);
                            string toUsername;
                            if (r.StatusCode == 404)
                            {
                                toUsername = "DELETED";
                            }
                            else
                            {
                                toUsername = r.Result.Username;
                            }
                            Console.WriteLine($"From: {toUsername}\n" +
                                $"To: YOU\n" +
                                $"Message: {message.Message}\n" +
                                $"Date: {message.CreatedAt}\n");
                        }


                    }
                }
                else
                {
                    Console.Clear();
                    break;
                }
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public async Task SearchAsync()
        {
        Get:
            Console.WriteLine($"1.Search by id\n" +
                $"2.Search by name\n" +
                $"~. To return main menu\n\n");

            Console.Write("Enter the part number you want to search: ");
            string number = Console.ReadLine();
            while (true)
            {
                if (number == "1")
                {
                    Console.Write("Enter the user id you want to search for: ");
                    int num = int.Parse(Console.ReadLine());
                    var model = await userService.GetByIdAsync(num);

                    if (model.StatusCode == 404)
                    {
                        Console.WriteLine("User not found.");
                        continue;
                    }

                    Console.WriteLine($"Id: {model.Result.Id} Name: {model.Result.FirstName} LastName: {model.Result.LastName} UserName: {model.Result.Username}");
                    Console.WriteLine($"Email: {model.Result.Email} Phone Number: {model.Result.PhoneNumber} Money: {model.Result.AvailableMoney}");

                    Console.ReadKey();
                    goto Get;
                }
                else if (number == "2")
                {
                    Console.Write("Enter the product name you want to search for: ");
                    string namesearch = Console.ReadLine();
                    var model = await userService.GetByNameAsync(namesearch);
                    if (model.StatusCode == 404)
                    {
                        Console.WriteLine(model.Message);
                        continue;
                    }
                    Console.WriteLine($"Id: {model.Result.Id} Name: {model.Result.FirstName} LastName: {model.Result.LastName} UserName: {model.Result.Username}");
                    Console.WriteLine($"Email: {model.Result.Email} Phone Number: {model.Result.PhoneNumber} Money: {model.Result.AvailableMoney}");

                    Console.ReadKey();
                    goto Get;
                }
                else
                {
                    return;
                }
            }
        }
        public async Task GetAsync()
        {
            var model = await userService.GetAllAsync(x => x == x);
            foreach (var item in model.Result)
            {
                Console.WriteLine("====================================================================================");
                Console.WriteLine($"Id: {item.Id} Name: {item.FirstName} LastName: {item.LastName} UserName: {item.Username}");
                Console.WriteLine($"Email: {item.Email} Phone Number: {item.PhoneNumber} Money: {item.AvailableMoney}");
                Console.WriteLine($"Role: {item.Role}, Created at: {item.CreatedAt}, Last updated at{item.UpdatedAt}");
            }
            Console.ReadKey();

        }
        public async Task DeleteUserAsync()
        {
            Console.Write("Enter the id of the User you want to delete: ");
            int deleteid = int.Parse(Console.ReadLine());
            var model = await userService.DeleteAsync(deleteid);
            if (model.Result == true)
            {
                Console.WriteLine("Deleted.");
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