﻿using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Service.Interfaces;
using ECommerce.Service.Services;

namespace ECommerce.Presentation.SellerUI
{
    public class SellerUI
    {
        private IProductService productService = new ProductService();
        private IChatService chatService = new ChatService();
        private IUserService userService = new UserService();
        private User user;
        public SellerUI(User user1)
        {
            user = user1;
        }
        public async Task Seller()
        {
        MainMenu:
            while (true)
            {
                Console.WriteLine("1.Create ");
                Console.WriteLine("2.Update ");
                Console.WriteLine("3.Get ");
                Console.WriteLine("4.GetAll ");
                Console.WriteLine("5.Delete ");
                Console.WriteLine("6. Chat\n" +
                    "7. Get recommendations\n" +
                    "8. Logout\n" +
                    "9. Delete account\n");
                Console.WriteLine();
                Console.Write("Enter the number of your chosen department: ");
                int number = int.Parse(Console.ReadLine());

                if (number == 1)
                {
                    await CreateProductAsync();

                    Console.Clear();
                    goto MainMenu;
                }
                else if (number == 2)
                {
                    await UpdateProductAsync();

                    Console.Clear();
                    goto MainMenu;
                }
                else if (number == 3)
                {
                    await GetproductAsync();

                    Console.Clear();
                    goto MainMenu;
                }
                else if (number == 4)
                {
                    await GetAllProductAsync();
                }
                else if (number == 5)
                {
                    await DeleteProductAsync();
                }
                else if (number == 6)
                {
                    await ChatAsync();
                }
                else if (number == 7)
                {
                    await RecommendAsync();
                }
                else if (number == 8)
                {
                    return;
                }
            }

        }

        private Task RecommendAsync()
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
        
        public async Task CreateProductAsync()
        {
        getname:
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            // bir xil ismliligiga teekshiradi
            var response1 = await productService.GetByNameAsync(name);

            if (response1.StatusCode != 404)
            {
                Console.WriteLine("Already exists.");
                goto getname;
            }

            Console.Write("Enter product Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter product Description: ");
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

            Console.Write("Enter the QR code of the product: ");
            string qrCode = Console.ReadLine();

            Console.Write("Can we deliver this product? {Yes, y, YES for Yes} or {Any other for Not}: ");
            string delivery = Console.ReadLine();
            bool candelivery = false;

            if (delivery.ToLower() == "yes" || delivery.ToLower() == "y")
            {
                candelivery = true;
            }
            Console.Write("Quantity of product: ");
            long quantity = long.Parse(Console.ReadLine());

            var model = new Product
            {
                Name = name,
                Price = price,
                Description = discription,
                Category = (ProductCategory)(choice * 10),
                HowManyLeft = quantity,
                CanDeliver = candelivery,
                OwnerId = user.Id,
                QRCode = qrCode,
                CreatedAt = DateTime.Now,
            };

            await productService.AddAsync(model);
        }

        // Update function
        public async Task UpdateProductAsync()
        {
            while (true)
            {
                Console.Write("Enter the id of the product you want to update, \"Q\" to exit: ");
                int numid;
                try
                {
                    numid = int.Parse(Console.ReadLine());
                }
                catch
                {
                    return;
                }

                var oldmodel = await productService.GetByIdAsync(numid);

                if (oldmodel.StatusCode == 404)
                {
                    Console.WriteLine(oldmodel.Message);
                    Console.Clear();
                }

                Console.WriteLine($"Name: {oldmodel.Result.Name} Price: {oldmodel.Result.Price} Description: {oldmodel.Result.Description}");
                Console.WriteLine($"Category: {oldmodel.Result.Category} QRCode: {oldmodel.Result.QRCode} {oldmodel.Result.CanDeliver}");

                Console.WriteLine();
                Console.WriteLine("1.Name Update ");
                Console.WriteLine("2.Price Update ");
                Console.WriteLine("3.Description Update ");
                Console.WriteLine("4.Category Update ");
                Console.WriteLine("5.CanDeliver Update ");
                Console.WriteLine("6.QRCode Update ");
                Console.WriteLine("7.Quantity Update ");
                Console.WriteLine("8.Return Main menu");
                Console.WriteLine();
                Console.Write("Enter the sequence of numbers separated by space to update: ");

                int[] num = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                //return Main menu
                if (Array.IndexOf(num, 8) != -1)
                {
                    Console.Clear();
                    return;
                }

                //Name Update 
                if (Array.IndexOf(num, 1) != -1)
                {
                    Console.Write("Enter product new name: ");
                    oldmodel.Result.Name = Console.ReadLine();
                    Console.Clear();
                }

                // Price update
                if (Array.IndexOf(num, 2) != -1)
                {
                    Console.Write("Enter product new price: ");
                    oldmodel.Result.Price = decimal.Parse(Console.ReadLine());
                    Console.Clear();
                }

                //discription update
                if (Array.IndexOf(num, 3) != -1)
                {
                    Console.Write("Enter product new discription: ");
                    oldmodel.Result.Description = Console.ReadLine();
                    Console.Clear();
                }

                //Catecgoryid update
                if (Array.IndexOf(num, 4) != -1)
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
                    oldmodel.Result.Category = (ProductCategory)(choice * 10);
                    Console.Clear();
                }

                // Deliver update
                if (Array.IndexOf(num, 5) != -1)
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
                    Console.Clear();
                }

