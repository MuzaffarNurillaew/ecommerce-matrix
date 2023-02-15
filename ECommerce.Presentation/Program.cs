using ECommerce.Domain.Entities;
using ECommerce.Presentation.LoginPageUI;
using ECommerce.Service.Interfaces;
using ECommerce.Service.Services;

namespace ECommerce.Presentation
{
    class Program
    {
        private static IOrderService orderService = new OrderService();
        private static Task Main()
        {

            var login = new LoginPageUI.LoginPageUI();
            login.LoginPage();

            return Task.CompletedTask;
        }
    }
}