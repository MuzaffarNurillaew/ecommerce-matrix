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
        private static IOrderService orderService = new OrderService();
        private static async Task Main()
        {
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
        }
    }
}