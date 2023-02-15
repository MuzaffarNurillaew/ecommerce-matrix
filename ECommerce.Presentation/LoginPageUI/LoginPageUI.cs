using ECommerce.Domain.Entities;
using ECommerce.Service.Interfaces;
using ECommerce.Service.Services;

namespace ECommerce.Presentation.LoginPageUI
{
    public class LoginPageUI
    {
        private IUserService userService = new UserService();
        //classni ichiga funksiya bermasak xatolik beryapti shuning uchun bu funksiya yozilgan
        public async Task<User> LoginPage()
        {
            while (true)
            {
                Console.WriteLine("     Main Menu   ");
                Console.WriteLine("1.Sign up");
                Console.WriteLine("2.Sign in");
                Console.WriteLine();
                Console.WriteLine("Enter the number of your chosen department: ");

                int number = int.Parse(Console.ReadLine());

                // Sign Up 
                if (number == 1)
                {
                signup:
                    Console.Clear();
                    Console.WriteLine("     Sign up ");

                    Console.Write("Enter your name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter your surname: ");
                    string surname = Console.ReadLine();

                    Console.Write("Enter a new login: ");
                    string login = Console.ReadLine();

                    Console.Write("Enter your email: ");
                    string email = Console.ReadLine();

                    Console.Write("Enter your phone number: ");
                    string phoneNumber = Console.ReadLine();
                password10:
                    Console.Write("Enter a new password: ");
                    string password1 = Console.ReadLine();

                    Console.Write("re-enter the password: ");
                    string password2 = Console.ReadLine();

                    // here the password is checked, if it's wrong, you can re-enter it. Sorry now I have to use goto
                    if (password1 != password2)
                    {
                        Console.Clear();
                        Console.WriteLine("The passwords you entered do not match. Please re-enter the password");
                        goto password10;
                    }

                    var person = new User()
                    {
                        FirstName = name,
                        LastName = surname,
                        Email = email,
                        Username = login,
                        PhoneNumber = phoneNumber,
                        Password = password1
                    };
                    // bu yerini keyin davom etadi chunki server bilan ulash kere
                   
                    var response = await userService.CreateAsync(person);
                 
                    if (response.StatusCode == 404)
                    {
                        Console.WriteLine("Bunaqa user mavjud");
                        goto signup;
                    }
                    else
                    {
                        Console.WriteLine("Created\n");
                        //return person;
                    }
                }

                //Sign in
                else if (number == 2)
                {
                    login1:
                    Console.Clear();
                    Console.WriteLine("     Sign in ");
                    Console.Write("Enter your Login or Email: ");
                    string loginoremail = Console.ReadLine();
                    Console.Write("Enter your password: ");
                    string password = Console.ReadLine();

                    //serverga ulanadigan joyi qoldi

                    var user = await userService.GetAsync(x => x.Username == loginoremail || x.Email == loginoremail);
                    if (user.StatusCode == 200)
                    {
                        if (password == user.Result.Password)
                        {
                            Console.WriteLine("Entered");
                            //return user.Result;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect password or login.");
                            goto login1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bunaqa user yo'q.");
                    }
                }
            }

        }
    }
}
