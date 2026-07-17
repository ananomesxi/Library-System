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
        private readonly ClientUser _currentClient;
        private readonly IBookService _bookService;

        public AdminMenu(IAuthenticationService authenticationService, IBookService bookService, ClientUser currentClient)
        {
            _authenticationService = authenticationService;
            _bookService = bookService;
            _currentClient = currentClient;
        }
        public void Show()
        {

            while (true)
            {
                Console.WriteLine("| ADMIN |");
                Console.WriteLine("1. Add book");
                Console.WriteLine("2. Remove book");
                Console.WriteLine("4. Manage book quantity"); 
                Console.WriteLine("5. Show borrow requests"); // ამ ფუნქციას რომ გახსნის შიგნით ჩამონათვალში მიეცემა არჩევანი approve/reject
                Console.WriteLine("6. Show all users");
                Console.WriteLine("7. Remove user");
                Console.WriteLine("8. Log out");
                // ასევე ბოლოს დავამატებ fine ფუნქციას, ოღონდ ჯერ ამას შევკრავ და მერე დავამატებ

                string userChoice = Console.ReadLine();

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
                            break;
                        }
                    case "7":
                        {
                            break;
                        }
                    case "8":
                        {
                            break;
                        }
                    default:
                        {
                            throw new InvalidChoice();
                        }
                }


            }
        }
    }
}
