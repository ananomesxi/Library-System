using Application.Interfaces;
using Core.Exceptions;
using Core.Models;
using Spectre.Console;

namespace UI.Menus
{
    public class AdminMenu
    {
        #region DI
        private readonly IAuthenticationService _authenticationService;
        private readonly AdminUser _currentAdmin;
        private readonly IBookService _bookService;
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminMenu(IAuthenticationService authenticationService, IBookService bookService, IAdminService adminService, IUserService userService, AdminUser currentAdmin)
        {
            _authenticationService = authenticationService;
            _bookService = bookService;
            _adminService = adminService;
            _userService = userService;
            _currentAdmin = currentAdmin;

        }
        #endregion
        public void Show()
        {
            while (true)
            {
                try
                {
                    string userChoice = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("[green]| ADMIN |[/]").AddChoices(
                    "Show all books",
                    "Add book",
                    "Remove book",
                    "Manage book quantity",
                    "Show borrow requests",
                    "Show all users",
                    "Remove user",
                    "View overdue books",
                    "Send overdue notifications",
                    "Log out"
                    ));

                    switch (userChoice)
                    {
                        case "Show all books":
                            {
                                _bookService.ShowAllBooks();
                                _userService.AddHistory(_currentAdmin, $"viewed all books");
                                break;
                            }

                        case "Add book":
                            {
                                _bookService.AddBook();
                                _userService.AddHistory(_currentAdmin, $"added a books");
                                break;
                            }

                        case "Remove book":
                            {
                                _bookService.RemoveBook();
                                _userService.AddHistory(_currentAdmin, $"removed a book");
                                break;
                            }

                        case "Manage book quantity":
                            {
                                _bookService.ManageBookQuantity();
                                _userService.AddHistory(_currentAdmin, $"managed book quantity");
                                break;
                            }

                        case "Show borrow requests":
                            {
                                _adminService.ManageBorrowRequests();
                                _userService.AddHistory(_currentAdmin, $"viewed borrow requests");
                                break;
                            }

                        case "Show all users":
                            {
                                _userService.ShowAllUsers();
                                _userService.AddHistory(_currentAdmin, $"viewed all users");
                                break;
                            }

                        case "Remove user":
                            {
                                _userService.RemoveUser();
                                _userService.AddHistory(_currentAdmin, $"removed a user");
                                break;
                            }

                        case "View overdue books":
                            {
                                _adminService.ViewOverdueBooks();
                                _userService.AddHistory(_currentAdmin, $"viewed overdue books");
                                break;
                            }

                        case "Send overdue notifications":
                            {
                                _adminService.SendOverdueNotifications();
                                _userService.AddHistory(_currentAdmin, $"sent overdue notifications");
                                break;
                            }

                        case "Log out":
                            {
                                _authenticationService.LogoutUser(_currentAdmin.Email);
                                _userService.AddHistory(_currentAdmin, $"Logged out");
                                return;
                            }
                    }
                }
                catch (BookExists ex)
                {
                    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                }
                catch (BookNotFound ex)
                {
                    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                }
                catch (UserIdDoesNotExist ex)
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
