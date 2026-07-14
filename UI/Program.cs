using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Core.Models;
using Repository;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserRepository userRepository = new FileRepository();
            IEmailService emailService = new EmailService();

            IUserService userService = new UserService(userRepository);
            IAuthenticationService authenticationService = new AuthenticationService(userRepository, emailService);

            // userService.RegisterUser("Anano", "mesxi032@gmail.com", "ajaja");
            //authenticationService.SendVerificationCode("mesxi032@gmail.com");
            // authenticationService.VerifyUser("mesxi032@gmail.com", "1212");
            //User user = authenticationService.LoginUser("mesxi032@gmail.com", "ajaja");
            //Console.WriteLine(user.Username);
            //userService.VerifyUser("mesxi032@gmail.com", "2050");
            //User authUser = userRepository.GetLastLoggedUser();
            // Console.WriteLine(authUser.Username);
           // authenticationService.LogoutUser("mesxi032@gmail.com");
        }
    }
}
