using Application.Interfaces;
using Application.Services;
using Core.Exceptions;
using Core.Models;
using Spectre.Console;

namespace UI.Menus
{
    public class ClientMenu
    {
        #region DI
        private readonly IAuthenticationService _authenticationService;
        private readonly ClientUser _currentClient;
        private readonly IBookService _bookService;
        private readonly IBorrowService _borrowService;
        private readonly IClientService _clientService;
        private readonly IUserService _userService;
        public ClientMenu(IAuthenticationService authenticationService, IBookService bookService, IBorrowService borrowService, IClientService clientService, IUserService userService, ClientUser currentClient)
        {
            _authenticationService = authenticationService;
            _bookService = bookService;
            _borrowService = borrowService;
            _currentClient = currentClient;
            _clientService = clientService;
            _userService = userService;
        }
        #endregion
        public void Show()
        {
            while (true)
            {
                try
                {
                    string userChoice = AnsiConsole.Prompt( new SelectionPrompt<string>().Title("[green]| Client |[/]").AddChoices(
                    "Show all books",
                    "Find a book",
                    "Borrow a book",
                    "Return a book",
                    "Show my books",
                    "View my fine",
                    "Logout",
                    "Change password"
                    ));

                    switch (userChoice)
                    {
                        case "Show all books":
                            {
                                _bookService.ShowAllBooks();
                                _userService.AddHistory(_currentClient, "viewed all books");
                                break;
                            }

                        case "Find a book":
                            {
                                _bookService.FindABook();
                                _userService.AddHistory(_currentClient, "searched a book");
                                break;
                            }

                        case "Borrow a book":
                            {
                                _borrowService.BorrowRequest(_currentClient.Id);
                                _userService.AddHistory(_currentClient, "requested to borrow a book");
                                break;
                            }

                        case "Return a book":
                            {
                                _borrowService.ReturnABook(_currentClient.Id);
                                _userService.AddHistory(_currentClient, "returned a book");
                                break;
                            }

                        case "Show my books":
                            {
                                _borrowService.ShowBorrowedBooks(_currentClient.Id);
                                _userService.AddHistory(_currentClient, "viewed their books");
                                break;
                            }

                        case "View my fine":
                            {
                                _clientService.ViewFine(_currentClient);
                                _userService.AddHistory(_currentClient, "viewed their fine");
                                break;
                            }
                        case "Change password":
                            {
                                _userService.ChangePassword(_currentClient);
                                _userService.AddHistory(_currentClient, "hanged their password");
                                return;
                            }
                        case "Logout":
                            {
                                _authenticationService.LogoutUser(_currentClient.Email);
                                _userService.AddHistory(_currentClient, "logged out");
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
                catch (InvalidPassword ex)
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
