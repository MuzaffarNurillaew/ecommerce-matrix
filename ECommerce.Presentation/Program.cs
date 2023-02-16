using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Presentation.LoginPageUI;
using ECommerce.Presentation.SellerUI;
using ECommerce.Service.Interfaces;
using ECommerce.Service.Services;

namespace ECommerce.Presentation
{
    class Program
    {
        //private static IOrderService orderService = new OrderService();
        private static async Task Main(string[] args)
        {
            while (true)
            {
                
                if (args.Length == 1)
                {
                    Console.WriteLine("you admin");
                    if (args[0] == "--neo")
                    {
                        //var tempAdmin = new AdminUI.AdminUI(new User() { });
                        //User anAdmin = await tempAdmin.Authorize();

                        //var adminAccount = new AdminUI.AdminUI(anAdmin);
                        //await adminAccount.Admin();
                    }
                }

                var login = new LoginPageUI.LoginPageUI();
                var currentUser = await login.LoginPage();

                if (currentUser.Role == UserRole.Merchant)
                {
                    var seller = new SellerUI.SellerUI(currentUser);
                    await seller.Seller();
                }
                else if (currentUser.Role == UserRole.Customer)
                {
                    Console.WriteLine("Customer UI chiqishi kerak edi.");
                }
                else if (currentUser.Role == UserRole.Admin)
                {
                    var admin = new AdminUI.AdminUI(currentUser);
                    await admin.Admin();
                }
            }
        }
    }
}