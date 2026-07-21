using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Repository;
using UI.Menus;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
     
            try
            {
                #region Repositories
                IUserRepository userRepository = new UserRepository();
                IBookRepository bookRepository = new BookRepository();
                IBorrowRepository borrowRepository = new BorrowRepository();
                #endregion

                #region Services
                IEmailService emailService = new EmailService();
                IAuthenticationService authenticationService = new AuthenticationService(userRepository, emailService);
                IUserService userService = new UserService(userRepository);
                IBookService bookService = new BookService(bookRepository);
                IBorrowService borrowService = new BorrowService(bookService, borrowRepository, bookRepository, userRepository);
                IClientService clientService = new ClientService(borrowRepository, userRepository);
                IAdminService adminService = new AdminService(borrowRepository, bookRepository, userRepository, emailService);
                #endregion

                MainMenu mainMenu = new MainMenu(authenticationService, bookService, borrowService, userService, clientService, adminService);

                mainMenu.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            
        }
    }
}
