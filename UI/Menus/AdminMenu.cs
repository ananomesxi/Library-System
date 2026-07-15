using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Menus
{
    public class AdminMenu
    {
        public void Show()
        {
            while (true)
            {
                Console.WriteLine("| CLIENT |");
                Console.WriteLine("1. Add book");
                Console.WriteLine("2. Remove book");
                Console.WriteLine("4. Manage book quantity"); 
                Console.WriteLine("5. Show borrow requests"); // ამ ფუნქციას რომ გახსნის შიგნით ჩამონათვალში მიეცემა არჩევანი approve/reject
                Console.WriteLine("6. Show all users");
                Console.WriteLine("7. Remove user");
                Console.WriteLine("8. Log out");
                // ასევე ბოლოს დავამატებ fine ფუნქციას, ოღონდ ჯერ ამას შევკრავ და მერე დავამატებ


            }
        }
    }
}