                if (Array.IndexOf(num, 6) != -1)
                {
                    Console.WriteLine("Enter product new QRCode: ");
                    oldmodel.Result.QRCode = Console.ReadLine();
                    Console.Clear();
                }

                if (Array.IndexOf(num, 7) != -1)
                {
                    Console.WriteLine("Enter product new Quantity: ");
                    oldmodel.Result.HowManyLeft = long.Parse(Console.ReadLine());
                    Console.Clear();
                }

                await productService.UpdateAsync(numid, oldmodel.Result);
                Console.WriteLine("Successfully updated.");

            }


        }

        public async Task GetproductAsync()
        {
            Console.WriteLine($"1.Search by id\n" +
                $"2.Search by name\n" +
                $"~. Quit\n");

            Console.Write("Enter the part number you want to search: ");
            string number = Console.ReadLine();

            if (number == "1")
            {
                Console.Write("Enter the product id you want to search for: ");
                int num = int.Parse(Console.ReadLine());

                var model = await productService.GetAsync(x => x.Id == num && x.OwnerId == user.Id);
                if (model.StatusCode == 404)
                {
                    Console.WriteLine(model.Message);
                }

                Console.WriteLine(value: $"Name: {model.Result.Name} Price: {model.Result.Price} Description: {model.Result.Description}");
                Console.WriteLine($"Category: {model.Result.Category} QRCode: {model.Result.QRCode} {model.Result.CanDeliver}");

            }
            else if (number == "2")
            {
                Console.Write("Enter the product name you want to search for: ");
                string namesearch = Console.ReadLine();
                var model = await productService.GetAsync(x => x.Name == namesearch && x.OwnerId == user.Id);
                if (model.StatusCode == 404)
                {
                    Console.WriteLine(model.Message);
                }
                Console.WriteLine(value: $"Name: {model.Result.Name} Price: {model.Result.Price} Description: {model.Result.Description}");
                Console.WriteLine($"Category: {model.Result.Category} QRCode: {model.Result.QRCode} {model.Result.CanDeliver}");
            }
            else
            {
                return;
            }
            Console.Write("Press any key to return main menu.");
            Console.ReadKey();

        }

        public async Task GetAllProductAsync()
        {
            Console.Write("1. Get all products.\n" +
                "2. Get all products in one category\n" +
                "~. Return to main menu.\n\n" +
                "Your choice: ");
            string choice1 = Console.ReadLine();

            if (choice1 == "1")
            {
                var response = await productService.GetAllAsync(x => x.OwnerId == user.Id);
                var models = response.Result;

                foreach (var item in models)
                {
                    Console.WriteLine("====================================================================================");
                    Console.Write($"Id: {item.Id} Name: {item.Name} Description: {item.Description} \n" +
                        $"Price: {item.Price} QRCode: {item.QRCode} Category: {item.Category} Can we deliver: {item.CanDeliver}\n" +
                        $"CreateAtTime: {item.CreatedAt}");
                }

            }
            else if (choice1 == "2")
            {
                Console.Clear();
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
                Console.Write("Enter the number of the  category: ");
                int choice = int.Parse(Console.ReadLine());

                var model = await productService.GetAllAsync();
                if (model.StatusCode == 404)
                {
                    Console.WriteLine(model.Message);
                }

                if (choice == 1)
                {
                    foreach (var item in model.Result)
                    {
                        if (item.Category == ProductCategory.Food)
                        {
                            Console.WriteLine("====================================================================================");
                            Console.Write($"Id: {item.Id} Name: {item.Name} Description: {item.Description} \n" +
                                $"Price: {item.Price} QRCode: {item.QRCode} Category: {item.Category} Can we deliver: {item.CanDeliver}\n" +
                                $"CreateAtTime: {item.CreatedAt}");
                        }

                    }
                    Console.ReadKey();
                }
                else if (choice == 2)
                {
                    foreach (var item in model.Result)
                    {
                        if (item.Category == ProductCategory.Electronics)
                        {
                            Console.WriteLine("====================================================================================");
                            Console.Write($"Id: {item.Id} Name: {item.Name} Description: {item.Description} \n" +
                                $"Price: {item.Price} QRCode: {item.QRCode} Category: {item.Category} Can we deliver: {item.CanDeliver}\n" +
                                $"CreateAtTime: {item.CreatedAt}");
                        }

                    }
                    Console.ReadKey();
                }
                else if (choice == 3)
                {
                    foreach (var item in model.Result)
                    {
                        if (item.Category == ProductCategory.Clothes)
                        {
                            Console.WriteLine("====================================================================================");
                            Console.Write($"Id: {item.Id} Name: {item.Name} Description: {item.Description} \n" +
                                $"Price: {item.Price} QRCode: {item.QRCode} Category: {item.Category} Can we deliver: {item.CanDeliver}\n" +
                                $"CreateAtTime: {item.CreatedAt}");
                        }

                    }
                    Console.ReadKey();
                }
                else if (choice == 4)
                {
                    foreach (var item in model.Result)
                    {
                        if (item.Category == ProductCategory.Accesories)
                        {
                            Console.WriteLine("====================================================================================");
                            Console.WriteLine($"Id: {item.Id} Name: {item.Name} Description: {item.Description} \n" +
                                $"Price: {item.Price} QRCode: {item.QRCode} Category: {item.Category} Can we deliver: {item.CanDeliver}\n" +
                                $"CreateAtTime: {item.CreatedAt}");
                        }

                    }
                    Console.ReadKey();
                }
                else if (choice == 5)
                {
                    foreach (var item in model.Result)
                    {
                        if (item.Category == ProductCategory.Furnitures)
                        {
                            Console.WriteLine("====================================================================================");
                            Console.WriteLine($"Id: {item.Id} Name: {item.Name} Description: {item.Description} \n" +
                                $"Price: {item.Price} QRCode: {item.QRCode} Category: {item.Category} Can we deliver: {item.CanDeliver}\n" +
                                $"CreateAtTime: {item.CreatedAt}");
                        }

                    }
                    Console.ReadKey();
                }
                else if (choice == 6)
                {
                    foreach (var item in model.Result)
                    {
                        if (item.Category == ProductCategory.Perfumes)
                        {
                            Console.WriteLine("====================================================================================");
                            Console.WriteLine($"Id: {item.Id} Name: {item.Name} Description: {item.Description} \n" +
                                $"Price: {item.Price} QRCode: {item.QRCode} Category: {item.Category} Can we deliver: {item.CanDeliver}\n" +
                                $"CreateAtTime: {item.CreatedAt}");
                        }

                    }
                    Console.ReadKey();
                }
                else if (choice == 7)
                {
                    foreach (var item in model.Result)
                    {
                        if (item.Category == ProductCategory.Souviners)
                        {
                            Console.WriteLine("====================================================================================");
                            Console.WriteLine($"Id: {item.Id} Name: {item.Name} Description: {item.Description} \n" +
                                $"Price: {item.Price} QRCode: {item.QRCode} Category: {item.Category} Can we deliver: {item.CanDeliver}\n" +
                                $"CreateAtTime: {item.CreatedAt}");
                        }

                    }
                    Console.ReadKey();
                }
                else if (choice == 8)
                {
                    foreach (var item in model.Result)
                    {
                        if (item.Category == ProductCategory.Toys)
                        {
                            Console.WriteLine("====================================================================================");
                            Console.WriteLine($"Id: {item.Id} Name: {item.Name} Description: {item.Description} \n" +
                                $"Price: {item.Price} QRCode: {item.QRCode} Category: {item.Category} Can we deliver: {item.CanDeliver}\n" +
                                $"CreateAtTime: {item.CreatedAt}");
                        }

                    }
                    Console.ReadKey();
                }
                else if (choice == 9)
                {
                    foreach (var item in model.Result)
                    {
                        if (item.Category == ProductCategory.Books)
                        {
                            Console.WriteLine("====================================================================================");
                            Console.WriteLine($"Id: {item.Id} Name: {item.Name} Description: {item.Description} \n" +
                                $"Price: {item.Price} QRCode: {item.QRCode} Category: {item.Category} Can we deliver: {item.CanDeliver}\n" +
                                $"CreateAtTime: {item.CreatedAt}");
                        }

                    }
                    Console.ReadKey();
                }
                else if (choice == 10)
                {
                    foreach (var item in model.Result)
                    {
                        if (item.Category == ProductCategory.Other)
                        {
                            Console.WriteLine("====================================================================================");
                            Console.WriteLine($"Id: {item.Id} Name: {item.Name} Description: {item.Description} \n" +
                                $"Price: {item.Price} QRCode: {item.QRCode} Category: {item.Category} Can we deliver: {item.CanDeliver}\n" +
                                $"CreateAtTime: {item.CreatedAt}");
                        }

                    }
                    Console.ReadKey();
                }
            }
            else
            {
                return;
            }
        }

        public async Task DeleteProductAsync()
        {
            Console.Write("Enter the id of the product you want to delete: ");
            int deleteid = int.Parse(Console.ReadLine());
            var model = await productService.DeleteByIdAsync(deleteid, user.Id);
            if (model.Result == true)
            {
                Console.WriteLine("Successfully deleted");
            }
            else
            {
                Console.WriteLine("Product is not found");
            }

            Console.Write("Press any key to return main menu.");
            Console.ReadKey(true);
        }
    }
}