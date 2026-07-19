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
                    string userChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]| Client |[/]")
                .AddChoices(
                    "Show all books",
                    "Find a book",
                    "Borrow a book",
                    "Return a book",
                    "Show my books",
                    "View my fine",
                    "Logout"
                ));

                    switch (userChoice)
                    {
                        case "Show all books":
                            {
                                _bookService.ShowAllBooks();
                                break;
                            }

                        case "Find a book":
                            {
                                _bookService.FindABook();
                                break;
                            }

                        case "Borrow a book":
                            {
                                _borrowService.BorrowRequest(_currentClient.Id);
                                break;
                            }

                        case "Return a book":
                            {
                                _borrowService.ReturnABook(_currentClient.Id);
                                break;
                            }

                        case "Show my books":
                            {
                                _borrowService.ShowBorrowedBooks(_currentClient.Id);
                                break;
                            }

                        case "View my fine":
                            {
                                _clientService.ViewFine(_currentClient);
                                break;
                            }

                        case "Logout":
                            {
                                _authenticationService.LogoutUser(_currentClient.Email);
                                return;
                            }
                    }
                }

                catch (NullOrWhiteSpace ex)
                {
                    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                }
                catch (BookNotFound ex)
                {
                    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                }
                catch (FormatException ex)
                {
                    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                }
            }
        }

        
    }
}
