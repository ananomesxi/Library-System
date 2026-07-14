using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class InvalidChoice : Exception
    {
        public InvalidChoice() : base("Invalid Choice.")
        {
        }
    }
}
