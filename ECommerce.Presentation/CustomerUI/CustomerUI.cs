using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Service.Interfaces;
using ECommerce.Service.Services;

namespace ECommerce.Presentation.CustomerUI
{
    public class CustomerUI
    {
        private readonly IOrderService orderService = new OrderService();
        private readonly IProductService productService = new ProductService();
        private readonly IUserService userService = new UserService();
        private readonly IPaymentService paymentService = new PaymentService();
        private readonly IChatService chatService = new ChatService();


        private User user;
        public CustomerUI(User user1)
        {
            user = user1;
        }
        public async Task Customer()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("1. Mahsulotlarni tomosha qilish.\n" +
                    "2. Order uchun ko'rish.\n" +
                    "3. Pul qo'shish\n" +
                    "4. Mening akkountim.\n" +
                    "5. Mahsulot qidirish\n" +
                    "6. Chat\n" +
                    "7. Hamma mahsulotlarni ko'rish\n" +
                    "8. Mening Orderlar ro'yxatim.\n" +
                    "~. Chiqish");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    // done
                    await ViewProductsAsync();
                }
                else if (choice == "2")
                {
                    await OrderProductsAsync();
                }
                else if (choice == "3")
                {
                    // done
                    await AddMoneyAsync();
                }
                else if (choice == "4")
                {
                    // done
                    PrintMyInfosAsync();
                }
                else if (choice == "5")
                {
                    // done
                    await PrintSearchAsync();
                }
                else if (choice == "6")
                {
                    // done
                    await ChatAsync();
                }
                else if (choice == "7")
                {
                    // done
                    await GetAllProductsAsync();
                }
                else if (choice == "8")
                {
                    // done
                    await PrintMyOrdersAsync();
                }
                else
                {
                    return;
                }

                Console.Write("Press any key.");
                Console.ReadKey();
            }
        }

        private async Task PrintMyOrdersAsync()
        {
            var response = await orderService.GetAllAsync(x => x.UserId == user.Id);
            var orders = response.Result;

            foreach ( var order in orders )
            {
                var paymentResponse = await paymentService.GetByIdAsync(order.PaymentId);
                decimal totalPaidMoney = 0;

                if ( paymentResponse.StatusCode == 200 )
                {
                    totalPaidMoney = paymentResponse.Result.Amount;
                }

                Console.WriteLine($"Ordered at: {order.CreatedAt}\n" +
                    $"Total paid money: {totalPaidMoney}");
                foreach (var item in order.Items)
                {
                    var productResponse = await productService.GetByIdAsync(item.ProductId);
                    var product = productResponse.Result;
                    Console.WriteLine($"Product: {product.Name}, Amount: {item.Quantity}, TotalPrice: {item.Quantity * product.Price}");
                }
            }
        }

        private async Task GetAllProductsAsync()
        {
            var response = await productService.GetAllAsync(x => x == x);

            var entities = response.Result;
            foreach (var entity in entities)
            {
                Console.WriteLine("=================================================");
                Console.WriteLine($"Category: {entity.Category}\n" +
                    $"Name: {entity.Name}, Price: {entity.Price}\n" +
                    $"Available amount: {entity.HowManyLeft}\n" +
                    $"Description: {entity.Description}");
                Console.WriteLine("=================================================");
            }
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
                    Console.Write("Enter the username of user to send message, or \"MATRIX\" to chat with admin: ");
                    string username = Console.ReadLine();

                    if (username == "MATRIX")
                    {
                        var rAdmin = await userService.GetAllAsync(x => x.Role == UserRole.Admin);
                        var admins = rAdmin.Result;

                        Console.Write("Type a message: ");
                        string message = Console.ReadLine();

                        foreach (var admin in admins)
                        {
                            await chatService.SendMessageAsync(new ChatInfo()
                            {
                                SenderId = user.Id,
                                RespondentId = admin.Id,
                                Message = message
                            });
                        }
                    }
                    else
                    {
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
                            SenderId = user.Id,
                            RespondentId = response.Result.Id,
                            Message = message
                        });
                        Console.WriteLine("Successfully sent.");
                    }
                }
                else if (choice == "1")
                {
                    var response = await chatService.GetAll(x => x.SenderId == user.Id || x.RespondentId == user.Id);
                    var allMessages = response.Result;

                    foreach (var message in allMessages)
                    {
                        if (user.Id == message.SenderId)
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


        private async Task PrintSearchAsync()
        {
            Console.Write("Enter the name of product: ");
            string name = Console.ReadLine();

            var response = await productService.GetByNameAsync(name);

            if (response.StatusCode == 200)
            {
                var entity = response.Result;

                Console.WriteLine("=================================================");
                Console.WriteLine($"Name: {entity.Name}, Price: {entity.Price}\n" +
                    $"Available amount: {entity.HowManyLeft}\n" +
                    $"Description: {entity.Description}");
                Console.WriteLine("=================================================");
            }
            else
            {
                Console.WriteLine("There is no such a product.");
            }
        }

        private void PrintMyInfosAsync()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine($"Id: {user.Id} Name: {user.FirstName} LastName: {user.LastName}\n" +
                $"UserName: {user.Username} Email: {user.Email} Phone number: {user.PhoneNumber}\n" +
                $"Total money: {user.AvailableMoney}");
            Console.WriteLine("=================================================");
        }

        private async Task AddMoneyAsync()
        {
            Console.WriteLine($"Your current balance is {user.AvailableMoney}");

            Console.Write("1. Add money.\n" +
                "~. Exit.\n\n" +
                "Your choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("How much money do you want to add: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                user.AvailableMoney += amount;
                await userService.UpdateAsync(user.Id, user);
            }
            else
            {
                return;
            }
        }

        private Task OrderProductsAsync()
        {
            throw new NotImplementedException();
        }

        private async Task ViewProductsAsync()
        {
            Console.Write($"\tOur Product categories:\n" +
                $"1.Food\n" +
                       $"2.Electronics\n" +
                       $"3.Clothes\n" +
                       $"4.Accesories\n" +
                       $"5.Furnitures\n" +
                       $"6.Perfumes\n" +
                       $"7.Souviners\n" +
                       $"8.Toys\n" +
                       $"9.Books\n" +
                       $"10.Others\n" +
                       $"~. To Quit\n\n" +
                       $"Select category: ");
            string input = Console.ReadLine();
            int categoryChoice;
            try
            {
                categoryChoice = Convert.ToInt32(input);
            }
            catch (Exception)
            {
                return;
            }

            var chosenCategory = (ProductCategory)(categoryChoice * 10);

            var response = await productService.GetAllAsync(x => x.Category == chosenCategory);

            var entities = response.Result;
            foreach (var entity in entities)
            {
                Console.WriteLine("=================================================");
                Console.WriteLine($"Name: {entity.Name}, Price: {entity.Price}\n" +
                    $"Available amount: {entity.HowManyLeft}\n" +
                    $"Description: {entity.Description}");
                Console.WriteLine("=================================================");
            }
        }
    }
}
