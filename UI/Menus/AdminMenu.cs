using Application.Interfaces;
using Core.Exceptions;
using Core.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Menus
{
    public class AdminMenu
    {
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
        public void Show()
        {

            while (true)
            {
                try
                {
                    string userChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]| ADMIN |[/]")
                .AddChoices(
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
                                break;
                            }

                        case "Add book":
                            {
                                _bookService.AddBook();
                                break;
                            }

                        case "Remove book":
                            {
                                _bookService.RemoveBook();
                                break;
                            }

                        case "Manage book quantity":
                            {
                                _bookService.ManageBookQuantity();
                                break;
                            }

                        case "Show borrow requests":
                            {
                                _adminService.ManageBorrowRequests();
                                break;
                            }

                        case "Show all users":
                            {
                                _userService.ShowAllUsers();
                                break;
                            }

                        case "Remove user":
                            {
                                _userService.RemoveUser();
                                break;
                            }

                        case "View overdue books":
                            {
                                _adminService.ViewOverdueBooks();
                                break;
                            }

                        case "Send overdue notifications":
                            {
                                _adminService.SendOverdueNotifications();
                                break;
                            }

                        case "Log out":
                            {
                                _authenticationService.LogoutUser(_currentAdmin.Email);
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
