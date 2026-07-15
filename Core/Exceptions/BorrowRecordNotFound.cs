using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class BorrowRecordNotFound : Exception
    {
        public BorrowRecordNotFound() : base("Borrow record not found.")
        {

        }
    }
}
