using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class NullOrWhiteSpace : Exception
    {
        public NullOrWhiteSpace() : base("Your choice can not be null or empty.")
        {
        }
    }
}
