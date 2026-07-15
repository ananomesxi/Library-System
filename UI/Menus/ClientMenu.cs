using Application.Interfaces;
using Core.Exceptions;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Menus
{
    public class ClientMenu
    {
       
        private readonly IAuthenticationService _authenticationService;
        private readonly ClientUser _currentClient;

        public ClientMenu(IAuthenticationService authenticationService, ClientUser currentClient)
        {
            _authenticationService = authenticationService;
            _currentClient = currentClient;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("| ADMIN |");
                Console.WriteLine("1. Show all books");
                Console.WriteLine("2. Find a book");
                Console.WriteLine("3. Borrow a book");
                Console.WriteLine("4. Return a book");
                Console.WriteLine("5. Show my books");
                Console.WriteLine("6. Logout");
                Console.Write("Choose an option: ");

                string userChoice = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(userChoice))
                {
                    throw new NullOrWhiteSpace();
                }

                switch (userChoice)
                {
                    case "1":
                        {
                            break;
                        }
                    case "2":
                        {
                            break;
                        }
                    case "3":
                        {
                            break;
                        }
                    case "4":
                        {
                            break;
                        }
                    case "5":
                        {
                            break;
                        }
                    case "6":
                        {
                            Logout();
                            break;
                        }
                    default:
                        {
                            throw new InvalidChoice();
                        }
                }
            }
        }

        public void Logout()
        {
            Console.WriteLine("Logging out.");
            _authenticationService.LogoutUser(_currentClient.Email);
        }
    }
}
