using ECommerce.Domain.Entities;
using ECommerce.Presentation.LoginPageUI;
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
            await login.LoginPage();

        }
    }
}