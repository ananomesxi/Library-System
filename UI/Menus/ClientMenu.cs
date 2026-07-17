using Application.Interfaces;
using Application.Services;
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
        private readonly IBookService _bookService;
        private readonly IBorrowService _borrowService;
        public ClientMenu(IAuthenticationService authenticationService, IBookService bookService, IBorrowService borrowService, ClientUser currentClient)
        {
            _authenticationService = authenticationService;
            _bookService = bookService;
            _borrowService = borrowService;
            _currentClient = currentClient;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("| Client |");
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
                            _bookService.ShowAllBooks();
                            break;
                        }
                    case "2":
                        {
                            _bookService.FindABook();
                            break;
                        }
                    case "3":
                        {
                            _borrowService.BorrowRequest(_currentClient.Id);
                            break;
                        }
                    case "4":
                        {
                            _borrowService.ReturnABook(_currentClient.Id);
                            break;
                        }
                    case "5":
                        {
                            _borrowService.ShowBorrowedBooks(_currentClient.Id, true);
                            break;
                        }
                    case "6":
                        {
                            Logout();
                            return;
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
