using Application.Interfaces;
using Application.Services;
using Core.Exceptions;
using Core.Models;
using Spectre.Console;
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
                    string userChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Welcome back to the library!")
                            .AddChoices(
                                "Register",
                                "Login",
                                "Exit"
                            ));
                    
                    switch (userChoice)
                    {
                        case "Register":
                            EnterRegistrationInfo();
                            break;

                        case "Login":
                            EnterLoginInfo();
                            break;

                        case "Exit":
                            return;
                    }
                }

                catch (UserEmailExists ex)
                {
                    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                }
                catch (UserEmailDoesNotExist ex)
                {
                    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Unexpected error: {ex.Message}[/]");
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
                    AnsiConsole.MarkupLine("[green]Verification successful.[/]");
                    AnsiConsole.MarkupLine("[green]Registration completed successfully.[/]");
                    return;
                }
                AnsiConsole.MarkupLine("[red]Invalid verification code.[/]");
            }

            AnsiConsole.MarkupLine("[red]Registration failed. Too many incorrect verification attempts.[/]");
        }

    }
}
