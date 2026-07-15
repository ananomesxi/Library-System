using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class BookExists : Exception
    {
        public BookExists () : base ("Book already exists.")
        {

        }
    }
}
