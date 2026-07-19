using Application.Interfaces;
using Core.Exceptions;
using Core.Models;
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
                    Console.WriteLine("| ADMIN |");
                    Console.WriteLine("1.  Show all books");
                    Console.WriteLine("2.  Add book");
                    Console.WriteLine("3.  Remove book");
                    Console.WriteLine("4.  Manage book quantity");
                    Console.WriteLine("5.  Show borrow requests");
                    Console.WriteLine("6.  Show all users");
                    Console.WriteLine("7.  Remove user");
                    Console.WriteLine("8.  View overdue books");
                    Console.WriteLine("9.  Send overdue notifications");
                    Console.WriteLine("10. Log out");

                    string userChoice = Console.ReadLine();

                    switch (userChoice)
                    {
                        case "1":
                            {
                                _bookService.ShowAllBooks();
                                break;
                            }
                        case "2":
                            {
                                _bookService.AddBook();
                                break;
                            }
                        case "3":
                            {
                                _bookService.RemoveBook();
                                break;
                            }
                        case "4":
                            {
                                _bookService.ManageBookQuantity();
                                break;
                            }
                        case "5":
                            {
                                _adminService.ManageBorrowRequests();
                                break;
                            }
                        case "6":
                            {
                                _userService.ShowAllUsers();
                                break;
                            }
                        case "7":
                            {
                                _userService.RemoveUser();
                                break;
                            }
                        case "8":
                            {
                                _adminService.ViewOverdueBooks();
                                break;
                            }
                        case "9":
                            {
                                _adminService.SendOverdueNotifications();
                                break;
                            }
                        case "10":
                            {
                                _authenticationService.LogoutUser(_currentAdmin.Email);
                                return;
                            }
                        default:
                            {
                                throw new InvalidChoice();
                            }
                    }

                }
                catch (BookExists ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (BookNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (UserIdDoesNotExist ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidChoice ex)
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
