using Application.Interfaces;
using Application.Services;
using Core.Exceptions;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Menus
{
    public class MainMenu
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IBookService _bookService;
        private readonly IBorrowService _borrowService;
        private readonly IUserService _userService;
        private readonly IClientService _clientService;
        private readonly IAdminService _adminService;
        public MainMenu(IAuthenticationService authenticationService, IBookService bookService, IBorrowService borrowService,IUserService userService, IClientService clientService, IAdminService adminService)
        {
            _authenticationService = authenticationService;
            _bookService = bookService;
            _borrowService = borrowService;
            _clientService = clientService;
            _userService = userService;
            _adminService = adminService;
        }

        public void Show()
        {
            

            while (true)
            {
                try
                {
                    Console.WriteLine("Welcome back to the library!");
                    Console.WriteLine("1. Register");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("3. Exit");
                    Console.Write("Choose an option: ");

                    string userChoice = Console.ReadLine().Trim();
                    if (String.IsNullOrWhiteSpace(userChoice))
                    {
                        throw new NullOrWhiteSpace();
                    }
                    switch (userChoice)
                    {
                        case "1":
                            {
                                EnterRegistrationInfo();
                                break;
                            }
                        case "2":
                            {
                                EnterLoginInfo();
                                break;
                            }
                        case "3":
                            {
                                Console.WriteLine("Operation has been closed. Exiting program.");
                                return;
                            }
                        default:
                            {
                                throw new InvalidChoice();
                            }
                    }
                }
                catch (InvalidChoice ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (UserEmailExists ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (UserEmailDoesNotExist ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }

        public void EnterLoginInfo()
        {
            Console.Write("Email: ");
            string inputEmail = Console.ReadLine();

            Console.Write("Password: ");
            string inputPassword = Console.ReadLine();

            User user = _authenticationService.LoginUser(inputEmail, inputPassword);
           
            if (user is ClientUser)
            {
                _clientService.UpdateClientFine((ClientUser)user);
                ClientMenu clientMenu = new ClientMenu(_authenticationService, _bookService, _borrowService, _clientService, (ClientUser)user);
                clientMenu.Show();
            }
            else 
            {
                AdminMenu adminMenu = new AdminMenu(_authenticationService, _bookService, _adminService, _userService, (AdminUser)user);
                adminMenu.Show();
            }
        }

        public void EnterRegistrationInfo()
        {
            Console.Write("Username: ");
            string inputUsername = Console.ReadLine();

            Console.Write("Email: ");
            string inputEmail = Console.ReadLine();

            Console.Write("Password: ");
            string inputPassword = Console.ReadLine();

            _userService.RegisterUser(inputUsername, inputEmail, inputPassword);

            _authenticationService.SendVerificationCode(inputEmail);

            for (int i = 0; i < 3; i++)
            {
                Console.Write("Enter verification code: ");
                string code = Console.ReadLine();

                if (_authenticationService.VerifyUser(inputEmail, code))
                {
                    Console.WriteLine("Registration completed successfully.");
                    return;
                }

                Console.WriteLine("Incorrect code.");
            }

            Console.WriteLine("Registration failed. Too many incorrect verification attempts.");
        }

    }
}
