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
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public MainMenu(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        public void Show()
        {
            

            while (true)
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
                ClientMenu clientMenu = new ClientMenu();
                clientMenu.Show();
            }
            else if (user is AdminUser)
            {
                AdminMenu adminMenu = new AdminMenu();
                adminMenu.Show();
            }
        }

        public void EnterRegistrationInfo()
        {
            Console.Write("Username: ");
            string inputUsername = Console.ReadLine();

            Console.Write("Email: ");
            string inputEmail = Console.ReadLine();

            Console.Write("password: ");
            string inputPassword = Console.ReadLine();

            _userService.RegisterUser(inputUsername, inputEmail, inputPassword);

            Console.WriteLine("Registration has been completed successfully.");
        }
    }
}
