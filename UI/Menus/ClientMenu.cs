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
        private readonly IClientService _clientService;

        public ClientMenu(IAuthenticationService authenticationService, IBookService bookService, IBorrowService borrowService, IClientService clientService, ClientUser currentClient)
        {
            _authenticationService = authenticationService;
            _bookService = bookService;
            _borrowService = borrowService;
            _currentClient = currentClient;
            _clientService = clientService;
        }

        public void Show()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("| Client |");
                    Console.WriteLine("1. Show all books");
                    Console.WriteLine("2. Find a book");
                    Console.WriteLine("3. Borrow a book");
                    Console.WriteLine("4. Return a book");
                    Console.WriteLine("5. Show my books");
                    Console.WriteLine("6. View my fine");
                    Console.WriteLine("7. Logout");
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
                                _borrowService.ShowBorrowedBooks(_currentClient.Id);
                                break;
                            }
                        case "6":
                            {
                                _clientService.ViewFine(_currentClient);
                                return;
                            }
                        case "7":
                            {
                                _authenticationService.LogoutUser(_currentClient.Email);
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
                catch (NullOrWhiteSpace ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (BookNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

      

        
    }
}
