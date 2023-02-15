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

            if (currentUser.Status == UserStatus.Seller)
            {
                var seller = new SellerUI.SellerUI(currentUser); 
            }
            else if (currentUser.Status == UserStatus.Customer)
            {

            }
        }
    }
}