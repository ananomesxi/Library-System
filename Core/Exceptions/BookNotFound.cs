using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class BookNotFound : Exception
    {
        public BookNotFound() : base("Book not found.")
        {

        }
    }
}
